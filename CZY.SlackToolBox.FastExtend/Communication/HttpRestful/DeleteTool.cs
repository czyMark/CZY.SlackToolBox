using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace  CZY.SlackToolBox.FastExtend
{
	public static class DeleteTool
	{
        //Accept
        public static string Accept = "text/html, application/xhtml+xml, */*";
        //发送内容类型
        public static string ContentType = "application/json";
        //传递的字符集
        public static Encoding Encoding = Encoding.UTF8;
        //证书路径
        public static string CertFileName;
        private static bool Certificate;

        public static void SetDeleteCertFileName(this string filename)
        {
            CertFileName = filename;
        }

        /// <summary>
        /// 是否启动证书验证
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
        public static void SetDeleteCertificate(this bool status)
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
        /// 字符串以Post方式请求网页
        /// </summary>
        /// <param name="url">post请求地址</param>
        /// <param name="body">
        /// ContentType="application/json"  参数格式:  {username:"admin",password:"123} 如果参数不是json类型会报错
        /// ContentType="application/x-www-form-urlencoded" 参数格式:  username=admin&password=123 如果参数是json格式或者参数写错不会报错的
        /// </param>
        /// <returns></returns>
        public static string WebRequestDelete(this string url, string body)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "DELETE"; //Delete请求方式
                request.Accept = Accept;
                // 内容类型
                request.ContentType = ContentType;

                byte[] buffer = Encoding.GetBytes(body);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);

                //是否启动证书认证
                if (Certificate == true)
                {
                    request.ClientCertificates.Add(X509Certificate.CreateFromCertFile(CertFileName));
                    request.KeepAlive = true;
                }

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


    }
}
