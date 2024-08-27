using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web.UI.WebControls.WebParts;

namespace CZY.SlackToolBox.FastExtend
{
    public interface IAIClient
    {
        string ChatAI(string input, string systemInput);
    }
    public abstract class AIClientBase : IAIClient
    {
        protected AIConfig Config;

        public AIClientBase(AIConfig config)
        {
            Config = config;
        }

        public abstract string ChatAI(string input, string systemInput);
    }
    public class AIConfig
    {
        /// <summary>
        /// 请求的ApiKey
        /// </summary>
        public string ApiKey { get; set; }
        /// <summary>
        /// 请求的Api地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 推理模型点名称
        /// </summary>
        public string EndpointId { get; set; }
    }
    public class AIClient : AIClientBase
    {
        private static readonly HttpClient client = new HttpClient();
         
        public AIClient(AIConfig config) : base(config)
        {
            this.Config = config;
        }

        public override string ChatAI(string input, string systemInput = "你是一个编程高手，精通各种编程语言")
        {
            var requestBody = new
            {
                model = Config.EndpointId,
                messages = new[]
                {
                new { role = "system", content =systemInput},
                new { role = "user", content = input }
                },
                stream = true
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(Config.ApiUrl),
                Content = content
            };

            request.Headers.Add("Authorization", $"Bearer {Config.ApiKey}");

            var response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

            var fullResponse = new StringBuilder();
             

            using (var stream = response.Content.ReadAsStreamAsync().Result)
            using (var reader = new System.IO.StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line.StartsWith("data:"))
                    {
                        var jsonString = line.Substring(5).Trim();
                        if (jsonString == "[DONE]")
                        {
                            break;
                        }
                        var jsonObj = JObject.Parse(jsonString);
                        var contentDelta = jsonObj["choices"]?[0]?["delta"]?["content"]?.ToString();
                        if (contentDelta != null)
                        {
                            fullResponse.Append(contentDelta);
                        }
                    }
                }
            }

            return fullResponse.ToString();
        }
    }

}
