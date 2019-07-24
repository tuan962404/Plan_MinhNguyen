﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Plan.HasCredentialAttribute
{
    public partial class _2HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { set; get; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var session = (User)HttpContext.Current.Session["USER_SESSION"];

            if (session == null)
            {
                return false;
            }

            List<string> privilegeLevels = this.GetCredentialByLoggeInUser(session.User1);

            if (privilegeLevels.Contains(this.RoleID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var session = (User)HttpContext.Current.Session["USER_SESSION"];
            if (session == null)
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Account/Login.cshtml"
                };
            }
            else
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = "~/Views/Home/Error401.cshtml"
                };
            }
        }

        private List<string> GetCredentialByLoggeInUser(string userName)
        {
            var credentials = (List<string>)HttpContext.Current.Session["SESSION_CREDENTIALS"];
            return credentials;
        }
    }
}