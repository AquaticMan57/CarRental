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
            var result = _service.GetAll();
            if (result.Success)
            {
                return Ok(result);
                
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbyid")]
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
        [HttpPost("update")]
        public IActionResult Update(Colors colors)
        {
            var result = _service.Update(colors);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Colors colors)
        {
            var result = _service.Delete(colors);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("deletebyid")]
        public IActionResult DeleteById(int id)
        {
            var result = _service.DeleteById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

    }
}
