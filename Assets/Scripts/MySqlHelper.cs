using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using UnityEngine;

namespace Helpers
{
    /// <summary>
    /// MySqlHelper操作类
    /// </summary>
    public sealed partial class MySqlHelper
    {
        /// <summary>
        /// 批量操作每批次记录数
        /// </summary>
        public static int BatchSize = 2000;

        /// <summary>
        /// 超时时间
        /// </summary>
        public static int CommandTimeOut = 600;

        private static string server = "gofbox.com";
        private static string database = "arseeu";
        private static string uid = "root2";
        private static string password = "Xqm2328549@";
        private static string connectionStr = null;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string getConnectionStr()
        {
            if (connectionStr == null)
            {
                connectionStr = string.Format("Server={0}; database={1}; UID={2}; password={3}", server, database, uid, password);
            }

            return connectionStr;
        }

        public static List<Dictionary<string, string>> query(string sql, Dictionary<string, object> parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(getConnectionStr()))
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                prepareCommand(command, connection, parameters);
                List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

                MySqlDataReader dataReader = command.ExecuteReader();

                //获取列名称
                List<string> columnList = new List<string>();
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    columnList.Add(dataReader.GetName(i).Trim());
                }

                while (dataReader.Read() == true)
                {
                    Dictionary<string, string> row = new Dictionary<string, string>();
                    foreach (string column in columnList)
                    {
                        row.Add(column, dataReader.GetString(column));
                    }

                    result.Add(row);
                }

                dataReader.Close();
                return result;
            }
        }

        public static void insertOrUpdate(string sql, Dictionary<string, object> parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(getConnectionStr()))
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();
                prepareCommand(command, connection, parameters);
                MySqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    int x = command.ExecuteNonQuery();
                    Debug.Log("执行结果：" + x);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.StackTrace);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception en)
                    {
                        throw;
                    }
                }
            }
        }

        public static void delete(string sql, Dictionary<string, object> parameters)
        {
            using (MySqlConnection connection = new MySqlConnection(getConnectionStr()))
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();
                prepareCommand(command, connection, parameters);
                MySqlTransaction transaction = connection.BeginTransaction();
                command.Transaction = transaction;

                try
                {
                    int x = command.ExecuteNonQuery();
                    Debug.Log("执行结果：" + x);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Debug.Log(ex.StackTrace);
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception en)
                    {
                        throw;
                    }
                }
            }
        }

        private static void prepareCommand(MySqlCommand command, MySqlConnection connection, Dictionary<string, object> parameters)
        {
            if (connection.State != ConnectionState.Open) connection.Open();

            command.Connection = connection;
            command.CommandTimeout = 600;

            if (parameters != null)
            {
                MySqlParameter parameter;
                foreach (string key in parameters.Keys)
                {
                    object value;
                    parameters.TryGetValue(key, out value);
                    parameter = new MySqlParameter(key, value.ToString());
                    command.Parameters.Add(parameter);
                }
            }
        }

        private void example()
        {
            Debug.Log("开始插入");
            string insertSQL = "insert into test(id) values(@id)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@id", 3);
            insertOrUpdate(insertSQL, parameters);
            Debug.Log("插入完毕");
        }
    }
}