namespace  CZY.SlackToolBox.FastExtend
{
    public static class RestfulApiTool
    { 
        /// <summary>
        /// 设置所有的web请求证书
        /// </summary>
        /// <param name="status">是否启用证书验证</param>
        public static void SetCertificate(this bool status)
        {
            status.SetDeleteCertificate();
            status.SetGetCertificate();
            status.SetPostCertificate();
            status.SetPutCertificate();
        }
        /// <summary>
        /// 设置所有的web请求证书路径
        /// </summary>
        /// <param name="filename">证书路径</param>
        public static void SetCertFileName(this string filename)
        {
            filename.SetDeleteCertFileName();
            filename.SetGetCertFileName();
            filename.SetPostCertFileName();
            filename.SetPutCertFileName();
        }

    }
}
