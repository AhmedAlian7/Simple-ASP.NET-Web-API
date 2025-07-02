using Back_End.Authurization;
using Back_End.Models;
using Back_End.Models.DTOs;
using Back_End.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("AllProducts")]
        [CheckPermision(Permissions.Read)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = _productService.GetAllProducts();
            if (products == null)
                return NotFound("No Products Founded!");

            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            if (id < 0)
                return BadRequest("ID less than Zero is not Accepted!");

            var product = await _productService.GetProductById(id);
            if (product == null) return NotFound($"Product With ID = {id} not Found!");

            return Ok(product);
        }

        [HttpPost(Name = "AddProduct")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AddProduct(ProductDTO newProduct)
        {
            if (newProduct == null || string.IsNullOrEmpty(newProduct.Name) || newProduct.Price < 0)
                return BadRequest("Invalid Student Data!");


            var createdProductID = await _productService.AddProduct(newProduct);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProductID }, createdProductID);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id < 1)
                return BadRequest($"Invalid Product ID = {id}!");


            var deleted = await _productService.DeleteProduct(id);
            if (!deleted) return StatusCode(StatusCodes.Status500InternalServerError);

            return Ok($"Product With ID = {id} has been deleted!");
        }


        //here we use http put method for update
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDTO>> UpdateStudent(int id, ProductDTO updatedProduct)
        {
            if (updatedProduct == null || string.IsNullOrEmpty(updatedProduct.Name) || updatedProduct.Price < 0)
                return BadRequest("Invalid Student Data!");

            var product =
                await _productService.UpdateProduct(id, updatedProduct);

            if (product == null) return NotFound($"Product With ID = {id} not Found!");

            return Ok(product);
        }

    }
}
