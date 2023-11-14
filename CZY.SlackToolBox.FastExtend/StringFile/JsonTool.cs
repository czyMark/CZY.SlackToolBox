using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace  CZY.SlackToolBox.FastExtend
{
    public static class JsonTool
    {
        //json文件存储的字符集
        public static Encoding Encoding = Encoding.UTF8;
        //设置自动缩进
        public static bool Indent = true;

        /// <summary>
        /// 读取json配置文件,如果文件不存在；新建文件赋予默认值后在返回实体
        /// </summary>
        /// <typeparam name="T">读取出对应的实体</typeparam>
        /// <param name="path">文件路径</param>
        /// <returns>读取出的对应的实体 如果没有找到或转换出错返回null</returns>
        public static T LoadJsonData<T>(this string path)
        {
            try
            {
                //读取文件
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs, Encoding))
                    {
                        var json = sr.ReadToEnd().ToString();
                        var newT = json.DeserializeJson<T>();
                        return newT;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("加载文件错误：" + e.Message);
                return (T)new object();
            }
        }

        /// <summary>
        /// 将json存储本地
        /// </summary>
        /// <typeparam name="T">读取出对应的实体</typeparam>
        /// <param name="path">文件路径</param>
        /// <param name="info">要序列化保存的信息</param>
        /// <param name="encoding">字符集</param>
        /// <returns></returns>
        public static bool SaveJsonFile(this string path, object info)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(path)))//验证文件路径是否存在
                {
                    //验证文件路径是否存在，不存在就创建
                    Path.GetDirectoryName(path).CreateDirectory();
                }
                System.IO.File.WriteAllText(path, info.SerializeJson(), Encoding);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("保存文件错误：" + e.Message);
                return false;
            }
        }

        #region 反序列化

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeJson<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        #endregion

        #region 序列化

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SerializeJson(this object obj)
        {
            if (Indent)
                return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented);
            else
                return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None);

        }
        #endregion

    }
}
