using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models.Shop;
using Microsoft.AspNetCore.Mvc;
using SupplierApp.Services;
using Validation.Validators;

namespace SupplierApp.Controllers {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class ShopController : ControllerBase {
		private readonly ShopService _service;

		public ShopController(ShopService service) {
			_service = service;
		}

		[HttpPost]
		public async Task<IActionResult> SaveShop([FromBody] ShopPostModel model) {
			var validator = new ShopValidator();
			var result = await validator.ValidateAsync(model);

			if (!result.IsValid) {
				var error = string.Join(';', result.Errors);
				throw new Exception(error);
			}
			await _service.SaveShop(model);
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> GetShops() {
			var result = await _service.GetShops();

			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetShopsByProduct(int productId) {
			var result = await _service.GetShopsByProductId(productId);

			return Ok(result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteShop(int shopId) {
			 await _service.DeleteShop(shopId);

			return Ok();
		}

	}


}
