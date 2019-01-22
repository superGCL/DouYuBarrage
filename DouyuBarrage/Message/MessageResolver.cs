using DouyuBarrage.Utils;
using System;
using System.Collections.Generic;

namespace DouyuBarrage.Message
{
    public static class MessageResolver
    {
        public static LoginResponseMessage ResolveLoginResponseMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                LoginResponseMessage loginResponseMessage = new LoginResponseMessage
                {
                    UserId = keyValues.GetValueOrDefault("userid"),
                    UserName = keyValues.GetValueOrDefault("username"),
                    NickName = keyValues.GetValueOrDefault("nickname"),
                    LiveStat = keyValues.GetValueOrDefault("live_stat"),
                    Original = keyValues
                };

                return loginResponseMessage;
            }

            return null;
        }

        public static ChatMessage ResolveChatMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                ChatMessage chatMessage = new ChatMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    UserId = keyValues.GetValueOrDefault("uid"),
                    NickName = keyValues.GetValueOrDefault("nn"),
                    Text = keyValues.GetValueOrDefault("txt"),
                    ChatId = keyValues.GetValueOrDefault("cid"),
                    Level = Convert.ToInt32(keyValues.GetValueOrDefault("level")),
                    Icon = keyValues.GetValueOrDefault("ic"),
                    AnchorLevel = Convert.ToInt32(keyValues.GetValueOrDefault("ol")),
                    IsFans = keyValues.GetValueOrDefault("ifs") == "1",
                    Original = keyValues
                };

                return chatMessage;
            }
            return null;
        }

        public static OnlineGiftMessage ResolveOnlineGiftMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                OnlineGiftMessage onlineGiftMessage = new OnlineGiftMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    UserId = keyValues.GetValueOrDefault("uid"),
                    NickName = keyValues.GetValueOrDefault("nn"),
                    Level = Convert.ToInt32(keyValues.GetValueOrDefault("level")),
                    Sil = Convert.ToInt32(keyValues.GetValueOrDefault("sil")),
                    If = Convert.ToInt32(keyValues.GetValueOrDefault("if")),
                    Ur = Convert.ToInt32(keyValues.GetValueOrDefault("ur")),
                    Original = keyValues
                };

                return onlineGiftMessage;
            }
            return null;
        }

        public static DgbMessage ResolveDgbMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                DgbMessage dgbMessage = new DgbMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    UserId = keyValues.GetValueOrDefault("uid"),
                    NickName = keyValues.GetValueOrDefault("nn"),
                    Level = Convert.ToInt32(keyValues.GetValueOrDefault("level")),
                    GiftId = Convert.ToInt32(keyValues.GetValueOrDefault("gfid")),
                    GiftStyle = keyValues.GetValueOrDefault("gs"),
                    GiftCount = Convert.ToInt32(keyValues.GetValueOrDefault("gfcnt")),
                    NobleLevel = Convert.ToInt32(keyValues.GetValueOrDefault("nl")),
                    Hits = Convert.ToInt32(keyValues.GetValueOrDefault("hits")),
                    Original = keyValues
                };

                return dgbMessage;
            }
            return null;
        }

        public static UserEnterMessage ResolveUserEnterMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                UserEnterMessage userEnterMessage = new UserEnterMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    UserId = keyValues.GetValueOrDefault("uid"),
                    NickName = keyValues.GetValueOrDefault("nn"),
                    Level = Convert.ToInt32(keyValues.GetValueOrDefault("level")),
                    NobleLevel = Convert.ToInt32(keyValues.GetValueOrDefault("nl")),
                    Icon = keyValues.GetValueOrDefault("ic"),
                    Original = keyValues
                };

                return userEnterMessage;
            }
            return null;
        }

        public static BcBuyDeserveMessage ResolveBcBuyDeserveMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                BcBuyDeserveMessage bcBuyDeserveMessage = new BcBuyDeserveMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    Level = Convert.ToInt32(keyValues.GetValueOrDefault("level")),
                    Count = Convert.ToInt32(keyValues.GetValueOrDefault("cnt")),
                    Hits = Convert.ToInt32(keyValues.GetValueOrDefault("hits")),
                    Original = keyValues
                };

                return bcBuyDeserveMessage;
            }
            return null;
        }

        public static RssMessage ResolveRssMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                RssMessage rssMessage = new RssMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    Living = keyValues.GetValueOrDefault("ss") == "1",
                    Code = keyValues.GetValueOrDefault("code"),
                    Notify = keyValues.GetValueOrDefault("notify"),
                    Endtime = keyValues.GetValueOrDefault("endtime"),
                    Original = keyValues
                };

                return rssMessage;
            }
            return null;
        }

        public static RankListMessage ResolveRankListMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                RankListMessage rankListMessage = new RankListMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    Timestamp = Convert.ToInt64(keyValues.GetValueOrDefault("ts")),
                    Sequence = Convert.ToInt32(keyValues.GetValueOrDefault("seq")),
                    Original = keyValues
                };

                return rankListMessage;
            }
            return null;
        }

        public static SsdMessage ResolveSsdMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                SsdMessage ssdMessage = new SsdMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    Id = Convert.ToInt32(keyValues.GetValueOrDefault("sdid")),
                    JumpRoomId = Convert.ToInt32(keyValues.GetValueOrDefault("trid")),
                    ClientType = Convert.ToInt32(keyValues.GetValueOrDefault("clitp")),
                    JumpType = Convert.ToInt32(keyValues.GetValueOrDefault("jmptp")),
                    Content = keyValues.GetValueOrDefault("content"),
                    Url = keyValues.GetValueOrDefault("url"),
                    Original = keyValues
                };

                return ssdMessage;
            }
            return null;
        }

        public static SpbcMessage ResolveSpbcMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                SpbcMessage spbcMessage = new SpbcMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    SenderNickName = keyValues.GetValueOrDefault("sn"),
                    DeservedNickName = keyValues.GetValueOrDefault("dn"),
                    GiftName = keyValues.GetValueOrDefault("gn"),
                    GiftCount = Convert.ToInt32(keyValues.GetValueOrDefault("gc")),
                    DeservedRoomId = Convert.ToInt32(keyValues.GetValueOrDefault("drid")),
                    Original = keyValues
                };

                return spbcMessage;
            }
            return null;
        }

        public static GgbbMessage ResolveGgbbMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                GgbbMessage spbcMessage = new GgbbMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    GiftType = Convert.ToInt32(keyValues.GetValueOrDefault("gt")),
                    Count = Convert.ToInt32(keyValues.GetValueOrDefault("sl")),
                    SenderId = Convert.ToInt32(keyValues.GetValueOrDefault("sid")),
                    DeservedId = Convert.ToInt32(keyValues.GetValueOrDefault("did")),
                    SenderNickName = keyValues.GetValueOrDefault("snk"),
                    DeservedNickName = keyValues.GetValueOrDefault("dnk"),
                    Type = Convert.ToInt32(keyValues.GetValueOrDefault("rpt")),
                    Original = keyValues
                };

                return spbcMessage;
            }
            return null;
        }

        public static RankUpMessage ResolveRankUpMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                RankUpMessage rankUpMessage = new RankUpMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    UserId = keyValues.GetValueOrDefault("uid"),
                    DestRoomId = Convert.ToInt32(keyValues.GetValueOrDefault("drid")),
                    NickName = keyValues.GetValueOrDefault("nk"),
                    RankType = keyValues.GetValueOrDefault("rkt"),
                    RankNow = Convert.ToInt32(keyValues.GetValueOrDefault("rn")),
                    Original = keyValues
                };

                return rankUpMessage;
            }
            return null;
        }

        public static AnchorLeaveMessage ResolveAnchorLeaveMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                AnchorLeaveMessage alMessage = new AnchorLeaveMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    AnchorId = Convert.ToInt32(keyValues.GetValueOrDefault("aid")),
                    Original = keyValues
                };

                return alMessage;
            }
            return null;
        }

        public static AnchorBackMessage ResolveAnchorBackMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                AnchorBackMessage abMessage = new AnchorBackMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    AnchorId = Convert.ToInt32(keyValues.GetValueOrDefault("aid")),
                    Original = keyValues
                };

                return abMessage;
            }
            return null;
        }

        public static RuclpMessage ResolveRuclpMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                RuclpMessage ruclpMessage = new RuclpMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    Original = keyValues
                };

                return ruclpMessage;
            }
            return null;
        }

        public static GpbcMessage ResolveGpbcMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                GpbcMessage gpbcMessage = new GpbcMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    Count = Convert.ToInt32(keyValues.GetValueOrDefault("cnt")),
                    SenderId = keyValues.GetValueOrDefault("sid"),
                    SenderNickName = keyValues.GetValueOrDefault("snk"),
                    ReceiverId = keyValues.GetValueOrDefault("did"),
                    ReceiverNickName = keyValues.GetValueOrDefault("dnk"),
                    RedPacketType = keyValues.GetValueOrDefault("rpt"),
                    PacketName = keyValues.GetValueOrDefault("pnm"),
                    Original = keyValues
                };

                return gpbcMessage;
            }
            return null;
        }

        public static OnlineNobleListMessage ResolveOnlineNobleListMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                OnlineNobleListMessage onlineNobleListMessage = new OnlineNobleListMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    Count = Convert.ToInt32(keyValues.GetValueOrDefault("num")),
                    Original = keyValues
                };

                return onlineNobleListMessage;
            }
            return null;
        }

        public static UpgradeMessage ResolveUpgradeMessage(string message)
        {
            Dictionary<string, string> keyValues = STTDeserializer.Deserialize(message);
            if (keyValues != null)
            {
                UpgradeMessage upgradeMessage = new UpgradeMessage
                {
                    GroupId = Convert.ToInt32(keyValues.GetValueOrDefault("gid")),
                    RoomId = Convert.ToInt32(keyValues.GetValueOrDefault("rid")),
                    Uid = keyValues.GetValueOrDefault("uid"),
                    Level = Convert.ToInt32(keyValues.GetValueOrDefault("level")),
                    Icon = keyValues.GetValueOrDefault("ic"),
                    Original = keyValues
                };

                return upgradeMessage;
            }
            return null;
        }
    }
}
