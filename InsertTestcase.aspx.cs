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
using System.IO;
using LabelingFramework.Utility;
using System.Transactions;
using System.Web.Routing;
using System.Net.Http;
using System.Net.Http.Headers;
using LabelingFramework.Class;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Diagnostics; // *for testing purposes*


//*********************************************************************************************************//
//  page which creates / updates a test case (stored in MySQL-DB)
//*********************************************************************************************************//

namespace LabelingFramework
{
    public partial class InsertTestcase : System.Web.UI.Page
    {
      
        static List<Class.Group> listGroup = new List<Class.Group>();
        List<string> listtypechecked = new List<string>();
        List<Class.Path> listDatabase = DataAccessPath.getPaths();
        List<TypScaleContinuous> tsc = DataAccessTestCase.getScaleContinuous();
        List<Class.Group> listGroup_ = new List<Group>();
        
        static List<Class.Group> listAddedGroups = new List<Group>();
        static List<long> listRemovedGroups = new List<long>();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                if (HttpContext.Current.Session["User"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");
                }else{

                    listGroup = new List<Class.Group>();
                    listAddedGroups = new List<Group>();
                    listRemovedGroups = new List<long>();

                    // check for duplicate descriptions, add the version to the list and remove duplicates
                    for (int i = 0; i < tsc.Count(); i++)
                    {
                        tsc[i].scaleVersions = new List<string>();
                        tsc[i].scaleVersions.Add(tsc[i].verScaleCont + "");

                        for (int c = i + 1; c < tsc.Count(); c++)
                        {
                            if (tsc[i].DescriptionScaleContinuous == tsc[c].DescriptionScaleContinuous)
                            {
                                tsc[i].scaleVersions.Add(tsc[c].verScaleCont + "");
                                tsc.RemoveAt(c);
                                c--;
                            }
                        }
  
                        tsc[i].ddlContinuous.DataSource = tsc[i].scaleVersions;
                        tsc[i].ddlContinuous.DataBind();
                    }


                PopulateTypeUser();// insert in the drop down list all existing users
                PopulateTypeScaleContinuous();// insert in the drop down list all existing scale continous
                PopulateTypeScaleDiscrete();
                PopulateDatabases();

                cbxDiscreteScale.Attributes.Add("style", "display:none");

                string labeling = listDatabase[0].DatabasePath;
                Constant.currentDatabase = labeling;

                long? id_TestCase = null;
                if (Session["IdTestCase"] != null)
                    id_TestCase = Convert.ToInt64(Session["IdTestCase"]);
                if (!IsPostBack)
                {
                    listGroup = new List<Class.Group>();
                    // if admin wants to change a test case from view test case
                    if (id_TestCase != null)// es gibt schon ein id
                    {
                        //listGroup = new List<Class.Group>();
                        long idTestCase = (long)id_TestCase;
                        Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase().Where(p => p.IDTestcase == idTestCase).FirstOrDefault();
                        List<Class.TypUser> listtu = DataAccess.DataAccessUser.GetTypeUserForTestCase(idTestCase);
                        listGroup_ = DataAccess.DataAccessTestCase.getGroupFromIdTestCase(idTestCase);
                        // name test case and testquestion cant be changed
                        txtNameTestCase.Text = tc.NameTestCase;
                        txtNameTestCase.Enabled = false;
                        txtTestQuestion.Text = tc.TestQuestion;
                        txtTestQuestion.Enabled = false;
                        txtGeneralInfo.Text = tc.GeneralInfo;
                        cbxActiveLearning.Checked = tc.ActiveLearning;

                        if (tc.ActiveLearning) {
                            txtActiveUserThreshold.Text = tc.userThreshold + "";
                            txtActiveInitialThreshold.Text = tc.initialThreshold + "";
                        }

                        hddTCSeed.Value = Convert.ToString(tc.iSeed);

                        // schaut welche skala ausgesucht ist
                        if (tc.DiskreteScale)
                            rblTypeScale.SelectedValue = "0";
                        else
                            rblTypeScale.SelectedValue = "1";

                        rblTypeScale.Enabled = false;

                        btnInsertTestCase.Visible = false;// no button insert, but instead later button change

                        // takes all the selected type of user
                        foreach (Class.TypUser tu in listtu)
                        {
                            ListItem app = rblTypeUserAssigned.Items.FindByValue(tu.ID_Typ.ToString());

                            int id = rblTypeUserAssigned.Items.IndexOf(app);

                            rblTypeUserAssigned.Items[id].Selected = true;
                        }

                        // insert all the already created images groups
                        foreach (Class.Group g in listGroup_)
                        {
                            List<Class.GroupImage> listGroupImage = DataAccess.DataAccessTestCase.GetGrouphasImageFromGroup(g.IdGroup, 0);
                            Class.Group appgroup = new Class.Group();
                            appgroup.Name = g.Name;
                            appgroup.GroupExistDB = true;
                            appgroup.IdGroup = g.IdGroup;
                            appgroup.IdTestCase = g.IdTestCase;
                            appgroup.ReferenceIsGlobal = g.ReferenceIsGlobal;
                            appgroup.IsPatientChosen = g.IsPatientChosen;
                            appgroup.PageStyle = g.PageStyle;
                            appgroup.GroupHasReference = g.GroupHasReference;
                            appgroup.ImagesPerPage = g.ImagesPerPage;



                            if (g.ReferenceIsGlobal)
                                appgroup.ReferenceName = "GLOBAL";
                            else
                                appgroup.ReferenceName = "";

                            int numberOfImages = 0;
                            g.ImagesPerPage = 0;
                            string Patient = "";
                            for (int i = 0; i < listGroupImage.Count; i++ )
                            {
                                if(i == 0){
                                    Patient = listGroupImage[0].PatienteName;
                                }

                                if (listGroupImage[i].PatienteName == Patient)
                                {
                                    g.ImagesPerPage++;
                                }

                                appgroup.LoopOver += listGroupImage[i].Path.Replace("/", "_") + System.Environment.NewLine;
                                numberOfImages++;
                                if (listGroupImage[i].IsReference)
                                    appgroup.ReferenceName = listGroupImage[i].Path.Split('/')[1];
                            }

                            if (g.PageStyle > 0) {
                                g.ImagesPerPage = Convert.ToInt32(g.PageStyle);
                            }

                            if (g.ImagesPerPage < 1)
                            {
                                g.ImagesPerPage = -1;
                            }

                            appgroup.PageStyleDescription = getText(g.PageStyle, numberOfImages);
                            appgroup.OverallNumber = numberOfImages;
                            double numberOfPages = (numberOfImages / g.ImagesPerPage);
                            appgroup.NumberOfPages = (int)Math.Ceiling(numberOfPages);


                            listGroup.Add(appgroup);

                        }

                        Session["Group"] = listGroup;
                        gvGroupName.DataSource = listGroup;
                        gvGroupName.DataBind();
                        EnabledSelectedTypeUser();
                        btnUpdateTestCase.Visible = true;

                    }
                    else
                    {
                        btnUpdateTestCase.Visible = false;// es wurde kein id gefunden und desshalb muessen keine infos geladen werden
                    }


                     
                }
                        }

                gvContinuous.DataSource = tsc;
                gvContinuous.DataBind();
                gvContinuous.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true); 
                }
            if (Session["Group"] != null) {

                listGroup = (List<Class.Group>)Session["Group"];
            
            }

            if (Session["typeckeched"] != null) {

                 listtypechecked = (List<string>)Session["typeckeched"];

            }
        }


        // aspsnippets.com/Articles/How-to-populate-DropDownList-in-GridView-in-ASPNet.aspx
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddlMin = (e.Row.FindControl("ddlContinuous") as DropDownList);

                for(int i = 0; i < tsc.Count; i++){
                    if (e.Row.RowIndex == i)
                    {
                        ddlMin.DataSource = tsc[i].scaleVersions;
                        ddlMin.DataBind();
                        tsc[i].ddlContinuous.Items[0].Selected = true;      // select first entry of drop down list
                    }
                }

            }
        }




        private string getText(long id, int numberOfImages)
        {
            string text = "";

                                       switch (id) {
                               case 1:
                                   {
                                       try
                                       {
                                           if (numberOfImages < Int32.Parse(txtmImagesPerPage.Text))
                                           {
                                               text = numberOfImages + "";
                                           }
                                           else if (1 > Int32.Parse(txtmImagesPerPage.Text))
                                           {
                                               text = "1";
                                           }
                                           else{
                                               text = txtmImagesPerPage.Text + "";
                                           }
                                       }
                                       catch (Exception) { }

                                       break;
                                   }
                               default:
                                   {
                                       text = "all selected images of one patient";
                                       break;
                                   }

                           }

            return text;
        }





        // click button 
        protected void btnInsertTestCase_Click(object sender, EventArgs e)
        {
            try
            {
                List<long> listScales = GetSelectedTypeScaleContinuous();
                if (Page.IsValid)
                {

                        // takes infos from textboxes 
                        Class.TestCase tc = new Class.TestCase();
                        tc.NameTestCase = txtNameTestCase.Text;
                        tc.DiskreteScale = rblTypeScale.SelectedValue == "0" ? true : false;
                        tc.GeneralInfo = txtGeneralInfo.Text;
                        tc.State = (int)LabelingFramework.Utility.Enum.StatoTestCase.towork;
                        tc.TestQuestion = txtTestQuestion.Text;
                        if (txtminvalue.Text != string.Empty)
                        {
                            tc.MinDiskreteScale = Convert.ToInt32(txtminvalue.Text);
                        }
                        else
                        {
                            tc.MinDiskreteScale = 0;
                        }
                        if (txtmaxvalue.Text != string.Empty)
                        {
                            tc.MaxDiskreteScale = Convert.ToInt32(txtmaxvalue.Text);
                        }
                        else
                        {
                            tc.MaxDiskreteScale = 5;
                        }

                        tc.dbPath = Constant.currentDatabase; // - Martin
                        tc.ActiveLearning = cbxActiveLearning.Checked;


                    if (tc.ActiveLearning)
                    {      // only first group is added in case of active learning
                        while (listGroup.Count > 1)
                        {
                            listGroup.RemoveAt(listGroup.Count - 1);
                        }
                    }


                    tc.userThreshold = Int32.Parse(txtActiveUserThreshold.Text);
                    tc.initialThreshold = Int32.Parse(txtActiveInitialThreshold.Text);
                    tc.iSeed = Guid.NewGuid().GetHashCode();


                    //List<long> listScales;
                    if (tc.DiskreteScale)
                    {
                        listScales = new List<long>();
                        listScales.Add(GetSelectedScaleDiscrete());
                    }
                    else
                    {
                        listScales = GetSelectedTypeScaleContinuous();
                    }


                    Int64 IdTestCase = 0;



                    // checks is everything is fine between mysql, testcase ,repository und bilder , wenn alles funktioniert dann wird das testcase gespeichert sonst nicht
                    using (TransactionScope scope = new TransactionScope())
                    {


                        DataAccess.DataAccessTestCase.InsertTestCase(tc, ref IdTestCase);
                        DataAccess.DataAccessTestCase.InsertRelationshipTestTypeUser(IdTestCase, GetSelectedTypeUser());
                        DataAccess.DataAccessTestCase.InsertRelationshipTestScale(IdTestCase, listScales, tc.DiskreteScale);
                        DataAccess.DataAccessTestCase.InsertGroup(IdTestCase, listGroup);
                        writeConfigFile(IdTestCase);
                        scope.Complete();

                        DivSuccess.Visible = true;
                        DivError.Visible = false;

                    }


                    if (Constant.sendNotificationMailTC)
                    {
                        List<Class.User> users = DataAccess.DataAccessTestCase.getUsersFromTestCaseID(IdTestCase);
                        //string subject = "New test case '" + tc.NameTestCase +  "' available on labeling framework website";
                        MyEmail mail = new MyEmail();


                        foreach (Class.User user in users)
                        {
                            //string messageText = "Dear " + user.title + " " + user.Surname + ",\n a new test case is available for labeling on: " + "<a href=''" + Constant.webSiteAddress + "''>" + Constant.webSiteAddress + "</a> ";

                            List<string> message = Constant.testCaseNotificationMail(user, tc);

                            mail.sendEmail(user.Email, message[0], message[1], false);
                        }
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "fVersionScale", "fVersionScale();", true);
                    Response.Redirect("~/TestCase/ViewTestCase.aspx"); // return to previous page ('ViewTestCase') 
                }
                else {
                    DivError.Visible = true;
                    lblError.Text = "Pleasse check if all fields are filled out properly";
                    DivSuccess.Visible = false;
                }
            }
            catch (Exception ex)
            {
                int line = (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber(); 
                DivError.Visible = true;
                lblError.Text = ex.Message + " Error occurred in C# (\"InsertTestCase.aspx\" line: " + line + ")";
                DivSuccess.Visible = false;
            }
            
        }

        protected void RemoveGroup_Click(object sender, EventArgs e)
        {
            Button btnID = (Button)sender;
            int groupId = Int32.Parse(btnID.CommandArgument);
            for (int i = 0; i < listGroup.Count; i++)
            {
                if (listGroup[i].IdGroup == groupId)
                {
                    listRemovedGroups.Add(groupId);
                    listGroup.RemoveAt(i);
                    break;
                }
            }

            gvGroupName.DataSource = listGroup;
            gvGroupName.DataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true); 
        }



        //Update Test Case
        protected void btnUpdateTestCase_Click(object sender, EventArgs e)
        {
            // checks is everything is fine between mysql, testcase ,repository und bilder , wenn alles funktioniert dann wird das testcase gespeichert sonst nicht
            using (TransactionScope scope = new TransactionScope())
            {
                Int64 IdTestCase = Convert.ToInt64(Session["IdTestCase"]);

                bool activeLearning = false;
                List<Class.Group> groups = new List<Class.Group>();


                if (cbxActiveLearning.Checked)  // only first group is added in case of active learning
                {
                    activeLearning = true;

                    while (listGroup.Count > 1)
                    {
                        listGroup.RemoveAt(listGroup.Count - 1);
                    }
                    groups = listGroup;
                }else {
                    groups = listAddedGroups;
                }

                int initalTreshold = -1;
                int userThreshold = -1;

                if (activeLearning) {
                    initalTreshold = Int32.Parse(txtActiveInitialThreshold.Text);
                    userThreshold = Int32.Parse(txtActiveUserThreshold.Text);
                }

                DataAccess.DataAccessTestCase.UpdateTestCase(IdTestCase, activeLearning, txtGeneralInfo.Text, initalTreshold, userThreshold);
                DataAccess.DataAccessTestCase.InsertRelationshipTestTypeUser(IdTestCase, GetSelectedTypeUser());
                DataAccess.DataAccessTestCase.InsertGroup(IdTestCase, groups);
                listAddedGroups = new List<Group>();
                writeConfigFile(IdTestCase);
                
                
                DivSuccess.Visible = true;
                DivError.Visible = false;
                Session["IdTestCase"] = null;

                if (listRemovedGroups.Count > 0)
                {
                    DataAccess.DataAccessTestCase.deleteGroup(IdTestCase, listRemovedGroups);
                    listRemovedGroups = new List<long>();
                }
                scope.Complete();
            }



            ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true); 
        }

        // kontrolliert das mindestens ein type user eingegeben worden ist
        protected void CVTypeUserAssigned_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
            for (int i = 0; i < rblTypeUserAssigned.Items.Count; i++)
            {

                if (rblTypeUserAssigned.Items[i].Selected)
                {
                    args.IsValid = true;
                    break;
                }
            
            }

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Class.Group group = new Class.Group();
            string labeling = Constant.currentDatabase;
            //string labeling = DataAccessBase.GetLabeling();

           int numberOfImages = 0;
            
            group.IsPatientChosen = Convert.ToBoolean(HiddenFieldIsPatientChosen.Value);

            // - Martin - split at '|' (no regular directory symbol)
            string[] path = HiddenFieldPathSel.Value.Split('|');
            string reference = HiddenFieldReferenceName.Value;

            group.ImagesPerPatient = Int32.Parse(hddNumberImagePatient.Value);

            if (reference != "Choose a Reference")
            {
                group.GroupHasReference = true;
            }
            bool isNotLocal = false;

            // definition of variables used later
            if ((reference.ToUpper() == "GLOBAL") || (reference.ToUpper() == "CHOOSE A REFERENCE"))
                isNotLocal = true;

             if ((reference.ToUpper() == "GLOBAL"))
                 group.ReferenceIsGlobal=true;

           for (int i = 0; i < path.Length-1; i++)
           {
               numberOfImages++;
               Class.Directory dir = new Class.Directory();
               dir.Path = path[i];

               if (!isNotLocal)
               {
                   if (path[i].ToUpper().Contains(reference.ToUpper()))
                   {
                       dir.IsReference = true;
                       group.ReferenceName = path[i].Split('/')[1];
                       
                   }
                   else
                       dir.IsReference = false;

                   group.LoopOver += path[i].Replace("/", "_") + System.Environment.NewLine;
               }
               else
               {
                   group.LoopOver += path[i].Replace("/","_") + System.Environment.NewLine;

                   if (group.ReferenceIsGlobal)
                       group.ReferenceName = "GLOBAL";
                   else
                       group.ReferenceName = "";
               }

               
               group.SubDirectory.Add(dir);               
           
           }
           group.PageStyle = Int64.Parse(hddImagesPerPage.Value);
           int images = 0;      // max amount of images to be shown on a a single page
           switch (group.PageStyle) {
               case 0: { group.ImagesPerPage = -1; images = group.ImagesPerPatient ;break; }
              // case 1: { group.ImagesPerPage = 1; break; }
               case 1: { try { group.ImagesPerPage = Int32.Parse(txtmImagesPerPage.Text); group.ImagesPerPage = (group.ImagesPerPage < 0) ? -1 : group.ImagesPerPage; } catch (Exception) { group.ImagesPerPage = -1; }; break; }
               default: { group.ImagesPerPage = -1; break; }
           }


            // check if value is within codomain [1, number of selected images]
           if (group.ImagesPerPatient == 0) { group.ImagesPerPatient = 1; }

           if (0 < group.ImagesPerPage)
           {
               images = group.ImagesPerPage;
           }

           if (group.ImagesPerPatient < group.ImagesPerPage)
           {
               images = group.ImagesPerPatient;
           }

     
            string patient = "";
            int numberOfPatients = 0;

            for(int i = 0; i < path.Length; i++){
                if ((path[i] != "") && (path[i].Substring(0, path[i].IndexOf("/")) != patient))
                {
                    patient = path[i].Substring(0, path[i].IndexOf("/"));
                    numberOfPatients++;
                }
            }

            if (group.GroupHasReference && !group.ReferenceIsGlobal) {
                group.ImagesPerPatient--;
            }

           group.PageStyleDescription = getText(group.PageStyle, images);
           group.OverallNumber = numberOfImages;
           double numberOfPages = ((double)group.ImagesPerPatient / images);
           group.NumberOfPages = (int)Math.Ceiling(numberOfPages) * numberOfPatients;



           listAddedGroups.Add(group);
           listGroup.Add(group);
           

            // to avoid adding the same group again after a postback
           if (listGroup.Count > 1)
           {
               if ((listGroup[listGroup.Count - 1].GroupHasReference == listGroup[listGroup.Count - 2].GroupHasReference) && (listGroup[listGroup.Count - 1].LoopOver == listGroup[listGroup.Count - 2].LoopOver) && (listGroup[listGroup.Count - 1].ReferenceName == listGroup[listGroup.Count - 2].ReferenceName))
               {
                   listGroup.RemoveAt(listGroup.Count() - 1);
               }
           }

           


           if (listGroup[listGroup.Count() - 1].LoopOver == null)
           {
               listGroup.RemoveAt(listGroup.Count() - 1); // remove last entry in case of an added empty group
           }
           else {
               group.Name = (listGroup.Count()).ToString();
               Session["Group"] = listGroup;
               HiddenFieldPathSel.Value = "";
               gvGroupName.DataSource = listGroup;
               gvGroupName.DataBind();
           }
           //for (int i = 0; i < ddlDatabase.Items.Count; i++)
           //{
           //    if (ddlDatabase.Items[i].Selected)
           //    {
           //        Constant.currentDatabase = ddlDatabase.Items[i].Text;
           //        break;
           //    }
           //}
           ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true);   
        
        }

        // checks if the testcase name doesnt exist a second time
        protected void CVTestCaseName_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = true;

            if (DataAccessTestCase.VerifyExistTestCaseName(txtNameTestCase.Text))
                args.IsValid = false;
        }

        // takes selected type users from the page
        public List<long> GetSelectedTypeUser()
        {
            List<long> List = new List<long>();

            for (int i = 0; i < rblTypeUserAssigned.Items.Count; i++)
            {

                if ((rblTypeUserAssigned.Items[i].Selected) && (rblTypeUserAssigned.Items[i].Enabled))
                {
                    List.Add(Convert.ToInt64(rblTypeUserAssigned.Items[i].Value));

                }

            }
            return List;
        }

        // enabled saved type user if not needed anymore in change test case
        public void EnabledSelectedTypeUser()
        {         

            for (int i = 0; i < rblTypeUserAssigned.Items.Count; i++)
            {

                if (rblTypeUserAssigned.Items[i].Selected)
                {
                    rblTypeUserAssigned.Items[i].Enabled = false;

                }

            }          
        }

        // takes selected type scale continuous from the page
        public List<long> GetSelectedTypeScaleContinuous()
        {

            // check for duplicate descriptions, add the version to the list and remove duplicates
            for (int i = 0; i < tsc.Count(); i++)
            {
                tsc[i].scaleVersions = new List<string>();
                tsc[i].scaleVersions.Add(tsc[i].verScaleCont + "");

                for (int c = i + 1; c < tsc.Count(); c++)
                {
                    if (tsc[i].DescriptionScaleContinuous == tsc[c].DescriptionScaleContinuous)
                    {
                        tsc[i].scaleVersions.Add(tsc[c].verScaleCont + "");
                        tsc.RemoveAt(c);
                        c--;
                    }
                }
            }
            //------------------------------------------------------------------------------------

            List<TypScaleContinuous> tsc2 = DataAccessTestCase.getScaleContinuous();
            List<long> List = new List<long>();



            // retrieve the selected continuous scale versions from javascript
            ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var json = HiddenFieldScaleVersions.Value;
            List<string> scaleVersionsCont = serializer.Deserialize<List<string>>(json);


            if (rblTypeScale.SelectedValue == "1") {

                // get the corresponding versionIDs
                for (int i = 0; i < tsc2.Count; i++)
                {
                    for (int c = 0; c < tsc.Count; c++)
                    {

                        if ((tsc2[i].DescriptionScaleContinuous == tsc[c].DescriptionScaleContinuous))
                        {
                            for (int z = 0; z < tsc[c].scaleVersions.Count; z++)
                            {

                                if (tsc2[i].verScaleCont == int.Parse(scaleVersionsCont[c]))
                                {

                                    if (int.Parse(scaleVersionsCont[c]) > -1)
                                    {
                                        List.Add(tsc2[i].ID_TypScaleContinuous);
                                        break;
                                    }

                                }
                            }
                        }
                    }
                }
            
            }


            ////---------------------- old version ------------------------------//

            //for (int i = 0; i < cbxContinuousScale.Items.Count; i++)
            //{

            //    if (cbxContinuousScale.Items[i].Selected)
            //    {
            //        List.Add(Convert.ToInt64(cbxContinuousScale.Items[i].Value));

            //    }

            //}
            ////----------------------------------------------------------------//


            return List;
        }

        public long GetSelectedScaleDiscrete()
        {
			return Convert.ToInt32(cbxDiscreteScale.SelectedItem.Value);
            /*for (int i = 0; i < cbxDiscreteScale.Items.Count; i++)
            {
                if(cbxDiscreteScale.Items[i].Selected)
                {
                    return Convert.ToInt32(cbxDiscreteScale.Items[i].Value);
                }
            }
            return -1;*/
        }


        public void gvContinuous_OnDataPropertyChanged(Object sender, EventArgs e) {

            ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true); 
        }



        public void rblTypeScale_selectedIndexChanging(Object sender, EventArgs e)
        {
            if (rblTypeScale.SelectedItem.Text == "Discrete Scale")
            {
                trDiscrete.Visible = true;
                trContinuous.Visible = false;
                gvContinuous.Visible = false;
            }
            else
            {
                trDiscrete.Visible = false;
                trContinuous.Visible = true;
                gvContinuous.Visible = true;
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true); 
        }

        public void ddlDatabase_selectedIndexChanging(Object sender, EventArgs e)
        {
            for (int i = 0; i < ddlDatabase.Items.Count; i++)
            {
                if (ddlDatabase.Items[i].Selected)
                {
                    Constant.currentDatabase = ddlDatabase.Items[i].Text;
                    break;
                }
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true);        
        }




        public void Check_Clicked(Object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true); 
        }


        public void ddlContinuous_selectedIndexChanging(Object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, GetType(), "fUpdateTable", "fUpdateTable();", true); 
        }

        // insert in the drop down list all existing users
        public void PopulateTypeUser()
        {
            //fuellt mit den vorhandenen type user 
            rblTypeUserAssigned.DataSource = DataAccessUser.getTypeUser();
            rblTypeUserAssigned.DataTextField = "DescriptionTypUser";
            rblTypeUserAssigned.DataValueField = "ID_Typ";
            rblTypeUserAssigned.DataBind();


            rblTypeUserAssigned.Items.Remove(new ListItem { Value = "2", Text = "Administrator" });

        }

        public void writeConfigFile(long idtestcase)
        {

            Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase(idtestcase);
            List<GroupImage> listgroupImage = DataAccessTestCase.getAllImagesOfTestcase(idtestcase);
            string path = Constant.pathConfigFileTestcase;


            string listOfUsertypes = "";
            List<Class.TypUser> Usertypes = DataAccessUser.GetTypeUserForTestCase(idtestcase);

            foreach(Class.TypUser user in Usertypes){
                listOfUsertypes += user.ID_Typ + ",";
            }


            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + "\\" + tc.NameTestCase + "_" + idtestcase + ".cfg"))  
                { 
                    string activeLearning = "off";
                    string scale = "continuous";
                    int totalAmountOfImages = 0;

                    if(tc.DiskreteScale){
                        scale = "discrete";
                    }

                    if(tc.ActiveLearning){
                        activeLearning = "on";
                    }

                    file.WriteLine("Id:"+ idtestcase + "; name:" + tc.NameTestCase + "; general info:" + tc.GeneralInfo  + "; test question:" + tc.TestQuestion + "; scale type:" + scale
                                     + "; active learning:" + activeLearning + "; initial threshold:" + tc.initialThreshold + "; user interval:" + tc.userThreshold + "; database path:" + tc.dbPath + "; eligible user types: " + listOfUsertypes);
                    file.WriteLine("IdGroup; Path; IsReference; IdGroupHasImage; PatientName");

                    foreach (GroupImage image in listgroupImage) {
                        file.WriteLine(image.Idgroup  + "; " + image.Path  + "; " + image.IsReference  + "; " + image.IdGroupHasImage  + "; " + image.PatienteName + ";");
                        totalAmountOfImages++;
                    }
                    file.WriteLine("total amount of images in testcase: "+totalAmountOfImages);
                }
            }
            catch (Exception) { }
        
        
        }


        // insert in the drop down menu all existing scale continuous
        public void PopulateTypeScaleContinuous()
        {

        }

        // insert in the drop down menu all existing discrete scale
        public void PopulateTypeScaleDiscrete()
        {
            cbxDiscreteScale.DataSource = DataAccessTestCase.getScaleDiscrete();
            cbxDiscreteScale.DataTextField = "DescriptionScaleDiscrete";
            cbxDiscreteScale.DataValueField = "ID_TypScaleDiscrete";
            cbxDiscreteScale.DataBind();
        }

        // insert in the drop down menu all existing databases
        public void PopulateDatabases()
        {
            //fuellt mit den vorhandenen database paths 
            ddlDatabase.DataSource = DataAccessPath.getPaths();
            ddlDatabase.DataTextField = "DatabasePath";
            ddlDatabase.DataValueField = "Id_path";
            ddlDatabase.DataBind();
            //ddlDatabase.SelectedValue = Constant.currentDatabase;

        }
    }
}