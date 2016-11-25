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
using LabelingFramework.Utility;
using System.IO;
using System.Diagnostics; // *for testing purposes*
using LabelingFramework.DataAccess;

//*********************************************************************************************************//
// manage image class: determines test case structure 
//*********************************************************************************************************//

namespace LabelingFramework.Class
{
    public class manageImg
    {
        public int iIdxGroup { set; get; } // current group/page index for showing page i of loop over structure
        public int iIdxInsideGroup {set; get;} // current index inside the page for showing image i
        public int iNGroups {set; get;} // amount of to be shown pages
        public int iNImages {set; get;} // amount of images per page
        public double dAmount { set; get; } // labeled amount of complete testcase
        public List<String> sCurrentImageFolder { set; get; } //list of physical paths to current image folders - Martin

        public int currentPageIndex { set; get; }
        static Random _random = new Random();
        public long IDTestcase {set; get;}

        public long IDUser {set; get;}
        public Class.TestCase tc;
        public Class.User usr;
        public List<List<PackageImage>> allImages { set; get; } // complete list of all pages to be viewed with inside nested all images corresponding to that page

        public manageImg(Class.TestCase tc, Class.User usr)
        {
            this.IDTestcase = tc.IDTestcase;
            this.IDUser = usr.Id_user;
            this.tc = tc;
            this.usr = usr;

            this.iIdxGroup = 0;
            this.iNGroups = 0;
            this.iNImages = 0;
            this.allImages = new List<List<PackageImage>>();
            this.currentPageIndex = 1;
        }

        public void ThreadRun()
        {
            // create the complete loop-over structue
            // get amount of looped over image pages, get amount of images per page ==> very convoluted ==> trick: first create List<List> of allImages and then count amount of outer and inner List-length 

            // loop over all groups
           
            List<Class.Group> groupsTC = DataAccess.DataAccessTestCase.getGroupFromIdTestCase(IDTestcase);

            List<TypScaleContinuous> listScaleContinous;
            TypScaleDiscrete scaleDiscrete;
            List<PackageImage> pi;
            List<string> images = new List<string>();

            var extensions = new List<String> { ".dcm", ".DCM", ".ima", ".IMA", ".mhd", ".MHD" };
            // check if scale is discrete or cont
            if(tc.DiskreteScale)
            {
                scaleDiscrete = DataAccess.DataAccessTestCase.TypeScaleDiscreteFromIdTestCase(tc.IDTestcase);
                listScaleContinous = new List<TypScaleContinuous>();
            }
            else
            {
                scaleDiscrete = new TypScaleDiscrete();
                listScaleContinous = DataAccessTestCase.TypeScaleContinuousFromIdTestCase(tc.IDTestcase);
            }


                string path = "";
                string pathIn = Constant.pathALinitialInput + string.Format("{0:D4}", tc.IDTestcase) + ".csv";
                string pathInUser = Constant.pathALuserInput + string.Format("{0:D4}", tc.IDTestcase) + "_User" + string.Format("{0:D4}", usr.Id_user) + ".csv";

                bool mode = false;     


            // checks if files exist and set file path by following priority: user > initial; if no files exist start in standard mode
                if (tc.ActiveLearning)
                {
                    if (System.IO.File.Exists(pathInUser))      // high priority
                    {
                        mode = true;
                        path = pathInUser;
                    }
                    else if (System.IO.File.Exists(pathIn))     // low priority
                    {
                        mode = true;
                        path = pathIn;
                    }
                    else {
                        mode = false;       // (redundant); active learning activated, but no optimization files yet
                    }
                }else{
                    mode = false;           // (redundant); active learning deactivated
                }
            //-------------------------------------------------------------------------------------------------------------------------------------



                // standard procedure: normal case (no active learning) 
                {

                    foreach (Class.Group group in groupsTC)
                    {
                        List<GroupImage> listgroupImage = DataAccessTestCase.GetGrouphasImageFromGroup(group.IdGroup, usr.Id_user);
                        List<string> listNamePatiente = DataAccessTestCase.GetNamePatiente(group.IdGroup);

                        int index = 0;
                        int indexRelReference = 0;


                        switch (group.PageStyle)
                        {
                            case 0:
                                {
                                    iNImages = listgroupImage.Count;    // all images of group on one page
                                    break;
                                }
                            case 1:
                                {
                                    iNImages = group.ImagesPerPage;     // amount defined by user
                                    break;
                                }
                            default:
                                iNImages = listgroupImage.Count;        // default all images of group on one page
                                break;

                        }





                        foreach (string NamePatience in listNamePatiente)
                        {


                            IEnumerable<GroupImage> listImageReference = from p in listgroupImage
                                                                         where p.PatienteName.ToUpper() == NamePatience.ToUpper()
                                                                         && p.IsReference == true
                                                                         select p;



                            IEnumerable<GroupImage> listImagePatient = from p in listgroupImage
                                                                       where p.PatienteName.ToUpper() == NamePatience.ToUpper()
                                                                       && p.IsReference == false
                                                                       select p;

                            index++;
                            pi = new List<PackageImage>();
                            int page = 0;
                            bool referenceAdded = false;

                            foreach (GroupImage groupImg in listImagePatient)
                            {
                                PackageImage pack = new PackageImage();
                                pack.GroupId = groupImg.Idgroup;

                                pack.IdGroupImage = groupImg.IdGroupHasImage;
                                pack.Path = groupImg.Path;
                                pack.Lenght = System.IO.Directory.EnumerateFiles(tc.dbPath + "\\" + pack.Path, "*.*", SearchOption.AllDirectories).Where(s => Constant.lsExtensions.Any(e => s.EndsWith(e))).Count();
                                pack.DiskreteScale = tc.DiskreteScale;
                                pack.MinDiskreteScale = tc.MinDiskreteScale;
                                pack.MaxDiskreteScale = tc.MaxDiskreteScale;
                                pack.ContinuousScale = tc.DiskreteScale == false ? true : false;
                                pack.Index = index;
                                pack.dbPath = tc.dbPath;
                                pack.LableDiscrete = groupImg.LableDiscrete;
                                pi.Add(pack);

                                if ((group.GroupHasReference) && (!referenceAdded) && (!group.ReferenceIsGlobal))
                                {
                                    // referenze bild wird geladen
                                    GroupImage giRef = listImageReference.Where(p => p.PatienteName.ToUpper().Trim() == groupImg.PatienteName.ToUpper().Trim()).FirstOrDefault();

                                    // alle infos zum referenz bild werden in die variable pack ref gespeichert
                                    PackageImage packRef = new PackageImage();
                                    packRef.IdGroupImage = giRef.IdGroupHasImage;
                                    packRef.Path = giRef.Path;

                                    packRef.IsReference = true;
                                    packRef.Lenght = System.IO.Directory.EnumerateFiles(tc.dbPath + "\\" + packRef.Path, "*.*", SearchOption.AllDirectories).Where(s => extensions.Any(e => s.EndsWith(e))).Count();
                                    packRef.DiskreteScale = tc.DiskreteScale;
                                    packRef.MinDiskreteScale = tc.MinDiskreteScale;
                                    packRef.MaxDiskreteScale = tc.MaxDiskreteScale;
                                    packRef.ContinuousScale = tc.DiskreteScale == false ? true : false;
                                    packRef.IsReference = giRef.IsReference;
                                    packRef.Index = index;
                                    packRef.PageStyle = group.PageStyle;
                                    packRef.dbPath = tc.dbPath;
                                    pi.Add(packRef);
                                    referenceAdded = true;
                                }

                                if (group.ReferenceIsGlobal)
                                {
                                    pack.ReferenceGlobal = true;
                                    if (tc.DiskreteScale)
                                    {

                                        GlobalReference reference = new GlobalReference();
                                        reference.PathImageMax = scaleDiscrete.PathImageMax;
                                        reference.PathImageMin = scaleDiscrete.PathImageMin;

                                        reference.DescriptionScale = scaleDiscrete.DescriptionScaleDiscrete;
                                        reference.ID_TypScale = scaleDiscrete.ID_TypScaleDiscrete;
                                        reference.Idgroup = groupImg.Idgroup;
                                        pack.GlobalReferences.Add(reference);

                                        pack.PathWorseGlobalDiscrete = scaleDiscrete.PathImageMin;
                                        pack.PathBestGlobalDiscrete = scaleDiscrete.PathImageMax;
                                    }
                                    else
                                    {
                                        foreach (TypScaleContinuous typ in listScaleContinous)
                                        {
                                            GlobalReference reference = new GlobalReference();
                                            reference.PathImageMax = typ.PathImageMax;  
                                            reference.PathImageMin = typ.PathImageMin; 
                                            reference.DescriptionScale = typ.DescriptionScaleContinuous;
                                            reference.ID_TypScale = typ.ID_TypScaleContinuous;
                                            reference.Idgroup = groupImg.Idgroup;
                                            pack.GlobalReferences.Add(reference);

                                            typ.PathImageMin = typ.PathImageMin;
                                            typ.PathImageMax = typ.PathImageMax;    
                                        }
                                    }

                                }
                                else
                                    pack.ReferenceGlobal = false;

                                if (pack.IsReference)
                                    indexRelReference = index;
                                else
                                    pack.IndexRelReference = indexRelReference;

                                // nimmt die kontinuierlichen skalen wenn ausgewahlt
                                if (pack.ContinuousScale)
                                {
                                    pack.TypeScaleContinuous = listScaleContinous as List<TypScaleContinuous>;
                                    pack.LableContinuous = DataAccessTestCase.GetLableContinuous(pack.IdGroupImage, usr.Id_user, listScaleContinous);
                                }

                                pack.PageStyle = group.PageStyle;
                                page++;

                                if ((page >= iNImages) && (pack.PageStyle == 1))
                                {
                                    allImages.Add(pi);
                                    referenceAdded = false;
                                    pi = new List<PackageImage>();
                                    page = 0;
                                }


                            }
                            if (pi.Count > 0)
                            {
                                allImages.Add(pi);
                            }

                        }

                    }


                }


            iNGroups = allImages.Count();
            List<PackageImage> pilocal = new List<PackageImage>();



            //--------- shuffle images ---------------------

                Shuffle(allImages);     // shuffle pages

                for (int i = 0; i < allImages.Count(); i++)
                {   // shuffle images on page
                    Shuffle(allImages[i]);
                }
            //-----------------------------------------------



            // put reference image at first position
            for (int i = 0; i < allImages.Count(); i++)
            {
                for (int c = 1; c < allImages[i].Count(); c++) {
                    if (allImages[i][c].IsReference)
                    {
                        PackageImage temp = new PackageImage();
                        temp = allImages[i][0];
                        allImages[i][0] = allImages[i][c];
                        allImages[i][c] = temp;
                    }
                }
            }


            //--------------------------------------------------------- active learning ----------------------------------------------------------------------------//

            if (mode) // if active learning is activated and files have been found -> realign allImages in accordance to matlab output
            {
                // schaut ob es ein path gibt (redundant)
                if (System.IO.File.Exists(path))
                {
                    // read image paths into list
                    using (StreamReader streamReader = new StreamReader(path))
                    {
                        string currentLine;

                        while ((currentLine = streamReader.ReadLine()) != null)
                        {
                            if (currentLine != "") {
                                images.Add(currentLine.TrimEnd(';')); // remove semicolon and add image path
                            }
                            
                        }

                        images.RemoveAt(0); // remove first entry (number of optimizations)
                    }


                    List<PackageImage> remainingImages = new List<PackageImage>();
                    List<PackageImage> prioritizedImages = new List<PackageImage>();

                    // get prioritized images from matlab optimization output
                    for (int i = 0; i < images.Count; i++)
                    {
                        prioritizedImages.Add(getImage(images[i]));
                    }

                    // retrieve remaining (not yet labelled) images
                    for (int i = 0; i < allImages.Count; i++)
                    {
                        for (int c = 0; c < allImages[i].Count; c++)
                        {
                            if ((!allImages[i][c].IsReference) && (allImages[i][c].Path != "")) // dont consider local reference images
                            {
                                prioritizedImages.Add(allImages[i][c]);
                            }
                        }
                    }


                    // realign allImages in correspondence with list from matlab file
                    List<List<PackageImage>> tempAllimages = new List<List<PackageImage>>();
                    List<PackageImage> tempPage = new List<PackageImage>();

                    int counter = 0;  // pageindex

                    // adds local reference on page 1 if needed
                    if ((groupsTC[0].GroupHasReference) && (!groupsTC[0].ReferenceIsGlobal))
                    {
                        tempPage.Add(allImages[0][0]);        // if local reference has been added as first image to the page previously
                    }


                    for (int i = 0; i < prioritizedImages.Count; i++)
                    {

                        tempPage.Add(prioritizedImages[i]);

                        if (counter < iNImages)
                        { // number of imagers per page has been reached
                            tempAllimages.Add(tempPage);
                            counter++;            // next page
                            tempPage = new List<PackageImage>();

                            // adds reference on new page
                            if ((groupsTC[0].GroupHasReference) && (!groupsTC[0].ReferenceIsGlobal))
                            {
                                tempPage.Add(allImages[0][0]);
                            }
                            counter = 0;
                        }
                    }

                        allImages = tempAllimages; // change allimages

                    

                }


            }

            //------------------------------------------------------------------------------------------------------------------------------------------------------//

        }




        // ----------------------------------------------------- auxiliary functions --------------------------------------------------------------------------------//

   public PackageImage getImage(string imagePath){ // search image in allImages with the path of an image

    PackageImage image = new PackageImage();

     for(int c = 0; c < allImages.Count; c++) {
         for (int z = 0; z < allImages[c].Count; z++)
         {
             if ((allImages[c][z].dbPath + "\\" + allImages[c][z].Path.Replace("/", "\\")) == imagePath)
             {
                 image = allImages[c][z];
                 allImages[c].RemoveAt(z); // remove image (to prevent duplicates later on if not all images are contained in the matlab file)
                 break;
             }
         }
     }
     return image;
    }

            public string displayAllIm(){  // prints content of allImages in debug console (just for testing purposes)
                    string text = tc.NameTestCase+"\n";
                for (int k = 0; k < allImages.Count; k++)
                {
                    string inhalt = "";
                    string graphic ="|";
                    for (int c = 0; c < allImages[k].Count; c++) {
                        inhalt += allImages[k][c].Path+", ";
                        string label = "";
                            
                           
                        if (allImages[k][c].DiskreteScale)
                        {
                            label = allImages[k][c].LableDiscrete+"";
                        }else{
                            for (int t = 0; t < allImages[k][c].LableContinuous.Count; t++)
                            {
                            label += allImages[k][c].LableContinuous[t].Lable+";";
                            }
                        }



                        graphic += c + "(label:" + label + ")|";
                    }
                    text +="[" + k + "]:\t "+graphic+"\t\t"+  "Anzahl an Bildern pro Seite:" + allImages[k].Count + " Inhalt: " + inhalt+ "[" + k + "]; idGroup " + allImages[k][0].IdGroupImage+"\n";
                }
               return text;
        }



            // shuffle algorithm; source: h ttp://www.dotnetperls.com/fisher-yates-shuffle
            public static void Shuffle<T>(List<T> list)
            {
                int n = list.Count();
                for (int i = 0; i < n; i++)
                {
                    // NextDouble returns a random number between 0 and 1.
                    // ... It is equivalent to Math.random() in Java.
                    int r = i + (int)(_random.NextDouble() * (n - i));
                    T t = list[r];
                    list[r] = list[i];
                    list[i] = t;
                }
            }


        //------------------------------------------------------------------------------------------------------------------------------------------------------//

    }
}