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
                                new TextMessage("你抽到了愚者"),
                                new TextMessage("今天的你非常做自己，不會去在乎周圍的眼光，也非常享受過程，不在意任何事情的結果")
                                
                            };
                            break;
                        case 1:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了魔術師"),
                                new TextMessage("今天的你注意力非常的好，創造力也極佳，也意味著今天的你有說謊的可能")
                            };
                            break;
                        case 2:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了女祭司"),
                                new TextMessage("今天的你非常懶散，很沒有行動力，女祭司也有著隱藏自己的意思")
                            };
                            break;
                        case 3:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了皇后"),
                                new TextMessage("皇后有著豐饒的意思，也暗示了一些交際手腕，今天的你很願意去體驗享受生活")
                            };
                            break;
                        case 4:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了皇帝"),
                                new TextMessage("皇帝有著萬王之王的意思，今天的你非常清楚今天要做甚麼，會有強烈的掌控力")
                            };
                            break;
                        case 5:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了戀人"),
                                new TextMessage("戀人有著著主動選擇的含意，今天的你可能會面臨著選擇的問題，請一定好好想想哪個選擇對你最好")
                            };
                            break;
                        case 6:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了戰車"),
                                new TextMessage("戰車有失控的意味，也許表面看似穩定，但內部可能出現了隱患，請好好觀察並重視這件事情")
                            };
                            break;
                        case 7:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了力量"),
                                new TextMessage("今天的你可能受到來自外在的壓迫，這表示並不是自願，比如今天下雨你不得不帶把傘，這就是外在壓迫")
                            };
                            break;
                        case 8:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了隱者"),
                                new TextMessage("你的能力很好，但生不逢時，所有條件都不允許你發揮你的能力，今天的你會因此感到孤獨")
                            };
                            break;
                        case 9:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了命運之輪"),
                                new TextMessage("命運之輪有著利用規律的意思，我們在等待合適的時機做正確的事")
                            };
                            break;
                        case 10:
                            result = new List<ISendMessage>
                            {
                                new TextMessage("你抽到了正義"),
                                new TextMessage("正義並不是像天秤那樣公平公正，而是凸顯出個人的正義，這種正義一定是對你有利的，所以是個人的正義")
                                
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






