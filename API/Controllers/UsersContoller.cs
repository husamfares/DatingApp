using API.Data;
using API.DOTs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.Execution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

// [ApiController]
// [Route ("api/[controller]")]
public class UsersController(IUserRepository userRepository) : BaseApiController
{
[Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>>GetUsers()
    {
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }


   [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>>GetUsers(string username)
    {
        var user = await userRepository.GetMemberAsync(username);
            if (user == null)
                return NotFound();

            return user;
    }
}
