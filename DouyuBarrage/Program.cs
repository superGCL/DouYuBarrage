using DouyuBarrage.Client;
using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.IO;

namespace DouyuBarrage
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start(args);
        }

        static void Usage()
        {
            Console.WriteLine("Usage: DouyuBarrage <RoomId>");
        }

        void Start(string[] args)
        {
            // 第一个参数是房间号
            if (args.Length != 1)
            {
                Usage();
                return;
            }

            if (!Int32.TryParse(args[0], out int roomId))
            {
                Usage();
                return;
            }

            if (roomId <= 0)
            {
                Usage();
                return;
            }

            // 配置日志系统
            ILoggerRepository repository = LogManager.CreateRepository("Logger");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            ILog logger = LogManager.GetLogger("Logger", typeof(Program));

            client = new DouYuBarrageClient();
            client.Connect(roomId);
            
            client.OnChat += (Message.ChatMessage obj) => {
                logger.Info(obj.NickName + ":" + obj.Text);
            };

            client.OnUserEnterBroadcast += (Message.UserEnterMessage obj) =>
            {
                logger.Info(obj.NickName + " 进入了房间");
            };

            Console.CancelKeyPress += Console_CancelKeyPress;

            client.ThreadJoin();
        }

        private DouYuBarrageClient client = null;

        private void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            client?.Disconnect();
        }
    }
}
