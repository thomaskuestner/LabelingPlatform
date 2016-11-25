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


//*********************************************************************************************************//
//      all constants (user definitions)
//*********************************************************************************************************//


namespace LabelingFramework.Utility
{
    public static class Constant
    {

        public static string webSiteAddress = "http://labelingplatform";



        //******************************** security ************************************************//
        public static string pepper = "iWIuJXpkyTnNh5xGHwrMrg7Na/VoKXzxwScyJkWSKtw=";                   // Pepper for password handling

        // Please note: these characters must not be included in database / image paths (including file name)
        // prohibited characters: ! + # & ? % < = >
		// list of allowed characters for obfuscation: these characters can be used for database / image paths (including file name)
        public static string allowedCharacters = "!\"#$%&'(),-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿ";


        // values for ceasar chiffre 
        public static int ceasarOdd = 1;                                                        
        public static int ceasarEven = -2;
        //******************************************************************************************//

		

        //************************************ Paths ***********************************************//
        public static string currentDatabase = "\\DATA\\ImageDatabaseA";
        public static string sActiveLearningPath = "\\DATA\\ImageSimilarity";
        public static string sReferencePath = "\\DATA\\Reference";
        public static string pathALinitialInput = "\\DATA\\ActiveLearningData\\Initial\\DataInput\\";       // path for the initial active learning input from matlab
        public static string pathALuserInput = "\\DATA\\ActiveLearningData\\User\\DataInput\\";             // path for the user active learning input from matlab
        public static string pathALinitialOutput = "\\DATA\\ActiveLearningData\\Initial\\DataOutput\\";     // path from the initial active learning output for matlab 
        public static string pathALuserOutput = "\\DATA\\ActiveLearningData\\User\\DataOutput\\";           // path from the user active learning output for matlab 
        public static string pathUserList = "\\DATA\\ActiveLearningData\\Users.txt";                        // path for file containing all user datasets
        public static string pathConfigFileTestcase = "\\DATA\\ActiveLearningData\\Config_Testcase";        // path for testcase config file
        public static string pathTutorialPages = "\\DATA\\TutorialPages";                                
        public static string RepositoryTestCase = "\\DATA\\Testcase";
        //******************************************************************************************//

        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        }

        public static List<String> lsExtensions = new List<String> { ".dcm", ".DCM", ".ima", ".IMA", ".mhd", ".MHD", ".gipl", ".GIPL"};

        //********************** Admin Email *****************************//
        public static string adminEmailAddress = "admin@mail.com";
        public static string adminEmailName = "MR Image labeling";
        public static string adminEmailPW = "ADMIN_PWD";
        public static string adminEmailServer = "MAILSERVER.mail.com";
        public static int adminEmailPort = 25;
        public static string attachment = "\\DATA\\Shortcut.pdf";          // attachment file        

        // Options
        public static bool includeAttachment = false;                        // include attachment file to welcome email
        public static bool sendRegisteredAdmin = false;                     // send an registration email to admin (bcc)
        public static bool sendNotificationMailTC = true;                  // send an notification email after test case creation to all eligible users
        //*********************************************** EMAIL MESSAGES **************************************************
        // Register: Welcome email:
        public static List<string> registerMail(string name, string surname, string username, string password) {
            List<string> message = new List<string>();

            // subject:
            message.Add("Registration MR Image Labeling Framework");

            // message text:
            message.Add("Dear " + name + " " + surname + ",<br/><br/>thank you for registering to the MR Image Labeling Framework.<br/>Please stop by from time to time:<br/><a href=''"+ Constant.webSiteAddress + "''>" + Constant.webSiteAddress + "</a><br/>" + "Your account information:<br/>Username: " + username + "<br/>Password: " + password + "<br/><br/>For information about the usage and the controls, please press the ''?''-button during the image labeling or refer to <a href=''" + Constant.webSiteAddress + "/usage_help.html''>" + Constant.webSiteAddress + "/usage_help.html</a>.<br/><br/>Have fun with the labeling!");

            return message;
        }

        // Register notification to admin:
        public static List<string> registerNotificationAdminMail(string name, string surname, string username)
        {
            List<string> message = new List<string>();

            // subject:
            message.Add("A new user registered: Name: " + name + surname + "(" + username + ")");

            // message text
            message.Add("A new user registered: Name: " + name + surname + "(" + username + ")");

            return message;
        }

        // Password change: email with password
        public static List<string> changePasswordMail(string salutation, string password, string username) {
            List<string> message = new List<string>();

            // subject: 
            message.Add("Labelling Framework - change of your login password");

            // message text
            message.Add("Dear " + salutation + ",<br/><br/> your password has been changed due to request. If you have not requested your password to be changed, please contact the webmaster.<br/><br/> Your new password: " + password + "<br/>Username: " + username + "<br/> Please change your password after your first login.<br/><br/> Yours sincerely,<br/> the webmaster");

            return message;
        }

        // New test case: send test case notification to eligible users
        public static List<string> testCaseNotificationMail(Class.User user, Class.TestCase tc)
        {
            List<string> message = new List<string>();

            // subject: 
            message.Add("New test case " + tc.NameTestCase + " available on labeling framework website");

            // message text
            message.Add("Dear " + user.title + " " + user.Surname + ",<br/><br/> a new test case is available for labeling on: " + "<a href=''" + Constant.webSiteAddress + "''>" + Constant.webSiteAddress + "</a> <br/><br/>Please consider to participate!<br/>Thank you!");

            return message;
           
        }

        // Error report: report an error to the admin
        public static List<string> errorReportToAdminMail(Class.manageImg managerView, string messageFromUser) { 
            List<string> message = new List<string>();

            // subject: 
            message.Add("User: " + managerView.usr.title + " " + managerView.usr.Name + " " + managerView.usr.Surname + " (UserID: " + managerView.usr.Id_user + "; username: " + managerView.usr.Username + "; usertype: " + managerView.usr.DescriptionType + ")" + " has reported an error");

            // message text
            string messageText = "User info:<br/>";
            // add user info
            messageText += "UserID: " + managerView.usr.Id_user + "; User: " + managerView.usr.title + " " + managerView.usr.Name + " " + managerView.usr.Surname + " (username: " + managerView.usr.Username + ")" + "; usertype: " + managerView.usr.DescriptionType + "; email: " + managerView.usr.Email+";<br/><br/>";
            // add testcase info
            messageText += "testcase info:\nIdTestcase: " + managerView.IDTestcase + "; testcase name:" + managerView.tc.NameTestCase + "; test question: " + managerView.tc.TestQuestion + "; general info: " + managerView.tc.GeneralInfo + "; discrete: " + managerView.tc.DiskreteScale + "; active learning: " + managerView.tc.ActiveLearning + "; initial threshold: " + managerView.tc.initialThreshold + "; user interval: " + managerView.tc.userThreshold + ";<br/><br/>";
            messageText += messageFromUser;

            message.Add(messageText);

            return message;
        }
        //*****************************************************************************************************************
    }
}