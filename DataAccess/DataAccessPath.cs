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
using LabelingFramework.Class;
using MySql.Data.MySqlClient;

namespace LabelingFramework.DataAccess
{
    public class DataAccessPath
    {
        public static List<Class.Path> getPaths()
        {
            List<Class.Path> u = new List<Class.Path>();

            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetDatabasePath", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idpath", DBNull.Value);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.Path dbPath = new Class.Path();
                        dbPath.Id_path = reader.GetInt16(0);
                        dbPath.DatabasePath = reader.GetString(1);

                        u.Add(dbPath);
                    }
                }

            }
            return u;
        }

        public static Result DeletePath(int idpath)
        {
            Result esito = new Result();

            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("DeletePath", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("idpath", idpath);
                command.ExecuteNonQuery();


                esito.result = true;

            }

            return esito;
        }

        public static Class.Path getPathById(long id_path)
        {
            Class.Path dbpath = new Class.Path();

            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetDatabasePath", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idpath", id_path);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dbpath.Id_path = reader.GetInt16(0);
                        dbpath.DatabasePath = reader.GetString(1);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return dbpath;
        }


        public static Result UpdatePath(int idpath, string databasePathIn)
        {
            Result res = new Result();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UpdatePath", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("idpath", idpath);
                    command.Parameters.AddWithValue("pathLabel", databasePathIn);
                    command.ExecuteNonQuery();


                    res.result = true;

                }
            }
            catch (Exception ex)
            {
                res.result = false;
                res.Message = ex.Message;
            }

            return res;
        }



        public static Result InsertPath(string dbPath)
        {
            Result res = new Result();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("InsertPath", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("pathlabel", dbPath);

                command.ExecuteNonQuery();


                res.result = true;

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.result = false;
            //    esito.Message= ex.Message;            
            //}

            return res;
        }


    }
}