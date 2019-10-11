using Sigvardt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigvardt.Controllers
{
    public class ServiceController
    {
        public void Authenticate(string username, string password)
        {
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                throw new WrongCredentialsException();

            JackmanService.SigvardtServiceClient client = new JackmanService.SigvardtServiceClient();

            client.ClientCredentials.UserName.UserName = username;
            client.ClientCredentials.UserName.Password = password;

            try
            {
                client.Open();
                HttpContext.Current.Session["username"] = username;
                HttpContext.Current.Session["password"] = password;
            }
            catch (Exception e)
            {
                if (e.InnerException?.Message == "Forbidden")
                    throw new WrongCredentialsException();
                else
                    throw e;
            }
        }

        public void Logout()
        {
            HttpContext.Current.Session["username"] = null;
            HttpContext.Current.Session["password"] = null;
        }

        public JackmanService.ISigvardtService GetClient(JackmanService.ISigvardtService client = null)
        {
            if (client == null)
            {
                string username = HttpContext.Current.Session["username"]?.ToString() ?? "";
                string password = HttpContext.Current.Session["password"]?.ToString() ?? "";

                if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
                    throw new WrongCredentialsException(); //Just Continue debugging, to go to login-page

                JackmanService.SigvardtServiceClient ssc = new JackmanService.SigvardtServiceClient();

                ssc.ClientCredentials.UserName.UserName = username;
                ssc.ClientCredentials.UserName.Password = password;
                
                try
                {
                    ssc.Open();
                }
                catch (Exception e)
                {
                    if (e.InnerException?.Message == "Forbidden")
                        HttpContext.Current.Response.Redirect("~/Login");
                    else
                        throw e;
                }

                return ssc;
            }
            else
                return client;
        }
    }
}