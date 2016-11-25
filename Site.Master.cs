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
using LabelingFramework.Utility;
using System.Text;
using System.IO;
using System.Diagnostics; // *for testing purposes*

namespace LabelingFramework
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] != null)
            {
                if(!IsPostBack)
                     Session["Group"] = null;
                
                Class.User user = Session["User"] as Class.User;

                if (user.Type == 2)
                {
                    MenuNonAmministratore.Visible = false;
                    NavigationMenuAmministratore.Visible = true;
                }
                else
                {
                    MenuNonAmministratore.Visible = true;
                    NavigationMenuAmministratore.Visible = false;
                }

               lnklogout.Visible = true;
            }else{
                lnklogout.Visible = false;
            }
          //  Constant.currentDatabase = "S:\\Rohdaten\\ImageSimilarity\\ImageDatabaseA";
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {

            if (Session["User"] != null)
            {
                Class.User user = (Class.User)Session["User"];
                if (user.Id_user > 0)
                {
                    Session["Group"] = null;
                    Session["User"] = null;
                    Session["IdTestCase"] = null;
                    Session["EditTestcase"] = null;
                    Session["Seed"] = null;
                }
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        private static string WriteLableInfo(Lable lable)
        {
            StringBuilder stringBuilder = new StringBuilder();
            AddComma(lable.TestCaseName, stringBuilder);
            AddComma(lable.TestQuestion, stringBuilder);
            AddComma(lable.IsReference, stringBuilder);
            AddComma(lable.ReferenceImage, stringBuilder);
            AddComma(lable.NameUser, stringBuilder);
            AddComma(lable.SurnameUser, stringBuilder);
            AddComma(lable.PathImage.Split('/')[0], stringBuilder);
            AddComma(lable.PathImage.Split('/')[1], stringBuilder);
            //AddComma(lable.EmailUser, stringBuilder);
            AddComma(lable.Description, stringBuilder);
            AddComma(lable.TotalValue, stringBuilder);
            AddComma(string.Format("{0:0.00}", lable.lable), stringBuilder);
            //HttpContext.Current.Response.Write(stringBuilder.ToString());
            //HttpContext.Current.Response.Write(Environment.NewLine);

            return stringBuilder.ToString() ;
        }


        private static void AddComma(string value, StringBuilder stringBuilder)
        {
            stringBuilder.Append(value.Replace(';', ' '));
            stringBuilder.Append(";");
        }

        private static StringBuilder WriteColumnName()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string columnNames = "TestCaseName;TestQuestion;ReferenceImage;NameReferenceImage;NameUser;SurnameUser;Patient;NameImage;DescriptionScale;TotalValue;Lable";
            //HttpContext.Current.Response.Write(columnNames);
            //HttpContext.Current.Response.Write(Environment.NewLine);

            stringBuilder.AppendLine(columnNames);
            return stringBuilder;
        
        }

        public StringBuilder WriteFIle(long idtestcase,long iduser)
        {

            
            StringBuilder sb = new StringBuilder();
            foreach (Lable lable in DataAccess.DataAccessTestCase.GetLableforIdUserIdTestcase(idtestcase, iduser))
            {
                    sb.AppendLine(WriteLableInfo(lable));
            }


            return sb;
        }

        
        protected void btnToolbar_Click(Object sender, MenuEventArgs e)
        {
            //Session["LinkMenubar"] = true;
        }

        public int SessionLengthMinutes
        {
            get { return Session.Timeout; }
        }
        public string SessionExpireDestinationUrl
        {
            get { return "../../Account/Login.aspx"; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            logoutTime.Value = "" + SessionLengthMinutes;
            base.OnPreRender(e);

           /* this.headTime.Controls.Add(
                new LiteralControl(
                String.Format("<meta http-equiv='refresh' content='{0};url={1}'>",
                SessionLengthMinutes * 60, SessionExpireDestinationUrl))
                );
            */
        }
    }
}
