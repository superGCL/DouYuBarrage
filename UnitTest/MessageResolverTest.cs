using DouyuBarrage.Message;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTest
{
    public class MessageResolverTest
    {
        [Fact]
        public void LoginResponseMessageTest()
        {
            string response = "type@=loginres/userid@=0/roomgroup@=0/pg@=0/sessionid@=0/username@=/nickname@=/live_stat@=0/is_illegal@=0/ill_ct@=/ill_ts@=0/now@=0/ps@=0/es@=0/it@=0/its@=0/npv@=0/best_dlev@=0/cur_lev@=0/nrc@=0/ih@=0/sid@=72983/sahf@=0/sceneid@=0/";
            LoginResponseMessage loginResponse = MessageResolver.ResolveLoginResponseMessage(response);
            Assert.Equal("0", loginResponse.UserId);
        }

        [Fact]
        public void ChatMessageTest()
        {
            string message = @"type@=chatmsg/rid@=252802/ct@=3/uid@=20001020/nn@=教主的飞机杯/txt@=移动/cid@=91a61e962ac445b7df142b0000000000/ic@=avanew@Sface@S201711@S13@S01@S1fa4fc0f5e596e7eb7fd53281f3b9529/level@=33/sahf@=0/bnn@=/bl@=0/brid@=0/hc@=/el@=/lk@=/";
            ChatMessage chat = MessageResolver.ResolveChatMessage(message);
            Assert.Equal(252802, chat.RoomId);
            Assert.Equal(20001020, chat.UserId);
            Assert.Equal("教主的飞机杯", chat.NickName);
        }

        [Fact]
        public void OnlineGiftMessageTest()
        {
            string message = @"type@=onlinegift/rid@=1/gid@=-9999/sil@=1/if@=1/ct@=1/nn@=testuser/ur@=1/level@=6/btype@=1/";
            OnlineGiftMessage msg = MessageResolver.ResolveOnlineGiftMessage(message);

            Assert.Equal(-9999, msg.GroupId);
            Assert.Equal("testuser", msg.NickName);
            Assert.Equal(6, msg.Level);
        }

        [Fact]
        public void DgbMessageTest()
        {
            string message = @"type@=dgb/rid@=252802/gfid@=824/gs@=1/uid@=129851912/nn@=晴臣183/ic@=avatar@Sdefault@S12/eid@=0/level@=21/dw@=0/gfcnt@=30/hits@=30/bcnt@=1/bst@=5/ct@=0/el@=eid@AA=1500000113@ASetp@AA=1@ASsc@AA=1@ASef@AA=0@AS@S/cm@=0/bnn@=莉卡/bl@=11/brid@=2971401/hc@=a389dd77fc28612735c07b6ad218e628/sahf@=0/fc@=0/cbid@=87616/gpf@=1/";
            OnlineGiftMessage msg = MessageResolver.ResolveOnlineGiftMessage(message);

            Assert.Equal(252802, msg.RoomId);
            Assert.Equal("晴臣183", msg.NickName);
            Assert.Equal(21, msg.Level);
        }
    }
}
