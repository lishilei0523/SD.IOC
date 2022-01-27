using SD.IOC.Extension.NetFx.Tests.StubIAppServices;

namespace SD.IOC.Extension.NetFx.Tests.StubAppServices
{
    public class OrderContract : IOrderContract
    {
        public string GetOrder()
        {
            return "订单";
        }
    }
}
