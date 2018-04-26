using SD.IOC.Extension.NetCore.Tests.StubInterfaces;

namespace SD.IOC.Extension.NetCore.Tests.StubImplements
{
    public class OrderContract : IOrderContract
    {
        public string GetOrder()
        {
            return "订单";
        }
    }
}
