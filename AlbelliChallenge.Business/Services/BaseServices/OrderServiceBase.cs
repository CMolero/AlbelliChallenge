using AlbelliChallenge.Business.Models;
using AlbelliChallenge.Business.Validator;
using AlbelliChallenge.Data.DTOs;
using AlbelliChallenge.Data.Models;
using AutoMapper;
using System;
using System.Linq;

namespace AlbelliChallenge.Business.Services
{
    public class OrderServiceBase
    {
        protected IMapper _mapper;

        protected static OrderDetailsPersistence CalculateRequiredBinWidth(OrderDetailsPersistence orderDetailsPersistence)
        {
            double requiredBinWidth = 0.0;

            var productsGroupBy = orderDetailsPersistence.Products
                .GroupBy(obj => new { obj.ProductType })
                .OrderByDescending(obj => obj.Key.ProductType);

            foreach (var products in productsGroupBy.ToList())
            {
                requiredBinWidth += CalculateWidth(products.Key.ProductType, products.ToList().FirstOrDefault().Quantity);
            }

            orderDetailsPersistence.RequiredBinWidth = requiredBinWidth;

            return orderDetailsPersistence;
        }

        private static double CalculateWidth(Enums.ProductTypes productType, int quantity)
        {
            double requiredBinWidth = 0.0;

            switch (productType)
            {
                case Enums.ProductTypes.PhotoBook:
                    requiredBinWidth = quantity * 19;
                    break;
                case Enums.ProductTypes.Calendar:
                    requiredBinWidth = quantity * 10;
                    break;
                case Enums.ProductTypes.Canvas:
                    requiredBinWidth = quantity * 16;
                    break;
                case Enums.ProductTypes.Cards:
                    requiredBinWidth = quantity * 4.7;
                    break;
                case Enums.ProductTypes.Mug:
                    requiredBinWidth = quantity % 4 == 0 ? quantity / 4 * 94 : (quantity / 4) * 94 + 94;
                    break;
                default:
                    break;
            }
            return requiredBinWidth;
        }

        protected static bool IsValidOrder(Order order)
        {
            var validator = new OrderValidator().Validate(order);
            return validator.IsValid ? validator.IsValid : throw new ArgumentException(validator.ToString(", "));
        }

        protected static bool IsValidOrderId(int orderId)
        {
            return orderId > 0 ? true : throw new ArgumentException("OrderId is not valid");
        }
    }
}