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
using LabelingFramework.Class;


namespace LabelingFramework.User
{
    public partial class ViewUser : System.Web.UI.Page
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
                    BindUser();
                    PopulateTypeUser();
                }
            }

        }

        public void BindUser()
        {
            gvUser.DataSource = DataAccess.DataAccessUser.getUser();
            gvUser.DataBind();

        }       

        public void PopulateTypeUser()
        {
            ddlUserType.DataSource = DataAccess.DataAccessUser.getTypeUser();
            ddlUserType.DataTextField = "DescriptionTypUser";
            ddlUserType.DataValueField = "ID_Typ";
            ddlUserType.DataBind();

        }
       
        protected void gvUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            // get id user my datakey selected in the gridview
            long id_user = Convert.ToInt64(gvUser.DataKeys[e.NewSelectedIndex].Value);
            hddIdUser.Value = id_user.ToString();
            Class.User user = DataAccess.DataAccessUser.getUserById(id_user);

            txtName.Text = user.Name;
            txtSurname.Text = user.Surname;
            ddlUserType.SelectedValue = user.Type.ToString();

            fUpdateUser.Visible = true;
            fUserFooter.Visible = true;
            DivSuccess.Visible = false;
            DivError.Visible = false;

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
           Result res=  DataAccess.DataAccessUser.UpdateUser(Convert.ToInt32(hddIdUser.Value), Convert.ToInt32(ddlUserType.SelectedValue));
            BindUser();
            DataAccess.DataAccessUser.writeUserList();

            if (res.result)
            {
                DivSuccess.Visible = true;
                DivError.Visible = false;
                fUpdateUser.Visible = false;
            }
            else
            {
                DivSuccess.Visible = false;
                DivError.Visible = true;
                lblError.Text = res.Message;

            }
           
        
        }

        protected void gvUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
               int id_user = Convert.ToInt32(gvUser.DataKeys[e.RowIndex].Value.ToString());
               DataAccess.DataAccessUser.DeleteUser(id_user);
               BindUser();
               DataAccess.DataAccessUser.writeUserList();
            
        }

       

        
      

  
    }
}