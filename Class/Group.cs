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

namespace LabelingFramework.Class
{
    public class Group
    {
        public long IdGroup { get; set; }
        public long IdTestCase { get; set; }
        public string Name { get; set; }
        public List<Directory> SubDirectory { get; set; }
        public string LoopOver { get; set; }
        public bool ReferenceIsGlobal { get; set; }
        public long IdGroupHasImage { get; set; }
        public bool IsPatientChosen { get; set; }
        public string ReferenceName { get; set; }
        public bool GroupExistDB { get; set; }
        public int ImagesPerPage { get; set; }
        public int ImagesPerPatient { get; set; }
        public string PageStyleDescription { get; set; }
        public long PageStyle { get; set; }
        public bool GroupHasReference { get; set; }
        public int OverallNumber { get; set; }
        public int NumberOfPages { get; set; }


        public List<int> MaxPaths { get; set; }
        public List<int> MinPaths { get; set; }

        public Group()
        {
            SubDirectory = new List<Directory>();
            MaxPaths = new List<int>();
            MinPaths = new List<int>();
            GroupExistDB = false;
        }
    }
}