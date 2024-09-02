using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Decorator
{
    //水单
    public abstract class Drink
    {
        public string Detail { get; set; }
        public string Des { get; set; }
        public float Price { get; set; }
        //计算费用的抽象方法
        //会被子类来实现
        public abstract float Cost();
    }
    //装饰者
    public class Coffee : Drink
    {
        public override float Cost()
        {
            return base.Price;
        }
    }
    public class LatteCoffee : Coffee
    {
        public LatteCoffee()
        {
            Des = "拿铁咖啡";
            Price = 6;
        }
    }
    public class LongBlackCoffee : Coffee
    {
        public LongBlackCoffee()
        {
            Des = "美式咖啡";
            Price = 5;
        }
    }
    public class ShortBlackCoffee : Coffee
    {
        public ShortBlackCoffee()
        {
            Des = "ShortBlack";
            Price = 4.5f;
        }
    }

}
