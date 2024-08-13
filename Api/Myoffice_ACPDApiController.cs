using BackendExamHub.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
﻿using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
using System.Xml.Linq;

namespace BackendExamHub.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class Myoffice_ACPDApiController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly string _conn;

        public Myoffice_ACPDApiController(IConfiguration configuration)
        {
            Configuration = configuration;
            _conn = Configuration["ConnectionString"] ?? @"Data Source=./;Initial Catalog = DBPractise;Integrated Security = True;TrustServerCertificate=True;";
        }

        [HttpGet]
        public IEnumerable<Myoffice_ACPD> Get()
        {
            List<Myoffice_ACPD> list = new List<Myoffice_ACPD>();
            string queryString =
                "SELECT  [acpd_sid],[acpd_cname],[acpd_ename],[acpd_sname],[acpd_email],[acpd_status],[acpd_stop],[acpd_stopMemo],[acpd_LoginID],[acpd_LoginPW],[acpd_memo],[acpd_nowdatetime],[appd_nowid],[acpd_upddatetitme],[acpd_updid] FROM [BackendExamHub].[dbo].[Myoffice_ACPD]";

            using (SqlConnection connection =
                       new SqlConnection(_conn))
            {
                SqlCommand command =
                    new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    Myoffice_ACPD myoffice_ACPD = new Myoffice_ACPD();
                    myoffice_ACPD.acpd_sid = (string)reader[0];
                    myoffice_ACPD.acpd_cname = (string)reader[1];
                    myoffice_ACPD.acpd_sname = (string)reader[2];
                    myoffice_ACPD.acpd_email = (string)reader[3];
                    myoffice_ACPD.acpd_status = (int)reader[3];
                    myoffice_ACPD.acpd_stop = (bool)reader[4];
                    myoffice_ACPD.acpd_stopMemo = (string)reader[5];
                    myoffice_ACPD.acpd_LoginID = (string)reader[6];
                    myoffice_ACPD.acpd_LoginPW = (string)reader[7];
                    myoffice_ACPD.acpd_memo = (string)reader[8];
                    myoffice_ACPD.acpd_nowdatetime = (DateTime)reader[9];
                    myoffice_ACPD.appd_nowid = (string)reader[10];
                    myoffice_ACPD.acpd_upddatetitme = (DateTime)reader[11];
                    myoffice_ACPD.acpd_updid = (string)reader[12];
                    list.Add(myoffice_ACPD);
                }

                // Call Close when done reading.
                reader.Close();
            }
            return list;

        }

        [HttpPost]
        public void Insert(Myoffice_ACPD Myoffice_ACPD)
        {
            string query =
           "INSERT INTO[dbo].[Myoffice_ACPD] " +
           " ([acpd_sid]" +
           ", [acpd_cname]" +
           ", [acpd_ename]" +
           ", [acpd_sname]" +
           ", [acpd_email]" +
           ", [acpd_status]" +
           ", [acpd_stop]" +
           ", [acpd_stopMemo]" +
           ", [acpd_LoginID]" +
           ", [acpd_LoginPW]" +
           ", [acpd_memo]" +
           ", [acpd_nowdatetime]" +
           ", [appd_nowid]" +
           ", [acpd_upddatetitme]" +
           ", [acpd_updid])" +
           "VALUES(@acpd_sid,@acpd_cname,@acpd_ename,@acpd_sname,@acpd_email,@acpd_status,@acpd_stop,@acpd_stopMemo,@acpd_LoginID,@acpd_LoginPW,@acpd_memo,@acpd_nowdatetime,@appd_nowid,@acpd_upddatetitme,@acpd_updid";
            using (SqlConnection cn = new SqlConnection(_conn))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                SqlCommand spcmd;
                using (spcmd = new SqlCommand("NEWSID", cn))
                {
                    spcmd.CommandType = CommandType.StoredProcedure;

                    var returnParameter = spcmd.Parameters.Add("@TableName", SqlDbType.NVarChar,128).Value = nameof(Myoffice_ACPD);

                    spcmd.ExecuteNonQuery();
                }
                // define parameters and their values
                cmd.Parameters.Add("@acpd_sid", SqlDbType.Char, 20).Value = (string)spcmd.Parameters["@ReturnSID"].Value; ;
                cmd.Parameters.Add("@acpd_cname", SqlDbType.NVarChar, 60).Value = Myoffice_ACPD.acpd_cname;
                cmd.Parameters.Add("@acpd_ename", SqlDbType.NVarChar, 40).Value = Myoffice_ACPD.acpd_ename;
                cmd.Parameters.Add("@acpd_sname", SqlDbType.NVarChar, 40).Value = Myoffice_ACPD.acpd_sname;
                cmd.Parameters.Add("@acpd_email", SqlDbType.NVarChar, 60).Value = Myoffice_ACPD.acpd_email;
                cmd.Parameters.Add("@acpd_status", SqlDbType.SmallInt).Value = Myoffice_ACPD.acpd_status;
                cmd.Parameters.Add("@acpd_stop", SqlDbType.Bit, 50).Value = Myoffice_ACPD.acpd_stop;
                cmd.Parameters.Add("@acpd_stopMemo", SqlDbType.NVarChar, 600).Value = Myoffice_ACPD.acpd_stopMemo;
                cmd.Parameters.Add("@acpd_LoginID", SqlDbType.NVarChar, 30).Value = Myoffice_ACPD.acpd_LoginID;
                cmd.Parameters.Add("@acpd_LoginPW", SqlDbType.NVarChar, 60).Value = Myoffice_ACPD.acpd_LoginPW;
                cmd.Parameters.Add("@acpd_memo", SqlDbType.NVarChar, 120).Value = Myoffice_ACPD.acpd_memo;
                cmd.Parameters.Add("@acpd_nowdatetime", SqlDbType.DateTime).Value = Myoffice_ACPD.acpd_nowdatetime;
                cmd.Parameters.Add("@appd_nowid", SqlDbType.NVarChar, 20).Value = Myoffice_ACPD.appd_nowid;
                cmd.Parameters.Add("@acpd_upddatetitme", SqlDbType.DateTime).Value = Myoffice_ACPD.acpd_upddatetitme;
                cmd.Parameters.Add("@acpd_updid", SqlDbType.NVarChar, 20).Value = Myoffice_ACPD.acpd_updid;

                // open connection, execute INSERT, close connection
                cn.Open();
                int resultCode = cmd.ExecuteNonQuery();
                using (SqlCommand spLOGcmd = new SqlCommand("usp_AddLog", cn))
                {
                    spLOGcmd.CommandType = CommandType.StoredProcedure;

                    spLOGcmd.Parameters.Add("@RT_Status", SqlDbType.Bit).Value = Myoffice_ACPD.acpd_stop;
                    spLOGcmd.Parameters.Add("@RT_ActionValues", SqlDbType.NVarChar,2000).Value = resultCode.ToString();

                    spLOGcmd.ExecuteNonQuery();
                }
                cn.Close();
            }
        }

        [HttpPut]
        public void Update(Myoffice_ACPD Myoffice_ACPD))
        {
        }

        [HttpDelete]
        public void Delete(string sid) {
            string connectionString = _conn;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"DELETE FROM [dbo].[Myoffice_ACPD] where acpd_sid = {sid} ";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }

        }
    }
}
