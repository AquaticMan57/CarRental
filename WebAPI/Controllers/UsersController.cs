using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserService _userService;
        public UsersController(IUserService userService) { _userService = userService; }
        [HttpGet("getall")]
        public IActionResult GetUsers() { var result = _userService.GetAll(); if (result.Success) { return Ok(result); } return BadRequest(result); }
        [HttpGet("getuserbyid")]
        public IActionResult GetUserById(int id) { var result = _userService.GetUserById(id); if (result.Success) { return Ok(result); } return BadRequest(result); }
        [HttpPost("add")]
        public IActionResult Add(User user) { var result = _userService.Add(user); if (result.Success) { return Ok(result); } return BadRequest(result); }
        [HttpPost("delete")]
        public IActionResult Delete(User user) { var result = _userService.Delete(user); if (result.Success) { return Ok(result); } return BadRequest(result); }
        [HttpPost("update")]
        public IActionResult Update(User user) { var result = _userService.Update(user); if (result.Success) { return Ok(result); } return BadRequest(result); }
        [HttpGet("getuserbycarid")]
        public IActionResult GetByCarId(int id)
        {
            var result = _userService.GetUserByCarId(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getuserbymail")]
        public IActionResult GetByMail(string mail) 
        {
            var result = _userService.GetByMail(mail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getuserbyusername")]
        public IActionResult GetUserByUsername(string name)
        {
            var result = _userService.GetUserByUserName(name);
            if (result.Success)
            {
                Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getuserdetailsbyid")]
        public IActionResult GetUserDetailsById(int id)
        {
            var result = _userService.GetUserDetailsByUserId(id);
            if (result.Success) 
            { 
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getuserdetails")]
        public IActionResult GetUserDtos()
        {
            var result = _userService.GetAllUserDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
