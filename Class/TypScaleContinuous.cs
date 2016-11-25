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
    public class TypScaleContinuous
    {
        public int ID_TypScaleContinuous { get; set; }
        public string DescriptionScaleContinuous { get; set; }
        public int verScaleCont { get; set; }
        public string PathImageMin { get; set; }
        public string PathImageMax { get; set; }
        public int Testcase_IDTestcase { get; set; }

        public List<string> scaleVersions { get; set; }
        public global::System.Web.UI.WebControls.DropDownList ddlContinuous { get; set; }
        public global::System.Web.UI.WebControls.CheckBox checkBox { get; set; }

        public TypScaleContinuous()
        {
            scaleVersions = new List<string>();
            ddlContinuous = new System.Web.UI.WebControls.DropDownList();
            checkBox = new System.Web.UI.WebControls.CheckBox();
        }

    }
}