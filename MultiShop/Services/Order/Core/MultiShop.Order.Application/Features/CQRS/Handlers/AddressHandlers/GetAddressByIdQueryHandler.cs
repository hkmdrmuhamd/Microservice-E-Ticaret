﻿using MultiShop.Order.Application.Features.CQRS.Queries.AddresQueries;
using MultiShop.Order.Application.Features.CQRS.Results.AddressResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.AddressHandlers
{
    public class GetAddressByIdQueryHandler
    {
        private readonly IRepository<Address> _repository;

        public GetAddressByIdQueryHandler(IRepository<Address> repository)
        {
            _repository = repository;
        }

        public async Task<GetAddressByIdQueryResult> Handle(GetAddressByIdQuery request)
        {
            var values = await _repository.GetByIdAsync(request.Id);

            return new GetAddressByIdQueryResult
            {
                AddressId = values.AddressId,
                UserId = values.UserId,
                City = values.City,
                District = values.District,
                Detail = values.Detail
            };
        }
    }
}
