using System.Net.Mime;
using Line.Messaging;
using Line.Messaging.Webhooks;
using NuGet.ContentModel;
using System;

namespace A982100_linerobot;

public class LineBotApp : WebhookApplication
{
    private readonly LineMessagingClient _messagingClient;
    
    private static Dictionary<string, string> _pool = new Dictionary<string, string>();
    
    public LineBotApp(LineMessagingClient lineMessagingClient)
    {
        _messagingClient = lineMessagingClient;
    }

    protected override async Task OnMessageAsync(MessageEvent ev)
    {
        var result = null as List<ISendMessage>;

        switch (ev.Message)
        {
            //文字訊息
            case TextEventMessage textMessage:
            {
                //頻道Id
                var channelId = ev.Source.Id;
                //使用者Id
                var userId = ev.Source.UserId;
                //使用者輸入的文字
                var text = ((TextEventMessage)ev.Message).Text;

                if (PoolHasMsg(text))
                {
                    // 從記憶體池查詢資料
                    string response = GetResponse(text);
                    result = new List<ISendMessage>
                    {
                        new TextMessage(response)
                    };
                }
                else if (text == "日簽")
                {
                    var rand = new Random();
                    var randomNumber = rand.Next(10);
                    switch (randomNumber)
                    {
                        case 0:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId + "愚者")
                                
                            };
                            break;
                        case 1:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId + "魔術師")
                            };
                            break;
                        case 2:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId +"女祭司")
                            };
                            break;
                        case 3:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId +"皇后")
                            };
                            break;
                        case 4:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId +"皇帝")
                            };
                            break;
                        case 5:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId + "戀人")
                            };
                            break;
                        case 6:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId + "戰車")
                            };
                            break;
                        case 7:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId + "力量")
                            };
                            break;
                        case 8:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId + "隱者")
                            };
                            break;
                        case 9:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId + "命運之輪")
                            };
                            break;
                        case 10:
                            result = new List<ISendMessage>
                            {
                                new TextMessage(userId + "正義")
                            };
                            break;
                    }
                }
                else
                {
                    if (CheckFormat(text))
                    {
                        //將資料寫入記憶體池
                        TeachDog(text);
                    }
                }
                
                /*
                //回傳 hellow
                result = new List<ISendMessage>
                {
                    new TextMessage("你寂寞ㄇ" + ((TextEventMessage) ev.Message).Text)
                };
                */
                
            }
                break;
        }

        if (result != null)
            await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);
    }

    /// <summary>
    /// 確認是否已經學習過這個對話
    /// </summary>
    /// <param name="inputMsg"></param>
    /// <returns></returns>
    private bool PoolHasMsg(string inputMsg)
    {
        return _pool.ContainsKey(inputMsg);
    }

    
    /// <summary>
    /// 用於 已經學習過的對話
    /// </summary>
    /// <param name="inputMsg"></param>
    /// <returns></returns>
    private string GetResponse(string inputMsg)
    {
        return _pool[inputMsg];
    }

    private bool CheckFormat(string inputMsg)
    {
        bool result = false;
        
        try
        {
            string[] subs = inputMsg.Split(';');
            //檢查
            if (subs.Length == 3)
            {
                if (subs[0] == "珧幽")
                {
                    result = true;
                }
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return result;
    }

    private void TeachDog(string inputMsg)
    {
        
        try
        {
            string[] subs = inputMsg.Split(';');
            //檢查
            if (subs.Length == 3)
            {
                if (subs[0] == "珧幽")
                {
                    _pool.Add(subs[1],subs[2]);
                }
            }
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
}






