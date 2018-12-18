using System;

namespace DouyuBarrage
{
    class Program
    {
        static void Main(string[] args)
        {
            // C#是小端字节序
            //bool byteOrderIsBigEndin = Utils.IsBigEndin();
            //Console.WriteLine(byteOrderIsBigEndin);

            DouYuBarrageClient client = new DouYuBarrageClient();
            client.Connect(310904);
        }
    }
}
