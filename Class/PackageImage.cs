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
    public class PackageImage
    {
        public long IdGroupImage  { get; set; }
        public long GroupId { get; set; }
        public string Path          { get; set; }
        public int Lenght        { get; set; }
        public bool DiskreteScale   { get; set; }
        public int MinDiskreteScale { get; set; }
        public int MaxDiskreteScale { get; set; }
        public bool ContinuousScale { get; set; }
        public int Index { get; set; }
        public int IndexRelReference { get; set; }
        public int Type { get; set; }
        public string dbPath { get; set; }
        public bool IsReference { get; set; }
        public List<TypScaleContinuous> TypeScaleContinuous { get; set; }
        public int LableDiscrete { get; set; }
        public List<LableScaleContinuous> LableContinuous { get; set; }
        public string PathWorseGlobalDiscrete { get; set; }
        public string PathBestGlobalDiscrete { get; set; }
        public bool ReferenceGlobal { get; set; }
        public long PageStyle { get; set; }
        public List<GlobalReference> GlobalReferences{ get; set; }
        public string[] imagePaths { get; set; } // path directly to the image, containing the filename
        public int[] imageDimensions { get; set; }

        public string getPercentage { get; set; }
        public int getNumOfPages { get; set; }
        public int[] getUnlabelled { get; set; }


        public PackageImage()
        {
            TypeScaleContinuous = new List<TypScaleContinuous>();
            LableContinuous = new List<LableScaleContinuous>();
            GlobalReferences = new List<GlobalReference>();
        }

        public PackageImage(int iSize) // with preallocated size
        {
            TypeScaleContinuous = new List<TypScaleContinuous>(iSize);
            LableContinuous = new List<LableScaleContinuous>(iSize);
            GlobalReferences = new List<GlobalReference>(iSize);
        }
    }
}