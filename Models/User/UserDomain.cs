namespace SportCenter.WebAPI.Models.User;

public class UserDomain
{
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string phone { get; set; }
    public string name { get; set; }
    public bool isDeleted { get; set; }
    public string role { get; set; }

    public UserDomain(string username, string password, string email, string phone, string name, bool isDeleted, string role)
    {
        this.username = username;
        this.password = password;
        this.email = email;
        this.phone = phone;
        this.name = name;
        this.isDeleted = isDeleted;
        this.role = role;
    }
}