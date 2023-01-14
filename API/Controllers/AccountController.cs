using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : ChironAPIController
{
   
    private readonly DataContext _context;
    public AccountController(DataContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RegisterDTO registerDTO) 
    {
        if(await UserExists(registerDTO.Username.ToLower())) return BadRequest("Username is taken");

        //Used to hash the password. Key is saved as PasswordSalt.
        using var salter = new HMACSHA512();
        var user = new User
        {
            UserName = registerDTO.Username.ToLower(),
            PasswordHash = salter.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
            PasswordSalt = salter.Key    
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user; //TODO update this to something useful. User is a placeholder until a class is built for this.
    }  
    private async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
}
