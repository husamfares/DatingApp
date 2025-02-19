using API.DOTs;
using API.Entities;
using API.Extenstions;
using AutoMapper;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
     public AutoMapperProfiles()
     {
        CreateMap<AppUser , MemberDto>()
        .ForMember(d => d.Age , o => o.MapFrom(s => s.DataOfBirth.CalculateAge()))
        .ForMember(d => d.PhotoUrl , o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));

        CreateMap<Photo , PhotoDto>();

     }
}