using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Core.Interfaces.Repositories;
using ZintegrujPL.Core.Interfaces.Services;
using ZintegrujPL.DTOs;

namespace ZintegrujPL.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository productRepository,
            IInventoryRepository inventoryRepository,
            IPriceRepository priceRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
            _priceRepository = priceRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDetailsDTO>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            var productDetailsList = new List<ProductDetailsDTO>();

            foreach (var product in products)
            {
                var inventories = await _inventoryRepository.GetInventoryByProductIdAsync(product.ProductId);
                var prices = await _priceRepository.GetPricesByProductIdAsync(product.ProductId);

                var productDetails = _mapper.Map<ProductDetailsDTO>(product);

                if (inventories != null && inventories.Any())
                {
                    var inventoryDetails = inventories.FirstOrDefault(); // Pobiera pierwszy element, dostosuj logikę wg potrzeb
                    // Przypisz wartości z inventory do productDetails
                    productDetails.InventoryQty = inventoryDetails.Qty;
                    productDetails.Unit = inventoryDetails.Unit;
                    productDetails.ShippingCost = inventoryDetails.ShippingCost;
                }

                if (prices != null && prices.Any())
                {
                    var priceDetails = prices.FirstOrDefault(); // Pobiera pierwszy element, dostosuj logikę wg potrzeb
                    
                    productDetails.NetProductPrice = priceDetails.Column3;
                    //productDetails.NetProductPriceAfterDiscount = priceDetails.NetProductPriceAfterDiscount;
                    //productDetails.VATRate = priceDetails.VATRate;
                    //productDetails.ShippingCost = priceDetails.NetPriceForProductLogisticUnit;
                    
                }

                productDetailsList.Add(productDetails);
            }

            return productDetailsList;
        }

        public async Task<ProductDetailsDTO> GetProductDetailsBySKUAsync(string sku)
        {
            var product = await _productRepository.GetProductBySKUAsync(sku);
            if (product == null)
            {
                // Obsługa przypadku, gdy produkt nie istnieje
                return null;
            }

            var inventoryDetails = await _inventoryRepository.GetInventoryBySKUAsync(sku);
            var priceDetails = await _priceRepository.GetPriceBySKUAsync(sku);

            var productDetails = _mapper.Map<ProductDetailsDTO>(product);

            if (inventoryDetails != null )
            {
                
                // Przypisz wartości z inventory do productDetails
                productDetails.InventoryQty = inventoryDetails.Qty;
                productDetails.Unit = inventoryDetails.Unit;
                productDetails.ShippingCost = inventoryDetails.ShippingCost;
            }

            if (priceDetails != null)
            {
                
                // Przypisz wartości z price do productDetails
                productDetails.NetProductPrice = priceDetails.Column6;
                //productDetails.NetProductPriceAfterDiscount = priceDetails.NetProductPriceAfterDiscount;
                //productDetails.VATRate = priceDetails.VATRate;
                //productDetails.ShippingCost = priceDetails.NetPriceForProductLogisticUnit;
               
            }

            return productDetails;
        }

       
    }
}
