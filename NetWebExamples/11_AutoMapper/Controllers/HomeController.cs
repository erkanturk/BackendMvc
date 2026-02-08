
using _11_AutoMapper.Dto;
using _11_AutoMapper.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace _11_AutoMapper.Controllers
{
    public class HomeController : Controller
    {

        private readonly IMapper _mapper;
        public HomeController(IMapper mapper)
        {
            _mapper=mapper;//DI Dependency Injection ile IMapper'ý alýyoruz
        }

        public IActionResult Index()
        {
            User user = new User()
            {
                Id = 1,
                FirstName = "Hamza",
                LastName = "Altuntaþ",
                Email = "Yanlayanbmw@hamzababapro.com"
            };
            var userDto = _mapper.Map<UserDto>(user);
            return View(userDto);
        }

        public IActionResult Privacy()
        {
            return View();
        }

      
    }
}
