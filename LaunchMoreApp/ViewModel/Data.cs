using LaunchMoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace LaunchMoreApp.ViewModel
{
    public class Data
    {
        //id,name,process,type,paths,paths_args,values
        /// <summary>
        /// 存储软件默认的注册列表 信息，方便后续打开使用
        /// </summary>
        private static List<MapInfo> datas = new List<MapInfo>()
        {
            new MapInfo(){
                id = 1,title ="微信",details ="",name = "wechat",
                process ="WeChat",type =1,paths ="",paths_args ="",
                values = new string[] {"BaseNamedObjects\\_WeChat_App_Instance_Identity_Mutex_Name"},
            },
            new MapInfo(){
                id = 2,title ="企业微信",details ="",name = "wxwork",
                process ="WXWork",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\Tencent.WeWork.ExclusiveObjectInstance1",
                    "BaseNamedObjects\\Tencent.WeWork.ExclusiveObject"
                },
            },
            new MapInfo(){
                id = 3,title ="腾讯手游助手",details ="",name = "appmarket",
                process ="AppMarket",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\{AE10602C-2DBC-4a37-BC13-8E12012E16F1}_APPMARKET_1"
                },
            },
            new MapInfo(){
                id = 4,title ="bilibili投稿工具",details ="",name = "ugc_assistant",
                process ="ugc_assistant",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\{F2AD8D0A-F8AE-4467-8D1E-D406B3943CD8}"
                },
            },
            new MapInfo(){
                id = 5,title ="优酷",details ="",name = "youkudesktop",
                process ="YoukuDesktop",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\ikudesktop"
                },
            },
            new MapInfo(){
                id = 6,title ="钉钉",details ="",name = "dingtalklauncher",
                process ="DingTalk",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\{{239B7D43-86D5-4E5C-ADE6-CEC42155B475}}DingTalk_loginframe",
                    "BaseNamedObjects\\{{239B7D43-86D5-4E5C-ADE6-CEC42155B475}}DingTalk"
                },
            },
            new MapInfo(){
                id = 7,title ="拼多多商家平台",details ="",name = "pddworkbench",
                process ="PddWorkbench",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\com.pdd.workbench.single.mutex{50E829F2-9055-40DB-8701-04EF68F0F767}"
                },
            },
            new MapInfo(){
                id = 8,title ="RDO远程工具",details ="",name = "rdo",
                process ="RDO",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\fcf27a87-fc95-462f-bfe5-a8830c21b555"
                },
            },
            new MapInfo(){
                id = 9,title ="希沃白板",details ="",name = "swenlauncher",
                process ="EasiNote",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "Device\\NamedPipe\\E_a_s_i_N_o_t_e_5_",
                    "BaseNamedObjects\net.pipe:",
                    "BaseNamedObjects\net.pipe:"
                },
            },
            new MapInfo(){
                id = 10,title ="百度云盘",details ="",name = "baidunetdisk",
                process ="BaiduNetdisk",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\{DAF9CDB4-1826-4ba0-A6B6-52ABD4C8DE1A}",
                    "BaseNamedObjects\\YunBrowserSharedMemoryLock",
                    "BaseNamedObjects\\YunBrowserSharedMemoryLock",
                },
            },
            new MapInfo(){
                id = 11,title ="天翼云盘",details ="",name = "ecloud",
                process ="eCloud",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\ecloud_{8B817DAA-3143-498b-A2EA-439F08A0C83B}"
                },
            },
            new MapInfo(){
                id = 12,title ="魔兽争霸官方对战平台",details ="",name = "platform",
                process ="Platform",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\dotnetfranewarkt",
                    "BaseNamedObjects\\NETEASE_BATTLE_PLATFORM_"
                },
            },
            new MapInfo(){
                id = 13,title ="UU加速器",details ="",name = "uu",
                process ="uu",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\Netease GACC Running Event"
                },
            },
            new MapInfo(){
                id = 14,title ="腾讯会议",details ="",name = "wemeetapp",
                process ="wemeetapp",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\WEMEET_APP_{ABC4C2CC-D179-45DE-9422-6EA4E517F009}"
                },
            },
            new MapInfo(){
                id = 15,title ="有道云笔记",details ="",name = "youdaonote",
                process ="YoudaoNote",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\NetEase Youdao YNote Limit One Instance Mutex"
                },
            },
            new MapInfo(){
                id = 16,title ="梦幻西游手游",details ="",name = "myLauncher",
                process ="mymain",type =2,paths ="mymain",paths_args ="__MYLAUNCHER_MYMAIN_TAG__",
                values = new string[] {
                    "BaseNamedObjects\\mymainmutex0",
                    "BaseNamedObjects\\mymainmutex1"
                },
            },
            new MapInfo(){
                id = 17,title ="爱思助手7.0",details ="",name = "i4tools",
                process ="i4Tools",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "qtsingleapp-i4tool-340c-1-lockfile"
                },
            },
            new MapInfo(){
                id = 18,title ="大话西游手游",details ="",name = "xypclaunch",
                process ="xymain",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\dhxysywebshared_0",
                    "BaseNamedObjects\\dhxysywebshared_1",
                    "BaseNamedObjects\\dhxysywebshared_2",
                },
            },
            new MapInfo(){
                id = 19,title ="拼多多商家平台",details ="",name = "pddworkbench2",
                process ="PddWorkbench2",type =1,paths ="",paths_args ="",
                values = new string[] {
                    "BaseNamedObjects\\com.pdd.workbench.single.mutex{50E829F2-9055-40DB-8701-04EF68F0F767}"
                },
            },
        };
        public static ResultInfo getSoftInfo(string name)
        {
            name = name.ToLower();
            ResultInfo res = new ResultInfo();
            res.code = 1;
            res.msg = "";
            List<MapInfo> map_Info = new List<MapInfo>();
            foreach (MapInfo result_Info in datas)
            {
                if (name.Equals(result_Info.name.ToLower()))
                {
                    map_Info.Add(result_Info);
                }
                
            }

            res.data = map_Info;
            return res;
        }
        public static ResultAll getAllSoft()
        {
            ResultAll res = new ResultAll();
            res.code = 1;
            res.msg = "";
            List<MapAll> map_All = new List<MapAll>();
            foreach(MapInfo result_Info in datas)
            {
                map_All.Add(new MapAll()
                {
                    title =result_Info.title,
                    details =result_Info.details,
                });
            }
            res.data = map_All;
            return res;
        }
    }
}
