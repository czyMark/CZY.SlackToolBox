using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Bridge
{
    public class HW : IBrand
    {
        public string Call()
        {
            return "华为打电话";
        }

        public string Close()
        {
            return "华为关机";
        }

        public string Open()
        {
            return "华为开机";
        }
    }
    public class GCPhone : IBrand
    {
        public string Call()
        {
            return "国产打电话";
        }

        public string Close()
        {
            return "国产关机";
        }

        public string Open()
        {
            return "国产开机";
        }
    }
    public class MI : IBrand
    {
        public string Call()
        {
            return "小米打电话";
        }

        public string Close()
        {
            return "小米关机";
        }

        public string Open()
        {
            return "小米开机";
        }
    }
    public class IPhone : IBrand
    {
        public string Call()
        {
            return "苹果打电话";
        }

        public string Close()
        {
            return "苹果关闭";
        }

        public string Open()
        {
            return "苹果打开";
        }
    }
}
