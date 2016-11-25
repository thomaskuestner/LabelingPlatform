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
    public class User
    {
        public long Id_user { get; set; }
        public int titleId { get; set; }
        public string title { get; set; }
        public string Name { get; set;}
        public string Surname { get; set; }
        public string Password { get; set; }
        public string salt { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int YearsOfExperience { get; set; }
        public int Type { get; set; } // user type: radiologist, student etc.
        public string DescriptionType { get; set; }
        public DateTime DateInsert { get; set; }
        public string Percentual { get; set; }
    }
}