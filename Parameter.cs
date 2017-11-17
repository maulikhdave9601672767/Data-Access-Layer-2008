using System.Data.SqlClient;
using System.Data;
public class SQLDBParameter
{
    public enum SqlDatabaseType
    {


        SqlBigInt = SqlDbType.BigInt,
        SqlBinary = SqlDbType.Binary,
        SqlBit = SqlDbType.Bit,
        SQLChar = SqlDbType.Char,
        SQLDate = SqlDbType.DateTime,
        SQLDateTime = SqlDbType.DateTime,


        SQLDecimal = SqlDbType.Decimal,
        SQLFloat = SqlDbType.Float,
        SQLImage = SqlDbType.Image,
        SQLInt = SqlDbType.Int,
        SQLMoney = SqlDbType.Money,
        SQLNChar = SqlDbType.NChar,
        SQLNText = SqlDbType.NText,
        SQLNVarChar = SqlDbType.NVarChar,
        SQLReal = SqlDbType.Real,
        SQLSmallDateTime = SqlDbType.SmallDateTime,
        SQLSmallInt = SqlDbType.SmallInt,
        SQLSmallMoney = SqlDbType.SmallMoney,

        SQLText = SqlDbType.Text,

        SQLTimestamp = SqlDbType.Timestamp,
        SQLTinyInt = SqlDbType.TinyInt,
        SQLUdt = SqlDbType.Udt,
        SQLUniqueIdentifier = SqlDbType.UniqueIdentifier,
        SQLVarBinary = SqlDbType.VarBinary,
        SQLVarChar = SqlDbType.VarChar,
        SQLVariant = SqlDbType.Variant,
        SQLXml = SqlDbType.Xml
    }











    public static SqlParameter CreateParameter(string ParameterName, SqlDbType DBType)
    {
        SqlParameter objParameter = new SqlParameter();
        objParameter.ParameterName = ParameterName;

        objParameter.SqlDbType = DBType;

        objParameter.Direction = ParameterDirection.Output;


        return objParameter;

    }



    public static SqlParameter CreateParameter(string ParameterName, SqlDbType DBType, object Value)//, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(0)] // ERROR: Optional parameters aren't supported in C# int Size, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(ParameterDirection.Input)] // ERROR: Optional parameters aren't supported in C# ParameterDirection Direction, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(true)] // ERROR: Optional parameters aren't supported in C# bool isNullable, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(0)] // ERROR: Optional parameters aren't supported in C# byte Precision, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(0)] // ERROR: Optional parameters aren't supported in C# byte Scale, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")] // ERROR: Optional parameters aren't supported in C# string SourceColumn, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(DataRowVersion.Default)] // ERROR: Optional parameters aren't supported in C# DataRowVersion SourceVersion  ) 
    {
        SqlParameter objParameter = new SqlParameter();
        objParameter.ParameterName = ParameterName;
        objParameter.SqlDbType = DBType;


        objParameter.Value = Value;

        return objParameter;

    }

    public static SqlParameter CreateParameter(string ParameterName, SqlDbType DBType, object Value, ParameterDirection Direction, int Size)//, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(0)] // ERROR: Optional parameters aren't supported in C# int Size, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(ParameterDirection.Input)] // ERROR: Optional parameters aren't supported in C# ParameterDirection Direction, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(true)] // ERROR: Optional parameters aren't supported in C# bool isNullable, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(0)] // ERROR: Optional parameters aren't supported in C# byte Precision, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(0)] // ERROR: Optional parameters aren't supported in C# byte Scale, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute("")] // ERROR: Optional parameters aren't supported in C# string SourceColumn, [System.Runtime.InteropServices.OptionalAttribute, System.Runtime.InteropServices.DefaultParameterValueAttribute(DataRowVersion.Default)] // ERROR: Optional parameters aren't supported in C# DataRowVersion SourceVersion  ) 
    {
        SqlParameter objParameter = new SqlParameter();
        objParameter.ParameterName = ParameterName;
        objParameter.SqlDbType = DBType;
        if (Size != 0)
        {
            objParameter.Size = Size;
        }
        if (Direction != ParameterDirection.Input)
        {
            objParameter.Direction = Direction;
        }
        objParameter.Value = Value;

        return objParameter;

    }
    public static SqlParameter CreateParameter(string ParameterName)
    {
        SqlParameter objParameter = new SqlParameter();
        objParameter.ParameterName = ParameterName;

        //objParameter.SqlDbType = SqlDbType 

        objParameter.Direction = ParameterDirection.Output;


        return objParameter;

    }
}
