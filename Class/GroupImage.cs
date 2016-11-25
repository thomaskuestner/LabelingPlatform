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
    public class GroupImage
    {
        public long Idgroup { get; set; }
        public bool IsReference { get; set; }
        public string Path { get; set; }
        public string PatienteName { get; set; }
        public long IdGroupHasImage { get; set; } // auto-increment index from MySQL database group_has_image
        public int LableDiscrete { get; set; }



    }
}