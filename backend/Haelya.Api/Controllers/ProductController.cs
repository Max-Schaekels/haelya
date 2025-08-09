using Haelya.Application.DTOs.Brand;
using Haelya.Application.DTOs.Common;
using Haelya.Application.DTOs.Product;
using Haelya.Application.Interfaces;
using Haelya.Application.Services;
using Haelya.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haelya.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // Public (frontend)
        [HttpGet]
        [AllowAnonymous]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetAllVisible([FromQuery] PaginationQueryDTO pagination)
        {
            PagedResultDTO<ProductDTO> products = await _productService.GetAllVisibleAsync(pagination);
            return Ok(products);
        }

        [HttpGet("slug/{slug}")]
        [AllowAnonymous]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            ProductDTO? dto = await _productService.GetBySlugAsync(slug);
            return Ok(dto);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            ProductDTO? product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("visible")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilteredVisible([FromQuery] ProductFilterPublicDTO filter)
        {
            PagedResultDTO<ProductDTO> result = await _productService.GetFilteredVisibleAsync(filter);
            return Ok(result);
        }

        //Admin
        [HttpGet("admin")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetAllAdmin([FromQuery] PaginationQueryDTO pagination)
        {
            PagedResultDTO<ProductDTO> products = await _productService.GetAllAdminAsync(pagination);
            return Ok(products);
        }

        [HttpGet("admin/filtered")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> GetFilteredAdmin([FromQuery] ProductFilterAdminDTO filter)
        {
            PagedResultDTO<ProductDTO> result = await _productService.GetFilteredAdminAsync(filter);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO dto)
        {
            ProductDTO product = await _productService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> UpdateInfos(int id, [FromBody] ProductUpdateDTO dto)
        {
            await _productService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpPut("price")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> UpdatePrice([FromBody] ProductUpdatePriceDTO dto)
        {
            await _productService.UpdatePriceAsync(dto);
            return NoContent();
        }

        [HttpPut("margin")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> UpdateMargin([FromBody] ProductUpdateMarginDTO dto)
        {
            await _productService.UpdateMarginAsync(dto);
            return NoContent();
        }

        [HttpPut("{id}/active")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> SetActive(int id, [FromBody] bool isActive)
        {
            await _productService.SetActiveAsync(id, isActive);
            return NoContent();
        }

        [HttpPut("{id}/featured")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> SetFeatured(int id, [FromBody] bool featured)
        {
            await _productService.SetFeaturedAsync(id, featured);
            return NoContent();
        }

        [HttpPut("{id}/in-slide")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> SetInSlide(int id, [FromBody] bool inSlide)
        {
            await _productService.SetInSlideAsync(id, inSlide);
            return NoContent();
        }

        

    }
}
