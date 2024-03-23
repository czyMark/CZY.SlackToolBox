using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Singleton
{
    //单例模式：采取一定的方法保证在整个软件系统中，对某个类的只能存在一个对象实例，
    //并且该类只提供一个取得其对象的实例方法
    //单例模式实现方式有
    //饿汉式（静态常量）     
    //饿汉式（静态代码块）    √
    //懒汉式（线程不安全）
    //懒汉式（线程安全，同步方法）
    //懒汉式（线程安全，同步代码块）
    //双重检查
    //静态内部类
    //枚举
    public class Singleton2
    {
        //饿汉式（静态代码块） 
        //优点 在类装载的时候就完成实例化、避免了线程同步问题、代码简单
        //缺点 在类装载的时候就完成实例化，没有达到懒加载的效果
        //     如果程序运行过程中一次都没有用到这个示例则会造成内存的浪费

        //1、构造器私有化
        private Singleton2()
        {
        }
        //静态代码块示例化
        static Singleton2()
        {
            singleton = new Singleton2();
        }
        //2、类的内部创建对象
        private static Singleton2 singleton;
        //3、内外暴露一个静态的公共方法，getinstance
        public static Singleton2 getInstance()
        {
            return singleton;
        }
    }
}
