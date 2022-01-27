using System.ServiceModel;

namespace SD.IOC.StubIAppService.Interfaces
{
    /// <summary>
    /// 商品管理接口
    /// </summary>
    [ServiceContract]
    public interface IProductContract
    {
        /// <summary>
        /// 获取商品集
        /// </summary>
        /// <returns>商品集</returns>
        [OperationContract]
        string GetProducts();
    }
}
