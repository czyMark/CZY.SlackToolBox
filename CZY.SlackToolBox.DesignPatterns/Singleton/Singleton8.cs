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
    //静态内部类
    //枚举             √
    public enum Singleton8
    {
        //枚举单例是为了加上延迟加载特性，同时还线程安全，写起来还简单
        //C#枚举是结构，不能写方法没有多大参照意义，如果非要使用的话直接用Lazy<T>就可以。
        //JAVA枚举是一个特殊的枚举类的子类，可以跟其他类一样写方法 所以写这种模式是有意义的。
    }
}
