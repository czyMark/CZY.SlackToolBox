using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Adapter
{

    //原始电源
    public class PowerV
    {
        public string OutputV()
        {
            return "电压220V";
        }
        public string OutputPickV()
        {
            return "电压500V";
        }
    }
    //充电口
    public interface IPowerHole
    {
        string Request5V();
        string Request200V();
        string Request150V();
        string RequestPick300V();
    }

    //--场景说明
    //接入的电源的电压是固定的220V与地接500V 通过电源适配器将不同的接口的电压变成合适的电压返回回去。
    //返回回去的电压就可以用给其他的电器使用。

    //充电口适配方案
    public class PowerAdapterA : PowerV, IPowerHole
    {
        public string Request150V()
        {
            if (this.OutputV() == "电压150V")
            {
                this.OutputV();
            }
            return "电压150V";
        }

        public string Request200V()
        {
            if (this.OutputV() == "电压200V")
            {
                this.OutputV();
            }
            return "电压200V";
        }

        public string RequestPick300V()
        {
            if (this.OutputPickV() == "电压300V")
            {
                this.OutputPickV();
            }
            return "电压300V";
        }

        public string Request5V()
        {
            if (this.OutputV() == "电压5V")
            {
                this.OutputV();
            }
            return "电压5V";
        }
    }


    //--场景说明
    //接入的电源的电压是配置的220V与地接500V 通过电源适配器将不同的接口的电压变成合适的电压返回回去。
    //返回回去的电压就可以用给其他的电器使用。
    //充电口适配方案
    public class PowerAdapterB : IPowerHole
    {
        PowerV _power ;
        public PowerAdapterB(PowerV power)
        {
            _power = power;
        }
        public string Request150V()
        {
            if (_power.OutputV() == "电压150V")
            {
                _power.OutputV();
            }
            return "电压150V";
        }

        public string Request200V()
        {
            if (_power.OutputV() == "电压200V")
            {
                _power.OutputV();
            }
            return "电压200V";
        }

        public string RequestPick300V()
        {
            if (_power.OutputPickV() == "电压300V")
            {
                _power.OutputPickV();
            }
            return "电压300V";
        }

        public string Request5V()
        {
            if (_power.OutputV() == "电压5V")
            {
                _power.OutputV();
            }
            return "电压5V";
        }
    }



    //--场景说明
    //接入的电源的电压是配置的220V与地接500V 通过电源适配器将不同的接口的电压变成合适的电压返回回去。
    //返回回去的电压就可以用给其他的电器使用。
    //充电口适配接口抽象实现
    //abstract是抽象          ：抽象方法子类必须实现        拥有抽象方法的类必须是抽象类 抽象标记的类(抽象类)有且只有一个普通的构造方法 抽象标记的类不能被直接实例化 
    //virtual 是虚方法标记    ：虚方法标记告诉继承的子类方法可以被override，但非强制。 修饰的方法必须有方法实现。
    //虚方法子类实现如果不使用override关键字，仍调用父类的方法
    public abstract class PowerHoleAbstract : IPowerHole
    {
        public virtual string Request150V()
        {
            return string.Empty;
        }

        public virtual string Request200V()
        {
            return string.Empty;
        }

        public virtual string Request5V()
        {
            return string.Empty;
        }

        public virtual string RequestPick300V()
        {
            return string.Empty;
        }
    }

    public class PowerAdapterC : PowerHoleAbstract
    {
        PowerV _power;
        public PowerAdapterC(PowerV power)
        {
            _power = power;
        }
        public override  string Request150V()
        {
            if (_power.OutputV() == "电压150V")
            {
                _power.OutputV();
            }
            return "电压150V";
        }
        public override string Request200V()
        {
            if (_power.OutputV() == "电压200V")
            {
                _power.OutputV();
            }
            return "电压200V";
        }
    }
}
