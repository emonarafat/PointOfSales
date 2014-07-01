using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using System.Web.Http;

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

public class ProductsController : ApiController
{
    private IProductRepository productRepository;

    public ProductsController(IProductRepository productRepository)
    {        
        this.productRepository = productRepository;
    }
    public IEnumerable<Product> Get()
    {
        return productRepository.GetAll();
    }

    public IEnumerable<Product> Get(string search)
    {
        return productRepository.GetByNameOrDescription(search);
    }
}

public class Product
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    IEnumerable<Product> GetByNameOrDescription(string search);
}
