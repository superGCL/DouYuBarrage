using System.IO;
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
            client.Connect(310904);
        }
    }
}
