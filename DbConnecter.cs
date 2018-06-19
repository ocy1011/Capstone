using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;

public class DbConnecter : MonoBehaviour {
    public static DbConnecter instance;
    public GameObject networkErrorPanel;
    /*
    SqlConnection connection;
    SqlTransaction transaction;
    SqlCommand command;
    string connectionString = "Server=DESKTOP-MLDICB0;Database=test;User ID=yeong;Password=1234";
    */
    MySqlConnection connection;
    MySqlTransaction transaction;
    MySqlCommand command;

    private void Start()
    {
        instance = this;
        //Screen.SetResolution(540, 960,true);
        //SQLConnection();
    }

    void OnNetworkErrorPanel()
    {
        networkErrorPanel.SetActive(true);
    }

    public void OffNetworkErrorPanel()
    {
        networkErrorPanel.SetActive(false);
    }

    void SQLConnection()
    {
        string strConn = "Server=203.250.148.93;Database=test;Uid=student02;Pwd=student02;persistsecurityinfo=True;SslMode=none";
        MySqlConnection conn = new MySqlConnection(strConn);
        conn.Open();
        MySqlTransaction trans = conn.BeginTransaction();
        MySqlCommand cmd = new MySqlCommand();
        cmd.Connection = conn;
        cmd.Transaction = trans;
        string sql = "SELECT COUNT(id) FROM product;";
        cmd.CommandText = sql;
        int count = Convert.ToInt32(cmd.ExecuteScalar());
        Debug.Log(count);
        sql = "SELECT * FROM product;";
        MySqlDataReader reader = Reader(sql);
        reader.Read();
        Debug.Log(reader.GetString(4));

    }

    /*
    public void StringChange()
    {
        connectionString = inputfield.text;
        inputPanel.SetActive(false);
        Debug.Log(connectionString);
    }
    */
    public void Connect()
    {
        
        if (connection == null || connection.State == System.Data.ConnectionState.Closed)
        {
            connection = OpenConnection();
            if(connection==null)
            {
                
                OnNetworkErrorPanel();
            }
            else
            {
                
                transaction = connection.BeginTransaction();
                command = new MySqlCommand();
                command.Connection = connection;
                command.Transaction = transaction;
                OffNetworkErrorPanel();
            }
        }
    }

    public int Count(string column, string table)
    {
        Connect();
        string sql = "SELECT COUNT(" + column+") FROM " + table+";";
        command.CommandText = sql;
        int count = Convert.ToInt32(command.ExecuteScalar());
        return count;
    }

    public int Count(string select, string from, string where)
    {
        Connect();
        string sql = "SELECT COUNT(" + select + ") FROM " + from + " WHERE "+ where + ";";
        command.CommandText = sql;
        int count = Convert.ToInt32(command.ExecuteScalar());
        return count;
    }

    public void Check(string sql)
    {
        Connect();
        command.CommandText = sql;
        MySqlDataReader reader = command.ExecuteReader();
        Debug.Log(Convert.ToInt32(command.ExecuteScalar()));
        reader.Close();
        CloseConnection();
    }

    public void ExecuteSQL(string sql, bool commit)
    {
        Connect();
        command.CommandText = sql;
        command.ExecuteNonQuery();
        if(commit == true)
            transaction.Commit();
    }

    public MySqlDataReader Reader(string sql)
    {
        Connect();
        command.CommandText = sql;
        MySqlDataReader reader = command.ExecuteReader();
        return reader;
    }

    public void Insert(string sql)
    {
        Connect();

        //string sql = "INSERT INTO table2 VALUES(10,'temp','8801234123456')";
        command.CommandText = sql;
        command.ExecuteNonQuery();
        transaction.Commit();

        CloseConnection();
    }

    MySqlConnection OpenConnection()
    {
        try
        {
            //Server=DESKTOP-MLDICB0
            string connectionString = "Server=203.250.148.93;Database=test;Uid=student02;Pwd=student02;persistsecurityinfo=True;SslMode=none;Charset=utf8";
            connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        catch
        {
            return null;
        }
        
    }

    public void CloseConnection()
    {
        if (connection.State == System.Data.ConnectionState.Open)
            connection.Close();
    }
}
