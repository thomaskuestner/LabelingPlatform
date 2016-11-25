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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using LabelingFramework.DataAccess;
using LabelingFramework.Class;
using System.Threading;
using LabelingFramework.Utility;
using System.Diagnostics; // *for testing purposes*


//*********************************************************************************************************//
//  page for user registration
//*********************************************************************************************************//


namespace LabelingFramework.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //different then is post back -> load only first time 
            if (!IsPostBack)
            {
                PopulateTypeUser();
                PopulateTitle();
            }
        }
        // when click on button create user, eventArgs event of button
        protected void CreateUserButton_Click(object sender, EventArgs e)
        {
            Result result = new Result();
            result.result = false;
            Class.User user = null;
           
            // if every control is false -> alles ok
            if (Page.IsValid)
            {
                // populate object and insert in db new user
                user = new Class.User();
                user.Name = txtName.Text;
                user.Surname = txtSurname.Text;
                user.Password = txtPassword.Text;
                user.Username = txtUserName.Text;
                user.Email = txtEmail.Text;
                user.Type = Convert.ToInt32(ddlUserType.SelectedValue);
                user.titleId = Convert.ToInt32(ddlUserTitle.SelectedValue);

                user.YearsOfExperience = Convert.ToInt32(txtYearsExperience.Text);
                user.DateInsert = DateTime.Now;
                result = DataAccessUser.RegisterUser(user);
                DataAccessUser.writeUserList();
            }
            else {

            
            }

            if (result.result)
            {
                DivSuccess.Visible = true;
                DivError.Visible = false;

                List<string> message = Constant.registerMail(txtName.Text, txtSurname.Text, txtUserName.Text, txtPassword.Text);

                MyEmail mail = new MyEmail();
                mail.sendEmail(txtEmail.Text, message[0], message[1], Constant.includeAttachment);


                if (Constant.sendRegisteredAdmin)
                {

                    List<string> messageAdmin = Constant.registerNotificationAdminMail(txtName.Text, txtSurname.Text, txtUserName.Text);

                    mail.sendEmail(Constant.adminEmailAddress, messageAdmin[0], messageAdmin[1], false);
                }




                if (user != null)
                {
                    // user is available for the whole page  
                    Session["User"] = user;
                    Response.Redirect("~/Default.aspx"); // change to welcome page
                }
            }
            else
            {
                DivSuccess.Visible = false;
                DivError.Visible = true;
                lblError.Text = result.Message;
            
            }
           
        }


        // Redirect to login page
        protected void BackToLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        //  menu drop down, load existing type of users from db
        public void PopulateTypeUser()
        {
            ddlUserType.DataSource = DataAccessUser.getTypeUser();//takes value from db
            ddlUserType.DataTextField = "DescriptionTypUser";// wort das man sehen kann wird eingegeben
            ddlUserType.DataValueField = "ID_Typ";// id vom typ user wird angegeben, sieht man nicht aber nuetzlich
            ddlUserType.DataBind();
            ddlUserType.Items.Remove(new ListItem {Value="2",Text="Administrator"});// nicht admin auswahlbar
        }

        //  menu drop down, load existing titles/salutations from db
        public void PopulateTitle()
        {
            ddlUserTitle.DataSource = DataAccessUser.getTitle();//takes value from db
            ddlUserTitle.DataTextField = "title";// wort das man sehen kann wird eingegeben
            ddlUserTitle.DataValueField = "title_Id";// id vom typ user wird angegeben, sieht man nicht aber nuetzlich
            ddlUserTitle.DataBind();
        }

        // control lenght password 
        protected void CVPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if ((txtPassword.Text.Length< 6))
                args.IsValid = false;
        }
        // control if user already taken
        protected void CVUsername_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (DataAccessUser.VerifyExistUser(txtUserName.Text))
                args.IsValid = false;
        }

       

    }
}
