using Common.Models.Distribution;
using Microsoft.AspNetCore.Mvc;
using SupplierApp.Services;
using System;
using System.Threading.Tasks;
using Validation.Validators;

namespace SupplierApp.Controllers {
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class DistributionController : ControllerBase {
		private readonly DistributionService _service;

		public DistributionController(DistributionService service) {
			_service = service;
		}

		[HttpPost]
		public async Task<IActionResult> SaveDistribution([FromBody] DistributionPostModel model) {
			var validator = new DistributionValidator();
			var result = await validator.ValidateAsync(model);

			if (!result.IsValid) {
				var error = string.Join(';', result.Errors);
				throw new Exception(error);
			}
			await _service.SaveDistribution(model);

			return Ok();
		}
	}
}
