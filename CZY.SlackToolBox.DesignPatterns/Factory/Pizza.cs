using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Factory
{
    /// <summary>
    /// 披萨抽象类
    /// </summary>
    public abstract class Pizza
    {

        public string PizeeName;
        public string AreaName;
        /// <summary>
        /// 准备材料
        /// </summary>
        public abstract string Prepare();
        /// <summary>
        /// 烘烤披萨
        /// </summary>
        public abstract string Bake();
        /// <summary>
        /// 切割
        /// </summary>
        public string Cut()
        {
            return PizeeName + "切割";
        }
        /// <summary>
        /// 打包
        /// </summary>
        public string Box()
        {
            return PizeeName+"打包";
        }
    }
}
