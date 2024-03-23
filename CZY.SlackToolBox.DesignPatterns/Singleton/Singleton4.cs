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
    //懒汉式（线程安全，同步方法）    √
    //懒汉式（线程安全，同步代码块）
    //双重检查
    //静态内部类
    //枚举
    public class Singleton4
    {
        //懒汉式（线程安全，同步方法）
        //优点 用到的时候才实例化（可以懒加载），解决了线程不安全问题
        //缺点 效率低，每次getInstance是同步，每次都会同步执行

        //1、构造器私有化
        private Singleton4()
        {
        }
        //2、类的内部创建静态对象
        private static Singleton4 singleton;
        //3、内外暴露一个静态的公共方法，getinstance

        //定义一个保证线程同步的标识 保证同步
        private static readonly object locker = new object();
        public static Singleton4 getInstance()
        { 
            lock (locker)
            { 
                //通过线程锁的方式 同步singleton变量，确保判断只能进入一次
                if (singleton == null)
                {
                    singleton = new Singleton4();
                    //模拟多线程 执行时的未执行完成创建的 特殊情况 
                    Thread.Sleep(1000);
                }
                return singleton;
            }
        }
    }
}
