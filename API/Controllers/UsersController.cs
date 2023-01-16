
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
//TODO add Authorize tag
public class UsersController : ChironAPIController
{
    private readonly DataContext _context;

    public UsersController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers() //TODO might need to get rid of this
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserByID(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user;
    }

}
