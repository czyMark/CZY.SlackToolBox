using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Template
{
    /// <summary>
    /// 制作豆浆模板
    /// </summary>
    public abstract class MakeSoyMilk
    {
        public string SoyMilkName { get; set; }
        public string Make()
        {
            string str = string.Empty;
            if (CustomMaterial())
            {
                str += Material() + "\r\n";
            }
            str += Soak() + "\r\n";
            str += Machine() + "\r\n";
            str += "制作完成\r\n";
            return str;
        }
        //先选材料
        public abstract string Material();
        // 浸泡
        public virtual string Soak()
        {
            return "将材料放入水中浸泡";
        }
        //放到豆浆机中
        public virtual string Machine()
        {
            return "将浸泡好的材料放入机器";
        }
        // 是否需要更换材料
        public virtual bool CustomMaterial()
        {
            return true;
        }
    }
    //soybean 黄豆豆浆
    public class Soybean : MakeSoyMilk
    {
        public override string Material()
        {
            SoyMilkName = "黄豆豆浆";
            return "选择黄豆作为材料";
        }
    }

    //Peanut 花生豆浆
    public class Peanut : MakeSoyMilk
    {
        public override string Material()
        {
            SoyMilkName = "花生豆浆";
            return "选择花生作为材料";
        }
        //放到豆浆机中
        public override string Machine()
        {
            return "放入研磨机器将花生变碎。将浸泡好的材料放入机器";
        }
    }
    //Pure 花生豆浆
    public class Pure : MakeSoyMilk
    {
        //不实现其方法
        public override string Material()
        {
            return string.Empty;
        }
        public override bool CustomMaterial()
        {
            return false;
        }
    }
}
