using System.Net.Mime;
using Line.Messaging;
using Line.Messaging.Webhooks;

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
        ver text;

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
                var Text = ((TextEventMessage)ev.Message).Text;

                if (PoolHasMsg(Text))
                {
                    // 從記憶體池查詢資料
                    string response = GetResponse(Text);
                    result = new List<ISendMessage>
                    {
                        new TextMessage(response)
                    };
                }
                else
                {
                    if (CheckFormat(Text))
                    {
                        //將資料寫入記憶體池
                        TeachDog(Text);
                    }
                }

                var outputText = Text;
                
                if (Text.Contains("嗨") && Text.Contains("你好"))
                {
                    Text = "你好";
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
        return false;
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


public class ver
{
}



