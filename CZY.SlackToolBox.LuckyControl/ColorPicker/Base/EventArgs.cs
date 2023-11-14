using System;

namespace CZY.SlackToolBox.LuckyControl.ColorPicker
{
    public class EventArgs<T> : EventArgs
    {
        public EventArgs(T value)
        {
            mValue = value;
        }

        private readonly T mValue;

        public T Value
        {
            get { return mValue; }
        }


    }
}
