using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Visitor
{
    public abstract class Action
    {
        //获取男的评分
        public abstract string getManResult(Man man);

        //获取女的评分
        public abstract string getWomanResult(Woman man);
    }
    /// <summary>
    /// 良 好
    /// </summary>
    public class Good : Action
    {
        public override string getManResult(Man man)
        {
            return "男的评价为：良 好的";
        }

        public override string getWomanResult(Woman man)
        {
            return "女的评价为：良 好的";
        }
    }
    /// <summary>
    /// 不及格 差的
    /// </summary>
    public class Pass : Action
    {
        public override string getManResult(Man man)
        {
            return "男的评价为：不及格 差的";
        }

        public override string getWomanResult(Woman man)
        {
            return "女的评价为：不及格 差的";
        }
    }

    /// <summary>
    /// 优秀的 很棒
    /// </summary>
    public class Great : Action
    {
        public override string getManResult(Man man)
        {
            return "男的评价为：优秀的 很棒";
        }

        public override string getWomanResult(Woman man)
        {
            return "女的评价为：优秀的 很棒";
        }
    }



    /// <summary>
    /// 人
    /// </summary>
    public abstract class Person
    {
        //提供一个方法，让访问者访问
        public abstract string Accept(Action action);
    }
    /// <summary>
    /// 男人 这里我使用到了双分派、及首先在客户端程序中，
    /// 将具体的状态作为参数传递到Man中(第一次分派)
    /// 将Man中调用作为参数的具体方法，同时将自己的this作为参数传入。（第二次分派）
    /// </summary>
    public class Man : Person
    {
        public override string Accept(Action action)
        {
            return action.getManResult(this);
        }
    }
    /// <summary>
    /// 女人 这里我使用到了双分派、及首先在客户端程序中，
    /// 将具体的状态作为参数传递到Man中(第一次分派)
    /// 将Man中调用作为参数的具体方法，同时将自己的this作为参数传入。（第二次分派）
    /// </summary>
    public class Woman : Person
    {
        public override string Accept(Action action)
        {
            return action.getWomanResult(this);
        }
    }


    public class ObjectStructure
    {
        /// <summary>
        /// 所有参赛人
        /// </summary>
        private List<Person> persons = new List<Person>();
        /// <summary>
        /// 增加参赛人信息
        /// </summary>
        /// <param name="person"></param>
        public void Add(Person person)
        {
            persons.Add(person);
        }
        /// <summary>
        /// 删除参赛人信息
        /// </summary>
        /// <param name="person"></param>
        public void Del(Person person)
        {
            persons.Remove(person);
        }
        /// <summary>
        /// 打印所有参赛人信息
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public string Display(Action action)
        {
            string str = string.Empty;
            foreach (var item in persons)
            {
                str += item.Accept(action) + "\r\n";
            }
            return str;
        }
    }

}
