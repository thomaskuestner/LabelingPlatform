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
    public class Lable
    {
        public long     IdLable     { get; set; }
        public decimal  lable        { get; set; }
        public string   PathImage   { get; set; }
        public string   Description { get; set; }
        public string TestCaseName { get; set; }
        public string UserId { get; set; }
        public string NameUser { get; set; }
        public string SurnameUser { get; set; }
        public string EmailUser { get; set; }
        public string TestQuestion { get; set; }
        public string TotalValue { get; set; }
        public string IsReference { get; set; }
        public string ReferenceImage { get; set; }
        public string dbPath { get; set; }
        public long LabelCode { get; set; }
    }
}