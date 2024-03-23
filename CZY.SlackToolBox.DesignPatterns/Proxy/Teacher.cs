using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Proxy
{
    /// <summary>
    /// 代理对象 静态代理
    /// </summary>
    public class TeacherDaoProxy : ITeacherDao
    {
        private ITeacherDao target;

        //构造器
        public TeacherDaoProxy(ITeacherDao target)
        {
            this.target = target;
        }

        //优点：在不修改目标对象的功能前提下，能通过代理对象对目标功能扩展。
        public string teach()
        {
            string str = "代理对象开始代理...\r\n";
            str += target.teach();
            return str;
        }
    }
    /// <summary>
    /// 代理接口
    /// </summary>
    public interface ITeacherDao
    {
        string teach();
    }
    /// <summary>
    /// 被代理对象
    /// </summary>
    public class TeacherDao : ITeacherDao
    {
        public string teach()
        {
            return "老师授课中....";
        }
    }
}
