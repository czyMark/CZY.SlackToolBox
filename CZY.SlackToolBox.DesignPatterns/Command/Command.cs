using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.DesignPatterns.Command
{
    #region 发送命令者
    //遥控器
    public class RemoteControl
    {
        //打开按钮
        Command[] OnCommand;
        //关闭按钮
        Command[] OffCommand;
        //回退按钮
        Command UndoCommand;

        //初始化命令
        public RemoteControl()
        {
            OnCommand = new Command[5];
            OffCommand = new Command[5];
            for (int i = 0; i < 5; i++)
            {
                OnCommand[i] = new NullCommand();
                OffCommand[i] = new NullCommand();
            }
        }
        //给按钮设置需要的命令
        public void setCommand(int no, Command onCommand, Command offCommand)
        {
            OnCommand[no] = onCommand;
            OffCommand[no] = offCommand;
        }
        //打开按钮
        public string onButtonCommand(int no, string CommandCode)
        {
            //找到要出发的命令，并调用执行
            string str = OnCommand[no].Execute(CommandCode);
            //记录最后这次的操作、用于撤销
            UndoCommand = OnCommand[no];
            return str;
        }

        //关闭按钮
        public string offButtonCommand(int no, string CommandCode)
        {
            //找到要出发的命令，并调用执行
            string str = OffCommand[no].Execute(CommandCode);
            //记录最后这次的操作、用于撤销
            UndoCommand = OffCommand[no];
            return str;
        }

        //按下撤销按钮
        public string undoButtonCommand(string CommandCode)
        {
            //执行撤销操作
            return UndoCommand.Undo(CommandCode);
        }

    }


    #endregion

    #region 统一命令接口
    public interface Command
    {
        //执行命令
        string Execute(string commandCode);
        //撤销命令
        string Undo(string commandCode);
    }
    #endregion

    #region 命令
    /// <summary>
    /// 默认遥控器按钮是没有命令的，只有在绑定后才能有对应的命令，如果绑定空命令，就不需要在判断是否有无命令，可以提高效率性能。
    /// </summary>
    public class NullCommand : Command
    {
        public string Execute(string commandCode)
        {
            return "无命令\r\n";
        }

        public string Undo(string commandCode)
        {
            return "无命令\r\n";
        }
    }

    //电灯Light 
    public class LightCommand : Command
    {
        LightReceiver light;
        public string SetBinding(LightReceiver lightReceiver)
        {
            this.light = lightReceiver;
            return "电灯与命令链接成功！\r\n\r\n";
        }
        public string Execute(string commandCode)
        {
            if (commandCode == "001")
            {
                return light.Open();
            }
            else if (commandCode == "002")
            {
                return light.ChangeColor();
            }
            else if (commandCode == "003")
            {
                return light.Close();
            }
            return "没有找到对应的命令\r\n";
        }

        public string Undo(string commandCode)
        {
            return light.Close();
        }
    }
    //冰箱Refrigerator 
    public class RefrigeratorCommand : Command
    {
        LightReceiver light = new LightReceiver();
        public string Execute(string commandCode)
        {
            return light.Open();
        }

        public string Undo(string commandCode)
        {
            return light.Close();
        }
    }
    //空调AirConditioner
    public class AirConditionerCommand : Command
    {
        LightReceiver light = new LightReceiver();
        public string Execute(string commandCode)
        {
            return light.Open();
        }

        public string Undo(string commandCode)
        {
            return light.Close();
        }
    }
    //洗衣机washing machine
    public class WashingMachineCommand : Command
    {
        LightReceiver light = new LightReceiver();
        public string Execute(string commandCode)
        {
            return light.Open();
        }

        public string Undo(string commandCode)
        {
            return light.Close();
        }
    }
    //电饭煲Rice cooker
    public class RiceCookerCommand : Command
    {
        RiceCookerReceiver riceCooker; 
        public string SetBinding(RiceCookerReceiver riceCooker)
        {
            this.riceCooker = riceCooker;
            return "电饭煲与命令链接成功！\r\n\r\n";
        }
        public string Execute(string commandCode)
        {
            if (commandCode == "001")
            {
                return riceCooker.Open();
            }
            else if (commandCode == "002")
            {
                return riceCooker.SettingTime(30);
            }
            else if (commandCode == "00201")
            {
                return riceCooker.SettingTime(15);
            }
            else if (commandCode == "00202")
            {
                return riceCooker.SettingTime(30);
            }
            else if (commandCode == "00203")
            {
                return riceCooker.SettingTime(45);
            }
            else if (commandCode == "00204")
            {
                return riceCooker.SettingTime(60);
            }
            else if (commandCode == "003")
            {
                return riceCooker.Close();
            }
            return "没有找到对应的命令\r\n";
        }

        public string Undo(string commandCode)
        {
            return riceCooker.Close();
        }
    }
    #endregion

    #region 命令接收者

    /// <summary>
    /// 电灯
    /// </summary>
    public class LightReceiver
    {
        int i;
        public string Open()
        {
            return "打开电灯\r\n";
        }
        public string Close()
        {
            return "关闭电灯\r\n";
        }
        public string ChangeColor()
        {
            switch (i)
            {
                case 1: i++; return "电灯更换为黄色\r\n";
                case 2: i++; return "电灯更换为绿色\r\n";
                case 3: i++; return "电灯更换为红色\r\n";
                case 4: i = 0; return "电灯更换为白色暗色\r\n";
                default:
                    i++; return "电灯更换为白色\r\n";
            }

        }
    }


    /// <summary>
    /// 电饭煲
    /// </summary>
    public class RiceCookerReceiver
    {
        public string Open()
        {
            return "打开电饭煲\r\n";
        }
        public string Close()
        {
            return "关闭电饭煲\r\n";
        }
        public string SettingTime(int i)
        {
            return "设置煮饭时间" + i + "分钟\r\n";
        }
    }
    #endregion
}
