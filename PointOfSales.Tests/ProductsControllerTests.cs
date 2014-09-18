using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using System.Web.Http;
using PointOfSales.Domain.Repositories;
using PointOfSales.Web.Controllers;
using PointOfSales.Domain.Model;
using System.Net;
using System.Net.Http;

public class ProductsControllerTests
{
    [Fact]
    public void ShouldReturnAllProducts()
    {
        var repositoryMock = new Mock<IProductRepository>();
        var expectedProducts = Enumerable.Empty<Product>();
        repositoryMock.Setup(r => r.GetAll()).Returns(expectedProducts);
        var controller = new ProductsController(repositoryMock.Object);

        var products = controller.Get();

        Assert.Equal(expectedProducts, products);
    }

    [Fact]
    public void ShouldSearchProductsByName()
    {
        var search = "iphone";
        var repositoryMock = new Mock<IProductRepository>();
        var expectedProducts = Enumerable.Empty<Product>();
        repositoryMock.Setup(r => r.GetByNameOrDescription(search)).Returns(expectedProducts);
        var controller = new ProductsController(repositoryMock.Object);
        
        var products = controller.Get(search);

        repositoryMock.VerifyAll();
        Assert.Equal(expectedProducts, products);
    }

    [Fact]
    public void ShouldSaveNewProduct()
    {
        var product = new Product();
        var repositoryMock = new Mock<IProductRepository>();
        repositoryMock.Setup(r => r.Add(product)).Returns(product);
        var controller = new ProductsController(repositoryMock.Object) { 
            Request = Mock.Of<HttpRequestMessage>(), 
            Configuration = Mock.Of<HttpConfiguration>() 
        };

        var response = controller.Post(product);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Product actualProduct;
        Assert.True(response.TryGetContentValue(out actualProduct));
        Assert.Equal(product, actualProduct);
        repositoryMock.VerifyAll();
    }
}