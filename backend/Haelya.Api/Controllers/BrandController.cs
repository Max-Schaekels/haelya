using Haelya.Application.DTOs.Brand;
using Haelya.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haelya.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // Public (frontend)
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllVisible()
        {
            IEnumerable<BrandDTO> brands = await _brandService.GetAllVisibleAsync();
            return Ok(brands);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            BrandDTO? brand = await _brandService.GetByIdAsync(id);
            return Ok(brand);
        }

        // Admin only
        [HttpGet("admin")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAllAdmin()
        {
            IEnumerable<BrandDTO> brands = await _brandService.GetAllAdminAsync();
            return Ok(brands);
        }


        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Create([FromBody] BrandCreateDTO dto)
        {
            BrandDTO brand = await _brandService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = brand.Id }, brand);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Update(int id, [FromBody] BrandUpdateDTO dto)
        {
            await _brandService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpPut("{id}/active")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> SetActive(int id, [FromBody] bool isActive)
        {
            await _brandService.SetActiveAsync(id, isActive);
            return NoContent();
        }
    }
}
