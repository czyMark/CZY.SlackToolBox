using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FrameTemplate
{
    public static class StartFrame
    {
        public enum FrameTemplateType
        {
            YXKJ,
            AirportCS
        }
        public static void StartFrameTemplate(FrameTemplateType templateType)
        {
            switch (templateType)
            {
                case FrameTemplateType.YXKJ: 
                    //检查加载的资源

                    break;
                case FrameTemplateType.AirportCS:
                    //检查加载的资源

                    break;
            }
        }
    }
}
