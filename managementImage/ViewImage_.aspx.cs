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
using LabelingFramework.Class;
using System.Web.Services;
using LabelingFramework.Utility;
using System.IO;
using itk.simple;
using System.Threading;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Diagnostics; // *for testing purposes*
using System.Web.SessionState; //pcw



//*********************************************************************************************************//
//  labeling page
//*********************************************************************************************************//



namespace LabelingFramework.managementImage
{
    public partial class ViewImage_ : System.Web.UI.Page
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

                    long idTestCase = 0;
                    if (HttpContext.Current.Session["IdTestCase"] != null)
                        idTestCase = Convert.ToInt64(HttpContext.Current.Session["IdTestCase"]);
                    // infos die immer da sind und sich während des Labelns nicht ändern
                    Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase().Where(p => p.IDTestcase == idTestCase).FirstOrDefault();
                    lblNameTestCase.Text = tc.NameTestCase;
                    lblTestQuestion.Text = tc.TestQuestion;
                    lblGeneralInfo.Text = tc.GeneralInfo;


                    if (System.IO.File.Exists(Constant.pathTutorialPages + tc.IDTestcase + ".html"))
                    {
                        string html = System.IO.File.ReadAllText(Constant.pathTutorialPages + tc.IDTestcase + ".html");
                        hddTutorial.Value = html;
                        disableTutorial.Value = Constant.pathTutorialPages + tc.IDTestcase + ".html";
                    }
                    else
                    {
                        hddTutorial.Value = "";
                        disableTutorial.Value = "";
                    }



                    updateReference(0);

                    HttpContext.Current.Session["viewImage"] = this;

                    if (tc.ActiveLearning) {

                        // check if there are files
                        Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;

                        //  0: no filesfound; 1: user file found; 2: initial file found  
                        int statusFiles = 0;
                        if (System.IO.File.Exists(Constant.pathALuserInput + string.Format("{0:D4}", managerView.IDTestcase) + "_User" + string.Format("{0:D4}", managerView.IDUser) + ".csv"))
                        {
                            statusFiles += 2;
                            HttpContext.Current.Session["numberOfOptimizationsUser"] = int.Parse(System.IO.File.ReadLines(Constant.pathALuserInput + string.Format("{0:D4}", managerView.IDTestcase) + "_User" + string.Format("{0:D4}", managerView.IDUser) + ".csv").First());
                        }
                        else if (System.IO.File.Exists(Constant.pathALinitialInput + string.Format("{0:D4}", managerView.IDTestcase) + ".csv"))
                        {
                            statusFiles += 1;
                        }

                        HttpContext.Current.Session["statusFiles"] = statusFiles;
                    }
                }

            }
            else {

                if (HttpContext.Current.Session["User"] == null)
                {
                    Response.Redirect("~/Account/Login.aspx");

                }
                else
                {
                    try {

                        bool refresh = (bool)HttpContext.Current.Session["refresh"];

                        if (refresh)
                        {
                            long idTestCase = 0;
                            if (HttpContext.Current.Session["IdTestCase"] != null)
                                idTestCase = Convert.ToInt64(HttpContext.Current.Session["IdTestCase"]);
                            Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase().Where(p => p.IDTestcase == idTestCase).FirstOrDefault();
                            Thread task;
                            Class.manageImg managerView;
                            Class.User user = Session["User"] as Class.User;
                            // initialize manage object with all needed content
                            managerView = new manageImg(tc, user);

                            // spawn a thread to create the image packages
                            ThreadStart thread = new ThreadStart(managerView.ThreadRun);
                            task = new Thread(thread);

                            task.Start();

                            if (task.IsAlive) // creation of everything finished => all necessary information are in Session["managerView"]
                            {
                                Session["managerView"] = managerView;
                                Response.Redirect("~/managementImage/ViewImage_.aspx");
                            }
                        }

                    
                    }catch(Exception ex){}

                }


            }

                
        }


        protected void Button1_Click(object sender, EventArgs e) {
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;
            updateReference(managerView.currentPageIndex);
        }




        protected void updateReference(int index)
        {
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;

            if ((managerView.allImages.Count > 0) && (managerView.allImages[index][0].ReferenceGlobal))
            {
                List<GlobalReference> gloRef = new List<GlobalReference>();
               
                gvGlobalReference.DataSource = getGlobReferences(index);

                gloRef = getGlobReferences(index);



            }
            else {
                gvGlobalReference.DataSource = null;
            }
            gvGlobalReference.DataBind();
        }

        [System.Web.Services.WebMethod]
        public static void SaveLabelDiscrete(List<LableScaleDiskrete> Listlablediscrete)
        {

            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;


                if (Listlablediscrete.Count == 0)
                {
                    return;
                }
            // discrete label wird hier gespeichert
            Class.User user = HttpContext.Current.Session["User"] as Class.User;
            Result res = null;
            int index = managerView.currentPageIndex;

            foreach (LableScaleDiskrete lb in Listlablediscrete)
            {

                // save labels in current session
                for (int i = 0; i < managerView.allImages[index].Count; i++)
                {
                    if (managerView.allImages[index][i].IdGroupImage == lb.IdGroupImage)
                    {
                        managerView.allImages[index][i].LableDiscrete = lb.Lable;
                    }
                }



                if (lb.IdGroupImage != 0)
                {
                    LableScaleDiskrete lable = new LableScaleDiskrete();
                    lable.Lable = lb.Lable;
                    lable.IdGroupImage = lb.IdGroupImage;
                    lable.IdUser = Convert.ToInt64(user.Id_user);
                    res = DataAccessTestCase.InsertLableScaleDiskrete(lable);
                }

            }

        }

        [System.Web.Services.WebMethod]
        public static bool SaveLabelContinuous(List<LableScaleContinuous> Listlablecontinuous)
        {
            if (Listlablecontinuous.Count == 0)
            {
                return false;
            }
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;
            Class.User user = HttpContext.Current.Session["User"] as Class.User;
            Result res = null;
            int index = managerView.currentPageIndex;

            foreach (LableScaleContinuous lb in Listlablecontinuous)
            {
                // save labels in current session
                for (int i = 0; i < managerView.allImages[index].Count; i++)
                {
                    if (managerView.allImages[index][i].IdGroupImage == lb.IdGroupImage)
                    {
                        for (int c = 0; c < managerView.allImages[index][i].LableContinuous.Count; c++)
                        {
                            if (managerView.allImages[index][i].LableContinuous[c].IdLable == lb.IdLable)
                            {
                                managerView.allImages[index][i].LableContinuous[c].Lable = lb.Lable;

                            }
                        }
                        
                    }
                }


                if (lb.IdGroupImage != 0)
                {
                    LableScaleContinuous lable = new LableScaleContinuous();
                    lable.Lable = lb.Lable;
                    lable.IdGroupImage = lb.IdGroupImage;
                    lable.IdUser = Convert.ToInt64(user.Id_user);
                    lable.IdScaleContinuous = lb.IdScaleContinuous;
                    res = DataAccessTestCase.InsertLableScaleContinuous(lable);
                }

            }

            return res.result;
        }

        [System.Web.Services.WebMethod]
        public static void reportError(string messageUser)
        {
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;

            List<string> message = Constant.errorReportToAdminMail(managerView, messageUser);

            MyEmail notification = new MyEmail();
            notification.sendEmail(notification.sender, message[0], message[1]);

        }



        public static string obfuscate(string path) {
            string obfuscated = "";


            // old obfuscation
            //for (int p = 0; p < path.Length; p++)
            //{

            //    int add = Constant.ceasarOdd;
            //    if (p % 2 == 0)
            //    {
            //        add = Constant.ceasarEven;
            //    }

            //    char c = (char)(path[p] + add);
            //    obfuscated += c.ToString();
            //}


            string allowedChars = Constant.allowedCharacters;

            for (int p = 0; p < path.Length; p++)
            {
                for (int c = 0; c < allowedChars.Length; c++)
                {
                    if (path[p] == allowedChars[c])
                    { // character is allowed

                        int add = Constant.ceasarOdd;
                        if (p % 2 == 0)
                        {
                            add = Constant.ceasarEven;
                        }

                        // remove unneccessary loops
                        add = add % (allowedChars.Length - 1);

                        int shift = c + add;

                        if (shift > (allowedChars.Length - 1))
                        {
                            shift = shift - (allowedChars.Length - 1);
                        }
                        else if ((shift) < 0)
                        {
                            shift = shift + (allowedChars.Length - 1);
                        }

                        char character = allowedChars[shift];


                        obfuscated += character.ToString();

                        break;
                    }
                }

            }

          //Debug.Print("obfuscation: \t\t" + path + "  ->  " + obfuscated);





            return obfuscated;
        }


        public static List<GlobalReference> getGlobReferences(int index)
        {
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;

            List<GlobalReference> globRefList = new List<GlobalReference>();
            for (int k = 0; k < managerView.allImages[index][0].GlobalReferences.Count(); k++)
            {
                GlobalReference GlobRef = new GlobalReference();
                GlobRef.PathImageMin = "ShowImage.ashx?NqC3ke=4&tXt9X3=0&XwjRGm=1&Hsfke2=0&yAR8st=1&WkYTCe=" + obfuscate(managerView.allImages[index][0].GlobalReferences[k].PathImageMin);
                GlobRef.PathImageMax = "ShowImage.ashx?NqC3ke=4&tXt9X3=0&XwjRGm=1&Hsfke2=0&yAR8st=1&WkYTCe=" + obfuscate(managerView.allImages[index][0].GlobalReferences[k].PathImageMax);
                GlobRef.DescriptionScale = managerView.allImages[index][0].GlobalReferences[k].DescriptionScale;
                GlobRef.ID_TypScale = managerView.allImages[index][0].GlobalReferences[k].ID_TypScale;
                GlobRef.Idgroup = managerView.allImages[index][0].GlobalReferences[k].Idgroup;
                globRefList.Add(GlobRef);
            }

            return globRefList;
        }


        // [..] need it for use in jquery 
        [System.Web.Services.WebMethod]
        public static string getListOfPackageImage(int index)

        {

            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;
            managerView.currentPageIndex = index;
            HttpContext.Current.Session["managerView"] = managerView;

            ViewImage_ that = HttpContext.Current.Session["viewImage"] as ViewImage_;
            that.updateReference(index);

            // set default values
            managerView.allImages[index][0].getUnlabelled = new int[] { -1, -1};
            managerView.allImages[index][0].getPercentage = "-";
            
       
            if (managerView.tc.ActiveLearning) // when active learning is enabled, check if threshold is reached
            {
                
                if (DataAccess.DataAccessTestCase.checkThreshold(managerView))
                {
                    return "alert('Active learning procedure has been executed, page index will be reset');  setTimeout(function(){ Reset=true; }, 3000);";
                }
            }


            if (index >= 0 && index <= managerView.iNGroups)
            {
                managerView.sCurrentImageFolder = new List<String>(managerView.allImages[index].Count);
                for (var i = 0; i < managerView.allImages[index].Count; i++)
                {
                    managerView.sCurrentImageFolder.Add(null);
                    managerView.sCurrentImageFolder[i] = managerView.tc.dbPath + "\\" + managerView.allImages[index][i].Path.Replace('/', '\\'); //- Martin
                }


                // get the labelscales
                for (int i = 0; i < managerView.allImages[index].Count; i++)
                {
                    if (managerView.tc.DiskreteScale)
                    {
                        managerView.allImages[index][i].LableDiscrete = DataAccessTestCase.GetDiscreteLabel(managerView.IDUser, managerView.allImages[index][i].IdGroupImage);//pcw
                    }
                    else
                    {
                        managerView.allImages[index][i].LableContinuous = DataAccessTestCase.GetLableContinuous(managerView.allImages[index][i].IdGroupImage, managerView.IDUser, managerView.allImages[index][i].TypeScaleContinuous);//pcw
                    }
                }



                List<String> sPath = managerView.sCurrentImageFolder;
                if (sPath == null)
                {
                    throw new ArgumentException("path variable in manageImg is not specified!");
                }
                else
                {

                    // get paths and dimension of every individual image
                    for (int i = 0; i < managerView.allImages[index].Count; i++)
                    {

                        managerView.allImages[index][i].imagePaths = (System.IO.Directory.EnumerateFiles(sPath[i], "*.*", SearchOption.AllDirectories).Where(s => Constant.lsExtensions.Any(e => s.EndsWith(e)))).ToArray();
                        Array.Sort(managerView.allImages[index][i].imagePaths);
                        int[] iSize = new int[] { 0, 0, 0 };
                        string pfad = managerView.allImages[index][i].imagePaths[0];
                        itk.simple.Image itkImage = SimpleITK.ReadImage(pfad);              // bei DICOM werden nur die Dimensionen des ersten Bildes ausgelesen, diese sind bei den anderen Schichten identisch

                        VectorUInt32 UInt32Size = itkImage.GetSize();
                        iSize[0] = unchecked((int)UInt32Size[0]);
                        iSize[1] = unchecked((int)UInt32Size[1]);

                        if (managerView.allImages[index][i].imagePaths.Length > 1)// for DICOM files
                        {
                            iSize[2] = managerView.allImages[index][i].imagePaths.Length;
                        }
                        else if (managerView.allImages[index][i].imagePaths.Length == 1)//
                        {
                            iSize[2] = unchecked((int)UInt32Size[2]);
                            iSize[2]--;
                        }
                        managerView.allImages[index][i].imageDimensions = iSize;
                    }
                }



                // get index of the next/previous page with not completely labelled images
                try {
                    managerView.allImages[index][0].getNumOfPages = managerView.allImages.Count();
                    managerView.allImages[index][0].getUnlabelled = getUnlabelled(index);
                    managerView.allImages[index][0].getPercentage = getCurrPercentage();
                }catch(ArgumentOutOfRangeException ex){
                      return "alert('An error occurred while trying to load the image package, please contact the administrator');";
                }

                //Obfuscate image paths

                for (int c = 0; c < managerView.allImages[index].Count; c++)
                    {
                        for (int z = 0; z < managerView.allImages[index][c].imagePaths.Length; z++)
                        {
                            managerView.allImages[index][c].imagePaths[z] = obfuscate(managerView.allImages[index][c].imagePaths[z]);
                        }
                            
                    }

                // put the array in the JSON format
                var json = new JavaScriptSerializer().Serialize(managerView.allImages[index]);


                return json;
                //return managerView.allImages[index];
            }
            else
                return null;
        }

        // - Martin - get filesnames in directory
        [System.Web.Services.WebMethod]
        public static String[] fGetFilenames()
        {
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;
            List<String> sPath = managerView.sCurrentImageFolder;

         
            String[] sFilenames;

            if (sPath == null)
            {
                throw new ArgumentException("path variable in manageImg is not specified!");
            }
            else
            {
                   // return firstImagesOfPage;
                sFilenames = (System.IO.Directory.EnumerateFiles(sPath[0], "*.*", SearchOption.AllDirectories).Where(s => Constant.lsExtensions.Any(e => s.EndsWith(e)))).ToArray();
                     Array.Sort(sFilenames);

                   return sFilenames;
            }
        }

        [System.Web.Services.WebMethod]
        public static int[] fGetDatasetLength()
        {
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;
            String sPath = managerView.sCurrentImageFolder[0];
            String[] sFilenames;

            if (sPath == null)
            {
                throw new ArgumentException("path variable in manageImg is not specified!");
            }
            else
            {
                sFilenames = (System.IO.Directory.EnumerateFiles(sPath, "*.*", SearchOption.AllDirectories).Where(s => Constant.lsExtensions.Any(e => s.EndsWith(e)))).ToArray();
                int[] iSize = new int[] { 0, 0, 0 };
                // get dataset length and set hiddenfield
                itk.simple.Image itkImage = SimpleITK.ReadImage(sFilenames[0]); // Es wird nur das erste Listenelement ausgelesen?
                VectorUInt32 UInt32Size = itkImage.GetSize();
                iSize[0] = unchecked((int)UInt32Size[0]);
                iSize[1] = unchecked((int)UInt32Size[1]);
                iSize[2] = unchecked((int)UInt32Size[2]);
                return iSize;
            }
        }

        public static string getCurrPercentage()
        {
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;
            string percent = DataAccess.DataAccessTestCase.CalculatePercentual(managerView.usr.Id_user, managerView.IDTestcase, managerView.tc.DiskreteScale);
            return percent;
        }

        [System.Web.Services.WebMethod]
        public static int getNGroup()
        {
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;


            return managerView.iNGroups;
        }


        public static int[] getUnlabelled(int currentIndex) // if next -> get next unlabelled, if !next -> get previous unlabelled
        {
            Class.manageImg managerView = HttpContext.Current.Session["managerView"] as Class.manageImg;
            int indexNext = -1;
            int indexPrev = -1;
            bool found = false;

            // get next unlabelled
            for (int c = (currentIndex + 1); (c < managerView.allImages.Count)&&(!found); c++)
                {
                    for (int i = 0; i < managerView.allImages[c].Count; i++)
                    {
                 
                        if (managerView.allImages[0][0].DiskreteScale)
                        {
                            if ((managerView.allImages[c][i].LableDiscrete == 0) && (!managerView.allImages[c][i].IsReference))
                            {
                                indexNext = c + 1;
                                break;
                            }
                        }
                        else if (managerView.allImages[0][0].ContinuousScale)
                        {
                            for (int k = 0; k < managerView.allImages[c][i].LableContinuous.Count; k++)
                            {
                                if ((managerView.allImages[c][i].LableContinuous[k].Lable == -1) && (!managerView.allImages[c][i].IsReference))
                                {
                                    indexNext = c + 1;
                                    break;
                                }
                            }
                        }
                    }
                    if (indexNext != -1)
                    {
                        break;
                    }
                }

            // get previous unlabelled
                for (int c = (currentIndex - 1); c > -1; c--)   // start search with previous image
                {
                    for (int i = 0; i < managerView.allImages[c].Count; i++)
                    {
                        if (managerView.allImages[0][0].DiskreteScale)
                        {

                            if ((managerView.allImages[c][i].LableDiscrete == 0) && (!managerView.allImages[c][i].IsReference))
                            {
                                indexPrev = c + 1;
                                break;
                            }
                        }
                        else if (managerView.allImages[0][0].ContinuousScale)
                        {
                            for (int k = 0; k < managerView.allImages[c][i].LableContinuous.Count; k++)
                            {      
                                if ((managerView.allImages[c][i].LableContinuous[k].Lable == -1) && (!managerView.allImages[c][i].IsReference))
                                {
                                    indexPrev = c + 1;
                                    break;
                                }
                            }
                        }
                    }
                    if (indexPrev != -1)
                    {
                        break;
                    }
                }


            int[] unlabelled = {(indexPrev), (indexNext)};
 
            return unlabelled;
        }

        public int SessionLengthMinutes
        {
            get { return Session.Timeout; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            logoutTime.Value = "" + SessionLengthMinutes;
            base.OnPreRender(e);

        }

    

    }


}