using API.DOTs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface ILikedRepository
{
    
    Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId);
    Task<UserLike?> GetUserLike(int sourceUserId, int likedUserId);

    Task<PagedList<MemberDto>> GetUserLikes(LikesParams likesParams);


    void DeleteLike(UserLike like);

    void AddLike(UserLike like);

    Task<bool> SaveAllAsync();


}