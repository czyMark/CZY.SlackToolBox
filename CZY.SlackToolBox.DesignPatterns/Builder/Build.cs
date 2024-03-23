using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Builder
{
    //被建造的类 产品
    public class Hose
    {
        public string Name { get; set; }
        public string Make1 { get; set; }
        public string Make2 { get; set; }
        public string Make3 { get; set; }
    }
    //建造工厂 抽象制造者
    public abstract class BuildHose
    {
        public Hose hose = new Hose();
        /// <summary>
        /// 打桩
        /// </summary>
        public abstract string Pile();
        /// <summary>
        /// 砌墙
        /// </summary>
        public abstract string Wall();
        /// <summary>
        /// 封顶
        /// </summary>
        public abstract string Caps();

        public string StartBuild()
        {
            string str = "";
            str += Pile() + "\r\n";
            str += Wall() + "\r\n";
            str += Caps() + "\r\n";
            return str;
        }
        public Hose GetBuild()
        {
            Pile();
            Wall();
            Caps();
            return hose;
        }
    }

    //建造工厂具体实现工人  具体创建人团队
    public class SimpleBuild : BuildHose
    {
        /// <summary>
        /// 打桩
        /// </summary>
        public override string Pile()
        {
            hose.Name = "简易";
            hose.Make1 = "打桩";
            return "简易打桩";
        }
        /// <summary>
        /// 砌墙
        /// </summary>
        public override string Wall()
        {
            hose.Make2 = "砌墙";
            return "简易砌墙";
        }
        /// <summary>
        /// 封顶
        /// </summary>
        public override string Caps()
        {
            hose.Make3 = "封顶";
            return "简易封顶";
        }
    }
    public class TallBuild : BuildHose
    {
        /// <summary>
        /// 打桩
        /// </summary>
        public override string Pile()
        {
            hose.Name = "高楼";
            hose.Make1 = "打桩";
            return "高楼打桩";
        }
        /// <summary>
        /// 砌墙
        /// </summary>
        public override string Wall()
        {
            hose.Make2 = "砌墙";
            return "高楼砌墙";
        }
        /// <summary>
        /// 封顶
        /// </summary>
        public override string Caps()
        {
            hose.Make3 = "封顶";
            return "高楼封顶";
        }
    }
    public class BeautifulBuild : BuildHose
    {
        /// <summary>
        /// 打桩
        /// </summary>
        public override string Pile()
        {
            hose.Name = "漂亮房子";
            hose.Make1 = "打桩";
            return "漂亮房子打桩";
        }
        /// <summary>
        /// 砌墙
        /// </summary>
        public override string Wall()
        {
            hose.Make1 = "砌墙";
            return "漂亮房子砌墙";
        }
        /// <summary>
        /// 封顶
        /// </summary>
        public override string Caps()
        {
            hose.Make1 = "封顶";
            return "漂亮房子封顶";
        }
    }

    //建造指挥人
    public class BuilderCommand
    {
        public BuildHose buildHose { get; set; }
        public BuilderCommand()
        {
        }
        public BuilderCommand(BuildHose build)
        {
            buildHose = build;
        }
        public void SetBuilderCommand(BuildHose build)
        {
            buildHose = build;
        }
        public string GetBuildStr()
        {
            string str = "";
            //指挥人 指挥创建房子
            str += buildHose.Pile();
            str += buildHose.Wall();
            str += buildHose.Caps();
            return str;
        }
        public Hose GetBuild()
        {
            //指挥人 指挥创建房子
            buildHose.Pile();
            buildHose.Wall();
            buildHose.Caps();
            return buildHose.hose;
        }
    }
}
