using System.Collections.Generic;
using Common.Models.Distribution;
using Common.Models.Shop;
using FluentValidation;

namespace Validation.Validators {
	public class DistributionValidator : AbstractValidator<DistributionPostModel> {

		public DistributionValidator() {
			RuleFor(distr => distr.ProductId).NotNull().WithMessage("Please select product");
			RuleFor(distr => distr.Shops).Must(PercentageValidation).WithMessage("All percentages must have value and be positive");
			RuleFor(distr => distr.Shops).Must(ShopValidator).WithMessage("Sum of percentages for one limit must be less than 100");
		}

		private bool PercentageValidation(List<ShopDistributionModel> shops) {
			var isValid = true;
			for (int i = 0; i < shops.Count; i++) {
				for (int j = 0; j < shops[i].ProductDistribution.Count; j++) {
					var percentage = shops[i].ProductDistribution[j].PercentageDistribution;
					if (percentage.Value < 0.0m && !percentage.HasValue) {
						isValid = false;
						return isValid;
					}
				}
			}
			return isValid;
		}

		private bool ShopValidator(List<ShopDistributionModel> shops) {
			var isValid = true;
			for (int i = 0; i < shops.Count; i++) {
				decimal total = 0;
				for (int j = 0; j < shops[i].ProductDistribution.Count; j++) {
					for (int m = 0; m < shops.Count; m++) {
						total += shops[m].ProductDistribution[j].PercentageDistribution.Value;
					}
					if (total > 100) {
						isValid = false;
						return isValid;
					}
					total = 0;
				}

			}
			return isValid;
		}
	}


}
