using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;  
public interface ITrueVisionDataAccessLayer 
{ 
    
    
    SqlDataReader GetSQLDataReader(string SQLQuery);
    SqlDataReader GetSQLDataReader(string astrProcedure, List<SqlParameter> SQLDBParameter); 
    
    
    
    bool ExecuteSQL(string SQLQuery);
    bool ExecuteSQL(string astrProcedure, List<SqlParameter> SQLDBParameter); 
    
    
    DataSet GetSQLDataSet(string SQLQuery,string  astrTable );
    DataSet GetSQLDataSet(string astrProcedure, List<SqlParameter> SQLDBParameter);


    Hashtable GetSQLValue(string SQLQuery);
    Hashtable GetSQLValue(string astrProcedure, List<SqlParameter> SQLDBParameter);


    object  GetSingleValue(string SQLQuery);
    object GetSingleValue(string astrProcedure, List<SqlParameter> SQLDBParameter); 

} 
