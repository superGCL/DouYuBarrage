using DouyuBarrage.Message;
using Xunit;

namespace UnitTest
{
    public class STTDeserializerTest
    {
        [Fact]
        public void DeserializeTest()
        {
            string response = "type@=loginres/userid@=0/roomgroup@=0/pg@=0/sessionid@=0/username@=/nickname@=/live_stat@=0/is_illegal@=0/ill_ct@=/ill_ts@=0/now@=0/ps@=0/es@=0/it@=0/its@=0/npv@=0/best_dlev@=0/cur_lev@=0/nrc@=0/ih@=0/sid@=72983/sahf@=0/sceneid@=0/";
            LoginResponseMessage loginResponse = new LoginResponseMessage(response);
        }
    }
}