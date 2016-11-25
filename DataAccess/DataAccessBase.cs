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
using MySql.Data.MySqlClient;
using LabelingFramework.Utility;

namespace LabelingFramework.DataAccess
{
    public class DataAccessBase
    {
       // establish MySQL connection for each database access
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(Constant.GetConnectionString()); ;
        }

        
    }
}