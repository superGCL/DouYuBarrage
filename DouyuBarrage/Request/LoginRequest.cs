using System;
namespace DouyuBarrage.Request
{
    public class LoginRequest
    {
        private readonly int m_roomId;
        public LoginRequest(int roomId)
        {
            m_roomId = roomId;
        }

        public override string ToString()
        {
            return "type@=loginreq/roomid@=" + m_roomId + "/";
        }
    }
}
