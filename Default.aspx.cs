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

//*********************************************************************************************************//
// defaul page on website
//*********************************************************************************************************//

namespace LabelingFramework
{
   
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // just first time
            {
                Class.User user = Session["User"] as Class.User;
                if (user == null)       
                {
                    Response.Redirect("~/Account/Login.aspx");
                    return;
                }
                lblNominativo.Text =user.title + " " + user.Name + " " + user.Surname;
                bindTestCase(user.Type,user.Id_user);
            }
        }


        // put all accessible in table structure to display on page
        public void bindTestCase(int idtypeuser,long iduser)
        {
            // take data from MYSQL table
            gvTestCase.DataSource = DataAccess.DataAccessTestCase.getTestCase(idtypeuser,iduser);
            gvTestCase.DataBind();
        }


        // test case has been selected
        protected void gvTestCase_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            long id_testCase = Convert.ToInt64(gvTestCase.DataKeys[e.NewSelectedIndex].Value);
            Session["IdTestCase"] = id_testCase;
            Response.Redirect("~/TestCase/loadingTestCase.aspx");
        }
    }
}
