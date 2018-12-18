using DouyuBarrage;
using Xunit;

namespace UnitTest
{
    public class RequestTest
    {
        [Fact]
        public void TestLoginRequest()
        {
            LoginRequest request = new LoginRequest(9998);
            Assert.Equal("type@=loginreq/roomid@=9998/", request.ToString());
        }

        [Fact]
        public void TestLogoutRequest()
        {
            LogoutRequest request = new LogoutRequest();
            Assert.Equal("type@=logout/", request.ToString());
        }

        [Fact]
        public void TestKeepAliveRequest()
        {
            KeepAliveRequest request = new KeepAliveRequest();
            Assert.Equal("type@=mrkl/", request.ToString());
        }

        [Fact]
        public void TestJoinGroupRequest()
        {
            int roomId = 9999;
            JoinGroupRequest request = new JoinGroupRequest(roomId);
            Assert.Equal("type@=joingroup/rid@=9999/gid@=-9999/", request.ToString());
        }
    }
}
