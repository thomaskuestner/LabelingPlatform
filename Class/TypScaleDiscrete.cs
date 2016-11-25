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
    public class TypScaleDiscrete
    {
        public int ID_TypScaleDiscrete { get; set; }
        public string DescriptionScaleDiscrete { get; set; }
        public string PathImageMin { get; set; }
        public string PathImageMax { get; set; }
        public int Testcase_IDTestcase { get; set; }
    }
}