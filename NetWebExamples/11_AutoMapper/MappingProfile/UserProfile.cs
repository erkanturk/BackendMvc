using _11_AutoMapper.Dto;
using _11_AutoMapper.Models;
using AutoMapper;

namespace _11_AutoMapper.MappingProfile
{
    public class UserProfile: Profile
    {
        //User=>UserDto dönüşümü için bir eşleme tanımlayın
        //Burada firstname ve lastname'i birleştirerek fullname oluşturuyoruz
        public UserProfile()
        {
            CreateMap<User, UserDto>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // Tersine dönüşüm için UserDto'dan User'a da bir eşleme tanımlayabilirsiniz
            CreateMap<UserDto, User>();
        }
    }
}
