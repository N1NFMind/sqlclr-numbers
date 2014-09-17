using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    private struct ReturnValues
    { public int Value; }

    [Microsoft.SqlServer.Server.SqlFunction(DataAccess = DataAccessKind.None,
        IsDeterministic = true, IsPrecise = true,
        SystemDataAccess = SystemDataAccessKind.None,
        FillRowMethodName = "FillValues",
        TableDefinition = "N INT")]
    public static IEnumerable GetNumbers(SqlInt32 MaxValue)
    {
        if (MaxValue.IsNull)
        { yield break; }

        // we do not need the Generic List of <ReturnValues>
        ReturnValues Vals = new ReturnValues(); // each row

        for (int index = 1; index <= MaxValue.Value; index++)
        {
            Vals.Value = index;
            yield return Vals; // return row per each itteration
        }

        // we do not need to return everything at once
    }
    private static void FillValues(object obj, out SqlInt32 TheValue)
    {
        ReturnValues ReturnVals = (ReturnValues)obj;
        TheValue = ReturnVals.Value;
    }
}
