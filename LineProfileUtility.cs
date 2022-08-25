using System.Net;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using LineBotDemo.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LineBotDemo;

public class LineProfileUtility
{
    private static string accessToken  ;
    private static string lineMessageApiBaseUrl = "https://api.line.me/v2/bot/profile";

    public LineProfileUtility(IOptions<LineSetting> lineSetting)
    {
        accessToken = lineSetting.Value.ChannleAccessToken;
    }

    public async Task<UserProfile?> GetUserProfile(string userId)
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"{lineMessageApiBaseUrl}/{userId}"))
            {
               
                request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");
                var response = await httpClient.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.WriteLine(result);
                    throw new Exception("get_profile_error");
                }

                
                return JsonConvert.DeserializeObject<UserProfile>(result);
            }
        }
    }


    public async Task ReplyMessageAsync(string replyToken, params string[] messages)
    {
        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), $"{lineMessageApiBaseUrl}"))
            {
                request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {accessToken}");


                LineMessageReq req = new LineMessageReq();
                req.ReplyToken = replyToken;
                foreach (var message in messages) // 整合多個訊息於一個物件中
                {
                    req.Message.Add(new TextMessage()
                    {
                        Text = message
                    });
                }
                
                // 把要回復的訊息統整為Json格式
                var postJson = JsonConvert.SerializeObject(req, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy() //轉小寫
                    },
                    Formatting = Formatting.Indented
                });
                
                request.Content = new StringContent(postJson);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
                var response = await httpClient.SendAsync(request);
                
            }
        }
        
    }
}