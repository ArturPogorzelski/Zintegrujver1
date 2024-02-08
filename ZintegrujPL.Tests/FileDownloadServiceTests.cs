using AutoMapper;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZintegrujPL.Core.Interfaces.Repositories;
using ZintegrujPL.DTOs;
using ZintegrujPL.Infrastructure.Services;
using ZintegrujPL.Models;

namespace ZintegrujPL.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IInventoryRepository> _mockInventoryRepository;
        private readonly Mock<IPriceRepository> _mockPriceRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockInventoryRepository = new Mock<IInventoryRepository>();
            _mockPriceRepository = new Mock<IPriceRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new ProductService(_mockProductRepository.Object, _mockInventoryRepository.Object, _mockPriceRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task DownloadFileAsync_ShouldReturnFilePath_WhenFileIsDownloadedSuccessfully()
        {
            // Arrange
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            var messageHandler = new Mock<HttpMessageHandler>();
            messageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new ByteArrayContent(Encoding.UTF8.GetBytes("File content"))
                });

            var httpClient = new HttpClient(messageHandler.Object);
            httpClientFactoryMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);

            var fileDownloadService = new FileDownloadService(httpClientFactoryMock.Object);

            // Act
            var result = await fileDownloadService.DownloadFileAsync("http://example.com/file.csv", "localFolderPath", "file.csv");

            // Assert
            Assert.NotNull(result);
            // Additional asserts based on your logic for generating file path in DownloadFileAsync
        }
        [Fact]
        public async Task ProcessProductsFileAsync_ShouldProcessFileCorrectly_WhenFileIsValid()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var fileProcessingService = new FileProcessingService(productRepositoryMock.Object);
            var filePath = "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv"; // Użyj prawdziwej ścieżki do pliku testowego lub mocka systemu plików

            // Act
            await fileProcessingService.ProcessProductsFileAsync(filePath);

            // Assert
            productRepositoryMock.Verify(repo => repo.AddProductAsync(It.IsAny<Product>()), Times.AtLeastOnce);
        }
        [Fact]
        public async Task GetProductDetailsBySKUAsync_ShouldReturnProductDetails_WhenProductExists()
        {
            // Arrange
            var sku = "test-sku";
            var product = new Product
            {
                // Przykład użycia właściwych właściwości modelu produktu
                ID = 1,
                SKU = sku,
                Name = "Test Product",
                Category = "Test Category",
                ProducerName = "Test Producer",
                DefaultImage = "http://example.com/image.jpg",
                EAN = "1234567890123"
            };
            var inventory = new Inventory
            {
                // Przykład użycia właściwych właściwości modelu inwentaryzacji
                Qty = 100,
                SKU = sku,
                Unit = "Szt.",
                ShippingCost = 1m
            };
            var price = new Price
            {
                // Przykład użycia właściwych właściwości modelu ceny
                Column2 = sku,
                Column3 = 10m,
                Column4 = 8m,
                Column5 = 23,
                Column6 = 9m
            };
            var productDetailsDTO = new ProductDetailsDTO
            {
                // Przykład użycia właściwych właściwości DTO szczegółów produktu
                //SKU = sku,
                Name = "Test Product",
                EAN = "1234567890123",
                ProducerName = "Test Manufacturer",
                Category = "Test Category",
                DefaultImage = "http://example.com/image.jpg",
                InventoryQty = 100,
                Unit = "Szt.",
                ShippingCost = 2.0m,
                NetProductPrice = 8.0m,
                //NetProductPriceAfterDiscount = 10.0m,

               
                
            };

            _mockProductRepository.Setup(repo => repo.GetProductBySKUAsync(sku)).ReturnsAsync(product);
            _mockInventoryRepository.Setup(repo => repo.GetInventoryBySKUAsync(sku)).ReturnsAsync(inventory);
            _mockPriceRepository.Setup(repo => repo.GetPriceBySKUAsync(sku)).ReturnsAsync(price);
            _mockMapper.Setup(mapper => mapper.Map<ProductDetailsDTO>(It.IsAny<object>())).Returns(productDetailsDTO);

            // Act
            var result = await _service.GetProductDetailsBySKUAsync(sku);

            //Assert
                    Assert.NotNull(result);
            //Assert.Equal(productDetailsDTO.SKU, result.SKU);
            Assert.Equal(productDetailsDTO.Name, result.Name);
            Assert.Equal(productDetailsDTO.InventoryQty, result.InventoryQty);
            Assert.Equal(productDetailsDTO.NetProductPrice, result.NetProductPrice);
            Assert.Equal(productDetailsDTO.ProducerName, result.ProducerName);
            Assert.Equal(productDetailsDTO.Category, result.Category);
            
            //Assert.Equal(productDetailsDTO.NetProductPriceAfterDiscount, result.NetProductPriceAfterDiscount);
            Assert.Equal(productDetailsDTO.ShippingCost, result.ShippingCost);
            Assert.Equal(productDetailsDTO.Unit, result.Unit);
            Assert.Equal(productDetailsDTO.DefaultImage, result.DefaultImage);
        }

       
    }
}
