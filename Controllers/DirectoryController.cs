/*
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections;
using System.IO;
//using LabelingFramework.DataAccess;
using LabelingFramework.Class;
using LabelingFramework.Utility;
using System.Diagnostics; // *for testing purposes*

namespace LabelingFramework
{

    public class DirectoryController : ApiController
    {



        static int count = 1;

        // GET api/<controller>
        public IEnumerable GetAllDirectory()
        {
            List<string> text = new List<string>();

            //string labeling = DataAccessBase.GetLabeling();
            string labeling = Constant.currentDatabase;
            DirectoryInfo dir=null;  

            if (labeling != string.Empty)
            {
                dir = new DirectoryInfo(labeling);
            } else {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("no dir created!"),
                    ReasonPhrase = "labeling is empty"
                };
                throw new HttpResponseException(resp);
            }


            List<Class.Directory> listdir = new List<Class.Directory>();
            int i = 0;

            foreach (DirectoryInfo d in dir.GetDirectories())
            {


                Class.Directory dirprova = new Class.Directory();
                dirprova.NameDirectory = d.Name;
                i++;
                dirprova.IdDirectory = i;


                dirprova.SubDirectory = new List<Class.Directory>();
                text.Add("\n");
                foreach (DirectoryInfo d1 in d.GetDirectories())
                {
                    Class.Directory sub = new Class.Directory();
                    sub.NameDirectory = d1.Name;
                    sub.IdDirectory = i;
                    dirprova.SubDirectory.Add(sub);
                    text.Add(d1.Name);
                }

                listdir.Add(dirprova);
            }


           // writeFile(text);
            return listdir;

        }

        public IEnumerable GetDirectorybyNameSubDir(string id)
        {
            List<string> text = new List<string>();
            string[] s = id.Split('!');
            bool found = false;

            //string labeling = DataAccessBase.GetLabeling();
            string labeling = Constant.currentDatabase;
            DirectoryInfo dir = null;

            if (labeling != string.Empty)
                dir = new DirectoryInfo(labeling);


            List<Class.Directory> listdir = new List<Class.Directory>();


            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                Class.Directory dirprova = new Class.Directory();
                dirprova.NameDirectory = d.Name;
                dirprova.SubDirectory = new List<Class.Directory>();

                text.Add("\n");
                foreach (DirectoryInfo d1 in d.GetDirectories())
                {

                    LabelingFramework.Class.Directory sub = new Class.Directory();
                    sub.NameDirectory = d1.Name;
                    dirprova.SubDirectory.Add(sub);
                    text.Add(d1.Name);

                }

                for (int i = 0; i < s.Length - 1; i++)
                {
                    if (d.GetDirectories().Where(p => p.Name.ToUpper() == s[i].ToString().ToUpper()).Any())
                    {
                        found = true;
                    }
                    else
                    {
                        found = false;
                        break;
                    }
                }

             

                if ((found) || (s[0] == string.Empty)) {
                    listdir.Add(dirprova);
                }
                    
            }


           // writeFile(text);

            return listdir;
        }


        protected void writeFile(List<string> text) {
            using (System.IO.StreamWriter file =
new System.IO.StreamWriter(Constant.pathUserList.Replace("Users", "DEBUG_OUTPUT_FOLDERS_" + count)))
            {
                foreach (string line in text)
                {
                    // If the line doesn't contain the word 'Second', write the line to the file.
                    if (!line.Contains("Second"))
                    {
                        file.WriteLine(line);
                    }
                }
            }
            count++;
        }
        

    }
}