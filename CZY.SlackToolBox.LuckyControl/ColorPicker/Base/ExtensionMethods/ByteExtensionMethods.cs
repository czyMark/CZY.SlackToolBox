﻿using System;

namespace CZY.SlackToolBox.LuckyControl.ColorPicker.ExtensionMethods
{
    public static class ByteExtensionMethods
    {
        public static int AsPercent(this byte number)
        {
            return Convert.ToInt32((double)number / 255 * 100);
        }

    }

     
}
