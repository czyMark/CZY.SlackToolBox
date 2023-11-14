using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class GetTool
    {
        //
        public static string Accept = "text/html, application/xhtml+xml, */*";
        //发送内容类型
        public static string ContentType = "application/json";
        //传递的字符集
        public static Encoding Encoding = Encoding.UTF8;
        //证书路径
        public static string CertFileName;
        private static bool Certificate;

        public static void SetGetCertFileName(this string filename)
        {
            CertFileName = filename;
        }
        /// <summary>
        ///  /// 是否启动证书验证
        /// 证书需要对操作系统进行配置才能验证通过
        ///  Windows xp/2003
        ///  1. 单击 开始 ，单击 运行 ，键入 mmc ，然后单击 确定 。
        ///  2. 在 文件 菜单上单击 添加/删除管理单元 。
        ///  3. 在 添加/删除管理单元 对话框中，单击 添加 。
        ///  4. 在 添加独立管理单元 对话框单击 证书 ，然后单击 添加 。
        ///  5. 在在 证书管理单元中 对话框中单击 计算机帐户 ，然后单击 下一步
        ///  6. 在 选择计算机 对话框中，单击 完成 。
        ///  7. 在 添加独立管理单元 对话框单击 关闭 ，然后单击 确定 。
        ///  8. 展开 证书 （本地计算机） ，展开 个人 ，然后单击 证书 。
        ///  9. 右键 -》 所有任务-》导入 选择你的证书导入
        ///  Windows 7及以上
        ///  1. 单击 开始 ，单击 运行 ，键入 mmc ，然后单击 确定 。
        ///  2. 在 文件 菜单上单击 添加/删除管理单元 。
        ///  3. 在 可用的管理单元 列表中选择 证书 ，点击 添加 。
        ///  4. 在 证书管理 对话框中选择 计算机账户 ，然后单击 下一步
        ///  5. 在 选择计算机 对话框中，单击 完成 。
        ///  6. 在 添加或删除管理单元 对话框单击 确定 。
        ///  7. 展开 证书 （本地计算机） ，展开 个人 ，然后单击 证书 。
        ///  8. 右键 -》 所有任务-》导入 选择你的证书导入
        /// </summary>
        /// <param name="status">true:后续的请求启动证书验证，false后续的请求关闭证书验证</param>
        public static void SetGetCertificate(this bool status)
        {
            if (status == true && string.IsNullOrEmpty(CertFileName))
            {
                throw new System.SystemException("需要设置证书路径【CertFileName】后才能启用证书");
            }
            Certificate = status;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls;
        }
        // 证书验证

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            if (Certificate == true)//认证证书
            {
                if (sslPolicyErrors == SslPolicyErrors.None)
                    return true;
                return false;
            }
            else
            {
                //取消证书验证
                return true;//证书一直都通过
            }

        }

        /// <summary>
        /// 字符串以get方式请求网页
        /// </summary>
        /// <param name="url">访问的地址
        /// ：如果需要带参数在地址后自己拼接【?id=2&name=3】这样的格式</param>
        /// <returns>返回的结果</returns>
        public static string WebRequestGet(this string url)
        {
            try
            {
                //构造一个Web请求的对象
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Accept = Accept;
                request.ContentType = ContentType;
                if (Certificate == true)
                {
                    request.ClientCertificates.Add(X509Certificate.CreateFromCertFile(CertFileName));
                    request.KeepAlive = true;
                }
                //获取web请求的响应的内容
                HttpWebResponse response;
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string ReturnXml = reader.ReadToEnd();
                reader.Close();
                response.Close();
                return ReturnXml;
            }
            catch (System.Exception ex)
            {
                return $"通信异常:{ex.Message}";
            }
        }


        /// <summary>
        /// 字符串以Get方式请求网页
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpClienGet(this string url)
        {
            try
            {
                HttpClient hc = new HttpClient();
                string html = hc.GetStringAsync(url).Result;
                return html;
            }
            catch (System.Exception ex)
            {
                return $"通信异常:{ex.Message}";
            }
        }
        /// <summary>
        ///字符串以Get方式请求网页
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="body">传递的参数</param>
        /// <param name="headers">
        /// 默认key是Content-Type
        /// 默认value是application/x-www-form-urlencoded
        /// </param>
        /// <returns></returns>
        public static string WebClientGet(this string url, string body)
        {
            try
            {
                byte[] postData = Encoding.GetBytes(body);//编码，尤其是汉字，事先要看下抓取网页的编码方式
                WebClient webClient = new WebClient();
                
                byte[] responseData = webClient.UploadData(url, "Get", postData);//得到返回字符流 
                string srcString = Encoding.GetString(responseData);//解码
                return srcString;
            }
            catch (System.Exception ex)
            {
                return $"通信异常:{ex.Message}";
            }
        }



        /// <summary>
        /// HttpClientHandler 调用api返回 请求结果 
        /// </summary>
        /// <returns></returns>
        public static string AccessApi(this string ApiUrl)
        {
            try
            {
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback = ((httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    if (Certificate == true)//认证证书
                    {
                        if (policyErrors == SslPolicyErrors.None)
                            return true;
                        return false;
                    }
                    else
                    {
                        //取消证书验证
                        return true;//证书一直都通过
                    }
                });
                using (HttpClient clients = new HttpClient(handler))
                {
                    clients.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    return clients.GetAsync(ApiUrl).Result.Content.ReadAsStringAsync().Result;
                }
            }
            catch (Exception ex)
            {
                return $"通信异常:{ex.Message}";
            }
        }
    }
}
