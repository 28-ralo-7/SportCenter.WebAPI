using System.Data;
using Npgsql;
using SportCenter.WebAPI.Connection;
using SportCenter.WebAPI.Interfaces;
using SportCenter.WebAPI.Models.Session;

namespace SportCenter.WebAPI.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly NpgsqlConnection _connection;

    public SessionRepository(DbConnection dbConnection)
    {
        _connection = dbConnection.GetConnection();
    }
    public List<SessionDb> Get()
    {
        try
        {
            List<SessionDb> sessionDbs = new List<SessionDb>();
        
            _connection.Open();
            
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT * FROM sessions;";
        
            using NpgsqlDataReader reader = sqlCommand.ExecuteReader();

            while (reader.Read())
            {
                SessionDb sessionDb = new SessionDb(
                    Convert.ToInt32(reader["session_id"]),
                    Convert.ToDateTime(reader["session_start"]),
                    Convert.ToDateTime(reader["session_end"]),
                    Convert.ToInt32(reader["session_capacity"]),
                    Convert.ToBoolean(reader["isDeleted"])
                );
                sessionDbs.Add(sessionDb);
            }
            _connection.Close();
            return sessionDbs;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool Create(SessionDb entity)
    {
        try
        {
            bool flag = CheckExistsSession(entity.session_start, entity.session_end);
            if (flag)
            {
                return flag;
            }
            _connection.Open();
        
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            
            sqlCommand.Parameters.AddWithValue("@StartTime", entity.session_start);
            sqlCommand.Parameters.AddWithValue("@EndTime", entity.session_end);
            sqlCommand.Parameters.AddWithValue("@Capacity", entity.session_capacity);

            sqlCommand.CommandText = "INSERT INTO sessions (session_start, session_end, session_capacity) VALUES (@StartTime, @EndTime, @Capacity);";
            sqlCommand.ExecuteScalar();
            
            _connection.Close();
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public SessionDb Update(int id, SessionDb entity)
    {
        try
        {
            _connection.Open();
        
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@StartTime", entity.session_start);
            sqlCommand.Parameters.AddWithValue("@EndTime", entity.session_end);
            sqlCommand.Parameters.AddWithValue("@Capacity", entity.session_capacity);
            
            sqlCommand.CommandText = "UPDATE sessions SET session_start = @StartTime, session_end = @EndTime, session_capacity = @Capacity WHERE session_id = @Id RETURNING *;";
        
            using NpgsqlDataReader reader = sqlCommand.ExecuteReader();

            SessionDb sessionDb = new SessionDb();

            while (reader.Read())
            {
                sessionDb = new SessionDb(
                    Convert.ToInt32(reader["session_id"]),
                    Convert.ToDateTime(reader["session_start"]),
                    Convert.ToDateTime(reader["session_end"]),
                    Convert.ToInt32(reader["session_capacity"]),
                    Convert.ToBoolean(reader["isDeleted"])
                );
            }
            _connection.Close();
            return sessionDb;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool Delete(int id)
    {
        try
        {
            _connection.Open();
            
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.CommandText = "UPDATE sessions SET \"isDeleted\" = true WHERE session_id = @Id RETURNING \"isDeleted\"";
            
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

    public bool CheckExistsSession(DateTime start_time, DateTime end_time)
    {
        try
        {
            _connection.Open();
        
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            
            sqlCommand.Parameters.AddWithValue("@Start", start_time);
            sqlCommand.Parameters.AddWithValue("@End", end_time);

            sqlCommand.CommandText = "SELECT COUNT(*) FROM sessions WHERE session_start = @Start OR session_end = @End";
            
            int count = Convert.ToInt32(sqlCommand.ExecuteScalar());
            _connection.Close();
            if (count != 0) // Если уже есть запись с такими параметрами, то не добавляем новую
            {
                return true;
            }
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}