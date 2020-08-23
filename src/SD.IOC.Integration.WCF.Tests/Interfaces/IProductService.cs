using System.ServiceModel;

namespace SD.IOC.Integration.WCF.Tests.Interfaces
{
    /// <summary>
    /// 商品管理服务接口
    /// </summary>
    [ServiceContract]
    public interface IProductService
    {
        /// <summary>
        /// 获取商品集
        /// </summary>
        /// <returns>商品集</returns>
        [OperationContract]
        string GetProducts();
    }
}
