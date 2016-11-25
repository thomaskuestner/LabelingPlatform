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
using System.Diagnostics; // *for testing purposes*
using LabelingFramework.Utility;

//*********************************************************************************************************//
// handles communication between C# an MySQL database
//*********************************************************************************************************//


namespace LabelingFramework.DataAccess
{
    public static class DataAccessUser
    {


        public static  Result RegisterUser (Class.User u)
        {
            Result res = new Result();
           
            try
            {
                PasswordHandling pw = new PasswordHandling();
                pw.hashPassword(u.Password);

                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("RegisterUser", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("Name", u.Name);
                    command.Parameters.AddWithValue("Surname", u.Surname);
                    command.Parameters.AddWithValue("title_Id", u.titleId);
                    command.Parameters.AddWithValue("Password", pw.getHashedPassword());
                    command.Parameters.AddWithValue("Salt", pw.getSalt());
                    command.Parameters.AddWithValue("username", u.Username);
                    command.Parameters.AddWithValue("Email", u.Email);
                    command.Parameters.AddWithValue("Type", u.Type);
                    command.Parameters.AddWithValue("YearOfExperience", u.YearsOfExperience);
                    command.Parameters.AddWithValue("DateInsert", u.DateInsert);

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

        public static Class.User LoginUser(string username, string password)
        {
            Class.User user = null;

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("LoginUser", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("inusername", username);
                command.Parameters.AddWithValue("Password", password);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = new Class.User();
                        user.Name = reader.GetString(0);
                        user.Surname = reader.GetString(1);
                        user.Email = reader.GetString(2);
                        user.Type = reader.GetInt16(3);
                        user.YearsOfExperience = reader.GetInt16(4);
                        user.Id_user = reader.GetInt32(5);
                        user.title = reader.GetString(6);
                        user.Username = reader.GetString(7);
                        user.DescriptionType = reader.GetString(8);
                       
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return user;
        }

        public static void writeUserList() {

            List<Class.User> userList = new List<Class.User>();

               using (MySqlConnection conn = DataAccessBase.GetConnection()){
        
                conn.Open();
               MySqlCommand command = new MySqlCommand("getListOfUsers", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.User user = new Class.User();
                        user.Id_user = reader.GetInt64(0);
                        user.Name = reader.GetString(1);
                        user.Surname = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.DescriptionType = reader.GetString(4);
                        user.YearsOfExperience = reader.GetInt16(5);
                        user.titleId = reader.GetInt32(6);
                        user.Username = reader.GetString(7);
                        user.title = reader.GetString(8);

                        userList.Add(user);

                    }
                }
            }

               if (userList.Count > 0) {




                   //System.IO.File.Exists(pathInUser)
                   using (System.IO.StreamWriter file = new System.IO.StreamWriter(Constant.pathUserList))
                   {
                       int amountOfUsers = 0;
                       file.WriteLine("UserId; title/salutation; first name;surname; username; email; user type; years of experience;");
                        foreach(Class.User user in userList){
                            file.WriteLine(user.Id_user + "; " + user.title + "; " + user.Name + "; " + user.Surname + "; " + user.Username + "; " + user.Email + "; " + user.DescriptionType + "; " + user.YearsOfExperience + ";");
                            amountOfUsers++;
                        }
                        file.WriteLine("total amount of users: " + amountOfUsers);
                   }





               }

        }



        public static List<Class.User> getUser()
        {
            List<Class.User> u = new List<Class.User>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetUser", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@iduser", DBNull.Value);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.User user = new Class.User();
                        user.Id_user = reader.GetInt64(0);
                        user.Name = reader.GetString(1);
                        user.Surname = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.DescriptionType = reader.GetString(4);
                        user.YearsOfExperience = reader.GetInt16(5);
                        user.Username = reader.GetString(9);
                       



                        if (reader["DateInsert"] != DBNull.Value)
                        {
                            user.DateInsert = reader.GetDateTime("DateInsert");

                           user.YearsOfExperience =  (DateTime.Now.Year -user.DateInsert.Year) + user.YearsOfExperience;
                        }


                        u.Add(user);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return u;
        }




        public static List<TypUser> getTypeUser()
        {
            List<TypUser> tu = new List<TypUser>();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetTypeUsers", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.TypUser typeuser = new Class.TypUser();
                        typeuser.ID_Typ = reader.GetInt16(0);
                        typeuser.DescriptionTypUser = reader.GetString(1);
                        tu.Add(typeuser);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return tu;
        }



        public static List<Title> getTitle()
        {
            List<Title> tu = new List<Title>();

            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetTitles", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Class.Title titleUser = new Class.Title();
                        titleUser.title_Id = reader.GetInt16(0);
                        titleUser.title = reader.GetString(1);
                        tu.Add(titleUser);
                    }
                }

            }

            return tu;
        }


        // checks if password is correct and returns corresponding user
        public static Class.User checkPassword(string username, string password)
        {
            Class.User userLogin = null;
            PasswordHandling pw = new PasswordHandling();
            try
            {
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("getPassword", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("inusername", username);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // get salt and hash from db
                        string hashedPassword = reader.GetString(0);
                        string salt = reader.GetString(1);

                        // check if password is valid
                        pw.checkPassword(password, hashedPassword, salt);
                    }
                    
                }

                conn.Close();

                // log in if user is valid
                if (pw.userIsVerified())
                {
                    userLogin = LoginUser(username, pw.getHashedPassword());
                }

                pw = null; 
            }
            }
            catch (Exception)
            {
                return null;
            }

            return userLogin;
        }



        public static TypUser getTypeUser(int TypeUser)
        {
            Class.TypUser typeuser = new Class.TypUser();
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("GetTypeUser", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("idtypeuser", TypeUser);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        typeuser.ID_Typ = reader.GetInt16(0);
                        typeuser.DescriptionTypUser = reader.GetString(1);
                    }
                }

            }

            return typeuser;
        }

        public static Result InsertTypeUser(TypUser tu)
        {
            Result res = new Result();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("InsertTypeUser", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("DescriptionType", tu.DescriptionTypUser);

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

        public static Result UpdateTypeUser(TypUser tu)
        {
            Result res = new Result();

            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("UpdateTypeUser", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("idtypeuser", tu.ID_Typ);
                command.Parameters.AddWithValue("TypeUser", tu.DescriptionTypUser);

                command.ExecuteNonQuery();


                res.result = true;

            }

            return res;
        }

        public static Result UpdateUser(int iduser,int TypeUser)
        {
            Result res = new Result();
           try
           {
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("UpdateUser", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("iduser", iduser);
                command.Parameters.AddWithValue("TypeUser", TypeUser);
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

        public static Result UpdateProfileView(long iduser, Class.User user)
        {
            Result res = new Result();

            PasswordHandling newPassword = new PasswordHandling();
            newPassword.generateSalt();
            newPassword.hashPassword(user.Password);

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("UpdateProfileView", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("name", user.Name);
                    command.Parameters.AddWithValue("Surname", user.Surname);
                    command.Parameters.AddWithValue("PW", newPassword.getHashedPassword());
                    command.Parameters.AddWithValue("Salt", newPassword.getSalt());
                    command.Parameters.AddWithValue("username", user.Username);
                    command.Parameters.AddWithValue("Email", user.Email);
                    command.Parameters.AddWithValue("iduser", iduser);
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

        public static Result ChangePasswordUser( string mail)
        {
            Result res = new Result();
            PasswordHandling newPassword = new PasswordHandling();
            newPassword.salt_length = 10;
            newPassword.generateSalt();
            string password = newPassword.getSalt();
            newPassword.generateSalt();
            newPassword.hashPassword(password);
            string dbEmail = "mail address not in db";
            string username = "user does not exist";
         
          

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("ChangePassword", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("PW", newPassword.getHashedPassword());
                    command.Parameters.AddWithValue("salt", newPassword.getSalt());
                    command.Parameters.AddWithValue("Email", mail.Trim());
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["email"] != DBNull.Value)
                            {
                                dbEmail = reader.GetString(0);
                            }
                            else {
                                dbEmail = "mail address not in db";
                            }
                        }
                    }


                    res.result = true;

                }
            }
            catch (Exception ex)
            {
                res.result = false;
                res.Message = ex.Message;
            }


            if (res.result) // if password has been successfully changed, send an email to user
            {
                if (mail == dbEmail)
                {
                    string salutation = "Sir / Madam";
            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("getUserWithEmail", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("in_email", mail);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["Title"] != DBNull.Value)
                            {
                                salutation = reader.GetString(0) + " ";
                            }
                            if (reader["surname"] != DBNull.Value)
                            {
                                salutation += reader.GetString(1);
                            }
                            username = reader.GetString(2);
                        }
                    }


                }
            }catch(Exception){}

                    List<string> message = Constant.changePasswordMail(salutation, password, username);

                    MyEmail notification = new MyEmail();

                    // notification.email.Bcc.Add(notification.sender); // send a copy of the mail to the email address of the admin

                    notification.sendEmail(mail, message[0], message[1]);
                }
                else {

                }

            }



            return res;
        }

        public static Result DeleteUser(int iduser)
        {
            Result esito = new Result();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("DeleteUser", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("iduser", iduser);
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

        public static Result DeleteTypeUser(int idtype)
        {
            Result esito = new Result();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("DeleteTypeUser", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("idtype", idtype);
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

        public static bool VerifyExistUser( string username)
        {
            bool found = false;

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("SELECT username FROM user WHERE username=@username", conn);
                command.Parameters.AddWithValue("@username", username);

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

        public static Class.User getUserById(long id_user)
        {
           Class.User user = new Class.User();

            //try
            //{
            using (MySqlConnection conn = DataAccessBase.GetConnection())
            {
                conn.Open();
                MySqlCommand command = new MySqlCommand("getUser", conn);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@iduser",id_user);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Id_user = reader.GetInt64(0);
                        user.Name = reader.GetString(1);
                        user.Surname = reader.GetString(2);
                        user.Email = reader.GetString(3);
                        user.DescriptionType = reader.GetString(4);
                        user.YearsOfExperience = reader.GetInt16(5);
                        user.Type = reader.GetInt16(6);
                        user.Password = reader.GetString(8);
                        user.Username = reader.GetString(9);
                    }
                }

            }
            //}
            //catch (Exception ex)
            //{
            //    esito.esito = false;
            //    esito.Message= ex.Message;            
            //}

            return user;
        }

        public static List<Class.TypUser> GetTypeUserForTestCase(long idTestCase)
        {

            List<Class.TypUser> listtypeUser = new List<Class.TypUser>();

            try
            {
                using (MySqlConnection conn = DataAccessBase.GetConnection())
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand("GetTypeUserForTestCase", conn);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Idtestcase", idTestCase);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Class.TypUser typeUser = new Class.TypUser();
                            typeUser.ID_Typ = reader.GetInt32(1);

                            listtypeUser.Add(typeUser);
                        }
                    }

                }
            }
            catch (Exception)
            {

            }

            return listtypeUser;
        
        
        }
    }
}