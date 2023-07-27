using SD.IOC.Extension.Tests.StubIAppServices;

namespace SD.IOC.Extension.Tests.StubAppServices
{
    /// <summary>
    /// 单据服务契约实现
    /// </summary>
    public class OrderContract : IOrderContract
    {
        /// <summary>
        /// 获取单据
        /// </summary>
        public string GetOrder()
        {
            return "订单";
        }
    }
}
