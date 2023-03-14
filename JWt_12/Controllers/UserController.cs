using JWt_12.Constant;
using JWt_12.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWt_12.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Datas.users);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = Datas.users.FirstOrDefault(m => m.Id == id);
            return Ok(entity);
        } 
        [Authorize(Roles ="admin")]
        [HttpPost]
        public IActionResult Add(User user)
        {
            Datas.users.Add(user);
            return Ok();
        }
    }
}
