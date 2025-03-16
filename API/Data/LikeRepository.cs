using API.DOTs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class LikeRepository(DataContext context, IMapper mapper) : ILikedRepository
    {
        public void AddLike(UserLike like)
        {
            context.Likes.Add(like);
        }

        public void DeleteLike(UserLike like)
        {
            context.Likes.Remove(like);
        }

        public async Task<IEnumerable<int>> GetCurrentUserLikeIds(int currentUserId)
        {
            return await context.Likes
                .Where(x => x.SourceUserId == currentUserId)
                .Select(x => x.TargetUserId)
                .ToListAsync();
        }

        public async Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId)
        {
            return await context.Likes.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<PagedList<MemberDto>> GetUserLikes(LikesParams likesParams)
        {
            IQueryable<MemberDto> query;

            switch (likesParams.Predicate)
            {
                case "liked":
                    query = context.Likes
                        .Where(like => like.SourceUserId == likesParams.UserId)
                        .Select(like => like.TargetUser)
                        .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                    break;

                case "likedBy":
                    query = context.Likes
                        .Where(like => like.TargetUserId == likesParams.UserId)
                        .Select(like => like.SourceUser)
                        .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                    break;

                default:
                    var likedIds = await GetCurrentUserLikeIds(likesParams.UserId);
                    query = context.Likes
                        .Where(x => x.TargetUserId == likesParams.UserId && likedIds.Contains(x.SourceUserId))
                        .Select(x => x.SourceUser)
                        .ProjectTo<MemberDto>(mapper.ConfigurationProvider);
                    break;
            }

            return await PagedList<MemberDto>.CreateAsync(query, likesParams.PageNumber, likesParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
