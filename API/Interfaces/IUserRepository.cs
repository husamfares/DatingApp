using API.DOTs;
using API.Entities;
using API.Helpers;
using AutoMapper.Execution;

namespace API.Interfaces;

public interface IUserRepository
{
    void Updates(AppUser user);

    Task<bool> SaveAllAsync();
    Task<IEnumerable<AppUser>>GetUsersAsync();
    Task<AppUser?>GetUserByIdAsync(int id);
    Task<AppUser?>GetUserByUserNameAsync(string userName);

    Task<PagedList<MemberDto>>GetMembersAsync(UserParams userParams);
    Task<MemberDto?>GetMemberAsync(string username);

}