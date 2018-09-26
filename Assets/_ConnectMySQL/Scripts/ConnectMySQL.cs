using System;
using System.Collections;
using System.Collections.Generic;
using MySql.Data;
using MySql.Data.MySqlClient;
using UnityEngine;
using System.Data;
using MySqlHelper = Helpers.MySqlHelper;
using Newtonsoft.Json;

public class ConnectMySQL : MonoBehaviour
{
    private string server = "gofbox.com";
    private string database = "arseeu";
    private string uid = "root2";
    private string password = "Xqm2328549@";
    private MySqlConnection myConnection;

    void Start()
    {
//        Debug.Log("开始插入");
//        string insertSQL = "insert into comment_info(id, content) values(@id, @content)";
//        Dictionary<string, object> parameters = new Dictionary<string, object>();
//        parameters.Add("@id", 2);
//        parameters.Add("@content", "2131");
//        MySqlHelper.insertOrUpdate(insertSQL, parameters);
//        Debug.Log("插入完毕");

        query();

        Debug.Log("开始删除");
        string deletSQL = "delete from comment_info where id = @id";
        Dictionary<string, object> parameters1 = new Dictionary<string, object>();
        parameters1.Add("@id", 1);
        MySqlHelper.insertOrUpdate(deletSQL, parameters1);
        Debug.Log("删除完毕");

        query();
    }

    private void query()
    {
        string querySQL = "select * from comment_info ";
        List<Dictionary<string, string>> result = MySqlHelper.query(querySQL, null);
        foreach (Dictionary<string, string> row in result)
        {
            string id, va;
            row.TryGetValue("id", out id);
            row.TryGetValue("content", out va);
            Debug.Log(id + "   " + va);
        }
    }
}