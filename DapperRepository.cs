using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_Example
{
    public class DapperRepository
    {

        //step 1: install these packages :
        // - Dapper
        // -System.Data.SqlClient;

        // if you are running a simple peice of sql with some passed in arguments then do it like this  with no response 


        public async Task<bool> RunSqlQuery(long locationId, int employeeId, DateTime timeStamp)
        {
            try
            {
                using (var connection = new SqlConnection("SQL CONNECTION STRING GOES HERE "))
                { 
                    var sql = $"SELECT * FROM <SOME TABLE> WHERE LocationId='{locationId}'" ;
                    var values = new { Location_ID = locationId, Employee_ID = employeeId, Datetime = timeStamp };
                    var result = await connection.ExecuteAsync(sql);
                    return true;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
        }





        //If you are pulling from a stored Procedure then you would call it with something like this 
        public async Task<bool> CallStoredProcedure(long locationId, int employeeId, DateTime timeStamp)
        {
            try
            {
                using (var connection = new SqlConnection("SQL CONNECTION STRING GOES HERE "))
                {
                    var procedure = "[stocktake].[PR_Stocktake_CompleteFirstCount]";
                    var values = new { Location_ID = locationId, Employee_ID = employeeId, Datetime = timeStamp };
                    var result = await connection.ExecuteAsync(procedure, values, commandType: CommandType.StoredProcedure);
                    return true;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
        }


        // if you get rows of data back from sql then make an object that matches the columns and we can return the data as a list of that



        public async Task<List<returnobject>> RunSqlQuerywithObjectReturned(long locationId, int employeeId, DateTime timeStamp)
        {
            try
            {
                using (var connection = new SqlConnection("SQL CONNECTION STRING GOES HERE "))
                {
                    var sql = $"SELECT * FROM <SOME TABLE> WHERE LocationId='{locationId}'";
                    var values = new { Location_ID = locationId, Employee_ID = employeeId, Datetime = timeStamp };
                    var result = await connection.QueryAsync<returnobject>(sql);
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                return null;
            }
        }





        //If you are pulling from a stored Procedure then you would call it with something like this 
        public async Task<List<returnobject>> CallStoredProcedureWithObjectReturned (long locationId, int employeeId, DateTime timeStamp)
        {
            try
            {
                using (var connection = new SqlConnection("SQL CONNECTION STRING GOES HERE "))
                {
                    var procedure = "[stocktake].[PR_Stocktake_CompleteFirstCount]";
                    var values = new { Location_ID = locationId, Employee_ID = employeeId, Datetime = timeStamp };
                    var result = await connection.QueryAsync<returnobject>(procedure, values, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                return null;
            }
        }



    }
}
