using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Decorator
{
    //被装饰者
    public class Decortor : Drink
    {
        public Drink obj;
        public Decortor(Drink obj)
        {
            this.obj = obj;
        }
        public override float Cost()
        { 
            return base.Price + obj.Price;
        }
        public string GetDes()
        {
            //输出 obj 被装饰者的信息
            return this.obj.Des + " " + this.obj.Price + " && " + base.Des + " " + base.Price;
        }
    }
    //巧克力
    public class Chocolate : Decortor
    {
        public Chocolate(Drink obj) : base(obj)
        {
            base.Des = "巧克力";
            base.Price = 1;
            base.Detail = GetDes();
        }

    }
    //牛奶
    public class Milk : Decortor
    {
        public Milk(Drink obj) : base(obj)
        {
            base.Cost();
            base.Des = "牛奶";
            base.Price = 2;
            base.Detail = GetDes();
        }
    }
    //豆浆
    public class Soy : Decortor
    {
        public Soy(Drink obj) : base(obj)
        {
            base.Des = "豆浆";
            base.Price = 2;
            base.Detail = GetDes();
        }
    }
}
