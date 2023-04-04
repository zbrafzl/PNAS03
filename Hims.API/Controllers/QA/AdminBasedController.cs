using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Hims.API.Controllers
{
    [AdminBasicAuthentication]
    public class AdminBasedController : ApiController
    {
    }

    public class AdminBasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        //https://www.c-sharpcorner.com/article/asp-net-mvc-webapi-authorization-by-basic-auth47-oauthjwt/
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization != null)
                {
                    //Taking the parameter from the header  
                    var authToken = actionContext.Request.Headers.Authorization.Parameter;
                    //decode the parameter  
                    var decoAuthToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                    //split by colon : and store in variable  
                    var UserNameAndPassword = decoAuthToken.Split(':');
                    //Passing to a function for authorization  
                    if (IsAuthorizedUser(UserNameAndPassword[0], UserNameAndPassword[1]))
                    {
                        // setting current principle  
                        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(UserNameAndPassword[0]), null);
                    }
                    else
                    {
                        //Enum status = HttpStatusCode.Unauthorized;
                        //APIResponse httpResponse = new APIResponse();
                        //httpResponse.statusCode = status.TooString();
                        //httpResponse.message = "Invalid Username or Password";
                        actionContext.Response = actionContext.Request.CreateResponse("Invalid Username or Password");
                    }
                }
                else
                {
                    //Enum status = HttpStatusCode.Unauthorized;
                    //APIResponse httpResponse = new APIResponse();
                    //httpResponse.statusCode = status.TooString();
                    //httpResponse.message = "Invalid Username or Password";
                    //actionContext.Response = httpResponse;//  actionContext.Request.CreateResponse("Invalid User");
                    actionContext.Response = actionContext.Request.CreateResponse("Invalid Username or Password");

                    //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        public static bool IsAuthorizedUser(string Username, string Password)
        {
            bool isValidUser = false;

            if (Username == "pnas.admin" && Password == "pnas@admin.phf")
            {
                isValidUser = true;
            }
            // In this method we can handle our database logic here...  
            //Here we have given the hard-coded values   
            // Username == "shahbaz" && Password == "abc123";

            return isValidUser;
        }
    }
}
