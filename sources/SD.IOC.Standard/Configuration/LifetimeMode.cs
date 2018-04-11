namespace SD.IOC.Standard.Configuration
{
    /// <summary>
    /// 实例生命周期模式
    /// </summary>
    public enum LifetimeMode
    {
        /// <summary>
        /// 每次请求
        /// </summary>
        PerCall = 0,

        /// <summary>
        /// 每次会话
        /// </summary>
        PerSession = 1,

        /// <summary>
        /// 单例
        /// </summary>
        Singleton = 2
    }
}
