using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Builder
{
    public class Building
    {
        //传统建立房子
        public string BulidHose()
        {
            SimpleBuild s = new SimpleBuild();
            return s.StartBuild();
        }

        //建造者创建房子
        public Hose BuliderHose()
        {
            BuilderCommand b = new BuilderCommand(new SimpleBuild());
            return b.GetBuild();
        }

        //建造者创建房子流程
        public string BuliderHoseStr()
        {
            BuilderCommand b = new BuilderCommand();
            b.SetBuilderCommand(new TallBuild());
            return b.GetBuildStr();
        }
    }
}
