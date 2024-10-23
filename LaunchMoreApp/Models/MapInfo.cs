using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LaunchMoreApp.Models
{
    public class MapInfo
    {
        //id,name,process,type,paths,paths_args,values
        public int id { set; get; }
        public string title { set; get; }
        public string details { set; get; }
        public string name { set; get; }
        public string process { set; get; }
        public int type { set; get; }
        public string paths { set; get; }
        public string paths_args { set; get; }
        public string[] values { set; get; }

    }

}
