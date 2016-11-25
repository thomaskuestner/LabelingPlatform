/*
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LabelingFramework.DataAccess;

using System.Diagnostics; // *for testing purposes*

namespace LabelingFramework.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Group"] = null;
            Session["User"] = null;
            Session["IdTestCase"] = null;
            Session["EditTestcase"] = null;
            Session["Seed"] = null;
        }
        // if click on login
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Class.User user = null;
            // variable: user, get existing user if exist
            try
            {
                user = DataAccessUser.checkPassword(txtUsername.Text, txtPassword.Text);
            }
            catch (Exception)
            {
                Response.Redirect("Login.aspx");
            }
            if (user != null)
            {
                // user is available for the whole page  
                Session["User"] = user;
                Response.Redirect("~/Default.aspx"); // change to welcome page
            }
        }
    }
}
