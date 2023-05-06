using System.Data;
using Npgsql;
using SportCenter.WebAPI.Connection;
using SportCenter.WebAPI.Interfaces;
using SportCenter.WebAPI.Models.Auth;
using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly NpgsqlConnection _connection;
    private readonly IUserRepository _userRepository;
    
    public AuthRepository(DbConnection dbConnection, IUserRepository userRepository)
    {
        _connection = dbConnection.GetConnection();
        _userRepository = userRepository;
    }
    
    public bool Register(UserDb newUserDb)
    {
        try
        {
            var checkUserEmail = _userRepository.GetByEmail(newUserDb.email);
            if (!string.IsNullOrWhiteSpace(checkUserEmail.email))
            {
                return false;
            }
            _connection.Open();
        
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            
            sqlCommand.Parameters.AddWithValue("@Username", newUserDb.username);
            sqlCommand.Parameters.AddWithValue("@Password", newUserDb.password);
            sqlCommand.Parameters.AddWithValue("@Email", newUserDb.email);
            sqlCommand.Parameters.AddWithValue("@Phone", newUserDb.phone);
            sqlCommand.Parameters.AddWithValue("@Name", newUserDb.name);
            sqlCommand.Parameters.AddWithValue("@Role", newUserDb.role);

            sqlCommand.CommandText = "INSERT INTO users (username, password, email, phone, name, role) " +
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
    
    public UserDb Login(Auth authData)
    {
        _connection.Open();
        
        UserDb userDb = new UserDb();
        
        using NpgsqlCommand sqlCommand = _connection.CreateCommand();
        sqlCommand.CommandType = CommandType.Text;
        sqlCommand.Parameters.AddWithValue("@Login", authData.login);
        sqlCommand.Parameters.AddWithValue("@Password", authData.password);
        sqlCommand.CommandText = "SELECT * FROM \"users\" WHERE \"username\" = @Login AND \"password\" = @Password;";
        
        using NpgsqlDataReader reader = sqlCommand.ExecuteReader();
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
}