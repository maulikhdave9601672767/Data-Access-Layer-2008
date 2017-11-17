using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Collections;


public class Wraper
{
    ITrueVisionDataAccessLayer ObjDal;
  public Wraper()
    {
        ObjDal = new TrueVisionDataAccessLayer_V_0_0_0_1(((System.Collections.Specialized.NameValueCollection)System.Configuration.ConfigurationSettings.GetConfig("appSettings"))[0].ToString());
    }
  public Wraper(string ConnectionStiring)
    {
        ObjDal = new TrueVisionDataAccessLayer_V_0_0_0_1(ConnectionStiring);
    }

    public SqlDataReader GetSQLDataReader(string SQLQuery)
    {
        return ObjDal.GetSQLDataReader(SQLQuery);
    }
    public SqlDataReader GetSQLDataReader(string astrProcedure, List<SqlParameter>  SQLDBParameter)
    {
        return ObjDal.GetSQLDataReader(astrProcedure, SQLDBParameter);
    }



    public bool ExecuteSQL(string SQLQuery)
    {
        return ObjDal.ExecuteSQL(SQLQuery);
    }
    public bool ExecuteSQL(string astrProcedure, List<SqlParameter>  SQLDBParameter)
    {
        return ObjDal.ExecuteSQL(astrProcedure, SQLDBParameter);
    }


    public DataSet GetSQLDataSet(string SQLQuery, string astrTable)
    {
        return ObjDal.GetSQLDataSet(SQLQuery,astrTable );
    }
    public DataSet GetSQLDataSet(string astrProcedure, List<SqlParameter> SQLDBParameter)
    {
        return ObjDal.GetSQLDataSet(astrProcedure, SQLDBParameter);
    }


    public Hashtable GetSQLValue(string SQLQuery)
    {
        return ObjDal.GetSQLValue(SQLQuery);
    }
    public Hashtable GetSQLValue(string astrProcedure, List<SqlParameter> SQLDBParameter)
    {
        return ObjDal.GetSQLValue(astrProcedure, SQLDBParameter);
    }
}