using MySql.Data.MySqlClient;
using UnityEngine;
using System.Data;

public class ConnectMySQL : MonoBehaviour
{
    private string server = "gofbox.com";
    private string database = "haitao";
    private string uid = "root2";
    private string password = "Xqm2328549@";
    private MySqlConnection myConnection;

    void Start()
    {
        string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", server, database, uid, password);
        myConnection = new MySqlConnection(connstring);
        query("select * from deduct_percentage");
    }

    public void query(string sql)
    {
        MySqlCommand myCommand = new MySqlCommand(sql, myConnection);

        myConnection.Open();
        myCommand.ExecuteNonQuery();
        MySqlDataReader myDataReader = myCommand.ExecuteReader();
        while (myDataReader.Read() == true)
        {
            string generation = myDataReader.GetString("generation");
            string percentage = myDataReader.GetString("percentage");
            Debug.Log(generation);
            Debug.Log(percentage);
        }
        myDataReader.Close();
        myConnection.Close();
    }
}