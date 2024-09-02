using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Facade
{
    //电梯广告
    public class SysFacade
    {
        //定义所有参与子系统的数据
        DVDPlayer dvd;
        FoodMachine food;
        TV tv;
        Elevator elevator;
        //初始化对象
        public SysFacade()
        {
            dvd = DVDPlayer.getInstance();
            food = FoodMachine.getInstance();
            tv = TV.getInstance();
            elevator = Elevator.getInstance();
        }
        /// <summary>
        /// 电梯程序准备运行
        /// </summary>
        /// <returns></returns>
        public string Ready()
        {
            string str = string.Empty; 
            str += dvd.ON();
            str += tv.ON(); 
            str += tv.Conntion();
            str += tv.Play();
            return str;
        }
        /// <summary>
        /// 电梯程序关闭
        /// </summary>
        /// <returns></returns>
        public string Close()
        {
            string str = string.Empty;
            str += dvd.OFF();
            str += tv.OFF();
            return str;
        }
        //电梯上升中
        public string UpFloor()
        {
            string str = string.Empty;
            str += elevator.Up();
            str += dvd.Play(); 
            return str;
        }
        //电梯上升中
        public string DownFloor()
        {
            string str = string.Empty;
            str += elevator.Down();
            str += dvd.Play(); 
            return str;
        } 

        //电梯到达
        public string ArriveFloor()
        {
            string str = string.Empty;
            str += elevator.OpenDoor();
            str += dvd.Stop(); 
            str += elevator.CloseDoor();
            return str;
        } 
    } 

    //dvd播放器
    public class DVDPlayer
    {
        private static DVDPlayer dvd = new DVDPlayer();
        public static DVDPlayer getInstance()
        {
            return dvd;
        }
        private DVDPlayer()
        { 
        }
        public string ON()
        {
            return "打开DVD\r\n";
        }
        public string OFF()
        {
            return "关闭DVD\r\n";
        }
        public string Play()
        {
            return "开始播放\r\n";
        }
        public string Stop()
        {
            return "停止播放\r\n";
        }
    }

    //食物制造机器
    public class FoodMachine
    {
        private static FoodMachine food = new FoodMachine();
        public static FoodMachine getInstance()
        {
            return food;
        }
        private FoodMachine()
        {
        }
        public string ON()
        {
            return "打开食物机器\r\n";
        }
        public string OFF()
        {
            return "关闭食物机器\r\n";
        }
        public string Make()
        {
            return "制作食物\r\n";
        }
    }

    //电视
    public class TV
    {
        private static TV tv = new TV();
        public static TV getInstance()
        {
            return tv;
        }
        private TV()
        {
        }
        public string ON()
        {
            return "打开电视机\r\n";
        }
        public string OFF()
        {
            return "关闭电视机\r\n";
        }
        public string Conntion()
        {
            return "链接信号源\r\n";
        }
        public string Play()
        {
            return "播放电视剧\r\n";
        }
    }

    //电梯
    public class Elevator
    {
        private static Elevator elevator = new Elevator();
        public static Elevator getInstance()
        {
            return elevator;
        }
        private Elevator()
        {
        }
        public string Up()
        {
            return "上升\r\n";
        }
        public string Down()
        {
            return "下降\r\n";
        }
        public string OpenDoor()
        {
            return "打开门\r\n";
        }
        public string CloseDoor()
        {
            return "关闭门\r\n";
        }
    }



}
