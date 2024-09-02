using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.FlyWeight
{
    //根据需求返回一个网站
    public class WebsiteFactory
    {
        //网站池
        Dictionary<string, ConcreteWebsite> webList = new Dictionary<string, ConcreteWebsite>();

        //根据类型返回网站，如果没有就创建一个网站，并放入到池中
        public Website getWebsiteConcrete(string type)
        {
            if (!webList.ContainsKey(type))
            {
                //创建网站并放入池
                webList.Add(type, new ConcreteWebsite(type));
            }
            return (Website)webList[type];
        }
        public int getWebsiteCount()
        {
            return webList.Count;
        }
    }

    //内部状态
    public abstract class Website
    {
        public abstract string use(User user);
    }
    //ConcreteFlyWebsite
    public class ConcreteWebsite : Website
    {
        public string Type { get; set; }
        //构造器
        public ConcreteWebsite(string type)
        {
            this.Type = type;
        }
        //使用形式
        public override string use(User user)
        {
            return "网站的发布形式" + Type + "正在使用中... 使用者是"+ user.Name;
        }
    }

    //外部状态
    public class User
    {
        public string Name { get; set; }
        public User(string userName)
        {
            this.Name = userName;
        }
    }
}
