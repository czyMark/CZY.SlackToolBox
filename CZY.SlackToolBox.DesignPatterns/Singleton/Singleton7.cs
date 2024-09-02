using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Singleton
{
    //单例模式：采取一定的方法保证在整个软件系统中，对某个类的只能存在一个对象实例，
    //并且该类只提供一个取得其对象的实例方法
    //单例模式实现方式有
    //饿汉式（静态常量）     
    //饿汉式（静态代码块）    
    //懒汉式（线程不安全）    
    //懒汉式（线程安全，同步方法）    
    //懒汉式（线程安全，同步代码块）   
    //双重检查          
    //静态内部类         √
    //枚举
    public class Singleton7
    {
        //静态内部类
        //优点 利用了静态内部类不会在类加载的时候就加载。这样在调用getInstance时，使用到了静态内部类才加载。所以实现了懒加载。没有线程安全问题
        //      类的静态属性只会在第一次加载的时候才会加载,所以静态内部类的静态属性只会实例化一次，效率高
        //缺点 

        //1、构造器私有化
        private Singleton7()
        {
        }
        //2、类的内部创建静态对象
        private static class Singleton7Instance
        {
            public static Singleton7 singleton = new Singleton7();
        } 

        //3、内外暴露一个静态的公共方法，getinstance
        public static Singleton7 getInstance()
        { 
            return Singleton7Instance.singleton;
        }
    }
}
