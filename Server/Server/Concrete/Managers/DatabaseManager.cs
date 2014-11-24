using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Server.Concrete.Entities;

namespace Server.Concrete.Managers
{
  public class DatabaseManager
  {
    private static readonly string _mainDatabase = "Data Source=.\\SQLEXPRESS;Initial Catalog=ProjectDatabase;Persist Security Info=True;User ID=project;Password=project;";

    public static User GetUser(string login, string password)
    {
      var sqlConnection = new SqlConnection(_mainDatabase);
      var sqlCommand = new SqlCommand("SELECT * FROM Users WHERE Login = @login AND Password = @password", sqlConnection);
      sqlCommand.Parameters.AddWithValue("@login", login);
      sqlCommand.Parameters.AddWithValue("@password",password);
      try
      {
        sqlConnection.Open();
        var reader = sqlCommand.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
          var user = new User();
          user.UserId = Int32.Parse(reader[0].ToString());
          user.Login = login;
          user.Password = password;
          user.GameData = JsonConvert.DeserializeObject<UserGameData>(reader[3].ToString());
          return user;
        }
        else
        {
          return null;
        }
      }
      catch (Exception)
      {
        Console.WriteLine("Something went wrong reading user");
        throw;
      }
      finally
      {
        if(sqlConnection.State==ConnectionState.Open)
          sqlConnection.Close();
      }
    }

    public static User GetUser(int uid)
    {
      var sqlConnection = new SqlConnection(_mainDatabase);
      var sqlCommand = new SqlCommand("SELECT * FROM Users WHERE Id = @uid", sqlConnection);
      sqlCommand.Parameters.AddWithValue("@uid", uid);
      try
      {
        sqlConnection.Open();
        var reader = sqlCommand.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
          var user = new User();
          user.UserId = Int32.Parse(reader[0].ToString());
          user.Login = reader[1].ToString();
          user.Password = reader[2].ToString();
          user.GameData = JsonConvert.DeserializeObject<UserGameData>(reader[3].ToString());
          return user;
        }
        else
        {
          return null;
        }
      }
      catch (Exception)
      {
        Console.WriteLine("Something went wrong reading user");
        throw;
      }
      finally
      {
        if (sqlConnection.State == ConnectionState.Open)
          sqlConnection.Close();
      }
    }

    public static void AddUser(User user)
    {
      using (var sqlConnection = new SqlConnection(_mainDatabase))
      {
        sqlConnection.Open();
        var sqlCommand = new SqlCommand("INSERT INTO Users (Login,Password,JData) VALUES (@login,@password,@gamedata)", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@login",user.Login);
        sqlCommand.Parameters.AddWithValue("@password",user.Password);
        sqlCommand.Parameters.AddWithValue("@gamedata", JObject.FromObject(user.GameData).ToString());
        sqlCommand.ExecuteNonQuery();
      }
    }

    public static void UpdateUserGameData(User user)
    {
      using (var sqlConnection = new SqlConnection(_mainDatabase))
      {
        sqlConnection.Open();
        var sqlCommand = new SqlCommand("UPDATE Users SET JData = @gamedata WHERE Id = @userid", sqlConnection);
        sqlCommand.Parameters.Add(new SqlParameter("@gamedata", SqlDbType.VarChar)).Value = JObject.FromObject(user.GameData).ToString();
        sqlCommand.Parameters.Add(new SqlParameter("@userid", SqlDbType.Int)).Value = user.UserId;
        sqlCommand.ExecuteNonQuery();
      }
    }

    public static bool FindUser(string login, string password)
    {
      using (var sqlConnection = new SqlConnection(_mainDatabase))
      {
        sqlConnection.Open();
        var sqlCommand = new SqlCommand("SELECT COUNT(*) FROM Users WHERE Login = @login", sqlConnection);
        sqlCommand.Parameters.AddWithValue("@login", login);
        return Int32.Parse(sqlCommand.ExecuteScalar().ToString()) == 1;
      }
    }
  }
}