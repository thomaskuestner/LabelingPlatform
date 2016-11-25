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
using System.Text;
using LabelingFramework.Class;
using System.Diagnostics; // *for testing purposes*

//*********************************************************************************************************//
// page for test case selection
//*********************************************************************************************************//

namespace LabelingFramework.TestCase
{
    public partial class ViewTestCase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["User"] == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                if (!IsPostBack) // nur das erste mal
                    bindTestCase();
            }
        }

        public void bindTestCase()
        {
            // nimmt daten aus mysql in tabelle
            gvTestCase.DataSource = DataAccess.DataAccessTestCase.getTestCase();
            gvTestCase.DataBind();

            fuser.Visible = false;
            flable.Visible = false;
        }

        protected void gvTestCase_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            // get id testcase my datakey selected in the gridview
            long id_testCase = Convert.ToInt64(gvTestCase.DataKeys[e.NewSelectedIndex].Value);

            idTestCase.Value = id_testCase.ToString();

            gvUser.DataSource = DataAccess.DataAccessTestCase.getUserByIdTestCase(id_testCase);
            gvUser.DataBind();

            Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase(id_testCase);
            lTestcase.InnerText = "Testcase: " + tc.NameTestCase;
            fuser.Visible = true;
            flable.Visible = false;

        }

        protected void BtnUpdateTestCase_Click(object sender,EventArgs e)
        {
            Button btnSender = (Button)sender;

            Session["IdTestCase"] = btnSender.CommandArgument;
            Session["EditTestcase"] = 1;
            Response.Redirect("~/InsertTestCase.aspx");
        }

        protected void StartTestCase_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Session["IdTestCase"] = btnSender.CommandArgument;
            Response.Redirect("~/TestCase/loadingTestCase.aspx");
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            long testcaseID = Convert.ToInt64(btnSender.CommandArgument);
            DataAccess.DataAccessTestCase.removeTestcase(testcaseID);
            bindTestCase();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Session["EditTestcase"] = 0;
            Session["IdTestCase"] = null;
            Session["Group"] = null;
            Session["typeckeched"] = null;
            Response.Redirect("~/InsertTestCase.aspx");
        }

        protected void gvUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            // get id testcase my datakey selected in the gridview
            long id_user = Convert.ToInt64(gvUser.DataKeys[e.NewSelectedIndex].Value);
            long id_testcase = Convert.ToInt64(idTestCase.Value);

            gvLable.DataSource = DataAccess.DataAccessTestCase.GetLableforIdUserIdTestcase(id_testcase, id_user);
            gvLable.DataBind();

            Class.User usr = DataAccess.DataAccessUser.getUserById(id_user);

            lUser.InnerText = "Label of user: " + usr.Name + " " + usr.Surname;
            fuser.Visible = true;
            flable.Visible = true;
        }

        protected void BtnToggleTestCase_Click(object sender, EventArgs e)
        {
            Button testcase = (Button)sender;

            //long id_testcase = Convert.ToInt64(idTestCase.Value);
            long id_testcase = Convert.ToInt64(testcase.CommandArgument);
            
            DataAccess.DataAccessTestCase.toggleTestCase(id_testcase);

            bindTestCase();

        }

        protected void BtnExportTestCase_Click(object sender, EventArgs e)
        {
            Button exportlabeltestcase = (Button)sender;

            //long id_testcase = Convert.ToInt64(idTestCase.Value);
            long id_testcase = Convert.ToInt64(exportlabeltestcase.CommandArgument);

            ExportCsvTestcase(id_testcase, "Path;LabelDescriptionID;LabelId;Label;UserID");
        }

        protected void Export_Click(object sender, EventArgs e)
        { 
             Button exportlableuser=(Button)sender;

             long id_testcase = Convert.ToInt64(idTestCase.Value);
             long id_user = Convert.ToInt64(exportlableuser.CommandArgument);

             ExportCsvUser(id_testcase, id_user, "Path;LabelDescriptionID;LabelId;Label;UserID");
        
        }

        public void ExportCsvTestcase(long idtestcase, string header)
        {
            Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase(idtestcase);
            string attachment = "attachment; " + "filename=LabelTestCase" + string.Format("{0:D4}", idtestcase) + ".csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            WriteColumnName(header);

            List<Class.Lable> listlable = DataAccess.DataAccessTestCase.GetLabelsforIdTestcase(idtestcase);
                if (!listlable.Any())
                {
                    //Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase(idtestcase);
                   //WriteEmptyInfo(usr,tc.NameTestCase);
                }
                else
                {
                    foreach (Class.Lable lable in listlable)
                    {
                        lable.dbPath = tc.dbPath;
                        WriteLableInfo(lable);
                    }
                }
        
            HttpContext.Current.Response.End();
        }


        public void ExportCsvUser(long idtestcase,long iduser, string header)
        {

            Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase(idtestcase);
            string attachment = "attachment; " + "filename=LabelTestCase" + string.Format("{0:D4}",idtestcase) + "_User" + string.Format("{0:D4}",iduser) + ".csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");
            WriteColumnName(header);
            foreach (Lable lable in DataAccess.DataAccessTestCase.GetLableforIdUserIdTestcase(idtestcase,iduser))
            {
                lable.dbPath = tc.dbPath;
                WriteLableInfo(lable);
            }
            HttpContext.Current.Response.End();
        }

        private static void WriteEmptyInfo(Class.User usr, string TestCaseName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            AddComma(""+usr.Id_user, stringBuilder);
            AddComma("noLabel", stringBuilder);
            AddComma("noLabel", stringBuilder);
            AddComma("noLabel", stringBuilder);
            HttpContext.Current.Response.Write(stringBuilder.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        private static void WriteLableInfo(Class.Lable lable)
        {
            StringBuilder stringBuilder = new StringBuilder();
            AddComma(lable.dbPath + "\\" + (lable.PathImage).Replace("/", "\\"), stringBuilder);
            AddComma(""+lable.LabelCode, stringBuilder);
            AddComma(""+lable.IdLable, stringBuilder);
            AddComma(string.Format("{0:0.00}", lable.lable), stringBuilder);
            AddComma(lable.UserId, stringBuilder);
            HttpContext.Current.Response.Write(stringBuilder.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }



        private static void AddComma(string value, StringBuilder stringBuilder)
        {
            stringBuilder.Append(value.Replace(';', ' '));
            stringBuilder.Append(";");
        }

        private static void WriteColumnName(string column)
        {
            HttpContext.Current.Response.Write(column);
            HttpContext.Current.Response.Write(Environment.NewLine);
        }



    }
}