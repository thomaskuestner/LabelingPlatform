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
using System.IO;
using LabelingFramework.Utility;
using System.Diagnostics; // *for testing purposes*
using System.Windows.Forms;

//*********************************************************************************************************//
//  page for user type handling (admin)
//*********************************************************************************************************//

namespace LabelingFramework.User
{
    public partial class AddTypeUser : System.Web.UI.Page
    {
        static string textValue = "";
        static string versionValue = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) // nur das erste mal
            {
                if (HttpContext.Current.Session["User"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
                else
                {
                    //TabName.Value = Request.Form[TabName.UniqueID];
                    BindGridTypeUser();
                    BindGridTypeScaleContinuous();
                    BindGridTypeScaleDiscrete();

                    if (HttpContext.Current.Session["tabStatus"] == "cont")
                    {
                        tContScaleUpdate.Text = textValue;
                        tContScaleVerUpdate.Text = versionValue;

                        fUpdateContScale.Visible = true;
                        fConfigFooter.Visible = true;
                        DivSuccessConfig.Visible = false;
                        DivErrorConfig.Visible = false;
                        fAdd.Visible = false;
                    }
                    else if (HttpContext.Current.Session["tabStatus"] == "disc")
                    {
                        tUpDiscScale.Text = textValue;

                        fUpdateDisc.Visible = true;
                        fConfigFooter.Visible = true;
                        DivSuccessConfig.Visible = false;
                        DivErrorConfig.Visible = false;
                        fAddDiscreteScale.Visible = false;
                    }
                    else if (HttpContext.Current.Session["tabStatus"] == "discUpdate")
                    {
                        tContScaleUpdate.Text = "";
                        tContScaleVerUpdate.Text = "";
                        //tUpDiscScale.Text = "";
                        fUpdateDisc.Visible = false;
                        fAddDiscreteScale.Visible = true;
                    }
                    else if (HttpContext.Current.Session["tabStatus"] == "contUpdate")
                    {
                        fUpdateContScale.Visible = false;
                        fAdd.Visible = true;
                    }
                }
            }
        }

        public void BindGridTypeUser()
        {
            // add info in gridview
            gvTypeUser.DataSource = DataAccess.DataAccessUser.getTypeUser();
            gvTypeUser.DataBind();

        }

        public void BindGridTypeScaleContinuous()
        {
            // add info in gridview
            List<TypScaleContinuous> data = DataAccess.DataAccessTestCase.getScaleContinuous();
            gvTypeScaleContinuous.DataSource = data;
            gvTypeScaleContinuous.DataBind();


        }

        public void BindGridTypeScaleDiscrete()
        {
            // add info in gridview
            List<TypScaleDiscrete> data = DataAccess.DataAccessTestCase.getScaleDiscrete();
            gvTypeScaleDiscrete.DataSource = data;
            gvTypeScaleDiscrete.DataBind();
        }

        // new type user, eventargs-> nuovo evento se clicco su button
        protected void btnAddTypeUser_Click(object sender, EventArgs e)
        {
            // variabile tu della classe TypUser
            TypUser tu = new TypUser();
            tu.DescriptionTypUser = txtDescriptionType.Text;
            DataAccess.DataAccessUser.InsertTypeUser(tu); // id non lo inserisco perche è autoincrementante 

            BindGridTypeUser(); // refresh gridview 
            txtDescriptionType.Text = string.Empty;// textbox empty
        }

        // new type scalecontinuous, eventargs-> nuovo evento se clicco su button
        protected void btnAddScaleContinuous_Click(object sender, EventArgs e)
        {
            // variabile tu della classe TypScaleContinuous
            TypScaleContinuous tsc = new TypScaleContinuous();
            tsc.DescriptionScaleContinuous = txtScaleContinuous.Text;
            tsc.verScaleCont = Convert.ToInt16(tVerScaleCont.Text);


            if (fuLoadImageMin.PostedFile != null)
            {
                tsc.PathImageMin = Constant.sReferencePath + @"\Continuous\" + tsc.DescriptionScaleContinuous + "\\" + tsc.verScaleCont + "\\Worst\\" + fuLoadImageMin.PostedFile.FileName;
                saveOnServer(Constant.sReferencePath + @"\Continuous\" + tsc.DescriptionScaleContinuous + "\\" + tsc.verScaleCont + "\\Worst", fuLoadImageMin);

            }

            if (fuLoadImageMax.PostedFile != null)
            {
                tsc.PathImageMax = Constant.sReferencePath + @"\Continuous\" + tsc.DescriptionScaleContinuous + "\\" + tsc.verScaleCont + "\\Best\\" + fuLoadImageMax.PostedFile.FileName;// fUploadRefImage(fuLoadImageMax.PostedFile.FileName, "maxImg", "Best", "Continuous/" + txtScaleContinuous.Text + "_v" + tVerScaleCont.Text); - Martin
                saveOnServer(Constant.sReferencePath + @"\Continuous\" + tsc.DescriptionScaleContinuous + "\\" + tsc.verScaleCont + "\\Best", fuLoadImageMax);
            }



            DataAccess.DataAccessTestCase.InsertTypeScaleContinuous(tsc); // id non lo inserisco perche è autoincrementante 

            BindGridTypeScaleContinuous(); // refresh gridview 
            txtScaleContinuous.Text = string.Empty;// textbox empty
            tVerScaleCont.Text = string.Empty;// textbox empty
            DivInfo.Visible = true;
            tContScaleVerUpdate.Text = "1";
            tContScaleUpdate.Text = "";
        }

        protected void btnAddScaleDiscrete_Click(object sender, EventArgs e)
        {
            // variabile tu della classe TypScaleContinuous
            TypScaleDiscrete tsc = new TypScaleDiscrete();
            tsc.DescriptionScaleDiscrete = tDiscreteScale.Text;


            if (fuWorstImgDiscrete.PostedFile != null)
            {
                tsc.PathImageMin = Constant.sReferencePath + @"\Discrete\" + tsc.DescriptionScaleDiscrete + @"\Worst\" + fuWorstImgDiscrete.PostedFile.FileName;
                saveOnServer(Constant.sReferencePath + @"\Discrete\" + tsc.DescriptionScaleDiscrete + @"\Worst\", fuWorstImgDiscrete);
            }

            if (fuBestImgDiscrete.PostedFile != null)
            {
                tsc.PathImageMax = Constant.sReferencePath + @"\Discrete\" + tsc.DescriptionScaleDiscrete + @"\Best\" + fuBestImgDiscrete.PostedFile.FileName;
                saveOnServer(Constant.sReferencePath + @"\Discrete\" + tsc.DescriptionScaleDiscrete + @"\Best\", fuBestImgDiscrete);
            }

            DataAccess.DataAccessTestCase.InsertTypeScaleDiscrete(tsc);

            BindGridTypeScaleDiscrete(); // refresh gridview 
            tDiscreteScale.Text = string.Empty;// textbox empty
            Response.Redirect("~/configFramework/AddTypeUser.aspx#tabs-3");
        }

        protected void gvTypeUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id_typeuser = Convert.ToInt32(gvTypeUser.DataKeys[e.RowIndex].Value.ToString());
            Result res = DataAccess.DataAccessUser.DeleteTypeUser(id_typeuser);
            BindGridTypeUser();

            if (res.result)
            {
                fConfigFooter.Visible = false;
                DivSuccessConfig.Visible = false;
                DivErrorConfig.Visible = false;
            }
            else
            {
                fConfigFooter.Visible = true;
                DivSuccessConfig.Visible = false;
                DivErrorConfig.Visible = true;
                lblErrorConfig.Text = res.Message;
            }

        }

        protected void gvTypeUser_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            // get id user my datakey selected in the gridview
            int id_usertype = Convert.ToInt32(gvTypeUser.DataKeys[e.NewSelectedIndex].Value);

            hddIdTypeUser.Value = id_usertype.ToString();
            Class.TypUser typuser = DataAccess.DataAccessUser.getTypeUser(id_usertype);

            UpdateTypeUserText.Text = typuser.DescriptionTypUser;

            fUpdateTypeUser.Visible = true;
            fConfigFooter.Visible = true;
            DivSuccessConfig.Visible = false;
            DivErrorConfig.Visible = false;

            fAddUserType.Visible = false;

        }

        protected void btnUpdateTypeUser_Click(object sender, EventArgs e)
        {
            Class.TypUser typuser = new TypUser();
            typuser.ID_Typ = Convert.ToInt32(hddIdTypeUser.Value);
            typuser.DescriptionTypUser = UpdateTypeUserText.Text;
            Result res = DataAccess.DataAccessUser.UpdateTypeUser(typuser);
            BindGridTypeUser();


            if (res.result)
            {
                DivSuccessConfig.Visible = true;
                DivErrorConfig.Visible = false;
                fUpdateTypeUser.Visible = false;
            }
            else
            {
                DivSuccessConfig.Visible = false;
                DivErrorConfig.Visible = true;
                lblErrorConfig.Text = res.Message;
            }
            fAddUserType.Visible = true;
            fUpdateTypeUser.Visible = false;
            UpdateTypeUserText.Text = "";
        }

        protected void gvTypeScaleContinuous_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id_ScaleContinuous = Convert.ToInt32(gvTypeScaleContinuous.DataKeys[e.RowIndex].Value.ToString());
            Result res = DataAccess.DataAccessTestCase.DeleteScaleContinuous(id_ScaleContinuous);
            BindGridTypeScaleContinuous();
            if (res.result)
            {
                fConfigFooter.Visible = false;
                DivSuccessConfig.Visible = false;
                DivErrorConfig.Visible = false;
            }
            else
            {
                fConfigFooter.Visible = true;
                DivSuccessConfig.Visible = false;
                DivErrorConfig.Visible = true;
                lblErrorConfig.Text = res.Message;
            }
            Response.Redirect("~/configFramework/AddTypeUser.aspx#tabs-2");
        }

        protected void gvTypeScaleContinuous_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            // get id scalecontinuous my datakey selected in the gridview

            int id_scalecontinuous = Convert.ToInt32(gvTypeScaleContinuous.DataKeys[e.NewSelectedIndex].Value);


            HttpContext.Current.Session["hddIdScaleContinuous"] = id_scalecontinuous.ToString();
            hddIdScaleContinuous.Value = id_scalecontinuous.ToString();

            TypScaleContinuous scalec = DataAccess.DataAccessTestCase.getScaleContinuous(id_scalecontinuous);

            tContScaleUpdate.Text = scalec.DescriptionScaleContinuous;
            tContScaleVerUpdate.Text = scalec.verScaleCont.ToString();

            textValue = scalec.DescriptionScaleContinuous;
            versionValue = scalec.verScaleCont.ToString();

            fUpdateContScale.Visible = true;
            fConfigFooter.Visible = true;
            DivSuccessConfig.Visible = false;
            DivErrorConfig.Visible = false;
            fAdd.Visible = false;
            HttpContext.Current.Session["tabStatus"] = "cont";
            Response.Redirect("~/configFramework/AddTypeUser.aspx#tabs-2");
        }

        protected void btnUpdateContScale_Click(object sender, EventArgs e)
        {
            TypScaleContinuous tsc = new TypScaleContinuous();
            tsc.DescriptionScaleContinuous = tContScaleUpdate.Text;
            tsc.ID_TypScaleContinuous = Convert.ToInt32(HttpContext.Current.Session["hddIdScaleContinuous"]);
            tsc.verScaleCont = Convert.ToInt16(tContScaleVerUpdate.Text);

            TypScaleContinuous scalec = DataAccess.DataAccessTestCase.getScaleContinuous(tsc.ID_TypScaleContinuous);



            if (!String.IsNullOrEmpty(fuWorstUpdate.FileName))
            {
                if (((scalec.PathImageMin.Split('\\')).Last()) != fuWorstUpdate.FileName)
                {
                    saveOnServer(Constant.sReferencePath + @"\Continuous\" + tsc.DescriptionScaleContinuous + "\\" + tsc.verScaleCont + "\\Worst", fuWorstUpdate);
                }

                tsc.PathImageMin = Constant.sReferencePath + @"\Continuous\" + tsc.DescriptionScaleContinuous + "\\" + tsc.verScaleCont + "\\Worst\\" + fuWorstUpdate.PostedFile.FileName;
            }
            else
            {
                tsc.PathImageMin = null;
            }


            if (!String.IsNullOrEmpty(fuBestUpdate.FileName))
            {
                if (((scalec.PathImageMax.Split('\\')).Last()) != fuBestUpdate.FileName)
                {
                    saveOnServer(Constant.sReferencePath + @"\Continuous\" + tsc.DescriptionScaleContinuous + "\\" + tsc.verScaleCont + "\\Best", fuBestUpdate);
                }
                tsc.PathImageMax = Constant.sReferencePath + @"\Continuous\" + tsc.DescriptionScaleContinuous + "\\" + tsc.verScaleCont + "\\Best\\" + fuBestUpdate.PostedFile.FileName;// fUploadRefImage(fuLoadImageMax.PostedFile.FileName, "maxImg", "Best", "Continuous/" + txtScaleContinuous.Text + "_v" + tVerScaleCont.Text); - Martin

            }
            else
            {
                tsc.PathImageMax = null;
            }

            Result res = DataAccess.DataAccessTestCase.UpdateScaleContinuous(tsc.ID_TypScaleContinuous, tsc.DescriptionScaleContinuous, tsc.verScaleCont, tsc.PathImageMin, tsc.PathImageMax);
            BindGridTypeScaleContinuous();


            if (res.result)
            {
                DivSuccessConfig.Visible = true;
                DivErrorConfig.Visible = false;
                fUpdateTypeUser.Visible = false;
            }
            else
            {
                DivSuccessConfig.Visible = false;
                DivErrorConfig.Visible = true;
                lblErrorConfig.Text = res.Message;
            }

            fUpdateContScale.Visible = false;
            fAdd.Visible = true;
            HttpContext.Current.Session["tabStatus"] = "contUpdate";
            Response.Redirect("~/configFramework/AddTypeUser.aspx#tabs-2");
        }

        protected void gvTypeScaleDiscrete_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id_ScaleDiscrete = Convert.ToInt32(gvTypeScaleContinuous.DataKeys[e.RowIndex].Value.ToString());
            Result res = DataAccess.DataAccessTestCase.DeleteScaleDiscrete(id_ScaleDiscrete);
            BindGridTypeScaleDiscrete();
            if (res.result)
            {
                fConfigFooter.Visible = false;
                DivSuccessConfig.Visible = false;
                DivErrorConfig.Visible = false;
            }
            else
            {
                fConfigFooter.Visible = true;
                DivSuccessConfig.Visible = false;
                DivErrorConfig.Visible = true;
                lblErrorConfig.Text = res.Message;
            }
            Response.Redirect("~/configFramework/AddTypeUser.aspx#tabs-3");
        }

        protected void gvTypeScaleDiscrete_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

            // get id scalecontinuous my datakey selected in the gridview

            int id_scalediscrete = Convert.ToInt32(gvTypeScaleDiscrete.DataKeys[e.NewSelectedIndex].Value);

            hddIdScaleDiscrete.Value = id_scalediscrete.ToString();
            HttpContext.Current.Session["hddIdScaleDiscrete"] = id_scalediscrete.ToString();
            TypScaleDiscrete tsc = DataAccess.DataAccessTestCase.getScaleDiscrete(id_scalediscrete);
            tUpDiscScale.Text = tsc.DescriptionScaleDiscrete;

            textValue = tsc.DescriptionScaleDiscrete;

            fUpdateDisc.Visible = true;
            fConfigFooter.Visible = true;
            DivSuccessConfig.Visible = false;
            DivErrorConfig.Visible = false;
            fAddDiscreteScale.Visible = false;
            HttpContext.Current.Session["tabStatus"] = "disc";
            Response.Redirect("~/configFramework/AddTypeUser.aspx#tabs-3");

        }

        protected void btnUpdateDiscScale_Click(object sender, EventArgs e)
        {
            TypScaleDiscrete tsc = new TypScaleDiscrete();
            tsc.DescriptionScaleDiscrete = tUpDiscScale.Text;
            tsc.ID_TypScaleDiscrete = Convert.ToInt32(HttpContext.Current.Session["hddIdScaleDiscrete"]);

            TypScaleDiscrete scalec = DataAccess.DataAccessTestCase.getScaleDiscrete(tsc.ID_TypScaleDiscrete);

            if (!String.IsNullOrEmpty(fuWorstUpdateDisc.FileName))
            {
                if (((scalec.PathImageMin.Split('\\')).Last()) != fuWorstUpdateDisc.FileName)
                {
                    saveOnServer(Constant.sReferencePath + @"\Discrete\" + scalec.DescriptionScaleDiscrete + @"\Worst\", fuWorstUpdateDisc);
                }
                tsc.PathImageMin = Constant.sReferencePath + @"\Discrete\" + scalec.DescriptionScaleDiscrete + @"\Worst\" + fuWorstUpdateDisc.PostedFile.FileName;

            }
            else
            {
                tsc.PathImageMin = null;
            }


            if (!String.IsNullOrEmpty(fuBestUpdateDisc.FileName))
            {
                if (((scalec.PathImageMax.Split('\\')).Last()) != fuBestUpdateDisc.FileName)
                {
                    saveOnServer(Constant.sReferencePath + @"\Discrete\" + scalec.DescriptionScaleDiscrete + @"\Best\", fuBestUpdateDisc);
                }
                tsc.PathImageMax = Constant.sReferencePath + @"\Discrete\" + scalec.DescriptionScaleDiscrete + @"\Best\" + fuBestUpdateDisc.PostedFile.FileName;

            }
            else
            {
                tsc.PathImageMax = null;
            }

            Result res = DataAccess.DataAccessTestCase.UpdateScaleDiscrete(tsc.ID_TypScaleDiscrete, tsc.DescriptionScaleDiscrete, tsc.PathImageMin, tsc.PathImageMax);
            BindGridTypeScaleDiscrete();


            if (res.result)
            {
                DivSuccessConfig.Visible = true;
                DivErrorConfig.Visible = false;
                fUpdateTypeUser.Visible = false;
            }
            else
            {
                DivSuccessConfig.Visible = false;
                DivErrorConfig.Visible = true;
                lblErrorConfig.Text = res.Message;
            }

            tUpDiscScale.Text = "";
            fUpdateDisc.Visible = false;
            fAddDiscreteScale.Visible = true;


            HttpContext.Current.Session["tabStatus"] = "discUpdate";
            Response.Redirect("~/configFramework/AddTypeUser.aspx#tabs-3");
        }

        protected void btnCancelTypeUser_Click(object sender, EventArgs e)
        {
            fConfigFooter.Visible = false;
            DivSuccessConfig.Visible = false;
            DivErrorConfig.Visible = false;
            fUpdateTypeUser.Visible = false;
            fAddUserType.Visible = true;
            UpdateTypeUserText.Text = "";
        }

        protected void btnCancelContScale_Click(object sender, EventArgs e)
        {
            fConfigFooter.Visible = false;
            DivSuccessConfig.Visible = false;
            DivErrorConfig.Visible = false;
            fUpdateContScale.Visible = false;
            fAdd.Visible = true;

            HttpContext.Current.Session["tabStatus"] = "contUpdate";
            Response.Redirect("~/configFramework/AddTypeUser.aspx#tabs-2");
        }

        protected void btnCancelDiscScale_Click(object sender, EventArgs e)
        {
            fConfigFooter.Visible = false;
            DivSuccessConfig.Visible = false;
            DivErrorConfig.Visible = false;
            fUpdateDisc.Visible = false;
            fAddDiscreteScale.Visible = true;

            HttpContext.Current.Session["tabStatus"] = "discUpdate";
            Response.Redirect("~/configFramework/AddTypeUser.aspx#tabs-3");
        }

        protected void saveOnServer(string path, FileUpload file)
        {
            // check if folder exists, if this is not the case create folder 
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            file.SaveAs(path + "\\" + file.FileName);

        }
    }
}