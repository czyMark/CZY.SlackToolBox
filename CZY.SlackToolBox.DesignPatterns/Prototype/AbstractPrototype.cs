using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Prototype
{
    //羊原型抽象类
    [Serializable]
    public abstract class SheepPrototype
    {
        public abstract object clone();
        public abstract object cloneCopy();
    }
}
