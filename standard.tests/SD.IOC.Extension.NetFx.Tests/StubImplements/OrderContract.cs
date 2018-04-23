using SD.IOC.Extension.NetFx.Tests.StubInterfaces;

namespace SD.IOC.Extension.NetFx.Tests.StubImplements
{
    public class OrderContract : IOrderContract
    {
        public string GetOrder()
        {
            return "订单";
        }
    }
}
