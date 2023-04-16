using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private IBrandService _brandService;
        public BrandsController(IBrandService service)
        {
            _brandService = service;
        }
        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
