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
    public class GlobalReference
    {
        public int ID_TypScale { get; set; }
        public string DescriptionScale { get; set; }
        public string PathImageMin { get; set; }
        public string PathImageMax { get; set; }
        public long Idgroup { get; set; }
    }
}