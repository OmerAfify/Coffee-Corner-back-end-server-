﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoffeeCorner.DTOs;
using Domains.Models;
using Microsoft.Extensions.Configuration;

namespace CoffeeCorner.Helpers.ValueResolvers
{
    public class OrderPictureUrlResolver : IValueResolver<OrderedItem, OrderedItemDTO, string>
    {
        private readonly IConfiguration _configuration;

        public OrderPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderedItem source, OrderedItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ProductItemOrdered.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.ProductItemOrdered.PictureUrl;
            }

            return null;
        }
    }
}
