using API.DOTs;
using API.Entities;
using API.Extenstions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers;

public class LikesController(ILikedRepository likedRepository) : BaseApiController
{

    [HttpPost("{targetUserId:int}")]
    public async Task<ActionResult> ToggleLike(int targetUserId)
    {
        var sourceUserId = User.GetUserid();
        if(sourceUserId == targetUserId) return BadRequest("You cannot Like yourself");

        var existingLike = await likedRepository.GetUserLike(sourceUserId, targetUserId);

        if(existingLike == null) 
        {
            var like = new UserLike
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };
            likedRepository.AddLike(like);
        }
        else
        {
            likedRepository.DeleteLike(existingLike);
        }

        if(await likedRepository.SaveAllAsync()) return Ok();

        return BadRequest("Failed to like user");
    }


    [HttpGet ("list")]
    public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserLikeIds()
    {
        return Ok(await likedRepository.GetCurrentUserLikeIds(User.GetUserid()));
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<MemberDto>>> GetUserLikes([FromQuery]LikesParams likesParams )
    {
        likesParams.UserId = User.GetUserid();
        var users = await likedRepository.GetUserLikes(likesParams);

        Response.AddPaginationHeader(users);
        return Ok(users);
    }
    
}