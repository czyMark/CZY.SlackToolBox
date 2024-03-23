using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Composite
{
    //组织
    public abstract class SchoolComponent
    {
        public string Name { get; set; }
        public string Des { get; set; }
        public SchoolComponent(string name, string des)
        {
            this.Name = name;
            this.Des = des;
        }
        public virtual void Add(SchoolComponent component)
        {
            //默认实现
        }
        public virtual void Del(SchoolComponent component)
        {
            //默认实现
        }
        public abstract string Display();
    }
    // University 是Composite,可以关联College
    public class University : SchoolComponent
    {
        //当前存储的College
        List<SchoolComponent> components = new List<SchoolComponent>();
        public University(string name, string des) : base(name, des)
        {
        }
        public override void Add(SchoolComponent component)
        {
            components.Add(component);
        }
        public override void Del(SchoolComponent component)
        {
            components.Remove(component);
        }
        public override string Display()
        {
            string str = Name + "||" + Des + "\r\n";
            foreach (var item in components)
            {
                str += item.Display();
            }
            return str;
        }
    }

    // College 是Composite,可以关联Department
    public class College : SchoolComponent
    {
        //当前存储的Department
        List<SchoolComponent> components = new List<SchoolComponent>();
        public College(string name, string des) : base(name, des)
        {
        }
        public override void Add(SchoolComponent component)
        {
            components.Add(component);
        }
        public override void Del(SchoolComponent component)
        {
            components.Remove(component);
        }
        public override string Display()
        {
            string str = Name + "||" + Des + "\r\n";
            foreach (var item in components)
            {
                str += item.Name + "||" + item.Des + "\r\n";
            }
            return str;
        }
    }
    
    //Department是Leaf叶子节点，
    public class Department : SchoolComponent
    {
        public Department(string name, string des) : base(name, des)
        {
        }
        //由于是最下的节点，不需要写增加，删除，因为他就是最后一次的节点。
        public override string Display()
        {
            return Name + "||" + Des + "\r\n";
        }
    }
}
