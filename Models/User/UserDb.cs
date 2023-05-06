using System.ComponentModel.DataAnnotations;

namespace SportCenter.WebAPI.Models.User;

public class UserDb
{
    [Key]
    public Int32 user_id { get; set; }
    [Required]
    public string username { get; set; }
    [Required]
    public string password { get; set; }
    [Required]
    public string email { get; set; }
    [Required]
    public string phone { get; set; }
    [Required]
    public string name { get; set; }
    public bool isDeleted { get; set; }
    public string role { get; set; }

    public UserDb(int userId, string username, string password, string email, string phone, string name, bool isDeleted, string role)
    {
        user_id = userId;
        this.username = username;
        this.password = password;
        this.email = email;
        this.phone = phone;
        this.name = name;
        this.isDeleted = isDeleted;
        this.role = role;
    }

    public UserDb()
    {
        
    }
}