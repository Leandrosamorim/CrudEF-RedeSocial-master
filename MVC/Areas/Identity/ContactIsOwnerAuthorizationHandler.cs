using Domain.Models.Models;
using Domain.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication14.Controllers;

namespace WebApplication14.Areas.Identity
{
    public class ContactIsOwnerAuthorizationHandler
                 : AuthorizationHandler<OperationAuthorizationRequirement, Professor>
    {
        UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContactIsOwnerAuthorizationHandler(
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected override async Task
            HandleRequirementAsync(AuthorizationHandlerContext context,
                                   OperationAuthorizationRequirement requirement,
                                   Professor professor)
        {
            if (context.User == null || professor == null)
            {
                context.Fail();
                return;
            }

            // If not asking for CRUD permission, return.

            if (requirement.Name == Constants.UpdateOperationName)
            {
                var loggedIdentityUser = await _userManager.GetUserAsync(context.User);

                if (string.Equals(loggedIdentityUser.UserName, professor.Email, StringComparison.OrdinalIgnoreCase));
                {
                    context.Succeed(requirement);
                }

                return;
            }

            context.Fail();
            return;
        }
    }
}
