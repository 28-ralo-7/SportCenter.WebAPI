using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Converters;

// конвертер между моделью базы данных и доменной моделью
public static class UserConverter
{
    public static UserDomain ConvertFromDb(UserDb user)
    {
        try
        {
            return new UserDomain(
                user.username,
                user.password,
                user.email,
                user.phone,
                user.name,
                user.isDeleted,
                user.role
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

    }

    public static UserDb ConvertToDb(UserDomain user)
    {
        try
        {
            return new UserDb(
                0,
                user.username,
                user.password,
                user.email,
                user.phone,
                user.name,
                user.isDeleted,
                user.role
            );
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public static UserView ConvertFromDomain(UserDomain user)
    {
        try
        {
            return new UserView(
                user.username,
                user.password,
                user.email,
                user.phone,
                user.name,
                user.role
                );
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public static UserDomain ConvertToDomain(UserView user)
    {
        try
        {
            return new UserDomain(
                user.username,
                user.password,
                user.email,
                user.phone,
                user.name,
                false,
                user.role
                );
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }

    }
}
