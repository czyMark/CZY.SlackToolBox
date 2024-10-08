﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CZY.SlackToolBox.LuckyControl.ColorPicker.ExtensionMethods;

namespace CZY.SlackToolBox.LuckyControl.ColorPicker.ColorModels.CMYK
{
    class Black : ColorComponent
    {
        public static CMYKModel sModel = new CMYKModel();

        public override int MinValue
        {
            get {return 0; }
        }

        public override int MaxValue
        {
            get { return 100; }
        }

        public override int Value(System.Windows.Media.Color color)
        {
           return sModel.KComponent(color).AsPercent();
        }

        public override string Name
        {
            get { return  "CMYK_Black"; }
        }
    }
}
