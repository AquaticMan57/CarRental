using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        ICarService _carService;
        public CarsController(ICarService carService)
        {
            _carService = carService;
        }
        //[Authorize]
        [HttpGet("getall")]
        public IActionResult GetAll()
        {

            var result = _carService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getbycolorid")]
        public IActionResult GetCarByColorId(int id) 
        { var result = _carService.GetCarsByColorId(id);
            if (result.Success) 
            { 
                return Ok(result); 
            }; 
            return BadRequest(result.Message); 
        }
        [HttpGet("getbybrandid")]
        public IActionResult GetCarsByBrandId(int id)
        {
            var result = _carService.GetCarsByBrandId(id);
            if (result.Success) { return Ok(result); }; return BadRequest(result);
        }
        [HttpGet("getcardetails")]
        public IActionResult GetCarDetails()
        {
            var result = _carService.GetCarDetails();
            if (result.Success) { return Ok(result); };return BadRequest(result);
        }
        [HttpGet("getcarsdetailbybrandid")]
        public IActionResult GetCarsDetailByBrandId(int id)
        {
            var result = _carService.GetCarsDetailByBrandId(id);
            if (result.Success) 
            { 
                return Ok(result); 
            }; 
            return BadRequest(result.Message);
        }
        [HttpGet("getcarsdetailbycolorid")]
        public IActionResult GetCarsDetailByColorId(int id)
        {
            var result = _carService.GetCarsDetailByColorId(id);
            if (result.Success) 
            { 
                return Ok(result); 
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getcarsdetailbybrandidandcolorid")]
        public IActionResult GetCarsDetailByBrandIdAndColorId(int id, int colorId)
        {
            var result = _carService.GetCarsDetailByBrandAndColorId(id, colorId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        [HttpGet("getcardetailbycarid")]
        public IActionResult GetCarDetailByCarId(int id)
        {
            var result = _carService.GetCarDetailByCarId(id);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result.Message);
        }
        [HttpPost("add")]
        public IActionResult Add(Car car) 
        {
            var result = _carService.Add(car);
            if (result.Success) { return Ok(result); }
            return BadRequest(result.Message);
        }
        [HttpPost("delete")]
        public IActionResult Delete(Car car)
        {
            var result = _carService.Delete(car);
            if (result.Success) { return Ok(result); }
            return BadRequest(result.Message);
        }
        [HttpPost("update")]
        public IActionResult Update(Car car)
        {
            var result = _carService.Update(car);
            if (result.Success) { return Ok(result); }
            return BadRequest(result.Message);
        }
        [HttpGet("getcarbyid")]
        public IActionResult GetCarById(int id)
        {
            var result = _carService.GetCarByCarId(id);
            if (result.Success) { return Ok(result); };
            return BadRequest(result.Message);
        }
    }
}
