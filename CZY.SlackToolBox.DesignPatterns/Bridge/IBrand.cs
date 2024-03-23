using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Bridge
{
    public interface IBrand
    {
        string Call();
        string Open();
        string Close();
    }
}
