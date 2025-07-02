using Back_End.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Back_End.Authurization
{
    public class PermissionBasedAuthorization(UserService userService) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

            // Check if the action has the CheckPermissionAttribute
            var attribute = context.ActionDescriptor.EndpointMetadata
                .FirstOrDefault(x => x is CheckPermisionAttribute) as CheckPermisionAttribute;

            if (attribute == null)
            {
                // No permission check required for this action
                return;
            }

            var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
            if (claimIdentity == null || !claimIdentity.IsAuthenticated)
            {
                context.Result = new ForbidResult("User is not Authenticated!"); // Return 403
                return;
            }

            // Get User ID
            var userIdClaim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                context.Result = new ForbidResult("Invalid User Identifier!");
                return;
            }

            // Check if user has permission
            if (!userService.HasPermission(userId, attribute.Permissions))
            {
                context.Result = new ForbidResult("User is not Authorized!");
            }
        }
    }
}
