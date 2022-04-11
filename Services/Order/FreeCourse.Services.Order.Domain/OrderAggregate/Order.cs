using FreeCourse.Services.Order.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedDate { get; private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {
        }

        public Order(Address address, string buyerId)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
        }

        public void AddOrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            var existsOrderItem = _orderItems.Any(x => x.ProductId == productId);
            if (!existsOrderItem)
            {
                var orderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(orderItem);
            }

        }

        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
