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
    public class TestCase
    {
        public long IDTestcase { get; set; }
        public string NameTestCase { get; set; }
        public string GeneralInfo { get; set; }
        public string TestQuestion { get; set; }
        public long IdRepository { get; set; }
        public bool DiskreteScale { get; set; }
        public string  DescrState { get; set; }
        public int  State { get; set; }
        public int MinDiskreteScale { get; set; }
        public int MaxDiskreteScale { get; set; }
        public string Percentual { get; set; }
        public bool ActiveLearning { get; set; }
        public int initialThreshold { get; set; }
        public int userThreshold { get; set; }
        public string dbPath { get; set; }
        public int iSeed { get; set; }
        public bool isActive { get; set; }
    }
}