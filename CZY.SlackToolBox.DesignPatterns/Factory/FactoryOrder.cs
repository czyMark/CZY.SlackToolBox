using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Factory
{
    /// <summary>
    /// 工厂订购披萨
    /// </summary>
    public class FactoryOrder
    {
        //简单工厂模式
        SimpleFactory simpleFactory = new SimpleFactory();
        public string FactorySimpleOrder(string PizeeName)
        {
            string str = "";
            Pizza p = simpleFactory.CreatePizee(PizeeName);
            if (p != null)
            {
                str += p.PizeeName + "\r\n";
                str += p.Prepare() + "\r\n";
                str += p.Bake() + "\r\n";
                str += p.Cut() + "\r\n";
                str += p.Box() + "\r\n";
            }
            return str;
        }

        //工厂方法模式
        FunFactory funFactory;
        public FactoryOrder()
        {
        }

        public string FactoryFunOrder(string AreaName, string PizeeName)
        {
            if (AreaName == "CHINA")
                funFactory = new FunChinaFactory();
            if (AreaName == "USA")
                funFactory = new FunUSAFactory();
            string str = "";
            Pizza p = funFactory.CreatePizee(PizeeName);
            if (p != null)
            {
                str += p.AreaName;
                str += p.PizeeName + "\r\n";
                str += p.Prepare() + "\r\n";
                str += p.Bake() + "\r\n";
                str += p.Cut() + "\r\n";
                str += p.Box() + "\r\n";
            }
            return str;
        }

        //抽象工厂
        AbstractFactory abstractFactory;
        public string msg;
        public FactoryOrder(AbstractFactory factory)
        {
            msg=AbstractFactoryOrder(factory);
        }
        private string AbstractFactoryOrder(AbstractFactory factory)
        {
            this.abstractFactory = factory;
            string str = "";
            Pizza p = abstractFactory.CreatePizee();
            if (p != null)
            {
                str += p.AreaName;
                str += p.PizeeName + "\r\n";
                str += p.Prepare() + "\r\n";
                str += p.Bake() + "\r\n";
                str += p.Cut() + "\r\n";
                str += p.Box() + "\r\n";
            }
            return str;
        }
    }

    //简单工厂
    public class SimpleFactory
    {
        //特点：统一的创建对象方法，这样在扩展类的时候只修改工厂和增加实体类，统一的修改这样其他的地方就不用修改
        //优点：符合开闭原则 降低类之间的耦合关系
        //对比传统的在哪用就在哪创建的方式更容易维护
        //升级路线，当前的工厂方法可以使用单例模式。这样工厂类全局就实例一次，工厂就不会被频繁创建
        public Pizza CreatePizee(string PizeeName)
        {
            Pizza p = null;
            if (PizeeName == "Cheese")
            {
                p = new CheesePizza();
            }
            if (PizeeName == "Greek")
            {
                p = new GreekPizza();
            }
            return p;
        }
    }


    //工厂方法
    public abstract class FunFactory
    {
        //特点：统一的创建对象方法，这样在扩展类的时候只修改工厂和增加实体类，统一的修改这样其他的地方就不用修改
        //优点：符合开闭原则 降低类之间的耦合关系
        //对比传统的在哪用就在哪创建的方式更容易维护
        public abstract Pizza CreatePizee(string PizeeName);
    }
    public class FunChinaFactory : FunFactory
    {
        //创建北京Pizee
        public override Pizza CreatePizee(string PizeeName)
        {
            Pizza p = null;
            if (PizeeName == "Cheese")
            {
                p = new CheesePizza();
                p.AreaName = "中国";
                p.PizeeName = "奶酪披萨";
            }
            if (PizeeName == "Greek")
            {
                p = new GreekPizza();
                p.AreaName = "中国";
                p.PizeeName = "胡椒披萨";
            }
            return p;
        }
    }
    public class FunUSAFactory : FunFactory
    {
        //创建美国Pizee
        public override Pizza CreatePizee(string PizeeName)
        {
            Pizza p = null;
            if (PizeeName == "Cheese")
            {
                p = new CheesePizza();
                p.AreaName = "美国";
                p.PizeeName = "奶酪披萨";
            }
            if (PizeeName == "Greek")
            {
                p = new GreekPizza();
                p.AreaName = "美国";
                p.PizeeName = "胡椒披萨";
            }
            return p;
        }
    }

    //抽象工厂
    public interface AbstractFactory
    {
        //特点：统一的创建对象方法，这样在扩展类的时候只修改工厂和增加实体类，统一的修改这样其他的地方就不用修改
        //优点：符合开闭原则 降低类之间的耦合关系，依赖倒置原则，高层模板与低层模块直接不应该直接依赖应该依赖于接口。
        //使用工厂的地方只要调用这个接口就能直接创建

        /// <summary>
        /// 让下面的工厂子类来具体实现
        /// </summary>
        /// <param name="PizeeName"></param>
        /// <returns></returns>
        Pizza CreatePizee();
    }
    public class AbstractChinaFactory : AbstractFactory
    {
        string PizeeName;
        public AbstractChinaFactory(string pizeeName)
        {
            this.PizeeName = pizeeName;
        }
        //创建北京Pizee
        public Pizza CreatePizee()
        {
            Pizza p = null;
            if (PizeeName == "Cheese")
            {
                p = new CheesePizza();
                p.AreaName = "中国";
                p.PizeeName = "奶酪披萨";
            }
            if (PizeeName == "Greek")
            {
                p = new GreekPizza();
                p.AreaName = "中国";
                p.PizeeName = "胡椒披萨";
            }
            return p;
        }
    }
    public class AbstractUSAFactory : AbstractFactory
    {
        string PizeeName;
        public AbstractUSAFactory(string pizeeName)
        {
            this.PizeeName = pizeeName;
        }
        //创建美国Pizee
        public Pizza CreatePizee()
        {
            Pizza p = null;
            if (PizeeName == "Cheese")
            {
                p = new CheesePizza();
                p.AreaName = "美国";
                p.PizeeName = "奶酪披萨";
            }
            if (PizeeName == "Greek")
            {
                p = new GreekPizza();
                p.AreaName = "美国";
                p.PizeeName = "胡椒披萨";
            }
            if (PizeeName == "Chicken")
            {
                p = new GreekPizza();
                p.AreaName = "美国";
                p.PizeeName = "鸡肉披萨";
            }
            return p;
        }
    }
}
