using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Common
{
    public class WcfInvokeFactory
    {
        #region WCF factory
        public static T CreateServiceByUrl<T>(string url, int timeout = 10)
        {
            return CreateServiceByUrl<T>(url, "basicHttpBinding", timeout);
        }

        public static T CreateServiceByUrl<T>(string url, string bing, int timeout = 10)
        {
            try
            {
                if (string.IsNullOrEmpty(url)) throw new NotSupportedException("This url is null or empty!");
                EndpointAddress address = new EndpointAddress(url);
                Binding binding = CreateBinding(bing, timeout);
                ChannelFactory<T> factory = new ChannelFactory<T>(binding, address);
                return factory.CreateChannel();
            }
            catch (Exception )
            {
                throw new Exception("Error at creating Wcf factory.");
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        private static Binding CreateBinding(string binding, int timeout)
        {
            Binding bindinginstance = null;
            if (binding.ToLower() == "basichttpbinding")
            {
                BasicHttpBinding ws = new BasicHttpBinding
                {
                    MaxBufferSize = 2147483647,
                    MaxBufferPoolSize = 2147483647,
                    MaxReceivedMessageSize = 2147483647,
                    CloseTimeout = new TimeSpan(0, 30, 0),
                    OpenTimeout = new TimeSpan(0, 30, 0),
                    ReceiveTimeout = new TimeSpan(0, 30, 0),
                    SendTimeout = new TimeSpan(0, 0, 0, timeout),
                };
                ws.ReaderQuotas.MaxStringContentLength = 2147483647;
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "nettcpbinding")
            {
                NetTcpBinding ws = new NetTcpBinding
                {
                    MaxReceivedMessageSize = 65535000
                };
                ws.Security.Mode = SecurityMode.None;
                bindinginstance = ws;
            }
            else if (binding.ToLower() == "wshttpbinding")
            {
                WSHttpBinding ws = new WSHttpBinding(SecurityMode.None)
                {
                    MaxReceivedMessageSize = 65535000
                };
                ws.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
                ws.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
                bindinginstance = ws;
            }
            return bindinginstance;

        }

    }
}
