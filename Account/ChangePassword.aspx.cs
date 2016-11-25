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

namespace LabelingFramework.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        // button click 
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Class.User user = new Class.User();
                Class.Result res = new Class.Result();
                user.Email = txtEmail.Text;


                // controls if user already exists (with dataaccess), if yes: password will be replaced
                res = DataAccess.DataAccessUser.ChangePasswordUser(user.Email);

                if (res.result)
                {
                    DivSuccess.Visible = true;
                    DivError.Visible = false;

                }
                else
                {
                    DivSuccess.Visible = false;
                    DivError.Visible = true;
                    lblError.Text = res.Message;

                }
            }
        }
        
        protected void Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
      

        // control lenght password 
        protected void CVPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

        }
        // control if user already taken
        protected void CVUsername_ServerValidate1(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

        }
    }
}