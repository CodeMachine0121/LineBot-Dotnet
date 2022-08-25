using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using ClearBaeBot.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ClearBaeBot;

public class Properity
{
    private readonly string accessToken;

    public Properity(TokenSetting _tokenSetting)
    {
        accessToken = _tokenSetting.ChannelAccessToken;
    }

    public async Task<UserProfile?> getUserProfile (string UserID)
    {
        using (var httpClient = new HttpClient())
        {
            using (var request =
                   new HttpRequestMessage(new HttpMethod("GET"), $"https://api.line.me/v2/bot/profile/{UserID}"))
            {
                request.Headers.TryAddWithoutValidation($"Authorization", $"Bearer {accessToken}");
                var response = await httpClient.SendAsync(request);
                
                // 取得回傳值
                var result = await response.Content.ReadAsStringAsync();
               
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("get_profile_error");
                }
                var Jresult = JsonConvert.DeserializeObject<UserProfile>(result);
                
                Console.WriteLine("[+] 搜尋使用者");
                Console.WriteLine($"\t[-] userId: {Jresult.userId}");
                Console.WriteLine($"\t[-] userName: {Jresult.displayName}");
                Console.WriteLine($"\t[-] pictureUrl: {Jresult.pictureUrl}");
                Console.WriteLine($"\t[-] language: {Jresult.language}");
                return Jresult;
            }
        }
    }

    public async Task replyMessage(string replyToken, params string[] messages)
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.line.me/v2/bot/message/reply"))
            {
                request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}"); 

                // 整合訊息
                MessageResponse messageResponse = new MessageResponse();
                messageResponse.replyToken = replyToken;
                foreach (var m in messages)
                { // 在 MessageResponse 底下塞入 MessageText物件
                    messageResponse.messages.Add( new MessageText(){text = m});
                }
                
                // 把 MessageResponse轉Json格式
                var JsonMessageResponse = JsonConvert.SerializeObject(messageResponse);
                
                request.Content = new StringContent(JsonMessageResponse);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                
                var response = await httpClient.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
        }
    }
    
}