using System.Data;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Npgsql;
using SportCenter.WebAPI.Connection;
using SportCenter.WebAPI.Interfaces;
using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Repositories;

public class UserRepository : IUserRepository
{
    private readonly NpgsqlConnection _connection;

    public UserRepository(DbConnection dbConnection)
    {
        _connection = dbConnection.GetConnection();
    }
    public List<UserDb> Get()
    {
        List<UserDb> users = new List<UserDb>();
        try
        {
            _connection.Open();
            
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT * FROM users;";
        
            using NpgsqlDataReader reader = sqlCommand.ExecuteReader();
            
            while (reader.Read())
            {
                Console.WriteLine(reader);
                UserDb userDb = new UserDb(
                    Convert.ToInt32(reader["user_id"]),
                    Convert.ToString(reader["username"]),
                    Convert.ToString(reader["password"]),
                    Convert.ToString(reader["email"]),
                    Convert.ToString(reader["phone"]),
                    Convert.ToString(reader["name"]),
                    Convert.ToBoolean(reader["isDeleted"]),
                    Convert.ToString(reader["role"])                    
                );
                users.Add(userDb);
            }
            _connection.Close();
            return users;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public UserDb GetByEmail(string email)
    {
        try
        {
            _connection.Open();

            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.AddWithValue("@Email", email);
            sqlCommand.CommandText = "SELECT * FROM \"users\" WHERE email = @Email";
        
            using NpgsqlDataReader reader = sqlCommand.ExecuteReader();
            UserDb userDb = new UserDb();
            while (reader.Read())
            {
                userDb = new UserDb(
                    Convert.ToInt32(reader["user_id"]),
                    Convert.ToString(reader["username"]),
                    Convert.ToString(reader["password"]),
                    Convert.ToString(reader["email"]),
                    Convert.ToString(reader["phone"]),
                    Convert.ToString(reader["name"]),
                    Convert.ToBoolean(reader["isDeleted"]),
                    Convert.ToString(reader["role"])
                );
            }
            _connection.Close();
            return userDb;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

    }

    public bool Create(UserDb entity)
    {
        try
        {
            var checkUserEmail = GetByEmail(entity.email);
            if (!string.IsNullOrWhiteSpace(checkUserEmail.email))
            {
                return false;
            }
            _connection.Open();
        
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            
            sqlCommand.Parameters.AddWithValue("@Username", entity.username);
            sqlCommand.Parameters.AddWithValue("@Password", entity.password);
            sqlCommand.Parameters.AddWithValue("@Email", entity.email);
            sqlCommand.Parameters.AddWithValue("@Phone", entity.phone);
            sqlCommand.Parameters.AddWithValue("@Name", entity.name);
            sqlCommand.Parameters.AddWithValue("@Role", entity.role);
            
            sqlCommand.CommandText = "INSERT INTO users (username, password, email, phone, name, role ) " +
                                     "        VALUES (@Username, @Password, @Email, @Phone, @Name, @Role) ";
            sqlCommand.ExecuteScalar();
            
            _connection.Close();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
       
    }

    public UserDb Update(string email, UserDb entity)
    {
        try
        {

            _connection.Open();
        
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
        
            sqlCommand.Parameters.AddWithValue("@EmailSearcher", email);
            sqlCommand.Parameters.AddWithValue("@Username", entity.username);
            sqlCommand.Parameters.AddWithValue("@Password", entity.password);
            sqlCommand.Parameters.AddWithValue("@Email", entity.email);
            sqlCommand.Parameters.AddWithValue("@Phone", entity.phone);
            sqlCommand.Parameters.AddWithValue("@Name", entity.name);
            sqlCommand.Parameters.AddWithValue("@Role", entity.role);

            sqlCommand.CommandText = "UPDATE users SET username = @Username, password = @Password, email = @Email, phone = @Phone, name = @Name, role = @Role WHERE email = @EmailSearcher RETURNING *;";
        
            using NpgsqlDataReader reader = sqlCommand.ExecuteReader();
        
            UserDb editUserDb = new UserDb();
        
            while (reader.Read())
            {
                editUserDb = new UserDb(
                    Convert.ToInt32(reader["user_id"]),
                    Convert.ToString(reader["username"]),
                    Convert.ToString(reader["password"]),
                    Convert.ToString(reader["email"]),
                    Convert.ToString(reader["phone"]),
                    Convert.ToString(reader["name"]),
                    Convert.ToBoolean(reader["isDeleted"]),
                    Convert.ToString(reader["role"])
                );
            }
            
            _connection.Close();
            return editUserDb;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool Delete(string email)
    {
        try
        {
            _connection.Open();
            
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.AddWithValue("@Email", email);
            sqlCommand.CommandText = "UPDATE users SET \"isDeleted\" = true WHERE email = @Email RETURNING \"isDeleted\"";
            
            bool isDeleted = false;
            object result = sqlCommand.ExecuteScalar();
            if (result != null)
            {
                isDeleted = (bool)result;
            }
        
            _connection.Close();
            return isDeleted;
        }
    
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}