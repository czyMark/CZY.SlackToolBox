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
    //双重检查          √
    //静态内部类
    //枚举
    public class Singleton6
    {
        //双重检查
        //优点 用到的时候才实例化（可以懒加载），解决线程不安全问题
        //缺点 

        //1、构造器私有化
        private Singleton6()
        {
        }
        //2、类的内部创建静态对象
        private static volatile Singleton6 singleton;

        //volatile关键字修饰变量，关键字表示字段可能被多个并发执行线程修改。
        //声明为 volatile 的字段不受编译器优化（假定由单个线程访问）的限制。这样可以确保该字段在任何时间呈现的都是最新的值。

        //定义一个保证线程同步的标识 保证同步
        private static readonly object locker = new object();

        //3、内外暴露一个静态的公共方法，getinstance
        public static  Singleton6 getInstance()
        {
            if (singleton == null)
            {
                lock (locker)
                {
                    //通过线程锁的方式 同步singleton变量，确保判断只能进入一次
                    if (singleton == null)
                    {
                        singleton = new Singleton6();
                        //模拟多线程 执行时的未执行完成创建的 特殊情况 
                        Thread.Sleep(1000);
                    }
                    return singleton;
                }

            }
            return singleton;
        }
    }
}
