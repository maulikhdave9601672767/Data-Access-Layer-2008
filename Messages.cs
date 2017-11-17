using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;



    public class Messages
    {
        private static SortedList AllMessages
        {
            get
            {
                SortedList slMessages = new SortedList();
                slMessages.Add("[Invalid Login]", "Username and/or Password may be wrong");
                slMessages.Add("[Invalid Input]", "Please correct inputs as suggested");
                slMessages.Add("[Succesfull Login]", "Login successfully");
                slMessages.Add("[Inactive]", "User's current status is inactive");
                slMessages.Add("[InvalidUser]", "Please enter correct username");
                slMessages.Add("[InvalidPassword]", "Please enter correct password");
                slMessages.Add("[Password Update]", "Password has been updated successfully");                
                slMessages.Add("[Insert]", "[Entity] inserted successfully");
                slMessages.Add("[NoRecord]", "No records found");
                slMessages.Add("[Varify]", "[Entity] image varification successfully");
                slMessages.Add("[Update]", "[Entity] updated successfully");
                slMessages.Add("[Delete]", "[Entity] deleted successfully");
                slMessages.Add("[Already Exists]", "[Entity] already exists");
                slMessages.Add("[Error]", "Unspecified error occured, Please try again");
                slMessages.Add("[Session Error]", "You need to login before accessing this page");
                slMessages.Add("[Login Error]", "Invalid Username or Password");
                slMessages.Add("[SignUp]", "Your registration is successfully completed.<br>Click on confirmation link in your email to activate your account.");
                slMessages.Add("[Sent]", "[Entity] sent successfully");
                slMessages.Add("[Can't Delete]", "You can't delete [Entity], Subject exists for this user");
                slMessages.Add("[SubjectVarify]", "[Entity] varification successfully");
                slMessages.Add("[ForumMessage]", "This Topic is to discuss [Entity]. Please tell us what you like or dislike, if you disagree with choices or would like to give reasons behind your own choices. This is a discussion area, please remember everyone is entitled to their own opinion. If you want to be more detailed about a subject or item a blogging tool will be coming soon!");
                slMessages.Add("[Reset]", "[Entity] reset successfully");

                return slMessages;
            }
        }
        private static string Message(string sKey)
        {
            
            if ((AllMessages != null))
            {
                if (AllMessages.ContainsKey(sKey))
                {
                    object sk = sKey;
                    int index = AllMessages.IndexOfKey(sk);
                    return AllMessages.GetByIndex(index).ToString();
                }
                else
                {
                    sKey = sKey.Remove(sKey.Length - 1);
                    sKey = sKey.Remove(0, 1);
                    return sKey;
                }
            }
            else
            {
                sKey = sKey.Remove(sKey.Length - 1);
                sKey = sKey.Remove(0, 1);
                return sKey;
            }

        }

        public static string GetMessage(string sMessageType)
        {
            return (string)Message(sMessageType);
        }

        public static string GetMessage(string sMessageType, string sEntityName)
        {
            string sMessage = (string)Message(sMessageType);
            try
            {
                sMessage = sMessage.Replace(sMessage.Substring(sMessage.IndexOf("["), sMessage.IndexOf("]") + 1), sEntityName);
            }
            catch
            {
                return sMessage;
            }
            return sMessage;
        }

        public static object GetMail(string sMessagetype, string sName, string sCompanyname, string sLoginid, string sLink)
        {
            string sMessage = (string)Message(sMessagetype);
            try
            {
                sMessage = sMessage.Replace("[Name]", sName);
                sMessage = sMessage.Replace("[Companyname]", sCompanyname);
                sMessage = sMessage.Replace("[Loginid]", sLoginid);
                sMessage = sMessage.Replace("[Link]", sLink);
            }
            catch 
            {
                return sMessage;
            }
            return sMessage;
        }

    }

