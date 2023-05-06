namespace SportCenter.WebAPI.Models.User;

public class UserView
{
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }
    public string phone { get; set;}
    public string name { get; set;}
    public string role { get; set;}

    public UserView(string username, string password, string email, string phone, string name, string role)
    {
        this.username = username;
        this.password = password;
        this.email = email;
        this.phone = phone;
        this.name = name;
        this.role = role;
    }
    
}