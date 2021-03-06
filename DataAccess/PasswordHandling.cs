﻿/*
k-Space Astronauts labeling platform
    (c) 2016 under Apache 2.0 license 
    Thomas Kuestner, Martin Schwartz, Philip Wolf
    Please refer to https://sites.google.com/site/kspaceastronauts/iqa/labelingplatform for more information
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics; // *for testing purposes*
using LabelingFramework.Utility;

//*********************************************************************************************************//
//  class for password handling
//*********************************************************************************************************//

namespace LabelingFramework.DataAccess
{
    public class PasswordHandling
    {
      private string password;          // plain text password
      private string salt;              // salt
      private string hashedPassword;    // hashed values
      public int salt_length = 32;     // length of salt
      private bool isVerified = false;  // user is legit

        // constructor
      public PasswordHandling()
      {

      }

      // accessor methods

        // returns salt 
      public string getSalt()
      {
          return this.salt;
      }

        // returns hash value
      public string getHashedPassword()
      {
          return this.hashedPassword;
      }

        // returns true if user is legit
      public bool userIsVerified()
      {
          return this.isVerified;
      }

        // methods

        // generates a random string
      public void generateSalt()
      {
          byte[] salt_byte = new byte[salt_length];             // create a byte array with default length

          using (var random = new RNGCryptoServiceProvider())   // fill array with randomly generated bytes
          {
              random.GetNonZeroBytes(salt_byte);
          }

          this.salt = Convert.ToBase64String(salt_byte);        // convert byte array to a string
      }



      public void checkPassword(string password, string hashedPassword, string salt) {

          this.salt = salt;
          this.password = password;
          this.hashedPassword = hashedPassword;
          string combine = password + salt;                          // concatenate salt and plain text password

          combine += Constant.pepper; // add pepper to string
 
          hash(false);                                                // calculate hash value


          if (this.hashedPassword == hashedPassword)                 // check if calculated password is consistent with password from database
          {
              this.isVerified = true;
          }else{
              this.isVerified = false;
          }
      }



    // hash password
    public void hash(bool newSalt) {
        if (newSalt) {
            generateSalt();
        }

        string combined = password + salt + Constant.pepper;

        byte[] spiced = Encoding.ASCII.GetBytes(combined);
        var hashFunc = new SHA256CryptoServiceProvider();
        var hashed = hashFunc.ComputeHash(spiced);
        this.hashedPassword = Convert.ToBase64String(hashed);
    }


    // generate hash with given password
    public void hashPassword(string password)
    {
       generateSalt();
        this.password = password;

        string combined = password + salt + Constant.pepper;

        byte[] spiced = Encoding.ASCII.GetBytes(combined);
        var hashFunc = new SHA256CryptoServiceProvider();
        var hashed = hashFunc.ComputeHash(spiced);
        this.hashedPassword = Convert.ToBase64String(hashed);
    }



      // destructor
      ~PasswordHandling()
      {
          this.password = null;
          this.salt = null;
          hashedPassword = null;
      }
      
    }
}