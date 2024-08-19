using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class UpdateOrderDetailCommandHanler
    {
        private readonly IRepository<OrderDetail> _repository;

        public UpdateOrderDetailCommandHanler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task Handle(UpdateOrderDetailCommand request)
        {
            var orderDetail = await _repository.GetByIdAsync(request.OrderDetailId);
            orderDetail.ProductId = request.ProductId;
            orderDetail.ProductName = request.ProductName;
            orderDetail.ProductPrice = request.ProductPrice;
            orderDetail.ProductAmount = request.ProductAmount;
            orderDetail.ProductTotalPrice = request.ProductTotalPrice;
            orderDetail.OrderingId = request.OrderingId;
            await _repository.UpdateAsync(orderDetail);
        }
    }
}
