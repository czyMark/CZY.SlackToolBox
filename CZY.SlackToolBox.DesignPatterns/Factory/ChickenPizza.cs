using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Factory
{
    public class ChickenPizza : Pizza
    {
        public ChickenPizza()
        {
            PizeeName = "鸡肉披萨";
        }
        public override string Bake()
        {
            return PizeeName + "制作";
        }

        public override string Prepare()
        {
            return PizeeName + "准备材料";
        }
    }
}
