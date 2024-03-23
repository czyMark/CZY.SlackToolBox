using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Prototype
{
    //由于使用的二进制序列化和反序列化 来深拷贝 ，在序列化的时候类必须增加Serializable属性标记
    [Serializable]
    public class Sheep : SheepPrototype
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Color { get; set; }
        public Address addr { get; set; }
        public Sheep(string name, string age, string color)
        {
            this.Name = name;
            this.Age = age;
            this.Color = color;
            this.addr = new Address();
            this.addr.AreaName = "北京";
            this.addr.AreaRemark = "郊区";
        }

        //浅拷贝。由于对象存储的是地址，复制创建对象也是复制的地址，所以创建之前的类修改值后新的类的值也会修改。
        public override object clone()
        {
            return this.MemberwiseClone();
        }

        //深拷贝后之前的对象修改不影响新创建的对象
        //①将原始实例序列化，然后反序列化赋值给副本对象；
        //②浅拷贝 + 递归的方式，类似于遍历文件夹，对所有的复杂属性、复杂属性内部的复杂属性都进行浅拷贝。
        //利用二进制序列化和反序列化     √当前使用的方法
        public override object cloneCopy()
        {
            return DeepCopyByBinary<Sheep>(this);
        }

        public static T DeepCopyByBinary<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }
    }
    [Serializable]
    public class Address
    {
        public string AreaName { get; set; }
        public string AreaRemark { get; set; }
    }

}
