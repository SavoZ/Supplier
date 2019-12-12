using System.Threading.Tasks;
using Common.Models.Product;
using Microsoft.AspNetCore.Mvc;
using SupplierApp.Services;
using Validation.Validators;
using System;

namespace SupplierApp.Controllers {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ProductController : ControllerBase {
		private readonly ProductService _service;

		public ProductController(ProductService productService) {
			_service = productService;
		}

		[HttpPost]
		public async Task<IActionResult> SaveProduct([FromBody] ProductPostModel model) {
			var validator = new ProductValidator();
			var result = await validator.ValidateAsync(model);

			if (!result.IsValid) {
				var error = string.Join(';', result.Errors);
				throw new Exception(error);
			}
			await _service.SaveProduct(model);
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetProductById(int productId) {
			var model = await _service.GetProductById(productId);

			return Ok(model);
		}

		[HttpGet]
		public async Task<IActionResult> GetProducts() {
			var result = await _service.GetProducts();

			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetDropdownProducts() {
			var result = await _service.GetDropdownProducts();

			return Ok(result);
		}

	}
}
