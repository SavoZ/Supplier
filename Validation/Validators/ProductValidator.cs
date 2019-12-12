using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Common.Models.Product;
using FluentValidation;

namespace Validation.Validators {
	public class ProductValidator: AbstractValidator<ProductPostModel> {
		public ProductValidator() {
			RuleFor(product => product.Name).NotNull();
			RuleFor(product => product.Limits).Must(x => x.Count > 0).WithMessage("Limit is required");

		}

	}
}
