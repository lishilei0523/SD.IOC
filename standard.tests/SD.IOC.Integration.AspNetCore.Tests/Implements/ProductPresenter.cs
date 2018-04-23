using SD.IOC.Integration.AspNetCore.Tests.Interfaces;
using SD.IOC.StubInterface.Interfaces;

namespace SD.IOC.Integration.AspNetCore.Tests.Implements
{
    public class ProductPresenter : IProductPresenter
    {
        private readonly IProductContract _productContract;

        public ProductPresenter(IProductContract productContract)
        {
            this._productContract = productContract;
        }


        public string GetProducts()
        {
            return this._productContract.GetProducts();
        }
    }
}
