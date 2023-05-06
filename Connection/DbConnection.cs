using Npgsql;

namespace SportCenter.WebAPI.Connection;

public class DbConnection
{
    private readonly string connectionString;

    public DbConnection(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("DbConnectionKey");
    }
    public NpgsqlConnection GetConnection() {
        var connection = new NpgsqlConnection(connectionString);
        return connection;
    }
}