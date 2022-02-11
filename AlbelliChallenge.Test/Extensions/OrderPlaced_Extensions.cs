using AlbelliChallenge.Business.Models;
using AlbelliChallenge.Data.Models;
using Bogus;
using System.Linq;

namespace AlbelliChallenge.Test.Extensions
{
    public static class OrderPlaced_Extensions
    {
        public static Order Build(this Order instance)
        {
            instance = new Faker<Order>()
                .RuleFor(o => o.OrderId, 1)
                .RuleFor(o => o.Products,
                        new Faker<ProductDetails>()
                            .RuleFor(p => p.ProductType, new Faker().PickRandom<Enums.ProductTypes>().ToString())
                            .RuleFor(p => p.Quantity, new Faker().Random.Number(min: 1, max: 10))
                            .Generate(10))
                .Generate();

            return instance;
        }

        public static Order WithInvalidOrderId(this Order instance)
        {
            instance.OrderId = 0;

            return instance;
        }

        public static Order WithoutProductType(this Order instance)
        {
            instance.Products.ToList().FirstOrDefault().ProductType = string.Empty; 

            return instance;
        }
        public static Order WithInvalidProductType(this Order instance)
        {
            instance.Products.ToList().FirstOrDefault().ProductType = "cup";

            return instance;
        }
        public static Order WithNullProductType(this Order instance)
        {
            instance.Products.ToList().FirstOrDefault().ProductType = null;

            return instance;
        }

        public static Order WithoutQuantity(this Order instance)
        {
            instance.Products.ToList().FirstOrDefault().Quantity = 0;

            return instance;
        }
    }
}
