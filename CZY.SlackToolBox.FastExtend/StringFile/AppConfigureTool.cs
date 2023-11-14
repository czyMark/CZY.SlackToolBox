using System;
using System.Configuration;

namespace  CZY.SlackToolBox.FastExtend
{
	/// <summary>
	/// 程序运行设置Configure
	/// </summary>
	public static class AppConfigureTool
	{
		///<summary> 
		///依据连接串名字connectionName返回数据连接字符串  
		///</summary> 
		///<param name="connectionName"></param> 
		///<returns></returns> 
		public static string GetConnectionStringsConfig(this string connectionName)
		{
			string connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString.ToString();
			Console.WriteLine(connectionString);
			return connectionString;
		}
		///<summary> 
		///更新连接字符串  
		///</summary> 
		///<param name="connectionName">连接字符串名称</param> 
		///<param name="newConString">连接字符串内容</param> 
		///<param name="newProviderName">数据提供程序名称</param> 
		public static void SetConnectionStringsConfig(this string connectionName, string newConString, string newProviderName)
		{
			bool isModified = false;    //记录该连接串是否已经存在  
			if (ConfigurationManager.ConnectionStrings[connectionName] != null)
			{
				isModified = true;
			}
			ConnectionStringSettings mySettings = new ConnectionStringSettings(connectionName, newConString, newProviderName);

			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			if (isModified)
			{
				config.ConnectionStrings.ConnectionStrings.Remove(connectionName);
			}
			config.ConnectionStrings.ConnectionStrings.Add(mySettings);

			config.Save(ConfigurationSaveMode.Modified);

			ConfigurationManager.RefreshSection("ConnectionStrings");
		}
		///<summary> 
		///返回＊.exe.config文件中appSettings配置节的value项  
		///</summary> 
		///<param name="strKey"></param> 
		///<returns></returns> 
		public static string GetAppConfig(this string strKey)
		{
			foreach (string key in ConfigurationManager.AppSettings)
			{
				if (key == strKey)
				{
					return ConfigurationManager.AppSettings[strKey];
				}
			}
			return null;
		}

		///<summary>  
		///在＊.exe.config文件中appSettings配置节增加一对键、值对  
		///</summary>  
		///<param name="newKey"></param>  
		///<param name="newValue"></param>  
		public static void SetAppConfig(this string newKey, string newValue)
		{
			bool isModified = false;
			foreach (string key in ConfigurationManager.AppSettings)
			{
				if (key == newKey)
				{
					isModified = true;
				}
			}

			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			if (isModified)
			{
				config.AppSettings.Settings.Remove(newKey);
			}

			config.AppSettings.Settings.Add(newKey, newValue);

			config.Save(ConfigurationSaveMode.Modified);

			ConfigurationManager.RefreshSection("appSettings");
		}
	}
}
