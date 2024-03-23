using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Bridge
{
    //手机
    public abstract class Phone
    {
        protected IBrand brand;
        public void SetBrand(IBrand brand)
        {
            this.brand = brand;
        }
        public abstract string Call();
        public abstract string Open();
        public abstract string Close();
    }
    //直板手机
    public class FoldedPhone : Phone
    { 

        public override string Call()
        {
            return "直板"+this.brand.Call();
        }

        public override string Close()
        {
            return "直板" + this.brand.Close();
        }

        public override string Open()
        {
            return "直板" + this.brand.Open();
        }
    }
    //翻盖手机
    public class UpRightPhone : Phone
    {  
        public override string Call()
        {
           return "翻盖" + this.brand.Call();
        }

        public override string Close()
        {
            return "翻盖" + this.brand.Close();
        }

        public override string Open()
        {
            return "翻盖" + this.brand.Open();
        }
    }
}
