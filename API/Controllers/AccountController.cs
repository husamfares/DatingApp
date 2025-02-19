using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DOTs;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context , ITokenService tokenService) : BaseApiController
{
    [HttpPost ("register")]
    public async Task<ActionResult<UserDTO>>Register(RegisterDto registerDto)
    {
        if(await UserExists(registerDto.userName))
        {
            return BadRequest("username is taken");
        }
        
        return Ok();
        // using var hmac = new HMACSHA512();
        //     var user = new AppUser
        //     {
        //         Name = registerDto.userName,
        //         PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
        //         PasswordSalt = hmac.Key
        //     };

        

        // context.Users.Add(user);
        // await context.SaveChangesAsync();
        // return new UserDTO 
        // {
        //     userName = user.Name,
        //     Token = tokenService.CreateToken(user)

        // };

        
    }

  [HttpPost("login")]
public async Task<ActionResult<UserDTO>> Login(LoginDto loginDto)
{
    var user = await context.Users.FirstOrDefaultAsync(x => x.Name == loginDto.userName.ToLower());

    if (user == null) return Unauthorized("Invalid User");

    // Here we use the PasswordSalt that is stored in the database for this user
    using var hmac = new HMACSHA512(user.PasswordSalt);
    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.password));



    for (int i = 0; i < computedHash.Length; i++)
    {
        if (computedHash[i] != user.PasswordHash[i]) // Compare byte-by-byte
        {
            return Unauthorized("Invalid User");
        }
    }

    return new UserDTO
    {
        userName = user.Name,
        Token = tokenService.CreateToken(user)
    };
}


    
    public async Task<bool> UserExists(string username)
    {
        return await context.Users.AnyAsync(x => x.Name.ToLower() == username.ToLower());
    }
}