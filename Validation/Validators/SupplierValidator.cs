using Common.Models.Supplier;
using FluentValidation;

namespace Validation.Validators {
	public class SupplierValidator : AbstractValidator<OrderModel>{
		public SupplierValidator() {
			RuleFor(order => order.ProductId).NotNull().WithMessage("Please select product");
			RuleFor(order => order.Quantity).NotNull().WithMessage("Please enter quantity");
		}
	}
}
