using System;
using System.Collections.Generic;
using System.Text;
using Common.Models.Shop;
using FluentValidation;

namespace Validation.Validators {
	public class ShopValidator : AbstractValidator<ShopPostModel> {
		
		public ShopValidator() {
			RuleFor(shop => shop.Name).NotNull().Length(3, 100);
		}
	}
}
