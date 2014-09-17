using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    private struct RangeReturnValues
    { public int Value; }

    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.None,
        IsDeterministic = true, IsPrecise = true,
        SystemDataAccess = SystemDataAccessKind.None,
        FillRowMethodName = "FillRangeValues",
        TableDefinition = "N INT")]
    public static IEnumerable GetNumberRange(SqlInt32 FromValue, SqlInt32 ToValue)
    {
        if (FromValue.IsNull || ToValue.IsNull)
        { yield break; }

        // we do not need the Generic List of <ReturnValues>
        RangeReturnValues Vals = new RangeReturnValues(); // each row

        for (int index = FromValue.Value; index <= ToValue.Value; index++)
        {
            Vals.Value = index;
            yield return Vals; // return row per each itteration
        }

        // we do not need to return everything at once
    }
    private static void FillRangeValues(object obj, out SqlInt32 TheValue)
    {
        RangeReturnValues ReturnVals = (RangeReturnValues)obj;
        TheValue = ReturnVals.Value;
    }
}
