using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Netblaster.Hermes.DAL.Model.Enums;

namespace Netblaster.Hermes.WebUI.Attributes
{
    public class UserClaimCheck : System.Web.Mvc.AuthorizeAttribute
    {
        private readonly UserRoleEnum claimValue;

        public UserClaimCheck(UserRoleEnum value)
        {
            this.claimValue = value;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User as ClaimsPrincipal;
            if (user != null && user.HasClaim("Role", claimValue.ToString()))
            {
                base.OnAuthorization(filterContext);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}