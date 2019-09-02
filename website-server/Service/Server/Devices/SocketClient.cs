
using System;
using System.Net.Sockets;
using System.Threading;

namespace Server.Devices
{
    public class SocketClient
    {
        public delegate void ActionEventHandler();

        public delegate void MessageEventHandler(byte[] bytes);

        public delegate void ErrorEventHandler(string message, Exception ex);

        /// <summary>
        /// 客户端打开连接时调用
        /// </summary>
        public event ActionEventHandler ClientOpened;

        /// <summary>
        /// 客户端关闭连接时调用
        /// </summary>
        public event ActionEventHandler ClientClosed;

        /// <summary>
        /// 收到客户端信息时调用
        /// </summary>
        public event MessageEventHandler MessageReceived;

        /// <summary>
        /// 执行错误时调用
        /// </summary>
        public event ErrorEventHandler ErrorOccured;

        /// <summary>
        /// 服务
        /// </summary>
        private TcpClient client;
        /// <summary>
        /// 接收线程
        /// </summary>
        private Thread ReceivedThread;

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { private set; get; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { private set; get; }

        /// <summary>
        /// 是否重连
        /// </summary>
        public bool IsReConnection { set; get; }

        /// <summary>
        /// 是否已连接
        /// </summary>
        public bool IsConnection { get { return client.Connected; } }

        /// <summary>
        /// 重连间隔(毫秒)
        /// </summary>
        public int ReConnectionTime { set; get; }

        public SocketClient(string ip, int port)
        {
            Ip = ip;
            Port = port;
        }

        public SocketClient(string ip, int port, bool isReConnection = true, int reConnectionTime = 1000)
            : this(ip, port)
        {
            IsReConnection = isReConnection;
            ReConnectionTime = reConnectionTime;
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public async void Start()
        {
            IsReConnection = true;
            try
            {
                if (!IsConnection)
                {
                    client = new TcpClient();
                    await client.ConnectAsync(Ip, Port);
                    client.SendTimeout = 5000;
                    Console.WriteLine("WebSocket client Open!");
                    if (ReceivedThread != null)
                    {
                        ReceivedThread.Abort();
                    }
                    ReceivedThread = new Thread(Received);
                    ReceivedThread.IsBackground = true;
                    ReceivedThread.Start();
                    ClientOpened?.Invoke();//打开连接
                }
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                    ErrorOccured(ex.Message, ex);
                else
                    throw ex;
            }
        }
        private void Received()
        {
            try
            {
                while (client.Connected)
                {
                    byte[] bytes = new byte[128];
                    int byteSize = client.Client.Receive(bytes);
                    if (byteSize > 0)
                    {
                        this.MessageReceived(bytes);
                    }
                }
            }
            catch (Exception ex)
            {
                if (IsReConnection)
                {
                    Close();
                    Start();
                    Thread.Sleep(ReConnectionTime);
                }
                if (ErrorOccured != null)
                    ErrorOccured(ex.Message, ex);
                else
                    throw ex;
            }
        }

        public void Close()
        {
            IsReConnection = false;
            client.Close();
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        public void Send(byte[] message)
        {
            try
            {
                client.Client.Send(message);
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                    ErrorOccured(ex.Message, ex);
                else
                    throw ex;
            }
        }
    }
}