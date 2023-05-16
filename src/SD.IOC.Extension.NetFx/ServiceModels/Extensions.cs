// ReSharper disable once CheckNamespace
namespace System.ServiceModel.Extensions
{
    /// <summary>
    /// WCF扩展
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// 关闭信道扩展方法
        /// </summary>
        /// <param name="channel">信道实例</param>
        public static void CloseChannel(this object channel)
        {
            if (channel is ICommunicationObject communicationObject)
            {
                try
                {
                    if (communicationObject.State == CommunicationState.Faulted)
                    {
                        communicationObject.Abort();
                    }
                    else
                    {
                        communicationObject.Close();
                    }
                }
                catch (TimeoutException)
                {
                    communicationObject.Abort();
                }
                catch (CommunicationException)
                {
                    communicationObject.Abort();
                }
                catch (Exception)
                {
                    communicationObject.Abort();
                    throw;
                }
            }
        }
    }
}
