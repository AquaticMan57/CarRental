using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        IColorService _service;
        public ColorsController(IColorService service)
        {
            _service= service;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _service.GetAllColors();
            if (result.Success)
            {
                return Ok(result);
                
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbycolorid")]
        public IActionResult Get(int id)
        {
            var result = _service.GetColorById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("add")]
        public IActionResult Add(Colors colors)
        {
            var result = _service.Add(colors);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
