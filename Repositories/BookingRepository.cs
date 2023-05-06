using System.Data;
using Npgsql;
using SportCenter.WebAPI.Connection;
using SportCenter.WebAPI.Interfaces;
using SportCenter.WebAPI.Models.Booking;

namespace SportCenter.WebAPI.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly NpgsqlConnection _connection;

    public BookingRepository(DbConnection dbConnection)
    {
        _connection = dbConnection.GetConnection();
    }
    public List<BookingDb> Get()
    {
        
        List<BookingDb> bookingDbs = new List<BookingDb>();
        try
        {
            _connection.Open();
            
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT * FROM bookings;";
        
            using NpgsqlDataReader reader = sqlCommand.ExecuteReader();
            
            while (reader.Read())
            {
                Console.WriteLine(reader);
                BookingDb bookingDb = new BookingDb(
                    Convert.ToInt32(reader["booking_id"]),
                    Convert.ToInt32(reader["user_id"]),
                    Convert.ToInt32(reader["session_id"]),
                    Convert.ToBoolean(reader["isDeleted"])
                    
                );
                bookingDbs.Add(bookingDb);
            }
            _connection.Close();
            return bookingDbs;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool Create(BookingDb entity)
    {
        try
        {
            bool flag = CheckExistsSession(entity.userId, entity.sessionId);
            if (flag)
            {
                return false;
            }

            _connection.Open();
        
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            
            sqlCommand.Parameters.AddWithValue("@UserId", entity.userId);
            sqlCommand.Parameters.AddWithValue("@SessionId", entity.sessionId);
            
            sqlCommand.CommandText = "INSERT INTO bookings (user_id, session_id) VALUES (@UserId, @SessionId);";
            sqlCommand.ExecuteScalar();
            
            _connection.Close();
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public BookingDb Update(int id, BookingDb entity)
    {
        try
        {
            _connection.Open();

            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@UserId", entity.userId);
            sqlCommand.Parameters.AddWithValue("@SessionId", entity.sessionId);

            sqlCommand.CommandText = "UPDATE bookings SET user_id = @UserId, session_id = @SessionId WHERE booking_id = @Id RETURNING *;";
        
            using NpgsqlDataReader reader = sqlCommand.ExecuteReader();

            BookingDb bookingDb = new BookingDb();
            
            while (reader.Read())
            {
                bookingDb = new BookingDb(
                    Convert.ToInt32(reader["booking_id"]),
                    Convert.ToInt32(reader["user_id"]),
                    Convert.ToInt32(reader["session_id"]),
                    Convert.ToBoolean(reader["isDeleted"])
                );
            }
            _connection.Close();
            return bookingDb;
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
            sqlCommand.CommandText = "UPDATE bookings SET \"isDeleted\" = true WHERE booking_id = @Id RETURNING \"isDeleted\"";
            
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
    
    public bool CheckExistsSession(int userId, int sessionId)
    {
        try
        {
            _connection.Open();
        
            using NpgsqlCommand sqlCommand = _connection.CreateCommand();
            sqlCommand.CommandType = CommandType.Text;
            
            sqlCommand.Parameters.AddWithValue("@UserId", userId);
            sqlCommand.Parameters.AddWithValue("@SessionId", sessionId);

            sqlCommand.CommandText = "SELECT COUNT(*) FROM bookings WHERE user_id = @UserId AND session_id = @SessionId";
            
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