using System;
using System.IO;
using DouyuBarrage.Client;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace DouyuBarrage
{
    class Program
    {
        static void Main(string[] args)
        {
            // 配置日志系统
            ILoggerRepository repository = LogManager.CreateRepository("Logger");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));

            DouYuBarrageClient client = new DouYuBarrageClient();
            client.Connect(248753);

            ILog logger = LogManager.GetLogger("Logger", typeof(Program));
            client.OnChat += (Message.ChatMessage obj) => {
                logger.Info(obj.NickName + ":" + obj.Text);
            };

            client.OnUserEnter += (Message.UserEnterMessage obj) =>
            {
                logger.Info(obj.NickName + " 进入了房间");
            };

            Console.ReadKey();
            client.Disconnect();
        }
    }
}
