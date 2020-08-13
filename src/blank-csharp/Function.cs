using Amazon.Lambda.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace blankCsharp
{
    public class Function
    {
        public Response FunctionHandler(Request request, ILambdaContext context)
        {

            string connectString = "DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=RDSHOSTID.rds.amazonaws.com)(PORT=1521)))(CONNECT_DATA=(SID=SOLAR)));PERSIST SECURITY INFO=True;USER ID=USERID;PASSWORD=PAWSSWORD;";

            Response response = new Response(string.Empty, request);

            context.Logger.LogLine(string.Format("FunctionHandler:{0}\n", connectString));
            LambdaLogger.Log("[Log ln:24] FunctionHandler: " + connectString);

            using (var cn = new OracleConnection(connectString))
            {
                cn.Open();
                using (OracleCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandText = "select COUNT(*) from TABLE";
                    OracleDataReader eReader = cmd.ExecuteReader();
                    try
                    {
                        while (eReader.Read())
                        {
                            Console.WriteLine("Result: " + eReader.GetValue(0));
                            LambdaLogger.Log("[Log ln:38]  Result: " + eReader.GetValue(0).ToString());
                            response.Message = string.Format("TABLE Count={0}", eReader.GetValue(0).ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        LambdaLogger.Log("[Log ln:44] Exception Message: " + JsonConvert.SerializeObject(ex.Message));
                    }
                    finally
                    {
                        eReader.Close();
                    }
                    
                }
            }

            // Return the value
            context.Logger.LogLine(string.Format("Response Message:{0}\n", response.Message));
            LambdaLogger.Log("[Log ln:56] Response Message: " + JsonConvert.SerializeObject(response.Message));
            
            return response;
        }

        public class Response
        {
            public string Message { get; set; }
            public Request Request { get; set; }

            public Response(string message, Request request)
            {
                Message = message;
                Request = request;
            }
        }

        public class Request
        {
            public string param1 { get; set; }

            public string Body { get { return string.Format("param1:{0}", param1); } }
        }
    }
}
