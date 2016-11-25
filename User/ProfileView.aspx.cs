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

namespace LabelingFramework
{
    public partial class ProfileView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["User"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    Class.User user = Session["User"] as Class.User;
                    user = DataAccess.DataAccessUser.getUserById(user.Id_user);

                    txtName.Text = user.Name;
                    txtSurname.Text = user.Surname;
                    txtEmail.Text = user.Email;

                    txtPassword.Text = user.Password;
                    txtUserName.Text = user.Username;

                    iduser.Value = user.Id_user.ToString();
                    DivSuccess.Visible = false;
                }
            }
        }


        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Class.User user = new Class.User();
                Class.Result res = new Class.Result();
                user.Name = txtName.Text;
                user.Surname = txtSurname.Text;
                user.Username = txtUserName.Text;
                user.Email = txtEmail.Text;
                user.Password = txtPassword.Text;

                res = DataAccess.DataAccessUser.UpdateProfileView(Convert.ToInt64(iduser.Value), user);
                DataAccess.DataAccessUser.writeUserList();

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


        // control lenght password 
        protected void CVPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if ((txtPassword.Text.Length < 6))
                args.IsValid = false;
        }
        // control if user already taken
        protected void CVUsername_ServerValidate1(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (DataAccessUser.VerifyExistUser(txtUserName.Text))
                args.IsValid = false;
        }
    }
}