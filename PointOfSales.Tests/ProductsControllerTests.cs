using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using System.Web.Http;
using PointOfSales.Domain.Repositories;
using PointOfSales.Web.Controllers;
using PointOfSales.Domain.Model;

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
}