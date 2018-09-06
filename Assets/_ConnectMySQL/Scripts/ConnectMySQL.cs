using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using UnityEngine;
using System.Data;
using MySqlHelper = Helpers.MySqlHelper;
using Newtonsoft.Json;

public class ConnectMySQL : MonoBehaviour
{
    /// <summary>
    /// 批量操作每批次记录数
    /// </summary>
    public static int BatchSize = 2000;

    /// <summary>
    /// 超时时间
    /// </summary>
    public static int CommandTimeOut = 600;
    private string server = "gofbox.com";
    private string database = "arseeu";
    private string uid = "root2";
    private string password = "Xqm2328549@";
    private string connstring;
    private MySqlConnection myConnection;

    void Start()
    {
        connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", server, database, uid, password);
        /*insertFavorite("2");
        List<Dictionary<string, string>> result = queryFavoriteList("select * from favorite_list");
        foreach (Dictionary<string, string> row in result)
        {
            string va;
            row.TryGetValue("id", out va);
            Debug.Log(va);
        }*/

        MySqlHelper mySqlHelper = new MySqlHelper(connstring);
        MySqlParameter[] parameter = null;
        DataRow result = mySqlHelper.ExecuteDataRow("select * from favorite_list", parameter);
        Debug.Log(JsonConvert.SerializeObject(result));
    }

    public List<Dictionary<string, string>> queryFavoriteList(string sql)
    {
        using (myConnection = new MySqlConnection(connstring))
        {
            MySqlCommand myCommand = new MySqlCommand(sql, myConnection);

            myConnection.Open();
            
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            MySqlDataReader myDataReader = myCommand.ExecuteReader();
           
            while (myDataReader.Read() == true)
            {
                Dictionary<string, string> row = new Dictionary<string, string>();
                row.Add("id", myDataReader.GetString("id"));
                result.Add(row);
            }

            myDataReader.Close();
            Debug.Log(JsonConvert.SerializeObject(result));
            return result;
        }
    }

    public void insertFavorite(string value)
    {
        using (myConnection = new MySqlConnection(connstring))
        {
            string sql = string.Format("insert into favorite_list(id) values('{0}')", value);
            MySqlCommand myCommand = new MySqlCommand(sql, myConnection);

            myConnection.Open();

            MySqlTransaction transaction = myConnection.BeginTransaction();
            myCommand.Transaction = transaction;

            try
            {
                int x = myCommand.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
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
    
    private static void PrepareCommand(MySqlCommand command, MySqlConnection connection, MySqlTransaction transaction, CommandType commandType, string commandText, MySqlParameter[] parms)
    {
        if (connection.State != ConnectionState.Open) connection.Open();

        command.Connection = connection;
        command.CommandTimeout = CommandTimeOut;
        // 设置命令文本(存储过程名或SQL语句)
        command.CommandText = commandText;
        // 分配事务
        if (transaction != null)
        {
            command.Transaction = transaction;
        }
        // 设置命令类型.
        command.CommandType = commandType;
        if (parms != null && parms.Length > 0)
        {
            //预处理MySqlParameter参数数组，将为NULL的参数赋值为DBNull.Value;
            foreach (MySqlParameter parameter in parms)
            {
                if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                {
                    parameter.Value = DBNull.Value;
                }
            }
            command.Parameters.AddRange(parms);
        }
    }
}