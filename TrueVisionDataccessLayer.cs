using Microsoft.VisualBasic;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections.Generic;
using System.Collections;

using System.Web;
using System.Configuration;
using System.Web.UI;
using System.Data.SqlClient;
using System;






public enum g_enmErrorTypes
{

    etScriptError = 0,
    //'for scripting errors like type mismatch, overflow, instance not created etc. 

    etDBError = 1,
    //'for database errors 

    erHTTPError = 2
}
//'for http errors 








public class TrueVisionDataAccessLayer_V_0_0_0_1 : System.Web.UI.Page, ITrueVisionDataAccessLayer
{

    protected string mstrConnectString;
    private SqlConnection mobjCon;
    private SqlTransaction mobjTrans;
    public daTransactionState TransState;
    // private const char mstrSep = (char)",";
    private const string cstrParaPrefix = "";

    private SqlError Err;







    public enum daTransactionState
    {
        /// Enumarator for DataAccess transaction ''' Enumarator for DataAccess transaction ''' Enumarator for DataAccess transaction 
        daCloseTrans = 0,

        daOpenTrans = 1
    }



    public TrueVisionDataAccessLayer_V_0_0_0_1()
    {

    }
    public TrueVisionDataAccessLayer_V_0_0_0_1(string connectionstring)
    {
        if (mobjCon == null)
        {

            /// set connection property 

            mobjCon = new SqlConnection(connectionstring);
        }

    }




    public string ConnectionString
    {
        //Initalise COnnection String 

        get { ConnectionString = mstrConnectString; return mstrConnectString; }


        set
        {

            mstrConnectString = value;

            /// check connection object is created or not 

            if (mobjCon == null)
            {

                /// set connection property 

                mobjCon = new SqlConnection(value);
            }

        }

    }

    private bool InitValue()
    {

       

        bool returnValue = false;

        try
        {
            

            this.mobjCon = null;

            this.mobjTrans = null;

            this.TransState = daTransactionState.daCloseTrans;
            returnValue = true;
        }

        ///Maulik 
        catch (SqlException exp)
        {

            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {
        }

        //Comment By Maulik Throw New SQLException(Err.Number, GetErrorDescription(Err)) 

        return returnValue;
    }
    private bool OpenConnection()
    {
        Boolean returnValue = false;
        //open the Conection 
        try
        {
            IsolationLevel @lock = new IsolationLevel();
            //lock.ReadCommitted 

         
            if (mobjCon.State == ConnectionState.Closed)
            {

                mobjCon.Open();
                returnValue = true;
            }
            //mobjCon.BeginTransaction() ;
        }

        catch (SqlException exp)
        {

            throw new SQLServerException(exp.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {

        }
        return returnValue;
        //' Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 


    }
    private bool CloseConnection()
    {
        //Close Connection 
        Boolean returnValue = false;
        try
        {

            if (this.TransState == daTransactionState.daCloseTrans)
            {

                if (mobjCon.State == ConnectionState.Open)
                {

                    mobjCon.Close();
                    returnValue = true;
                }

            }

        }

        ///Maulik 
        catch (SqlException exp)
        {

            throw new SQLServerException(exp.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {
        }

        //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        return returnValue;
    }





    #region ITrueVisionDataAccessLayer Members

    SqlDataReader ITrueVisionDataAccessLayer.GetSQLDataReader(string SQLQuery)
    {
        //In This Funcion Query Will Come As Argument And It execute It And Return SQLDataReader 
        SqlCommand objCmd = default(SqlCommand);

        SqlDataReader objDr = default(SqlDataReader);

        try
        {

            /// execute sql 

            objCmd = new SqlCommand(SQLQuery, mobjCon);
             

            /// open connection 

            /// objCmd.Connection.Open() 

            OpenConnection();
            objCmd.Transaction = mobjTrans;
            /// assign transaction 

            //'Comment By Maulik objCmd.Transaction = Me.mobjTrans 

            objDr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        catch (SqlException exp)
        {
            //if Any Error Occure It Will Throw 
            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }


        catch (Exception exp)
        {
            RollbackTrans();
            throw new Exception(exp.Message.ToString());
        }

        //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        finally
        {
            CommitTrans();
            objCmd = null;

        }
        /// return 

        return objDr;
    }

    SqlDataReader ITrueVisionDataAccessLayer.GetSQLDataReader(string astrProcedure, List<SqlParameter> SQLDBParameter)
    {
        SqlCommand objCmd = new SqlCommand();
        Int16 i = default(Int16);

        //here sp name and return parameter will come as argument and it type must be ref cursor 

        try
        {

            /// execute store proc 

            objCmd = new SqlCommand(astrProcedure, mobjCon);

            objCmd.CommandType = CommandType.StoredProcedure;

            /// open connection 

            /// objCmd.Connection.Open() 

            OpenConnection();
            objCmd.Transaction = mobjTrans;
            /// assign transaction 

            //'Comment By Maulik  
            objCmd.Parameters.AddRange(SQLDBParameter.ToArray()); 
            //for (i = 0; i < SQLDBParameter.Count; i++)
            //{
            //    objCmd.Parameters.Add((SqlParameter)SQLDBParameter[i]);
            //}



        }
            

        ///changed by mAULIK for custom error handling 
        catch (SqlException exp)
        {

            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {
            RollbackTrans();
            throw new Exception(exp.Message.ToString());
        }

       // Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        finally
        {
            CommitTrans();
            
            //objCmd = null;
        }

        SqlDataReader dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
        

        return dr;
    }

    bool ITrueVisionDataAccessLayer.ExecuteSQL(string SQLQuery)
    {
        bool functionReturnValue = false;
        //'simple execute query with return boolean value 
        SqlCommand objCmd = default(SqlCommand);

        try
        {

            functionReturnValue = true;

            /// execute sql 

            objCmd = new SqlCommand(SQLQuery, mobjCon);

            /// try to open connection 

            /// objCmd.Connection.Open() 

            OpenConnection();
            objCmd.Transaction = mobjTrans;
            /// assign transaction 

            //'Comment By Maulik objCmd.Transaction = Me.mobjTrans 

            objCmd.ExecuteNonQuery();

            /// close connection 

            /// If objCmd.Connection.State = ConnectionState.Open Then objCmd.Connection.Close() 

            CloseConnection();
        }

        ///changed by mAULIK for custom error handling 
        catch (SqlException exp)
        {
            RollbackTrans();
            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {
            RollbackTrans();
            throw new Exception(exp.Message.ToString());
        }

        //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        finally
        {
            CommitTrans();
            objCmd = null;
        }

        return functionReturnValue;
    }

    bool ITrueVisionDataAccessLayer.ExecuteSQL(string astrProcedure, List<SqlParameter> SQLDBParameter)
    {
        bool functionReturnValue = false;
        //simple execute Sp with return boolean value 
        Int16 i = default(Int16);
        SqlCommand objCmd = default(SqlCommand);

        try
        {

            functionReturnValue = true;

            /// execute store proc 

            objCmd = new SqlCommand(astrProcedure, mobjCon);

            objCmd.CommandType = CommandType.StoredProcedure;

            /// create storeproc parameter 
            objCmd.Parameters.AddRange(SQLDBParameter.ToArray()); 
            //for (i = 0; i < SQLDBParameter.Count; i++)
            //{
            //    objCmd.Parameters.Add((SqlParameter)SQLDBParameter[i]);
            //}

            //With objCmd.Parameters 

            // If Not astrParaName.StartsWith(cstrParaPrefix) Then astrParaName = cstrParaPrefix & astrParaName 

            // .Add(astrParaName, CType(astrParaType, SQLDbType)).Value = IIf(astrParaValue.Trim.ToLower = "null", System.DBNull.Value, astrParaValue) 

            //End With 

            /// try to openconnection 

            /// objCmd.Connection.Open() 

            OpenConnection();
            objCmd.Transaction = mobjTrans;
            /// assign transaction 

            //'Comment By Maulik objCmd.Transaction = Me.mobjTrans 

            objCmd.ExecuteNonQuery();
        }

        ///changed by mAULIK for custom error handling 
        catch (SqlException exp)
        {
            RollbackTrans();
            throw new Exception(exp.Message.ToString());
            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {
            RollbackTrans();
            throw new Exception(exp.Message.ToString());
        }

        //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        finally
        {
            CommitTrans();
            objCmd = null;
        }


        /// close connection 

        /// If objCmd.Connection.State = ConnectionState.Open Then objCmd.Connection.Close() 

        CloseConnection();
        return functionReturnValue;
    }

    DataSet ITrueVisionDataAccessLayer.GetSQLDataSet(string SQLQuery, string astrTable)
    {
        //'simple execute Query And Return table 
        SqlDataAdapter objDA = default(SqlDataAdapter);
        DataSet objDS = default(DataSet);

        try
        {

            objDS = new DataSet();

            objDA = new SqlDataAdapter(SQLQuery, mobjCon);

            objDA.Fill(objDS, astrTable);
        }

        ///changed by mAULIK for custom error handling 
        catch (SqlException exp)
        {
            RollbackTrans();
            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {
            RollbackTrans();
            throw new Exception(exp.Message.ToString());
        }
        finally
        {
            CommitTrans();
        }

        //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 


        return objDS;

    }

    DataSet ITrueVisionDataAccessLayer.GetSQLDataSet(string astrProcedure, List<SqlParameter> SQLDBParameter)
    {
        //execute SP With Refrence Cursor 

        SqlCommand objCmd = default(SqlCommand);

        SqlDataAdapter objDA = default(SqlDataAdapter);

        DataSet objDS = default(DataSet);

        try
        {

            objDS = new DataSet();

            /// execute store proc 

            objCmd = new SqlCommand(astrProcedure, mobjCon);

            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.Parameters.AddRange(SQLDBParameter.ToArray()); 
            //for (int I = 0; I < SQLDBParameter.Count; I++)
            //{

            //    objCmd.Parameters.Add((SqlParameter)SQLDBParameter[I]);
            //}

            objDA = new SqlDataAdapter(objCmd);

            /// fill dataset 

            objDA.Fill(objDS);


        }
        ///changed by mAULIK for custom error handling 
        catch (SqlException exp)
        {
            RollbackTrans();
            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {
            RollbackTrans();
            throw new Exception(exp.Message.ToString());
        }

        //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        finally
        {

            objCmd = null;
            CommitTrans();
        }


        return objDS;

    }

    Hashtable  ITrueVisionDataAccessLayer.GetSQLValue(string SQLQuery)
    {
        //execute Query Retutn Object 
        SqlCommand objCmd = default(SqlCommand);
        Hashtable objVal = null;

        try
        {

            /// execute sql 

            objCmd = new SqlCommand(SQLQuery, mobjCon);

            /// try to open connection 

            /// objCmd.Connection.Open() 

            OpenConnection();

            /// assign transaction 
            objCmd.Transaction = mobjTrans;
            //'Comment By Maulik objCmd.Transaction = Me.mobjTrans 

            objVal.Add("Va;ue", objCmd.ExecuteScalar()); ;

            /// close connection 

            /// If objCmd.Connection.State = ConnectionState.Open Then objCmd.Connection.Close() 

            CloseConnection();
        }

        ///changed by mAULIK for custom error handling 
        catch (SqlException exp)
        {
            RollbackTrans();
            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {
            RollbackTrans();
            throw new Exception(exp.Message.ToString());
        }

        //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        finally
        {
            CommitTrans();
            objCmd = null;
        }


        if (objVal.Count  == 0) objVal.Clear();

        /// return object 

        return objVal;
    }

     Hashtable  ITrueVisionDataAccessLayer.GetSQLValue(string astrProcedure, List<SqlParameter> SQLDBParameter)
    {
        //execute sp With Single Input Parameter Retutn Object 
        SqlCommand objCmd = default(SqlCommand);
        SqlParameter objMyPara = default(SqlParameter);
        Int16 i = default(Int16);

        object objVal = null;
        Hashtable ReturnCollection = new Hashtable();


        try
        {

            /// execute store proc 

            objCmd = new SqlCommand(astrProcedure, mobjCon);

            objCmd.CommandType = CommandType.StoredProcedure;

            /// create storeproc parameter 

            /// add ret parameter , depending upon the size para 

            //If Not astrRetParaName.StartsWith(cstrParaPrefix) Then astrRetParaName = cstrParaPrefix & astrRetParaName 

            //If aintRetParaSize > 0 Then 

            // objMyPara = objCmd.Parameters.Add(astrRetParaName, CType(astrRetParaType, SQLDbType), aintRetParaSize) 

            //Else 
            objCmd.Parameters.AddRange(SQLDBParameter.ToArray());

            ////for (i = 0; i < SQLDBParameter.Count; i++)
            ////{

            ////    objCmd.Parameters.Add((SqlParameter)SQLDBParameter[i]);
            ////}


            //End If 

            //objMyPara.Direction = ParameterDirection.Output 

            /// execute store proc 

            /// objCmd.Connection.Open() 

            OpenConnection();

            /// assign transaction 
            objCmd.Transaction = mobjTrans;
            //'Comment By Maulik objCmd.Transaction = Me.mobjTrans 

            objCmd.ExecuteNonQuery();

            ///// ReturnCollection = new List<object>();
            ////for (i = 0; i < SQLDBParameter.Count; i++)
            ////{
            ////    if (((SqlParameter)SQLDBParameter[i]).Direction == ParameterDirection.Output | ((SqlParameter)SQLDBParameter[i]).Direction == ParameterDirection.InputOutput)
            ////    {
            ////        //ReturnCollection.Add(((SqlParameter)SQLDBParameter[i]).Value, ((SqlParameter)SQLDBParameter[i]).ParameterName);
            ////        ReturnCollection.Add(((SqlParameter)SQLDBParameter[i]).ParameterName, ((SqlParameter)SQLDBParameter[i]).Value);
            ////    }

            ////}

            /// close connection 

            /// If objCmd.Connection.State = ConnectionState.Open Then objCmd.Connection.Close() 

            CloseConnection();
        }

        ///changed by mAULIK for custom error handling 
        catch (SqlException exp)
        {
            RollbackTrans();
            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {
            RollbackTrans();
            throw new Exception(exp.Message.ToString());
        }

        //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        finally
        {
            CommitTrans();
            objCmd = null;
        }


        /// return object 

        if (objVal == System.DBNull.Value) objVal = "";

        return ReturnCollection;
       
    }





     Object  ITrueVisionDataAccessLayer.GetSingleValue (string SQLQuery)
     {
         //execute Query Retutn Object 
         SqlCommand objCmd = default(SqlCommand);
         Hashtable objVal = null;

         try
         {

             /// execute sql 

             objCmd = new SqlCommand(SQLQuery, mobjCon);

             /// try to open connection 

             /// objCmd.Connection.Open() 

             OpenConnection();

             /// assign transaction 
             objCmd.Transaction = mobjTrans;
             //'Comment By Maulik objCmd.Transaction = Me.mobjTrans 

             objVal.Add("Value", objCmd.ExecuteScalar()); ;

             /// close connection 

             /// If objCmd.Connection.State = ConnectionState.Open Then objCmd.Connection.Close() 

             CloseConnection();
         }

         ///changed by mAULIK for custom error handling 
         catch (SqlException exp)
         {
             RollbackTrans();
             throw new SQLServerException(Err.Number, GetErrorDescription(exp));
         }

         catch (Exception exp)
         {
             RollbackTrans();
             throw new Exception(exp.Message.ToString());
         }

         //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

         finally
         {
             CommitTrans();
             objCmd = null;
         }


         if (objVal.Count == 0) objVal.Clear();

         /// return object 

         return objVal;
     }

     Object ITrueVisionDataAccessLayer.GetSingleValue(string astrProcedure, List<SqlParameter> SQLDBParameter)
     {
         //execute sp With Single Input Parameter Retutn Object 
         SqlCommand objCmd = default(SqlCommand);
         SqlParameter objMyPara = default(SqlParameter);
         Int16 i = default(Int16);

         object objVal = null;
         Hashtable ReturnCollection = new Hashtable();


         try
         {

             /// execute store proc 

             objCmd = new SqlCommand(astrProcedure, mobjCon);

             objCmd.CommandType = CommandType.StoredProcedure;

             /// create storeproc parameter 

             /// add ret parameter , depending upon the size para 

             //If Not astrRetParaName.StartsWith(cstrParaPrefix) Then astrRetParaName = cstrParaPrefix & astrRetParaName 

             //If aintRetParaSize > 0 Then 

             // objMyPara = objCmd.Parameters.Add(astrRetParaName, CType(astrRetParaType, SQLDbType), aintRetParaSize) 

             //Else 
             objCmd.Parameters.AddRange(SQLDBParameter.ToArray ()); 
            


             //End If 

             //objMyPara.Direction = ParameterDirection.Output 

             /// execute store proc 

             /// objCmd.Connection.Open() 

             OpenConnection();

             /// assign transaction 
             objCmd.Transaction = mobjTrans;
             //'Comment By Maulik objCmd.Transaction = Me.mobjTrans 

           return   objCmd.ExecuteScalar();

             /////// ReturnCollection = new List<object>();
             //for (i = 0; i < SQLDBParameter.Count; i++)
             //{
             //    if (((SqlParameter)SQLDBParameter[i]).Direction == ParameterDirection.Output | ((SqlParameter)SQLDBParameter[i]).Direction == ParameterDirection.InputOutput)
             //    {
             //        //ReturnCollection.Add(((SqlParameter)SQLDBParameter[i]).Value, ((SqlParameter)SQLDBParameter[i]).ParameterName);
             //        ReturnCollection.Add(((SqlParameter)SQLDBParameter[i]).ParameterName, ((SqlParameter)SQLDBParameter[i]).Value);
             //    }

             //}

             /// close connection 

             /// If objCmd.Connection.State = ConnectionState.Open Then objCmd.Connection.Close() 

             CloseConnection();
         }

         ///changed by mAULIK for custom error handling 
         catch (SqlException exp)
         {
             RollbackTrans();
             throw new SQLServerException(Err.Number, GetErrorDescription(exp));
         }

         catch (Exception exp)
         {
             RollbackTrans();
             throw new Exception(exp.Message.ToString());
         }

         //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

         finally
         {
             CommitTrans();
             objCmd = null;
         }


         /// return object 

         if (objVal == System.DBNull.Value) objVal = "";

         return ReturnCollection;

     }



    #endregion













  

    public virtual bool BeginTrans()
    {

        try
        {

            /// first open the connection 

            OpenConnection();

            /// store transaction object 

            this.mobjTrans = mobjCon.BeginTransaction();

            this.TransState = daTransactionState.daOpenTrans;

            return true;
        }

        ///changed by mAULIK for custom error handling 
        catch (SqlException exp)
        {

            return false;

            throw new SQLServerException(Err.Number, GetErrorDescription(exp));
        }

        catch (Exception exp)
        {

            return false;
        }

        //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 


    }
    public virtual bool CommitTrans()
    {

        if (this.TransState == daTransactionState.daOpenTrans)
        {


            try
            {

                mobjTrans.Commit();

                mobjTrans = null;

                TransState = daTransactionState.daCloseTrans;

                /// close connection 

                CloseConnection();

                return true;
            }

            ///changed by mAULIK for custom error handling 
            catch (SqlException exp)
            {

                return false;

                throw new SQLServerException(Err.Number, GetErrorDescription(exp));
            }

            catch (Exception exp)
            {

                return false;
            }

            //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        }

        else
        {

            return false;
        }


    }
    public virtual bool RollbackTrans()
    {

        if (this.TransState == daTransactionState.daOpenTrans)
        {

            try
            {

                mobjTrans.Rollback();

                mobjTrans = null;

                TransState = daTransactionState.daCloseTrans;

                /// close connection 

                CloseConnection();

                return true;
            }

            ///changed by mAULIK for custom error handling 
            catch (SqlException exp)
            {

                return false;

                throw new SQLServerException(Err.Number, GetErrorDescription(exp));
            }

            catch (Exception exp)
            {

                return false;
            }

            //Throw New SqlServerException(Err.Number, GetErrorDescription(Err)) 

        }

        else
        {

            return false;
        }


    }



    public string GetErrorDescription(SqlException aobjExpOledb)
    {
        //return "";
        return aobjExpOledb.Message.ToString();
        //return GetErrorMsg(aobjExpOledb.Number, aobjExpOledb.Message, g_enmErrorTypes.etDBError);

    }
    private string GetErrorMsg(string astrErrorCode, string astrMessage, g_enmErrorTypes aertErrorType)
    {

        ///xml file name of error log 

        ///it will be created in web server's system32 directory 

        //Const cstrXmlLogFile As String = "AidaErrorLog.xml" 

        //Try 

        // Dim dtmErrorDateTime As DateTime = Now ''error occurr date time 

        // ''if file does not exists then create xml file 

        // If Not File.Exists(cstrXmlLogFile) Then 

        // Dim objStreamWriter As StreamWriter 

        // ''create xml file's basic template 

        // objStreamWriter = File.CreateText(cstrXmlLogFile) 

        // objStreamWriter.WriteLine("<?xml version=""1.0""?>") 

        // objStreamWriter.WriteLine("<AidaErrorLog>") 

        // objStreamWriter.WriteLine("</AidaErrorLog>") 

        // objStreamWriter.Flush() 

        // objStreamWriter.Close() 

        // objStreamWriter = Nothing 

        // End If 



        // Dim xdocAidaErrorLog As New XmlDocument() 

        // xdocAidaErrorLog.Load(cstrXmlLogFile) ''load xml file in memory 

        // ''create new error element to insert 

        // Dim xEleError As XmlElement = xdocAidaErrorLog.CreateElement("Error") 

        // ''adding different attributes of error in element object 

        // ''------------------------------------------------------------------------- 

        // ''error type attribute 

        // Dim xattErrorType As XmlAttribute = xdocAidaErrorLog.CreateAttribute("Type") 

        // If aertErrorType = g_enmErrorTypes.etScriptError Then 

        // xattErrorType.Value = "Script Error" 

        // ElseIf aertErrorType = g_enmErrorTypes.etDBError Then 

        // xattErrorType.Value = "Database Error" 

        // ElseIf aertErrorType = g_enmErrorTypes.erHTTPError Then 

        // xattErrorType.Value = "HTTP Error" 

        // End If 

        // xEleError.Attributes.Append(xattErrorType) 

        // ''error code attribute 

        // Dim xattErrorCode As XmlAttribute = xdocAidaErrorLog.CreateAttribute("Code") 

        // xattErrorCode.Value = astrErrorCode 

        // xEleError.Attributes.Append(xattErrorCode) 

        // ''message attribute 

        // Dim xattMessage As XmlAttribute = xdocAidaErrorLog.CreateAttribute("Message") 

        // xattMessage.Value = astrMessage 

        // xEleError.Attributes.Append(xattMessage) 

        // ''------------------------------------------------------------------------- 

        // ''append recently created error element with updated attributes into the 

        // ''loaded xml document 

        // xdocAidaErrorLog.DocumentElement.AppendChild(xEleError) 

        // ''save xml document 

        // xdocAidaErrorLog.Save(cstrXmlLogFile) 



        // ''mail error details to support and/or admin 

        // Dim strBody As String = "" 

        // ''prepare body part of page 

        // ''Dim objUserProp As UserProp 

        // ''objUserProp = CurrentUser(Me) 

        // ''strCompanyID = objUserProp.CompanyID 

        // ''strCompanyName = objUserProp.CompanyName 

        // ''objUserProp = Nothing 

        // strBody += "Dear Admin/Support," & vbCrLf 

        // strBody += " Please take note of following error occurred on " & dtmErrorDateTime 

        // 'strBody += " and faced by our client " & strCustomerName & "(Customer Code : " & strCustomerID & "). " & vbCrLf & vbCrLf 

        // strBody += " and faced by our client CUSTOMERNAMEHARDCODED (Customer Code : CUSTOMERIDHARDCODED). " & vbCrLf & vbCrLf 

        // strBody += "Error Type : " & xattErrorType.Value & vbCrLf 

        // strBody += "Error Code : " & xattErrorCode.Value & vbCrLf 

        // strBody += "Error Message : " & xattMessage.Value & vbCrLf 

        // 'strBody += "Error Source : " & xattSource.Value & vbCrLf 

        // 'strBody += "Error Detail : " & astrErrorDetail & vbCrLf 

        // Dim objNewMail As New MailMessage() 



        // ''prepare mail message 

        // objNewMail.From = "mycustomer@mydomain.com" 'strCustomerEmail 

        // objNewMail.To = "hetal@globaltech.com" 'strErrorLogEmail 

        // objNewMail.BodyFormat = MailFormat.Text 

        // objNewMail.Priority = MailPriority.High 

        // objNewMail.Subject = "ATTN : Error Report -- Date : " & dtmErrorDateTime 

        // objNewMail.Body = strBody 

        // ''set smtp server 

        // SmtpMail.SmtpServer = "netfin" 'strSMTPSever 

        // ''send mail 

        // SmtpMail.Send(objNewMail) 

        // ''TODO : get suitable error description which is to be shown to user 

        // ''destroy objects 

        // objNewMail = Nothing 

        // xdocAidaErrorLog = Nothing 

        // xEleError = Nothing 

        // xattErrorCode = Nothing 

        // xattErrorType = Nothing 

        // xattMessage = Nothing 

        //Catch exc As Exception ''in case of errror throw exception 

        // Throw exc 

        //End Try 

        ///uncomment following line 

        return astrMessage;

    }




}







public class SQLServerException : System.ApplicationException
{



    private int mintNumber;

    private string mstrMessage;

    public SQLServerException()
        : base()
    {


    }


    public SQLServerException(string astrMessage)
        : base(astrMessage)
    {

        this.mstrMessage = astrMessage;

    }

    public SQLServerException(int aintNumber)
        : base()
    {


        this.mintNumber = aintNumber;

    }


    public SQLServerException(int aintNumber, string astrMessage)
        : base(astrMessage)
    {

        this.mintNumber = aintNumber;

        this.mstrMessage = astrMessage;

    }

    /// read only prop for error number 

    public virtual int Number
    {


        get { return mintNumber; }

    }


    public override string Message
    {


        get { return mstrMessage; }

    }
}