using Haelya.Application.DTOs.Category;
using Haelya.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Haelya.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCategories()
        {
            IEnumerable<CategoryDTO> categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategory(int id)
        {
            CategoryDTO? dto = await _categoryService.GetByIdAsync(id);
            return Ok(dto);
        }

        // POST: api/Category
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDTO dto)
        {
            var created = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetCategory), new { id = created.Id }, created);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO dto)
        {
            await _categoryService.UpdateAsync(id, dto);
            return NoContent();
        }

        // DELETE: api/Category/5 (désactivation logique)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DisableCategory(int id)
        {
            await _categoryService.DisableAsync(id);
            return NoContent();
        }


    }
}
