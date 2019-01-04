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

        [Fact]
        public void RankListMessageTest()
        {
            string message = @"type@=ranklist/rid@=248753/gid@=0/list@=uid@AA=15575353@AScrk@AA=1@ASlrk@AA=32767@ASrs@AA=0@ASnickname@AA=凰凰不可终日啊@ASgold@AA=493400@ASlevel@AA=30@ASicon@AA=avatar@AAS015@AAS57@AAS53@AAS53_avatar@ASpg@AA=1@ASrg@AA=1@ASne@AA=2@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=58236774@AScrk@AA=2@ASlrk@AA=3@ASrs@AA=1@ASnickname@AA=我和春哥上王者@ASgold@AA=185000@ASlevel@AA=19@ASicon@AA=avatar@AASface@AAS201607@AAS14@AAS032a16387a59b4a0de0048190ff6a541@ASpg@AA=1@ASrg@AA=1@ASne@AA=7@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=22614132@AScrk@AA=3@ASlrk@AA=2@ASrs@AA=-1@ASnickname@AA=vicky颖最美了最爱她了@ASgold@AA=111200@ASlevel@AA=37@ASicon@AA=avatar@AASdefault@AAS09@ASpg@AA=1@ASrg@AA=4@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=4848229@AScrk@AA=4@ASlrk@AA=3@ASrs@AA=-1@ASnickname@AA=冷漠的眸子@ASgold@AA=100000@ASlevel@AA=18@ASicon@AA=avatar@AASface@AAS201609@AAS15@AASc5708e4296dfa7b3cf6363223c28e0ac@ASpg@AA=1@ASrg@AA=4@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=2513794@AScrk@AA=5@ASlrk@AA=4@ASrs@AA=-1@ASnickname@AA=画个圈圈诅咒你つ@ASgold@AA=100000@ASlevel@AA=31@ASicon@AA=avatar@AAS002@AAS51@AAS37@AAS94_avatar@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=45660018@AScrk@AA=6@ASlrk@AA=7@ASrs@AA=1@ASnickname@AA=大疯枫@ASgold@AA=95200@ASlevel@AA=33@ASicon@AA=avatar_v3@AAS201812@AAS4ab3179568a743c9b43915e99710d664@ASpg@AA=1@ASrg@AA=1@ASne@AA=7@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=714553@AScrk@AA=7@ASlrk@AA=6@ASrs@AA=-1@ASnickname@AA=XTB桀骜@ASgold@AA=49900@ASlevel@AA=20@ASicon@AA=avatar_v3@AAS201807@AAS65877a0955f3c09bb0feef0fab8661f0@ASpg@AA=1@ASrg@AA=1@ASne@AA=7@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=47620850@AScrk@AA=8@ASlrk@AA=7@ASrs@AA=-1@ASnickname@AA=小亮爱可可啊@ASgold@AA=45100@ASlevel@AA=16@ASicon@AA=avatar@AASface@AAS201605@AAS2f6d95d246e36d3e39eadc3518109671@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=1635491@AScrk@AA=9@ASlrk@AA=8@ASrs@AA=-1@ASnickname@AA=Pebai永远是Pebai@ASgold@AA=42000@ASlevel@AA=39@ASicon@AA=avatar@AAS001@AAS63@AAS54@AAS91_avatar@ASpg@AA=1@ASrg@AA=1@ASne@AA=1@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=61430350@AScrk@AA=10@ASlrk@AA=9@ASrs@AA=-1@ASnickname@AA=军统上海站明长官@ASgold@AA=35200@ASlevel@AA=29@ASicon@AA=avatar@AASface@AAS201608@AAS28@AAS0d2b6c524de2e9872da037dcbd3d2485@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@S/list_all@=uid@AA=178701121@AScrk@AA=1@ASlrk@AA=3@ASrs@AA=0@ASnickname@AA=Jonnyttt@ASgold@AA=21770000@ASlevel@AA=53@ASicon@AA=avatar@AASdefault@AAS06@ASpg@AA=1@ASrg@AA=4@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=31699284@AScrk@AA=2@ASlrk@AA=1@ASrs@AA=0@ASnickname@AA=大飞小王子@ASgold@AA=18251600@ASlevel@AA=48@ASicon@AA=avatar_v3@AAS201807@AASeeaa19c442894ff83dea7f3981187f5e@ASpg@AA=1@ASrg@AA=4@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=25656239@AScrk@AA=3@ASlrk@AA=2@ASrs@AA=0@ASnickname@AA=Vv家的小李哥@ASgold@AA=14946000@ASlevel@AA=52@ASicon@AA=avatar_v3@AAS201811@AAS72e86ccd58b774b875ded971084267e1@ASpg@AA=1@ASrg@AA=1@ASne@AA=7@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=34731751@AScrk@AA=4@ASlrk@AA=3@ASrs@AA=0@ASnickname@AA=释别w@ASgold@AA=12223000@ASlevel@AA=59@ASicon@AA=avanew@AASface@AAS201803@AAS14@AAS19@AASca246ef8b4ef8037b5b650bccde0b956@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=113615344@AScrk@AA=5@ASlrk@AA=6@ASrs@AA=0@ASnickname@AA=守护夏牛牛丶追梦@ASgold@AA=8888800@ASlevel@AA=48@ASicon@AA=avatar_v3@AAS201811@AASca53f5ef9fb14ae2734a97ecb36bbe84@ASpg@AA=1@ASrg@AA=4@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=52781683@AScrk@AA=6@ASlrk@AA=5@ASrs@AA=0@ASnickname@AA=KiNGFaR1003@ASgold@AA=8812200@ASlevel@AA=34@ASicon@AA=avanew@AASface@AAS201712@AAS26@AAS02@AAS4312bf11b74c0aa0dec84a6b986043fa@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=61977595@AScrk@AA=7@ASlrk@AA=6@ASrs@AA=0@ASnickname@AA=我就是泡沫阿i@ASgold@AA=7616800@ASlevel@AA=63@ASicon@AA=avatar_v3@AAS201812@AASac9ca4ec51b9e670cf920fb0eb6c30c1@ASpg@AA=1@ASrg@AA=4@ASne@AA=1@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=39786999@AScrk@AA=8@ASlrk@AA=7@ASrs@AA=0@ASnickname@AA=37丶安哥@ASgold@AA=6397900@ASlevel@AA=37@ASicon@AA=avanew@AASface@AAS201711@AAS15@AAS21@AAS522f017cc199e964590884b94b308f8a@ASpg@AA=1@ASrg@AA=4@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=107065296@AScrk@AA=9@ASlrk@AA=8@ASrs@AA=0@ASnickname@AA=豆豆豆豆豆怪@ASgold@AA=6038600@ASlevel@AA=34@ASicon@AA=avanew@AASface@AAS201703@AAS30@AAS00@AAS0853b8d813c4b2b9d8b127aff85e1152@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=57554853@AScrk@AA=10@ASlrk@AA=11@ASrs@AA=0@ASnickname@AA=当当声1991@ASgold@AA=5675400@ASlevel@AA=32@ASicon@AA=avatar@AASdefault@AAS04@ASpg@AA=1@ASrg@AA=4@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@S/list_day@=uid@AA=58236774@AScrk@AA=1@ASlrk@AA=2@ASrs@AA=1@ASnickname@AA=我和春哥上王者@ASgold@AA=185000@ASlevel@AA=19@ASicon@AA=avatar@AASface@AAS201607@AAS14@AAS032a16387a59b4a0de0048190ff6a541@ASpg@AA=1@ASrg@AA=1@ASne@AA=7@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=45660018@AScrk@AA=2@ASlrk@AA=5@ASrs@AA=1@ASnickname@AA=大疯枫@ASgold@AA=56200@ASlevel@AA=33@ASicon@AA=avatar_v3@AAS201812@AAS4ab3179568a743c9b43915e99710d664@ASpg@AA=1@ASrg@AA=1@ASne@AA=7@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=231424031@AScrk@AA=3@ASlrk@AA=2@ASrs@AA=-1@ASnickname@AA=用户78815976@ASgold@AA=32200@ASlevel@AA=6@ASicon@AA=avatar_v3@AAS201812@AASe9bbbb096c8f0bedba26a0db5fb804a4@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=4359950@AScrk@AA=4@ASlrk@AA=6@ASrs@AA=1@ASnickname@AA=社会人雄@ASgold@AA=24000@ASlevel@AA=20@ASicon@AA=avatar@AAS004@AAS35@AAS99@AAS50_avatar@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=1825305@AScrk@AA=5@ASlrk@AA=4@ASrs@AA=-1@ASnickname@AA=预言人生@ASgold@AA=12900@ASlevel@AA=26@ASicon@AA=avatar@AAS001@AAS82@AAS53@AAS05_avatar@ASpg@AA=1@ASrg@AA=1@ASne@AA=7@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=58568485@AScrk@AA=6@ASlrk@AA=5@ASrs@AA=-1@ASnickname@AA=比特比特丘@ASgold@AA=12400@ASlevel@AA=12@ASicon@AA=avatar@AASface@AAS201607@AAS16@AAScca9a381273f525eff8b683ec48a4aa1@ASpg@AA=1@ASrg@AA=1@ASne@AA=7@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=110576583@AScrk@AA=7@ASlrk@AA=6@ASrs@AA=-1@ASnickname@AA=s老板很OK@ASgold@AA=6100@ASlevel@AA=4@ASicon@AA=avatar_v3@AAS201901@AAS72ed5f2a25992d91fa2775e94a26ec94@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=738390@AScrk@AA=8@ASlrk@AA=32767@ASrs@AA=1@ASnickname@AA=忆未染@ASgold@AA=6000@ASlevel@AA=18@ASicon@AA=avatar@AAS000@AAS73@AAS83@AAS90_avatar@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=71437346@AScrk@AA=9@ASlrk@AA=8@ASrs@AA=-1@ASnickname@AA=风雨继续@ASgold@AA=6000@ASlevel@AA=18@ASicon@AA=avatar@AASface@AAS201609@AAS25@AAS15183715dd462c3a220eabeaed001e4d@ASpg@AA=1@ASrg@AA=1@ASne@AA=0@ASih@AA=0@ASsahf@AA=0@AS@Suid@AA=45432008@AScrk@AA=10@ASlrk@AA=9@ASrs@AA=-1@ASnickname@AA=狄三针@ASgold@AA=6000@ASlevel@AA=11@ASicon@AA=avatar_v3@AAS201812@AAS6f37310d5c8b2aa64e35771c1c0006e8@ASpg@AA=1@ASrg@AA=1@ASne@AA=7@ASih@AA=0@ASsahf@AA=0@AS@S/";
            RankListMessage msg = MessageResolver.ResolveRankListMessage(message);
            Assert.Equal(248753, msg.RoomId);
            Assert.Equal(0, msg.GroupId);

            IList<RankListItem> allList = msg.AllRankList;
            Assert.Equal(10, allList.Count);
        }

        [Fact]
        public void SsdMessageTest()
        {
            string message = @"type@=ssd/sdid@=20329/content@=哇！这个主播要送粉丝2000块钱！！/rid@=248753/gid@=0/url@=/trid@=5820716/clitp@=7/jmptp@=1/";
            SsdMessage msg = MessageResolver.ResolveSsdMessage(message);
            Assert.Equal(248753, msg.RoomId);
            Assert.Equal(0, msg.GroupId);
            Assert.Equal("", msg.Url);
            Assert.Equal(5820716, msg.JumpRoomId);
            Assert.Equal(20329, msg.Id);
        }

        [Fact]
        public void SpbcMessageTest()
        {
            string message = @"type@=spbc/sn@=俊家丶丶駟/dn@=小俊蜀黍/gn@=火箭/gc@=1/drid@=4895778/gs@=5/gb@=1/es@=1/gfid@=196/eid@=143/bgl@=3/ifs@=0/rid@=-1003548456/gid@=32609/bid@=1002012_1546520793_9382/sid@=194394756/cl2@=0/eic@=0/bbi@=0/";
            SpbcMessage spbcMessage = MessageResolver.ResolveSpbcMessage(message);

            Assert.Equal(-1003548456, spbcMessage.RoomId);
            Assert.Equal("小俊蜀黍", spbcMessage.DeservedNickName);
            Assert.Equal("俊家丶丶駟", spbcMessage.SenderNickName);
        }
    }
}
