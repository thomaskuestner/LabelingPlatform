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
    public class Directory
    {
        public int IdDirectory { get; set; }
        public string NameDirectory { get; set; }
        public string Path { get; set; }
        public bool IsReference { get; set; }
        public List<Directory> SubDirectory { get; set; }
        public List<File> Files { get; set; }

        public Directory()
        {
            Files = new List<Class.File>();
            SubDirectory = new List<Directory>();
        }

    }
}