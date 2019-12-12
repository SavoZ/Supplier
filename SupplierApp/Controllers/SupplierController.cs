using System;
using System.Threading.Tasks;
using Common.Models.Supplier;
using Microsoft.AspNetCore.Mvc;
using SupplierApp.Services;
using Validation.Validators;

namespace SupplierApp.Controllers {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class SupplierController : ControllerBase {
		private readonly SupplierService _service;

		public SupplierController(SupplierService service) {
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> CalculateOrder([FromQuery] OrderModel model) {
			var validator = new SupplierValidator();
			var result = await validator.ValidateAsync(model);

			if (!result.IsValid) {
				var error = string.Join(';', result.Errors);
				throw new Exception(error);
			}
			var order = await _service.CalculateOrder(model);

			return Ok(order);
		}
	}
}
