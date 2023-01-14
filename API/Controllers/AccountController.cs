using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : ChironAPIController
{
   
    private readonly DataContext _context;
    public AccountController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(string username, string password) 
    {
        //Used to hash the password. Key is saved as PasswordSalt.
        using var salter = new HMACSHA512();
        var user = new User
        {
            UserName = username,
            PasswordHash = salter.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = salter.Key    
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user; //TODO update this to something useful. User is a placeholder until a class is built for this.
    }  
}
