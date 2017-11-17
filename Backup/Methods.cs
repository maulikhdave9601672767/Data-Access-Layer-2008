using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Configuration;
using System.Web.UI.WebControls;
using System.Web.UI;

using StringHelpers;
using BooleanHelpers;



namespace BusinessRules
{
    public class Methods
    {
        /// <summary>
        /// CorrectDBNull - Convert null value to string
        /// </summary>
        /// <param name="oValue">Object</param>
        /// <returns>String</returns>
        public static string CorrectDBNull(object oValue)
        {
            if (Convert.IsDBNull(oValue))
            {
                return "";
            }
            else
            {
                return oValue.ToString()  ;



                

                 
            }
        }

        

        /// <summary>
        /// SendMail - send mail by using input given
        /// </summary>
        /// <param name="fromAddress">string</param>
        /// <param name="toAddress">string</param>
        /// <param name="sSubject">string</param>
        /// <param name="sMessage">string</param>
        /// <param name="bHTML">bool</param>
        /// <returns>bool</returns>
        public static bool SendMail(string fromAddress, string toAddress, string sSubject, string sMessage, bool bHTML)
        {
            MailMessage objMessage = new MailMessage();
            SmtpClient SmtpMail = new SmtpClient();
            try
            {
                
                objMessage.From = new MailAddress(fromAddress);
                objMessage.To.Add(new MailAddress(toAddress));

                objMessage.Subject = sSubject;
                objMessage.Body = sMessage;
                if (bHTML == true)
                {
                    objMessage.IsBodyHtml = true;
                }
                else
                {
                    objMessage.IsBodyHtml = false;
                }


                SmtpMail.Host = ((System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig("appSettings"))["SmtpServer"].ToString();
                SmtpMail.Send(objMessage);
            }
            catch
            {
                return false;
            }
            return true;
        }


        public static string doInsertable(string sValue)
        {
            string str = "";
            if (Convert.IsDBNull(sValue))
            {
                return str;
            }

            if (sValue != null)
            {
                str = sValue.Replace("'", "''").Trim() ;
            }

            return str;
        }



        /// <summary>
        /// doInt -  Replace NULL to Integer value
        /// </summary>
        /// <param name="oValue">Object</param>
        /// <returns>Integer</returns>
        public static int doInt(object oValue)
        {
            int functionReturnValue = 0;
            if ((oValue != null) && oValue != DBNull.Value)
            {
                if (oValue.ToString() != "")
                {
                    if (IsNumeric(oValue.ToString()))
                    {
                        functionReturnValue = Convert.ToInt32(oValue);
                    }
                    else
                    {
                        functionReturnValue = 0;
                    }
                }
                else
                {
                    functionReturnValue = 0;
                }
            }
            return functionReturnValue;
        }

        /// <summary>
        /// doLong -  Replace NULL to Long value
        /// </summary>
        /// <param name="oValue">Object</param>
        /// <returns>Long</returns>
        public static long doLong(object oValue)
        {
            long functionReturnValue = 0;
            if ((oValue != null) && oValue != DBNull.Value)
            {
                if (oValue.ToString() != "")
                {
                    if (IsNumeric(oValue.ToString()))
                    {
                        functionReturnValue = Convert.ToInt64(oValue);
                    }
                    else
                    {
                        functionReturnValue = 0;
                    }
                }
                else
                {
                    functionReturnValue = 0;
                    
                }
            }
            return functionReturnValue;
        }

        /// <summary>
        /// IsNumeric - check whether the value is numeric or not
        /// </summary>
        /// <param name="theValue">string</param>
        /// <returns>bool</returns>
        public static bool IsNumeric(string theValue)
        {
            Regex _isNumber = new Regex(@"^\d+$");
            Match m = _isNumber.Match(theValue);
            return m.Success;
        }

        /// <summary>
        /// IsEmail -  check whther the value passed is email or not
        /// </summary>
        /// <param name="theValue">string</param>
        /// <returns>bool</returns>
        public static bool IsEmail(string theValue)
        {
            Regex _IsEmail = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            Match m = _IsEmail.Match(theValue);
            return m.Success;
        }

        /// <summary>
        /// IsDouble -  check whther the value passed is double or not
        /// </summary>
        /// <param name="theValue">string</param>
        /// <returns>bool</returns>
        public static bool IsDouble(string theValue)
        {
            Regex _IsDouble = new Regex(@"\d[\d\,\.]*");
            Match m = _IsDouble.Match(theValue);
            return m.Success;
        }

        /// <summary>
        /// IsPhone -  check whther the value passed is email or not
        /// </summary>
        /// <param name="theValue">string</param>
        /// <returns>bool</returns>
        public static bool IsPhone(string theValue)
        {
            Regex _IsPhone = new Regex(@"^((\(?0\d{4}\)?\s?\d{3}\s?\d{3})|(\(?0\d{3}\)?\s?\d{3}\s?\d{4})|(\(?0\d{2}\)?\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$");
            Match m = _IsPhone.Match(theValue);
            return m.Success;
        }

        /// <summary>
        /// doBoolean - Replace NULL to boolean value
        /// </summary>
        /// <param name="oValue">Object</param>
        /// <returns>boolean</returns>
        public static bool doBoolean(object oValue)
        {
            bool functionReturnValue = false;
            if ((oValue != null) && (oValue != DBNull.Value))
            {
                if ((oValue.ToString() != "") && (Convert.ToBoolean(oValue) == true))
                {
                    functionReturnValue = true;
                }
                else
                {
                    functionReturnValue = false;
                }
            }
            return functionReturnValue;
        }

        /// <summary>
        /// doDouble - Replace NULL to Double value
        /// </summary>
        /// <param name="oValue">Object</param>
        /// <returns>Double</returns>
        public static double doDouble(object oValue)
        {
            double functionReturnValue = 0;
            if ((oValue != null) && (oValue != DBNull.Value))
            {
                if (oValue.ToString() != "")
                {
                    if (IsDouble(oValue.ToString()))
                    {
                        functionReturnValue = Convert.ToDouble(oValue);
                    }
                    else
                    {
                        functionReturnValue = 0;
                    }
                }
                else
                {
                    functionReturnValue = 0;
                }
            }
            return functionReturnValue;
        }


        /// <summary>
        /// DayCount - number of days between two days
        /// </summary>
        /// <param name="dFirstdate">DateTime</param>
        /// <param name="dLastDate">DateTime</param>
        /// <returns>Integer</returns>
        public static int DayCount(DateTime dFirstdate, DateTime dLastDate)
        {
            int nDays;
            TimeSpan ts = new TimeSpan();
            ts = dFirstdate - dLastDate;
            nDays = ts.Days;
            return nDays;
        }

        /// <summary>
        /// IsMobileNo - It checks whther the given input string is valid Mobile number or not
        /// </summary>
        /// <param name="theValue">string</param>
        /// <returns>bool</returns>
        public static bool IsMobileNo(string theValue)
        {
            Regex _IsMobileNo = new Regex(@"^((\(?0\d{4}\)?\s?\d{3}\s?\d{3})|(\(?0\d{3}\)?\s?\d{3}\s?\d{4})|(\(?0\d{2}\)?\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$");
            Match m = _IsMobileNo.Match(theValue);
            return m.Success;
        }

        /// <summary>
        /// IsPostalCode - It checks whther the given input string is valid postal code or not
        /// </summary>
        /// <param name="theValue">string</param>
        /// <returns>bool</returns>
        public static bool IsPostalCode(string theValue)
        {
            Regex _IsPostalCode = new Regex(@"^([A-PR-UWYZ0-9][A-HK-Y0-9][AEHMNPRTVXY0-9]?[ABEHMNPRVWXY0-9]? {1,2}[0-9][ABD-HJLN-UW-Z]{2}|GIR 0AA)$");
            Match m = _IsPostalCode.Match(theValue);
            return m.Success;
        }

        //============================================================================ 
        //Method Name : GetFileExt 
        //Description : get ext of file 
        //Parameter : 1. sFileName (String) 
        //Return : String 
        //============================================================================ 
        public static string GetFileExt(string sFileName)
        {
            return System.IO.Path.GetExtension(sFileName);
        }

        //============================================================================
        //Method Name    :   Base64Encode
        //Description    :   Encode string value
        //Parameter      :   1.  sValue (String)
        //Return         :   String
        //============================================================================


        public static string Base64Encode(string sValue)
        {
            try
            {
                byte[] strBytes = System.Text.Encoding.UTF8.GetBytes(sValue);
                return System.Convert.ToBase64String(strBytes);
            }
            catch (Exception ex)
            {
                return sValue;
            }
        }



        //============================================================================
        //Method Name    :   Base64Decode
        //Description    :   Decode string value
        //Parameter      :   1.  sValue (String)
        //Return         :   String
        //============================================================================



        public static string Base64Decode(string sValue)
        {
            try
            {
                byte[] strBytes = System.Convert.FromBase64String(sValue);
                return System.Text.Encoding.UTF8.GetString(strBytes);
            }
            catch (Exception ex)
            {
                return sValue;
            }
        }

   



        public static Bitmap GetThumbnail(ref System.Drawing.Bitmap source, int maxWidth, int maxHeight)
        {
            //Dim iHeight, iWidth As Integer
            //iHeight = maxHeight
            //iWidth = maxWidth
            //Dim dest As Bitmap = New Bitmap(source, iWidth, iHeight)
            //Return dest
            int iHeight = 0;
            int iWidth = 0;
            iHeight = source.Height;
            iWidth = source.Width;

            if (source.Width > maxWidth)
            {
                if (source.Width > source.Height)
                {
                    iWidth = maxWidth;
                    iHeight = source.Height * maxWidth / source.Width;
                }
                else
                {
                    iHeight = maxHeight;
                    iWidth = source.Width * maxHeight / source.Height;
                }
            }
            else if (source.Height > maxHeight)
            {
                if (source.Width > source.Height)
                {
                    iWidth = maxWidth;
                    iHeight = source.Height * maxWidth / source.Width;
                }
                else
                {
                    iHeight = maxHeight;
                    iWidth = source.Width * maxHeight / source.Height;
                }
            }
            Bitmap dest = new Bitmap(source, iWidth, iHeight);
            return dest;
        }



        //============================================================================
        //Method Name : FillYearDropDown
        //Description : Fill year data
        //Parameter : 1. DropDown (DropDownList)
        //Return : ---
        //============================================================================
        public static void FillYearDropDown(ref DropDownList DropDown)
        {
            try
            {
                for (int intI = 1900; intI <= System.DateTime.Now.Year; intI++)
                {
                    ListItem lstItem = new ListItem(intI.ToString (), intI.ToString ());
                    DropDown.Items.Add(lstItem);
                }
                DropDown.Items.Insert(0, new ListItem("Year", "0"));
            }
            catch (Exception ex)
            {
            }

        }
        //============================================================================
        //Method Name : FillMonthDropDown
        //Description : Fill month data
        //Parameter : 1. DropDown (DropDownList)
        //Return : ---
        //============================================================================
        public static void FillMonthDropDown(ref DropDownList DropDown)
        {
            try
            {
                string[] sArrMonth = { "Jan", "Fab", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct",
             "Nov", "Dec" };
                int intI = 1;
                foreach (string strI in sArrMonth)
                {
                    ListItem lstItem = new ListItem(strI.ToString (), intI.ToString());
                    DropDown.Items.Add(lstItem);
                    intI += 1;
                }
                DropDown.Items.Insert(0, new ListItem("Month", "0"));
            }
            catch (Exception ex)
            {
            }

        }
        //============================================================================
        //Method Name : FillDayDropDown
        //Description : Fill day data
        //Parameter : 1. DropDown (DropDownList)
        //Return : ---
        //============================================================================
        public static void FillDayDropDown(ref DropDownList DropDown)
        {
            try
            {
                int intI;
                for (intI = 1; intI <= 31; intI++)
                {
                    ListItem lstItem = new ListItem(intI.ToString (), intI.ToString ());
                    DropDown.Items.Add(lstItem);
                }
                DropDown.Items.Insert(0, new ListItem("Day","1" ));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public static void disableEnterKey_TextBox(TextBox textControl)
        {
            textControl.Attributes.Add("onKeyPress", "return disableEnterKey(event)");
        }

        public static void disableEnterKey_Dropdown(DropDownList drpControl)
        {
            drpControl.Attributes.Add("onKeyPress", "return disableEnterKey(event)");
        }

        public static void SetDefaultButton(Page page, TextBox textControl, Button defaultButton)
        {
            // Sets default buttons.
            string theScript = "<SCRIPT language=\"javascript\">" + "function fnTrapKD(btn, event){ if (document.all){ if (event.keyCode == 13){event.returnValue=false; event.cancel = true; btn.click();" + "}} else if (document.getElementById){ if (event.which == 13){event.returnValue=false; event.cancel = true; btn.click();}}" + "else if(document.layers){if(event.which == 13){event.returnValue=false;event.cancel = true;btn.click();}}}</SCRIPT>";

            page.ClientScript.RegisterStartupScript(textControl.GetType(), "ForceDefaultToScript", theScript);
            textControl.Attributes.Add("onkeydown", "fnTrapKD(" + defaultButton.ClientID + ", event)");

        }



        public static DropDownList DrpSelect_N(int SelVal, DropDownList drpObj)
        {

            drpObj.ClearSelection();

            int i = 0;

            for (i = 0; i <= drpObj.Items.Count - 1; i++)
            {
                if (Methods.doInt(drpObj.Items[i].Value) == SelVal)
                {
                    drpObj.SelectedIndex = i;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            return drpObj;

        }

        public static DropDownList DrpSelect_S(string SelVal, DropDownList drpObj)
        {

            drpObj.ClearSelection();

            int i = 0;

            for (i = 0; i <= drpObj.Items.Count - 1; i++)
            {
                if (Methods.CorrectDBNull(drpObj.Items[i].Value) == SelVal)
                {
                    drpObj.SelectedIndex = i;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            return drpObj;

        }

        public static string MakeURLClickable(string sURL)
        {

            string sFullURL = "";
            if (!string.IsNullOrEmpty(sURL.Trim()))
            {
                sURL = sURL.Replace("http://", "");

                //' remove all extra '/'
                int slashIndex = 0;
                if (!string.IsNullOrEmpty(sURL.Trim()))
                {
                    while (sURL.Length - 1 == sURL.LastIndexOf("/"))
                    {
                        slashIndex = sURL.LastIndexOf("/");
                        sURL = sURL.Substring(0, slashIndex);
                    }
                }
                //'
                sFullURL = "http://" + sURL;
            }

            return sFullURL;

        }



        public static bool ValidateEmail(string inputEmail, char Type)
        {

            string strRegex = null;

            //strRegex = "^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" & "\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" & ".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"

            if (Type == 'B')
            {
                strRegex = "<(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))>";
            }
            else
            {
                strRegex = "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$";
            }

            if (Regex.IsMatch(inputEmail, strRegex))
            {
                return (true);
            }
            else
            {
                return (false);
            }

        }

    }

}
