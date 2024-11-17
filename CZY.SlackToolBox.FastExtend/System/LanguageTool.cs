using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CZY.SlackToolBox.FastExtend
{
    public interface ILanguage
    {
        /// <summary>
        /// 元素ID
        /// </summary>
        string ElementID { get; }
        /// <summary>
        /// 语言编码
        /// </summary>
        string LanguageCode { set; get; }
        /// <summary>
        /// 注册控件元素
        /// </summary>
        /// <param name="element"></param>
        void RegisterElement(ILanguage element);
        /// <summary>
        /// 反注册控件元素
        /// </summary>
        /// <param name="element"></param>
        void UnRegisterElement(ILanguage element);
        /// <summary>
        /// 改变语言环境时触发
        /// </summary>
        void ChangeLanguaged();

    }
    public enum Language
    {
        [Description("中文")]
        Chinese,

        [Description("中文繁体")]
        TraditionalChinese,

        [Description("英文")]
        English,

        [Description("俄语")]
        Russian,

        [Description("德语")]
        German,

        [Description("法文")]
        French,

        [Description("日语")]
        Japanese,

        [Description("韩语")]
        Korean

        //有需要自己扩展
    }
    public class LanguageTool
    {
        public static LanguageTool Instance = new LanguageTool();
        #region 私有成员

        /// <summary>
        /// 注册语言包变化时 要更新的元素
        /// </summary>
        public IDictionary<string, ILanguage> LanguageElementCache = new Dictionary<string, ILanguage>();

        /// <summary>
        /// 语言文件
        /// </summary>
        public IDictionary<Language, string> LanguageResourceMaps = new Dictionary<Language, string>()
        {
            //{ Language.Chinese,@"pack://application:,,,/Language;component/Resources/Zh_CN.xaml" },
            //{ Language.English,@"pack://application:,,,/Language;component/Resources/Zh_EN.xaml" }
        };

        /// <summary>
        /// 缓存的资源
        /// </summary>
        private IDictionary<string, string> LanguageResourceCache = new Dictionary<string, string>();
         
        /// <summary>
        /// 当前语言
        /// </summary>
        private Language language = Language.Chinese;

        public Language Language
        {
            get { return language; }
            set { language = value; }
        }

        /// <summary>
        /// 支持的语言列表
        /// </summary>
        private List<Language> languages = new List<Language>();
        public List<Language> Languages { get { return languages; } }
        #endregion

        #region 公有属性 

        public Action<string> ChangeLanguageResourceHandle { get; set; }

        public Func<string, object> FindResourceHandle { get; set; }

        #endregion

        #region 公有函数
        private LanguageTool()
        {
            foreach (Language enlanguage in Enum.GetValues(typeof(Language)))
                Languages.Add(enlanguage);
        }

        /// <summary>
        /// 初始化资源文件
        /// </summary>
        /// <param name="languageResourceCache"></param>
        public void InitLanguageResourceCache(IDictionary<string, string> languageResourceCache)
        {
            this.LanguageResourceCache = languageResourceCache;
        }

        /// <summary>
        /// 根据文本找对应的翻译
        /// </summary>
        /// <param name="textValue"></param>
        /// <returns></returns>
        public string GetResourceTextValue(string textValue)
        {
            if (!LanguageResourceCache.ContainsKey(textValue)) return textValue;
            string resourceKey = LanguageResourceCache[textValue];
            var resourceValue = FindResource(resourceKey);
            if (resourceValue == null) return textValue;
            return resourceValue.ToString();
        }




        #region 语言改变时触发 --常用于 调整不同语言之间的布局
        public void RegisterLanguageElement(ILanguage element)
        {
            if (LanguageElementCache.ContainsKey(element.ElementID))
                LanguageElementCache.Add(element.ElementID, element);
            else
                LanguageElementCache[element.ElementID] = element;
        }

        public void UnRegisterLanguageElement(ILanguage element)
        {
            if (LanguageElementCache.ContainsKey(element.ElementID))
                LanguageElementCache.Remove(element.ElementID);
        }

        public void ChangeLanguaged()
        {
            foreach (var languageElement in LanguageElementCache)
                languageElement.Value.ChangeLanguaged();
        } 
        #endregion

        public void ChangeLanguageResource()
        {
            if (ChangeLanguageResourceHandle == null) return;
            if (!LanguageResourceMaps.ContainsKey(Language)) return;
            ChangeLanguageResourceHandle(LanguageResourceMaps[Language]);
        }

        #endregion

        #region 私有函数
        private object FindResource(string resourceKey)
        {
            if (FindResourceHandle == null) return null;
            return FindResourceHandle(resourceKey);
        }

        #endregion
    }
}
