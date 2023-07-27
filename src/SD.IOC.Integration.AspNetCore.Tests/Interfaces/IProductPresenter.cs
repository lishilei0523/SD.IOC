namespace SD.IOC.Integration.AspNetCore.Tests.Interfaces
{
    /// <summary>
    /// 产品呈现器接口
    /// </summary>
    public interface IProductPresenter
    {
        /// <summary>
        /// 获取产品列表
        /// </summary>
        string GetProducts();
    }
}
