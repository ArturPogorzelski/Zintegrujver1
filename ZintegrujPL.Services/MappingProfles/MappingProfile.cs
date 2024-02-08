using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Models; 
using ZintegrujPL.DTOs; 

namespace ZintegrujPL.Services.MappingProfles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDetailsDTO>()
                .ForMember(dto => dto.Name, conf => conf.MapFrom(ol => ol.Name))
                //.ForMember(dto => dto.SKU, conf => conf.MapFrom(ol => ol.SKU))
                .ForMember(dto => dto.EAN, conf => conf.MapFrom(ol => ol.EAN))
                .ForMember(dto => dto.ProducerName, conf => conf.MapFrom(ol => ol.ProducerName))
                .ForMember(dto => dto.Category, conf => conf.MapFrom(ol => ol.Category))
                .ForMember(dto => dto.DefaultImage, conf => conf.MapFrom(ol => ol.DefaultImage));

            CreateMap<Inventory, ProductDetailsDTO>()
                .ForMember(dto => dto.InventoryQty, conf => conf.MapFrom(ol => ol.Qty))
                .ForMember(dto => dto.Unit, conf => conf.MapFrom(ol => ol.Unit))
                .ForMember(dto => dto.ShippingCost, conf => conf.MapFrom(ol => ol.ShippingCost));

            CreateMap<Price, ProductDetailsDTO>()
                .ForMember(dto => dto.NetProductPrice, conf => conf.MapFrom(ol => ol.Column3));
                //.ForMember(dto => dto.NetProductPriceAfterDiscount, conf => conf.MapFrom(ol => ol.NetProductPriceAfterDiscount))
                //.ForMember(dto => dto.VATRate, conf => conf.MapFrom(ol => ol.VATRate))
                //.ForMember(dto => dto.ShippingCost, conf => conf.MapFrom(ol => ol.NetPriceForProductLogisticUnit));
        }
    }
}
