using Common;
using Data.Contracts;
using Entities.User;
using Microsoft.AspNetCore.Mvc;
using WebFramework.Api;
using WebFramework.DTOs;

namespace MyApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository=userRepository;
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<User>> Get(int id,CancellationToken cancellationToken)
        {
            var user = await  _userRepository.GetByIdAsync(cancellationToken,id);
            if (user == null)
                return NotFound(); // The Implicit Operators for ApiResult has been Implemented
            return new ApiResult<User>(true,ApiResultStatusCode.Success,user);
        }

        [HttpPost]
        public async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                FullName = userDto.FullName,
                Age = userDto.Age,
                Gender = userDto.Gender,
                UserName = userDto.UserName,
                IsActive = true
            };
            if (await _userRepository.IsExistAsync(u => u.UserName == user.UserName, cancellationToken))
                return BadRequest("کاربری با این نام کاربری از قبل وجود دارد");
            await _userRepository.AddUser(user, userDto.Password, cancellationToken);
            return new ApiResult<User>(true,ApiResultStatusCode.Success,user);
        }
    }
}
