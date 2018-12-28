using System;
namespace DouyuBarrage.Request
{
    public class JoinGroupRequest
    {
        private readonly int m_roomId;
        private readonly int m_groupId;
        public JoinGroupRequest(int roomid, int groupid = -9999)
        {
            m_roomId = roomid;
            m_groupId = groupid;
        }

        public override string ToString()
        {
            return "type@=joingroup/rid@=" + m_roomId + "/gid@=" + m_groupId + "/";
        }
    }
}
