using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchMoreApp.Models
{
    public class ResultAll
    {
        public int code { set; get; }
        public string msg { set; get; }
        public List<MapAll> data { set; get; }
    }

}
