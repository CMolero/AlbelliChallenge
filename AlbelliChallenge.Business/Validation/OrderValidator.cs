using AlbelliChallenge.Business.Models;
using AlbelliChallenge.Data.Models;
using FluentValidation;

namespace AlbelliChallenge.Business.Validator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(c => c.OrderId).NotNull().NotEmpty().WithMessage("OrderId is required");
            RuleForEach(x => x.Products).SetValidator(new ProductValidator());
        }
    }

    public class ProductValidator : AbstractValidator<ProductDetails>
    {
        public ProductValidator()
        {
            RuleFor(c => c.ProductType)
                .NotNull().IsEnumName(typeof(Enums.ProductTypes), caseSensitive: false).WithMessage("ProductType is not valid");
            RuleFor(c => c.Quantity).NotNull().NotEmpty().WithMessage("Quantity is required");
        }
    }
}