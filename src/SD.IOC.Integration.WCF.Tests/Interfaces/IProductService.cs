using System.ServiceModel;

namespace SD.IOC.Integration.WCF.Tests.Interfaces
{
    /// <summary>
    /// 产品管理服务接口
    /// </summary>
    [ServiceContract]
    public interface IProductService
    {
        /// <summary>
        /// 获取产品列表
        /// </summary>
        [OperationContract]
        string GetProducts();
    }
}
