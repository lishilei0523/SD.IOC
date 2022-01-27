using SD.IOC.Extension.NetCore.Tests.StubIAppServices;

namespace SD.IOC.Extension.NetCore.Tests.StubAppServices
{
    public class OrderContract : IOrderContract
    {
        public string GetOrder()
        {
            return "订单";
        }
    }
}
