using System.ServiceModel;

namespace SD.IOC.StubIAppService.Interfaces
{
    /// <summary>
    /// 产品管理服务契约接口
    /// </summary>
    [ServiceContract]
    public interface IProductContract
    {
        /// <summary>
        /// 获取产品列表
        /// </summary>
        [OperationContract]
        string GetProducts();
    }
}
