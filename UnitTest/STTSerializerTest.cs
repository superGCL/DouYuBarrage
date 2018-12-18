using System.Collections.Generic;
using DouyuBarrage;
using Xunit;

namespace UnitTest
{
    public class STTSerializerTest
    {
        [Fact]
        public void SerializeTest()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                { "type", "loginreq" },
                { "roomid", "9999" }
            };
            string result = STTSerializer.Serialize(dictionary);
            Assert.Equal("type@=loginreq/roomid@=9999/", result);
        }
    }
}
