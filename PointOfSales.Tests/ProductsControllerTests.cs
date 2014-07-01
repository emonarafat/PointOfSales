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
        var expectedProducts = new List<Product>();
        repositoryMock.Setup(r => r.GetAll()).Returns(expectedProducts);
        var controller = new ProductsController(repositoryMock.Object);

        var products = controller.Get();

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
        throw new NotImplementedException();
    }
}

public class Product
{

}

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
}
