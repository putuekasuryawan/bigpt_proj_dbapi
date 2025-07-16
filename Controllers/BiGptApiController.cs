using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using BiGptWebApi.Helpers;
using Npgsql;
using MySqlConnector;

namespace BiGptWebApi.Controllers
{
    [ApiKey]
    public class BiGptApiController : Controller
    {
        [HttpGet]
        public JsonResult SqlServerQuery(string conn, string q)
        {
            if (q == null || q == "")
            {
                var result = "Your Query or Command is blank. Please write something.";
                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                using (SqlConnection db = new SqlConnection())
                {
                    db.ConnectionString = conn;

                    try
                    {
                        db.Open();
                        using (SqlCommand cmd = new SqlCommand(q, db))
                        {
                            cmd.CommandTimeout = 180;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                var result = GetTableRows(dt);
                                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        string message;
                        switch (ex.Number)
                        {
                            case 18456:
                                message = "Login failed. Please check your SQL Server credentials.";
                                break;
                            case 4060:
                                message = "The database is not accessible or does not exist.";
                                break;
                            case 208:
                                message = "The specified table does not exist.";
                                break;
                            case 102:
                                message = "Syntax error in your SQL query.";
                                break;
                            default:
                                message = ex.Message;
                                break;
                        }
                        var result = $"SQL Server Error ({ex.Number}): {message}";
                        return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
                    }
                }
            }
        }
        [HttpGet]
        public JsonResult PostgreQuery(string conn, string q)
        {
            if (q == null || q == "")
            {
                var result = "Your Query or Command is blank. Please write something.";
                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                using (NpgsqlConnection db = new NpgsqlConnection())
                {
                    db.ConnectionString = conn;

                    try
                    {
                        db.Open();
                        using (NpgsqlCommand cmd = new NpgsqlCommand(q, db))
                        {
                            cmd.CommandTimeout = 180;
                            using (NpgsqlDataReader reader = cmd.ExecuteReader())
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                var result = GetTableRows(dt);
                                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
                            }
                        }
                    }
                    catch (PostgresException ex)
                    {
                        string message;
                        switch (ex.SqlState)
                        {
                            case "28000":
                                message = "Authentication failed. Please check your username or password.";
                                break;
                            case "3D000":
                                message = "The specified database does not exist.";
                                break;
                            case "42601":
                                message = "Syntax error in your SQL query.";
                                break;
                            case "42P01":
                                message = "The specified table does not exist.";
                                break;
                            default:
                                message = ex.Message;
                                break;
                        }
                        var result = $"PostgreSQL Error (SQLSTATE {ex.SqlState}): {message}";
                        return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
                    }
                }
            }
        }
        [HttpGet]
        public JsonResult MySqlQuery(string conn, string q)
        {
            if (q == null || q == "")
            {
                var result = "Your Query or Command is blank. Please write something.";
                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
            }
            else
            {
                using (MySqlConnection db = new MySqlConnection())
                {
                    db.ConnectionString = conn;

                    try
                    {
                        db.Open();
                        using (MySqlCommand cmd = new MySqlCommand(q, db))
                        {
                            cmd.CommandTimeout = 180;
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                DataTable dt = new DataTable();
                                dt.Load(reader);
                                var result = GetTableRows(dt);
                                return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        string message;
                        switch (ex.Number)
                        {
                            case 1045:
                                message = "Authentication failed. Please check your username or password.";
                                break;
                            case 1049:
                                message = "The specified database does not exist.";
                                break;
                            case 1064:
                                message = "Syntax error in your SQL query.";
                                break;
                            case 1146:
                                message = "The specified table does not exist.";
                                break;
                            case 2002:
                                message = "Cannot connect to MySQL server. Check your server address.";
                                break;
                            default:
                                message = ex.Message;
                                break;
                        }
                        var result = $"MySQL Error ({ex.Number}): {message}";
                        return Json(result, new Newtonsoft.Json.JsonSerializerSettings());
                    }
                }
            }
        }
        public List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            List<Dictionary<string, object>>
            listRows = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dtData.Rows)
            {
                Dictionary<string, object> dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in dtData.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                listRows.Add(dictRow);
            }
            return listRows;
        }
    }
}
