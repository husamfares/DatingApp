using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [ApiController]
// [Route ("api/[controller]")]
public class UsersController(DataContext context) : BaseApiController
{

[AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>>GetUsers()
    {
        var users = await context.Users.ToListAsync();
        return Ok(users);
    }

[Authorize]
   [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>>GetUsers(int id)
    {
        var users = await context.Users.FindAsync(id);
            if (users == null)
                return NotFound();

        return Ok(users);
    }
}
