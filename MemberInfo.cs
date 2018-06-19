using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemberInfo : MonoBehaviour {

    public Member member;

    public bool Login(string email, string password)
    {
        int count = DbConnecter.instance.Count("age", "member", "email = '" + email + "' AND password = '" + password + "';");
        if (count == 0)
        {
            return false;
        }
        else
        {
            string sql = "SELECT * FROM member WHERE email = '" + email + "' AND password = '"+ password + "';";
            MySql.Data.MySqlClient.MySqlDataReader reader = DbConnecter.instance.Reader(sql);
            reader.Read();
            member.email = email;
            member.password = password;
            member.gender = reader.GetInt32(2);
            member.age = reader.GetInt32(3);
            member.child = reader.GetInt32(4);
            member.productBookmarks = "";
            if (reader.IsDBNull(5)==false)
                member.productBookmarks = reader.GetString(5);
            member.ingredientBookmarks = "";
            if (reader.IsDBNull(6) == false)
                member.ingredientBookmarks = reader.GetString(6);
            reader.Close();
            DbConnecter.instance.CloseConnection();

            return true;
        }
    }

    public bool isNull()
    {
        if (member.email.Length>0 && member.password.Length> 0)
            return false;
        else
            return true;
    }

}
