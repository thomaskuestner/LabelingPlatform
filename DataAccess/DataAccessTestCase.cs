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
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using LabelingFramework.TestCase;
using System.IO;
using LabelingFramework.Utility;


using System.Diagnostics; // *for testing purposes*


//*********************************************************************************************************//
// handles communication between C# an MySQL database
//*********************************************************************************************************//



namespace LabelingFramework.DataAccess
{
    public class DataAccessTestCase
    {

        // insert new testcase
        public static Result InsertTestCase(Class.TestCase tc,ref Int64 IdTestCase)
        {
            Result esito = new Result();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("InsertTestCase", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // zuweisung der variablen tc... in die tabellen im mysql ueber die stored procedures
                command.Parameters.AddWithValue("GeneralInfo", tc.GeneralInfo);
                command.Parameters.AddWithValue("Testquestion", tc.TestQuestion);
                command.Parameters.AddWithValue("DiskreteScala", tc.DiskreteScale);
                command.Parameters.AddWithValue("NameTestCase", tc.NameTestCase);
                command.Parameters.AddWithValue("StateTestCase", tc.State);
                command.Parameters.AddWithValue("MinDiskreteScale", tc.MinDiskreteScale);
                command.Parameters.AddWithValue("MaxDiskreteScale", tc.MaxDiskreteScale);
                command.Parameters.AddWithValue("ActiveLearning", tc.ActiveLearning);
                command.Parameters.AddWithValue("DBPathIn", tc.dbPath);
                command.Parameters.AddWithValue("SeedIn", tc.iSeed);
                command.Parameters.AddWithValue("userThreshold", tc.userThreshold);
                command.Parameters.AddWithValue("initialThreshold", tc.initialThreshold);
                

                // neue id fuer das neue test case
                command.Parameters.Add("IDTestCase", MySqlDbType.Int64);
                command.Parameters["IDTestCase"].Direction = ParameterDirection.Output;
                
                command.ExecuteNonQuery();
                IdTestCase = (Int64)command.Parameters["IDTestCase"].Value;

                esito.result = true;

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.result = false;
            //    esito.Message= ex.Message;            
            //}

            return esito;
        }

        // change existing test case
        public static Result UpdateTestCase(long IdTestCase, bool ActiveLearning, string Generalinfo, int initalTreshold, int userThreshold)
        {
            Result res = new Result();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {

                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UPDATE testcase SET GeneralInfo=@generalinfo, ActiveLearning=@activelearning, userThreshold=@userThreshold, initalTreshold=@initialThreshold where IDTestcase=@idtestcase", conn);
                    // ersetzt die drei daten im database mit den neuen
                    command.Parameters.AddWithValue("@idtestcase", IdTestCase);
                    command.Parameters.AddWithValue("@activelearning", ActiveLearning);
                    command.Parameters.AddWithValue("@generalinfo", Generalinfo);
                    command.Parameters.AddWithValue("@userThreshold", userThreshold);
                    command.Parameters.AddWithValue("@initialThreshold", initalTreshold);

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

        // add link between test case and continuous scale
        public static Result InsertRelationshipTestScale(long IdTestCase, List<long> ListScale, bool isDiscrete)
        {
            Result esito = new Result();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();

                foreach (long idScale in ListScale)
                {
                    MySqlCommand command = new MySqlCommand("InsertTestCasetoScale", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // einfuegen des verbindung durch die stored procedure insert test case to scale continuous durch die beiden id 
                    command.Parameters.AddWithValue("IdTestCase", IdTestCase);
                    command.Parameters.AddWithValue("IdScale", idScale);
                    command.Parameters.AddWithValue("lIsDiscrete", Convert.ToInt16(isDiscrete));
                    command.ExecuteNonQuery();

                }
                esito.result = true;

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.result = false;
            //    esito.Message= ex.Message;            
            //}

            return esito;
        }

        // add link between test case and type user
        public static Result InsertRelationshipTestTypeUser(long IdTestCase, List<long> ListTypeUser)
        {
            Result esito = new Result();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();

                foreach (long idtypeuser in ListTypeUser)
                {
                    MySqlCommand command = new MySqlCommand("InsertTestCasetoTypeUser", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // einfuegen des verbindung durch die stored procedure insert test case to type user durch die beiden id 
                    command.Parameters.AddWithValue("IdTestCase", IdTestCase);
                    command.Parameters.AddWithValue("IdTypeuser", idtypeuser);

                    command.ExecuteNonQuery();
                }

                esito.result = true;

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.result = false;
            //    esito.Message= ex.Message;            
            //}

            return esito;
        }

        // speichert im db die neuen scale continuous 
        public static Result InsertTypeScaleContinuous(TypScaleContinuous tsc)
        {
            Result esito = new Result();
            int verScaleCont = 0;
            if ((tsc.verScaleCont > -1))
            {
                verScaleCont = tsc.verScaleCont;
            }
            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                // fuegt durch die stored procedure insert type scale continuous die drei infos zu jeder scala in die tabelle type scale continuous
                conn.Open();
                MySqlCommand command = new MySqlCommand("InsertTypeScaleContinuous", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("DescriptionScaleContinuous", tsc.DescriptionScaleContinuous);
                command.Parameters.AddWithValue("VersionScaleContinuous", verScaleCont);
                command.Parameters.AddWithValue("PathImageMin", tsc.PathImageMin);
                command.Parameters.AddWithValue("PathImageMax", tsc.PathImageMax);

                command.ExecuteNonQuery();


                esito.result = true;

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.result = false;
            //    esito.Message= ex.Message;            
            //}

            return esito;
        }

        // speichert im db die neuen scale discrete 
        public static Result InsertTypeScaleDiscrete(TypScaleDiscrete tsc)
        {
            Result esito = new Result();

            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                // fuegt durch die stored procedure insert type scale continuous die drei infos zu jeder scala in die tabelle type scale continuous
                conn.Open();
                MySqlCommand command = new MySqlCommand("InsertTypeScaleDiscrete", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("DescriptionScaleDiscrete", tsc.DescriptionScaleDiscrete);
                command.Parameters.AddWithValue("PathImageMin", tsc.PathImageMin);
                command.Parameters.AddWithValue("PathImageMax", tsc.PathImageMax);

                command.ExecuteNonQuery();


                esito.result = true;

            }

            return esito;
        }

        // fuegt die infos zu jeder gruppe in die tabelle im db ein 
        public static Result InsertGroup(long IdTestcase,List<Group> Listgroup)
        {
            Result esito = new Result();
            long IdGroup = 0;
            try
            {
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();

                // fuer jede gruppe in der liste group
                foreach (Class.Group group in Listgroup)
                {
                    // nimmt die infos und speichert diese in den db 
                    MySqlCommand command = new MySqlCommand("InsertGroup", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Name", group.Name);
                    command.Parameters.AddWithValue("IdTestCase", IdTestcase);
                    command.Parameters.AddWithValue("ReferenceIsGlobal", group.ReferenceIsGlobal);
                    command.Parameters.AddWithValue("IsPatientChosen", group.IsPatientChosen);
                    command.Parameters.Add("IdGroup", MySqlDbType.Int64);
                    command.Parameters.AddWithValue("PageStyle", group.PageStyle);
                    command.Parameters.AddWithValue("HasReference", group.GroupHasReference);
                    command.Parameters.AddWithValue("ImagesPerPage", group.ImagesPerPage);

                    command.Parameters["IdGroup"].Direction = ParameterDirection.Output;

                    // definiert ein id fuer jede neue gruppe
                    command.ExecuteNonQuery();
                    IdGroup = (Int64)command.Parameters["IdGroup"].Value;

                    // das gleiche wie oben aber fuer sequenzen also subdirectories
                    foreach (Class.Directory dir in group.SubDirectory)
                    {
                        command = new MySqlCommand("InsertRelationshipImageTestCase", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("IdGroup", IdGroup);
                        command.Parameters.AddWithValue("PathImage",dir.Path );
                        command.Parameters.AddWithValue("IsReference", dir.IsReference);
                        command.Parameters.AddWithValue("NamePatiente", dir.Path.Split('/')[0]);
                        command.ExecuteNonQuery();
                    
                    }

                }

                esito.result = true;

            }
            }
            catch (Exception ex)
            {
                esito.result = false;
                esito.Message = ex.Message;
            }

            return esito;
        }

        // removes groups in db deleted by admin
        public static Result deleteGroup(long IdTestcase, List<long> listRemovedGroups)
        {
            Result res = new Result();

         try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();

                    foreach (long group in listRemovedGroups)
                    {
                        MySqlCommand command = new MySqlCommand("deleteGroup", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("IdGroup", group);
    
                        command.ExecuteNonQuery();
                    }

                }

                res.result = true;
            }
         catch (Exception ex)
         {
             res.result = false;
             res.Message = ex.Message;           
         }


            return res;
        }





        // nimmt aus dem db den test case
        public static List<Class.TestCase> getTestCase()
        {
            List<Class.TestCase> tc = new List<Class.TestCase>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetTestCase", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idTypeUser", DBNull.Value);
                command.Parameters.AddWithValue("@idTestCaseIn", DBNull.Value);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // weisst jeder info eine variable zu 
                        Class.TestCase testcase = new Class.TestCase();
                        testcase.IDTestcase = reader.GetInt64(0);
                        testcase.GeneralInfo = reader.GetString(1);
                        testcase.TestQuestion = reader.GetString(2);
                        testcase.DiskreteScale = reader.GetBoolean(3);
                        testcase.DescrState = reader.GetString(4);
                        testcase.NameTestCase = reader.GetString(5);
                        if (reader["MinDiskreteScale"] != DBNull.Value)
                            testcase.MinDiskreteScale = reader.GetInt32(6);
                        if (reader["MaxDiskreteScale"] != DBNull.Value)
                                testcase.MaxDiskreteScale = reader.GetInt32(7);
                        if (reader["ActiveLearning"] != DBNull.Value)
                            testcase.ActiveLearning = reader.GetBoolean(8);
                        testcase.dbPath = reader.GetString(9);
                        if (reader["isActive"] != DBNull.Value)
                            testcase.isActive = reader.GetBoolean(10);
                        if (reader["initialThreshold"] != DBNull.Value)
                            testcase.initialThreshold = reader.GetInt32(11);
                        if (reader["userThreshold"] != DBNull.Value)
                            testcase.userThreshold = reader.GetInt32(12);

                        tc.Add(testcase);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return tc;
        }

        // nimmt testcase nur fuer angegeben user type 
        public static List<Class.TestCase> getTestCase(int idtypeuser,long iduser)
        {
            List<Class.TestCase> tc = new List<Class.TestCase>();
            bool isActiveTC = true;
            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand();

                command = new MySqlCommand("GetTestCase", conn);
                 

                
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idTypeUser", idtypeuser);
                command.Parameters.AddWithValue("@idTestCaseIn", DBNull.Value);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.TestCase testcase = new Class.TestCase();
                        testcase.IDTestcase = reader.GetInt64(0);
                        testcase.GeneralInfo = reader.GetString(1);
                        testcase.TestQuestion = reader.GetString(2);
                        //testcase.LinkQuestion = reader.GetString(3);
                        //testcase.ReferenzImage = reader.GetString(4);
                        testcase.DiskreteScale = reader.GetBoolean(3);
                        testcase.DescrState = reader.GetString(4);
                        testcase.NameTestCase = reader.GetString(5);

                        if (reader["ActiveLearning"] != DBNull.Value)
                            testcase.ActiveLearning = reader.GetBoolean(8);
                        testcase.dbPath = reader.GetString(9);
                        //if (reader["isActive"] != DBNull.Value)
                        isActiveTC = reader.GetBoolean(10);

                        if (isActiveTC)
                        {
                            testcase.Percentual = DataAccess.DataAccessTestCase.CalculatePercentual(iduser, testcase.IDTestcase, testcase.DiskreteScale);
                            tc.Add(testcase);
                        }
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return tc;
        }


        // nimmt testcase fuer angegeben ID testcase 
        public static Class.TestCase getTestCase(long idtestcase)
        {

            Class.TestCase testcase = new Class.TestCase();
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetTestCase", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idTypeUser", DBNull.Value);
                command.Parameters.AddWithValue("@idTestCaseIn", idtestcase);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                        testcase.IDTestcase = reader.GetInt64(0);
                        testcase.GeneralInfo = reader.GetString(1);
                        testcase.TestQuestion = reader.GetString(2);
                        //testcase.LinkQuestion = reader.GetString(3);
                        //testcase.ReferenzImage = reader.GetString(4);
                        testcase.DiskreteScale = reader.GetBoolean(3);
                        testcase.DescrState = reader.GetString(4);
                        testcase.NameTestCase = reader.GetString(5);

                        if (reader["ActiveLearning"] != DBNull.Value)
                            testcase.ActiveLearning = reader.GetBoolean(8);
                        testcase.dbPath = reader.GetString(9);

                    }
                }

            }

            return testcase;
        }

        // switched den Zustand des Testcase
        public static Result toggleTestCase(long idtestcase)
        {
            Result res = new Result();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("ToggleTestCase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("IDTestcaseIn", idtestcase);
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

        // nimmt die user fuer jeden test case, wird fuer view testcase benutzt
        public static List<Class.User> getUserByIdTestCase(long idtestcase)
        {
            List<Class.User> listuser = new List<Class.User>();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("GetlistUserByIdTestCase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idTestCase", idtestcase);
                    // durch den oben angegebenen id von test case nimmt das program aus dem db die infos vom user
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Class.User user = new Class.User();
                            user.Name = reader.GetString(0);
                            user.Surname = reader.GetString(1);
                            user.DescriptionType = reader.GetString(2);
                            user.Email = reader.GetString(3);
                            bool discretescale = reader.GetBoolean(4);
                            user.Id_user = reader.GetInt64(5);

                            // berechnung des prozensatz
                            user.Percentual = DataAccess.DataAccessTestCase.CalculatePercentual(user.Id_user, idtestcase, discretescale);
                            listuser.Add(user);
                        }
                    }

                }
            }
            catch (Exception)
            {
                
            }

            return listuser;
        }

        // nimmt fuer einen angegebene id user alle labels 
        public static List<Class.Lable> GetLableforIdUserIdTestcase(long idtestcase,long iduser)
        {
            List<Class.Lable> listlable = new List<Class.Lable>();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("GetLableforIdUserIdTestcase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // angeben des id testcase und id user damit die noten von DEM user zu DEM testcase gefunden werden
                    command.Parameters.AddWithValue("@idtestcase", idtestcase);
                    command.Parameters.AddWithValue("@iduser", iduser);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Class.Lable lable = new Class.Lable();

                            lable.IdLable = reader.GetInt64(0);
                            lable.lable = reader.GetDecimal(1);
                            lable.PathImage = reader.GetString(2);
                            lable.Description = reader.GetString(3);
                            lable.TestCaseName = reader.GetString(4);
                            lable.NameUser = reader.GetString(5);
                            lable.SurnameUser = reader.GetString(6);
                            lable.EmailUser = reader.GetString(7);
                            lable.TestQuestion = reader.GetString(8);
                            lable.TotalValue = reader.GetString(9);
                            if (reader[10]!= DBNull.Value)
                                 lable.IsReference = reader.GetString(10);
                            if (reader[11]!= DBNull.Value)
                                lable.ReferenceImage = reader.GetString(11);

                            lable.LabelCode = reader.GetInt64(12);
                            lable.UserId = ""+iduser;
                            listlable.Add(lable);
                        }
                    }

                }
            }
            catch (Exception)
            {

            }


            return listlable;
        }




        // gets all labelled labels for a testcase
        public static List<Class.Lable> GetLabelsforIdTestcase(long idtestcase)
        {
            List<Class.Lable> listlable = new List<Class.Lable>();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("GetLabelsforIdTestcase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("idtestcase", idtestcase);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Class.Lable lable = new Class.Lable();

                            lable.IdLable = reader.GetInt64(0);
                            lable.lable = reader.GetDecimal(1);
                            lable.PathImage = reader.GetString(2);
                            lable.Description = reader.GetString(3);
                            lable.TestCaseName = reader.GetString(4);
                            lable.NameUser = reader.GetString(5);
                            lable.SurnameUser = reader.GetString(6);
                            lable.EmailUser = reader.GetString(7);
                            lable.TestQuestion = reader.GetString(8);
                            lable.TotalValue = reader.GetString(9);
                            if (reader[10] != DBNull.Value)
                                lable.IsReference = reader.GetString(10);
                            if (reader[11] != DBNull.Value)
                                lable.ReferenceImage = reader.GetString(11);
                            lable.UserId = reader.GetString(12);
                            lable.LabelCode = reader.GetInt64(13);

                            listlable.Add(lable);
                        }
                    }

                }
            }
            catch (Exception)
            {

            }

            return listlable;
        }


        // gets all unique labelled labels for a testcase
        public static List<Class.Lable> getUniqueLabelsforIdTestcase(long idtestcase)
        {
            List<Class.Lable> listlable = new List<Class.Lable>();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("getUniqueLabelsforIdTestcase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("idtestcase", idtestcase);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            Class.Lable lable = new Class.Lable();

                            lable.IdLable = reader.GetInt64(0);
                            lable.lable = reader.GetDecimal(1);
                            lable.PathImage = reader.GetString(2);
                            lable.Description = reader.GetString(3);
                            lable.TestCaseName = reader.GetString(4);
                            lable.NameUser = reader.GetString(5);
                            lable.SurnameUser = reader.GetString(6);
                            lable.EmailUser = reader.GetString(7);
                            lable.TestQuestion = reader.GetString(8);
                            lable.TotalValue = reader.GetString(9);
                            if (reader[10] != DBNull.Value)
                                lable.IsReference = reader.GetString(10);
                            if (reader[11] != DBNull.Value)
                                lable.ReferenceImage = reader.GetString(11);
                            lable.UserId = reader.GetString(12);
                            lable.LabelCode = reader.GetInt64(13);

                            listlable.Add(lable);
                        }
                    }

                }
            }
            catch (Exception)
            {

            }

            return listlable;
        }


        // nimmt die continuouierlichen skalen aus db
        public static List<TypScaleContinuous> getScaleContinuous()
        {
            List<TypScaleContinuous> tsc = new List<TypScaleContinuous>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetScaleContinuous", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idScaleCont", DBNull.Value);
                // durch die stored procedure get scale continuous nimmt des code sich die infos und speichert sie als variabeln
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.TypScaleContinuous typescalecontinuous = new Class.TypScaleContinuous();
                        typescalecontinuous.ID_TypScaleContinuous = reader.GetInt16(0);
                        typescalecontinuous.DescriptionScaleContinuous = reader.GetString(1);
                        typescalecontinuous.verScaleCont = reader.GetInt16(2);
                        typescalecontinuous.PathImageMin = reader.GetString(3); //(reader.GetString(3)).Replace("\\", "\\\\");
                        typescalecontinuous.PathImageMax = reader.GetString(4); //(reader.GetString(4)).Replace("\\", "\\\\");

                        //typescalecontinuous.PathImageMin = ((typescalecontinuous.PathImageMin).Split('\\').Last());
                        //typescalecontinuous.PathImageMax = ((typescalecontinuous.PathImageMax).Split('\\').Last());

                        if (typescalecontinuous.PathImageMin != "0" && typescalecontinuous.PathImageMin.Contains("/"))
                        {
                            typescalecontinuous.PathImageMin = typescalecontinuous.PathImageMin.Split('/')[1];
                        }

                        if (typescalecontinuous.PathImageMax != "0" && typescalecontinuous.PathImageMax.Contains("/"))
                        {
                            typescalecontinuous.PathImageMax = typescalecontinuous.PathImageMax.Split('/')[1];


                        }
                        // nimmt nur eine BEstimmte laenge, sonst zu lang
                        //if (typescalecontinuous.PathImageMax.Length > 25)
                        //{
                        //    typescalecontinuous.PathImageMax = typescalecontinuous.PathImageMax.Substring(0, 25) + "...";
                        //}


                        //if (typescalecontinuous.PathImageMin.Length > 25)
                        //{

                        //    typescalecontinuous.PathImageMin = typescalecontinuous.PathImageMin.Substring(0, 25) + "...";
                        //}



                        tsc.Add(typescalecontinuous);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return tsc;
        }

        // nimmt die continuouierlichen skalen aus db abhaengig von id
        public static Class.TypScaleContinuous getScaleContinuous(int idtypscalecont)
        {
            TypScaleContinuous tsc = new TypScaleContinuous();

            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetScaleContinuous", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idScaleCont", idtypscalecont);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tsc.ID_TypScaleContinuous = reader.GetInt16(0);
                        tsc.DescriptionScaleContinuous = reader.GetString(1);
                        tsc.verScaleCont = reader.GetInt16(2);
                        tsc.PathImageMin = reader.GetString(3);
                        tsc.PathImageMax = reader.GetString(4);

                        if (tsc.PathImageMin != "0" && tsc.PathImageMin.Contains("/"))
                        {
                            tsc.PathImageMin = tsc.PathImageMin.Split('/')[1];
                        }

                        if (tsc.PathImageMax != "0" && tsc.PathImageMax.Contains("/"))
                        {
                            tsc.PathImageMax = tsc.PathImageMax.Split('/')[1];


                        }
                        // nimmt nur eine BEstimmte laenge, sonst zu lang
                        if (tsc.PathImageMax.Length > 25)
                        {
                            tsc.PathImageMax = tsc.PathImageMax.Substring(0, 25) + "...";
                        }


                        if (tsc.PathImageMin.Length > 25)
                        {

                            tsc.PathImageMin = tsc.PathImageMin.Substring(0, 25) + "...";
                        }
                    }
                }

            }

            return tsc;
        }

        // kontrolle ob test case name schon existiert
        public static bool VerifyExistTestCaseName(string testcasename)
        {
            bool found = false;

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT NameTestCase FROM testcase WHERE NameTestCase=@testcasename", conn);
                command.Parameters.AddWithValue("@testcasename", testcasename);
                
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        found = true;
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return found;
        }

        public static Result DeleteScaleContinuous(int idtype)
        {
            Result esito = new Result();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("DeleteScaleContinuous", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // ruft die stored procedure  delete scale continuous auf und gibt ihr den id 
                    command.Parameters.AddWithValue("TypScaleContinuous", idtype);
                    command.ExecuteNonQuery();


                    esito.result = true;

                }
            }
            catch (Exception ex)
            {
                esito.result = false;
                esito.Message = ex.Message;
            }

            return esito;
        }

        public static Result DeleteScaleDiscrete(int idtype)
        {
            Result esito = new Result();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("DeleteScaleDiscrete", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // ruft die stored procedure  delete scale continuous auf und gibt ihr den id 
                    command.Parameters.AddWithValue("IDScaleDisc", idtype);
                    command.ExecuteNonQuery();


                    esito.result = true;

                }
            }
            catch (Exception ex)
            {
                esito.result = false;
                esito.Message = ex.Message;
            }

            return esito;
        }

        // nimmt die diskreten skalen aus db
        public static List<TypScaleDiscrete> getScaleDiscrete()
        {
            List<TypScaleDiscrete> tsc = new List<TypScaleDiscrete>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetScaleDiscrete", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idScaleDisc", DBNull.Value);
                // durch die stored procedure get scale continuous nimmt des code sich die infos und speichert sie als variabeln
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.TypScaleDiscrete typescaledisc = new Class.TypScaleDiscrete();
                        typescaledisc.ID_TypScaleDiscrete = reader.GetInt16(0);
                        typescaledisc.DescriptionScaleDiscrete = reader.GetString(1);
                        typescaledisc.PathImageMin = reader.GetString(2);
                        typescaledisc.PathImageMax = reader.GetString(3);

                        if (typescaledisc.PathImageMin != "0" && typescaledisc.PathImageMin.Contains("/"))
                        {
                            typescaledisc.PathImageMin = typescaledisc.PathImageMin.Split('/')[1];
                        }

                        if (typescaledisc.PathImageMax != "0" && typescaledisc.PathImageMax.Contains("/"))
                        {
                            typescaledisc.PathImageMax = typescaledisc.PathImageMax.Split('/')[1];
                        }

                        //typescaledisc.PathImageMin = ((typescaledisc.PathImageMin.Split('\\')).Last());
                        //typescaledisc.PathImageMax = ((typescaledisc.PathImageMax.Split('\\')).Last());


                        // nimmt nur eine BEstimmte laenge, sonst zu lang
                        //if (typescaledisc.PathImageMax.Length > 25)
                        //{
                        //    typescaledisc.PathImageMax = typescaledisc.PathImageMax.Substring(0, 25) + "...";
                        //}


                        //if (typescaledisc.PathImageMin.Length > 25)
                        //{

                        //    typescaledisc.PathImageMin = typescaledisc.PathImageMin.Substring(0, 25) + "...";
                        //}



                        tsc.Add(typescaledisc);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return tsc;
        }

        // nimmt die diskreten skalen aus db abhaengig von id
        public static Class.TypScaleDiscrete getScaleDiscrete(int idscaledisc)
        {
            Class.TypScaleDiscrete typescaledisc = new Class.TypScaleDiscrete();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetScaleDiscrete", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@idScaleDisc", idscaledisc);
                // durch die stored procedure get scale continuous nimmt des code sich die infos und speichert sie als variabeln
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        typescaledisc.ID_TypScaleDiscrete = reader.GetInt16(0);
                        typescaledisc.DescriptionScaleDiscrete = reader.GetString(1);
                        typescaledisc.PathImageMin = reader.GetString(2);
                        typescaledisc.PathImageMax = reader.GetString(3);

                        if (typescaledisc.PathImageMin != "0" && typescaledisc.PathImageMin.Contains("/"))
                        {
                            typescaledisc.PathImageMin = typescaledisc.PathImageMin.Split('/')[1];
                        }

                        if (typescaledisc.PathImageMax != "0" && typescaledisc.PathImageMax.Contains("/"))
                        {
                            typescaledisc.PathImageMax = typescaledisc.PathImageMax.Split('/')[1];


                        }
                        // nimmt nur eine BEstimmte laenge, sonst zu lang
                        if (typescaledisc.PathImageMax.Length > 25)
                        {
                            typescaledisc.PathImageMax = typescaledisc.PathImageMax.Substring(0, 25) + "...";
                        }


                        if (typescaledisc.PathImageMin.Length > 25)
                        {

                            typescaledisc.PathImageMin = typescaledisc.PathImageMin.Substring(0, 25) + "...";
                        }

                    }
                }

            }

            return typescaledisc;
        }

        // nimmt alle gruppen von Bildern die in einem Testcase gespeichert sind, output liste der gruppen
        public static List<Class.Group> getGroupFromIdTestCase(long IdTestCase)
        {
             List<Class.Group> listgr = new List<Class.Group>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetGroupFromIdTestCase", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdTestCase", IdTestCase);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.Group gr = new Class.Group();

                        gr.IdGroup =reader.GetInt64(0);
                        gr.Name = reader.GetString(1);
                        gr.IdTestCase =reader.GetInt64(2);
                        gr.ReferenceIsGlobal=reader.GetBoolean(3);
                        if (reader["IsPatientChosen"] != DBNull.Value)
                             gr.IsPatientChosen = reader.GetBoolean(4);
                        gr.PageStyle = reader.GetInt64(5);
                        if (reader["HasReference"] != DBNull.Value)
                             gr.GroupHasReference = reader.GetBoolean(6);
                        gr.ImagesPerPage = reader.GetInt32(7);
                        listgr.Add(gr);                  
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return listgr;
        }

        // nimmt die infos aus der tabelle group_has_image 
        public static List<Class.GroupImage> GetGrouphasImageFromGroup(long idgroup, long iduser)
        {
            List<Class.GroupImage> listgr = new List<Class.GroupImage>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetGrouphasImageFromGroup", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // nimmt die infos in abhangigkeit von id group und id user
                command.Parameters.AddWithValue("@id_group", idgroup);
                command.Parameters.AddWithValue("@iduser", iduser);
                 
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GroupImage g = new GroupImage();

                        g.Idgroup=reader.GetInt64(0);
                        g.Path=reader.GetString(1);
                        g.IsReference=reader.GetBoolean(2);
                        g.IdGroupHasImage = reader.GetInt64(3);
                        g.PatienteName = reader.GetString(4);

                        if (reader["VoteDiscrete"] != DBNull.Value)
                            g.LableDiscrete = reader.GetInt32(5);
                        else
                            g.LableDiscrete = 0;

                        listgr.Add(g);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return listgr;
        }


        public static List<Class.GroupImage> getAllImagesOfTestcase(long testcaseId)
        {
            List<Class.GroupImage> listgr = new List<Class.GroupImage>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("getAllImagesOfTestcase", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@testcaseId", testcaseId);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        GroupImage g = new GroupImage();

                        g.Idgroup = reader.GetInt64(0);
                        g.Path = reader.GetString(1);
                        g.IsReference = reader.GetBoolean(2);
                        g.IdGroupHasImage = reader.GetInt64(3);
                        g.PatienteName = reader.GetString(4);

                        listgr.Add(g);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return listgr;
        }




        // nicht mehr benutzt, ist fuer die kontrolle im view image den namen der bilder zu sehen
        public static List<string> GetNamePatiente(long idgroup)
        {
            List<string> s = new List<string>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetNamePatiente", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdGroup", idgroup);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                     
                        string Name = reader.GetString(0);

                        s.Add(Name);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return s;
        }

        // nimmt fuer jeden test case die kontinuierliche scalen 
        public static List<Class.TypScaleContinuous> TypeScaleContinuousFromIdTestCase(long IdTestCase)
        {
            List<Class.TypScaleContinuous> listtc = new List<Class.TypScaleContinuous>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("TypeScaleContinuousFromIdTestCase", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdTestCase", IdTestCase);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.TypScaleContinuous tc = new Class.TypScaleContinuous();

                        tc.Testcase_IDTestcase = reader.GetInt32(0);
                        tc.ID_TypScaleContinuous = reader.GetInt32(1);
                        tc.DescriptionScaleContinuous = reader.GetString(2);
                        tc.verScaleCont = reader.GetInt16(3);
                        tc.PathImageMin = reader.GetString(4);
                        tc.PathImageMax = reader.GetString(5);
                        
                        listtc.Add(tc);


                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return listtc;
        }

        // nimmt fuer jeden test case die diskrete Skala
        public static Class.TypScaleDiscrete TypeScaleDiscreteFromIdTestCase(long IdTestCase)
        {
            Class.TypScaleDiscrete tsd = new Class.TypScaleDiscrete();

            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("TypeScaleDiscreteFromIdTestCase", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdTestCase", IdTestCase);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tsd.Testcase_IDTestcase = reader.GetInt32(0);
                        tsd.ID_TypScaleDiscrete = reader.GetInt32(1);
                        tsd.DescriptionScaleDiscrete = reader.GetString(2);
                        tsd.PathImageMin = (reader.GetString(3)).Replace("\\", "\\\\");
                        tsd.PathImageMax = (reader.GetString(4)).Replace("\\", "\\\\");
                    }
                }

            }

            return tsd;
        }

        public static Result InsertLableScaleDiskrete(LableScaleDiskrete lable)
        {
            Result res = new Result();
            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand("InsertLableScaleDiskrete", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Lable", lable.Lable);
                    command.Parameters.AddWithValue("IdUser", lable.IdUser);
                    command.Parameters.AddWithValue("IdGroupImage", lable.IdGroupImage);
                    command.ExecuteNonQuery();


                }

                res.result = true;


            }
            catch (Exception ex)
            {
                res.result = false;
                res.Message = ex.Message;
            }

            return res;
        }

        public static Result InsertLableScaleContinuous(LableScaleContinuous lable)
        {
            Result res = new Result();
            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand("InsertLableScaleContinuous", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Lable", lable.Lable);
                    command.Parameters.AddWithValue("IdUser", lable.IdUser);
                    command.Parameters.AddWithValue("IdGroupImage", lable.IdGroupImage);
                    command.Parameters.AddWithValue("IdScaleContinuous", lable.IdScaleContinuous);
                    command.ExecuteNonQuery();


                }

                res.result = true;


            }
            catch (Exception ex)
            {
                res.result = false;
                res.Message = ex.Message;
            }

            return res;
        }

        public static Result UpdateScaleContinuous(int idtypescalecontinuous, string TypScaleContinuous, int VersionScaleCont, string  ImageMin, string  ImageMax)
        {
            Result res = new Result();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command;
                    if (ImageMin == null && ImageMax == null)
                    {
                        command = new MySqlCommand("UpdateScaleContinuousNone", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("idtypescalecontinuous", idtypescalecontinuous);
                        command.Parameters.AddWithValue("TypScaleContinuous", TypScaleContinuous);
                        command.Parameters.AddWithValue("VersionScaleContIn", VersionScaleCont);
                    }
                    else if (ImageMin == null && ImageMax != null)
                    {
                        command = new MySqlCommand("UpdateScaleContinuousMax", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("idtypescalecontinuous", idtypescalecontinuous);
                        command.Parameters.AddWithValue("TypScaleContinuous", TypScaleContinuous);
                        command.Parameters.AddWithValue("VersionScaleContIn", VersionScaleCont);
                        command.Parameters.AddWithValue("ImageMax", ImageMax);
                    }
                    else if (ImageMin != null && ImageMax == null)
                    {
                        command = new MySqlCommand("UpdateScaleContinuousMin", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("idtypescalecontinuous", idtypescalecontinuous);
                        command.Parameters.AddWithValue("TypScaleContinuous", TypScaleContinuous);
                        command.Parameters.AddWithValue("VersionScaleContIn", VersionScaleCont);
                        command.Parameters.AddWithValue("ImageMin", ImageMin);
                    }
                    else
                    {

                        command = new MySqlCommand("UpdateScaleContinuous", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("idtypescalecontinuous", idtypescalecontinuous);
                        command.Parameters.AddWithValue("TypScaleContinuous", TypScaleContinuous);
                        command.Parameters.AddWithValue("VersionScaleContIn", VersionScaleCont);
                        command.Parameters.AddWithValue("ImageMin", ImageMin);
                        command.Parameters.AddWithValue("ImageMax", ImageMax);
                    }
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

        public static Result UpdateScaleDiscrete(int idtypescalediscrete, string TypScaleDiscrete, string ImageMin, string ImageMax)
        {
            Result res = new Result();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command;
                    if (ImageMin == null && ImageMax == null)
                    {
                        command = new MySqlCommand("UpdateScaleDiscreteNone", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("idtypescalediscrete", idtypescalediscrete);
                        command.Parameters.AddWithValue("TypScaleDiscrete", TypScaleDiscrete);
                    }
                    else if (ImageMin == null && ImageMax != null)
                    {
                        command = new MySqlCommand("UpdateScaleDiscreteMax", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("idtypescalediscrete", idtypescalediscrete);
                        command.Parameters.AddWithValue("TypScaleDiscrete", TypScaleDiscrete);
                        command.Parameters.AddWithValue("ImageMax", ImageMax);
                    }
                    else if (ImageMin != null && ImageMax == null)
                    {
                        command = new MySqlCommand("UpdateScaleDiscreteMin", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("idtypescalediscrete", idtypescalediscrete);
                        command.Parameters.AddWithValue("TypScaleDiscrete", TypScaleDiscrete);
                        command.Parameters.AddWithValue("ImageMin", ImageMin);
                    }
                    else
                    {

                        command = new MySqlCommand("UpdateScaleDiscrete", conn);
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("idtypescalediscrete", idtypescalediscrete);
                        command.Parameters.AddWithValue("TypScaleDiscrete", TypScaleDiscrete);
                        command.Parameters.AddWithValue("ImageMin", ImageMin);
                        command.Parameters.AddWithValue("ImageMax", ImageMax);
                    }
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

        public static List<LableScaleContinuous> GetLableContinuous(long idgroupimage, long iduser, List<TypScaleContinuous> label_list)
        {
           
            List<LableScaleContinuous> listlable = new List<LableScaleContinuous>();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                   
                    MySqlCommand command = new MySqlCommand("GetVoteLableContinuous", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@idgroupimage", idgroupimage);
                    command.Parameters.AddWithValue("@iduser", iduser);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {

                        
                        // initialisiert die Labels, falls diese noch nicht in der MySQL-Datenbank enthalten sind 
                        for (int i = 0; i < label_list.Count; i++)
                        {
                            LableScaleContinuous lable = new LableScaleContinuous();
                            lable.Lable = -1;//pcw
                            lable.IdScaleContinuous = label_list[i].ID_TypScaleContinuous;
                            lable.IdGroupImage = idgroupimage;
                            lable.IdUser = iduser;

                            listlable.Add(lable);
                        }

                        while (reader.Read())
                        {
                            // überschreibt, falls in der SQL-Datenbank vorhanden, die intialisierten Labels
                            decimal temp_label = reader.GetDecimal(0);
                            int temp_IdScaleContinuous = reader.GetInt32(1);
                            long temp_IdGroupImage = reader.GetInt64(2);

                            for (int u = 0; u < listlable.Count; u++){
                                if (listlable[u].IdScaleContinuous == temp_IdScaleContinuous)
                                {
                                    listlable[u].Lable = temp_label;
                                    listlable[u].IdScaleContinuous = temp_IdScaleContinuous;
                                    listlable[u].IdGroupImage = temp_IdGroupImage;
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception)
            {
     
            }

            return listlable;
        }

        public static string CalculatePercentual (long iduser,long idtestcase,bool discretescale)
        {

            string Percentual = "" ;
            decimal numimage = 0;
            int CountImage = 0;
            int CountScaleContinue = 0;
            decimal CountImageTot = 0;


            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand("GetPercentualViewTestCase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdUser", iduser);
                    command.Parameters.AddWithValue("@idtestcase", idtestcase);


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (discretescale)
                            {
                                Percentual = Math.Round(Convert.ToDecimal(reader["PercDiscrete"].ToString()),0) + "%";
                                break;
                            }
                            else
                            {   // get number of labelled labels
                                numimage=(Convert.ToInt32(reader["LableContinuous"]));           
                                CountScaleContinue = Convert.ToInt32(reader["CountScaleContinue"]);

                                break;

                            }
                        }
                    }



                    if (!discretescale)
                    {
                        // get number of images
                        MySqlCommand command2 = new MySqlCommand("GetNumberOfPictures", conn);
                        command2.CommandType = System.Data.CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@idtestcase", idtestcase);

                        using (MySqlDataReader reader = command2.ExecuteReader())
                        {
                            while (reader.Read())
                            {   
                                    CountImage = Convert.ToInt32(reader["CountImage"]);
 
                            }
                        }



                    }

                }
            }
            catch (Exception)
            {

            }

            if (!discretescale)
            {
                try
                {
                    CountImageTot = ((CountImage) * CountScaleContinue);

                    decimal p = Math.Round(((numimage / CountImageTot) * 100), 0);

                    Percentual = p.ToString() + "%";
                }
                catch (System.DivideByZeroException)
                {
                    Percentual ="Error";
                }
 
            }

            return Percentual;
        }

        public static double GetLabeledAmount(long iduser, long idtestcase, bool discretescale)
        {

            double dAmount= 0;



                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();

                    MySqlCommand command = new MySqlCommand("GetPercentualViewTestCase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdUser", iduser);
                    command.Parameters.AddWithValue("@idtestcase", idtestcase);

                    /*
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (discretescale)
                            {
                                dAmount = Convert.ToDouble(reader["PercDiscrete"].ToString());
                                break;
                            }
                            else
                            {
                                int numimage = (Convert.ToInt32(reader["LableContinuous"]));
                                int CountImageTot = (Convert.ToInt32(reader["CountImage"]) * Convert.ToInt32(reader["CountScaleContinue"]));


                                dAmount = ((numimage / CountImageTot) * 100);

                                break;

                            }
                        }
                    }*/
                }
            

            return dAmount;
        }

        // checks if active learning thershold is reached
        public static bool checkThreshold(Class.manageImg managerview)
        {
            int statusFiles = (int)HttpContext.Current.Session["statusFiles"];
            int currentStatusFiles = 0;
            HttpContext.Current.Session["refresh"] = false;
            bool reloadPage = false;
            int numberOfLabelledTotal = 0;
            int numberOfLabelledUser = 0;

            // get number of completely labelled images from db 
            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();


                    MySqlCommand command = new MySqlCommand("getOverallNumberOfLabelled", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("idTestCase", managerview.IDTestcase);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            numberOfLabelledTotal += reader.GetInt32(0);
                        }
                    }

                    MySqlCommand command2 = new MySqlCommand("getOverallNumberOfLabelledByUser", conn);
                    command2.CommandType = System.Data.CommandType.StoredProcedure;
                    command2.Parameters.AddWithValue("idTestCase", managerview.IDTestcase);
                    command2.Parameters.AddWithValue("UserID", managerview.IDUser);
                    
                    using (MySqlDataReader reader = command2.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            numberOfLabelledUser += reader.GetInt32(0);
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception)
            {

            }

            string pathIn = Constant.pathALinitialInput + string.Format("{0:D4}", managerview.IDTestcase) + ".csv";
            string pathInUser = Constant.pathALuserInput + string.Format("{0:D4}", managerview.IDTestcase) + "_User" + string.Format("{0:D4}", managerview.IDUser) + ".csv";
            int numberOfOptimizations = 1;
            int numberOfOptimizationsUser = 1;


            // check user threshold
            // check if matlab output files exist
            if (System.IO.File.Exists(pathInUser)) // check if file created by matlab code exists, if not, no optimizations have taken place yet
            {
                numberOfOptimizationsUser = int.Parse(System.IO.File.ReadLines(pathInUser).First()); // get number of previous matlab optimizations

                int opti = (int)HttpContext.Current.Session["numberOfOptimizationsUser"];

            

                if (opti != numberOfOptimizationsUser)
                {
                    currentStatusFiles = -1;
                }else{
                    currentStatusFiles += 2;
                } 
            }
            // check initial threshold
            else if (System.IO.File.Exists(pathIn)) // check if file created by matlab code exists, if not, no optimizations have taken place yet
            {
                numberOfOptimizations = int.Parse(System.IO.File.ReadLines(pathIn).First());  // get number of previous matlab optimizations

                currentStatusFiles += 1;

            }    
            else { // no input files found
                HttpContext.Current.Session["fileFound"] = false;
            }


         //   Debug.Print("initial: " + managerview.tc.initialThreshold + " <= " + (numberOfLabelledTotal / numberOfOptimizations) + "\t" + (managerview.tc.initialThreshold <= (numberOfLabelledTotal / numberOfOptimizations)));
         //   Debug.Print("user: " + managerview.tc.userThreshold + " <= " + (numberOfLabelledUser / numberOfOptimizationsUser) + "\t" + ((numberOfLabelledTotal / numberOfOptimizations)));

            // write files containing the labels for matlab
            if (managerview.tc.initialThreshold <= (numberOfLabelledTotal / numberOfOptimizations))
            {
                string pathOut = Constant.pathALinitialOutput + string.Format("{0:D4}", managerview.IDTestcase) + ".csv";

                // write *.csv file
                WriteFile(managerview.IDTestcase, -1, numberOfOptimizations + "", pathOut);

            }

            if (managerview.tc.userThreshold <= (numberOfLabelledUser / numberOfOptimizationsUser))
            {
                string pathOut = Constant.pathALuserOutput + string.Format("{0:D4}", managerview.IDTestcase) + "_User" + string.Format("{0:D4}", managerview.IDUser) + ".csv";

                // write *.csv file
                WriteFile(managerview.IDTestcase, managerview.IDUser, numberOfOptimizationsUser + "", pathOut);
            }

            if (currentStatusFiles != statusFiles)
            {
                reloadPage = true; // force JavaScript to do a postback 
                HttpContext.Current.Session["statusFiles"] = currentStatusFiles;
                HttpContext.Current.Session["refresh"] = true;
            }

            return reloadPage;
        }

        public static void WriteFile(long idtestcase, long iduser, string header, string path)
        {

            List<Lable> label = new List<Lable>();
            if (iduser != -1)
            {
                label = DataAccess.DataAccessTestCase.GetLableforIdUserIdTestcase(idtestcase, iduser);
            }
            else {
                label = DataAccess.DataAccessTestCase.GetLabelsforIdTestcase(idtestcase);
            }


            Class.TestCase tc = DataAccess.DataAccessTestCase.getTestCase(idtestcase);
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {
                    file.WriteLine(header);
                    foreach (Lable lable in label)
                    {
                        lable.dbPath = tc.dbPath;
                        file.WriteLine(lable.dbPath + "\\" + (lable.PathImage).Replace("/", "\\") + ";" + lable.LabelCode + ";" + lable.IdLable + ";" + string.Format("{0:0.00}", lable.lable) + ";" + lable.UserId + ";");
                    }
                }
            }catch(Exception){
                
            }
        }

        // get all user information from a test case
        public static List<Class.User> getUsersFromTestCaseID(long IdTestCase)
        {
            List<Class.User> users = new List<Class.User>();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("getUsersFromTestcase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@idTestCaseIn", IdTestCase);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Class.User user = new Class.User();

                            user.Id_user = reader.GetInt64(0);                         
                            user.Username = reader.GetString(1);
                            user.titleId = reader.GetInt32(2);
                            user.title = reader.GetString(3);
                            user.Name = reader.GetString(4);
                            user.Surname = reader.GetString(5);
                            user.Type = reader.GetInt32(6);
                            user.DescriptionType = reader.GetString(7);
                            user.Email = reader.GetString(8);
                            user.YearsOfExperience = reader.GetInt32(9);

                            users.Add(user);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

            }

            return users;
        }

        // removes testcase entry from MYSQL-db (not everything is deleted, only entries in table 'testcase', 'testcase_has_typuser' and 'testcase_has_scale')
        public static void removeTestcase(long testcaseID)
        {
            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("removeTestcase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("testcaseID", testcaseID);
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {

            }

        }


        public static int GetDiscreteLabel(long iduser, long IdGroupImage)
        {
            int label = 0;

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("GetDiscreteLabel", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    // angeben des id testcase und id user damit die noten von DEM user zu DEM testcase gefunden werden
                    command.Parameters.AddWithValue("@IdGroupImage", IdGroupImage);
                    command.Parameters.AddWithValue("@iduser", iduser);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            label = (int)reader.GetInt64(0);
                        }
                    }

                }
            }
            catch (Exception )
            {
               
            }

            return label;
        }
    }
}