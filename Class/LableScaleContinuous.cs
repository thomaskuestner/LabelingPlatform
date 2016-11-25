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
    public class LableScaleContinuous
    {
        public long IdLable {get;set;}
        public decimal Lable {get;set;}
        public long IdUser {get;set;}
        public long IdGroupImage {get;set;}
        public long IdScaleContinuous {get;set;}
       
        public LableScaleContinuous()
        {
            this.Lable = -1;
        }

        public void printToDebug() {

            System.Diagnostics.Debug.Print("IdLable: " + IdLable + " Lable: " + Lable + " IdUser: " + IdUser + " IdGroupImage: " + IdGroupImage + " IdScaleContinuous: " + IdScaleContinuous);

        }
    }
}