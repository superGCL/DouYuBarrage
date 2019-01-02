using System;
using System.Collections.Generic;
using DouyuBarrage.Utils;

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
    }
}
