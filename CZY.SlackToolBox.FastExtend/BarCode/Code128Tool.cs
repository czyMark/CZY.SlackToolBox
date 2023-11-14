using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace  CZY.SlackToolBox.FastExtend
{

    /// <summary>
    /// 用于生成标准的Code128条码图片
    /// </summary>
    public static class Code128Tool
    {

        /// <summary>
        /// Code128 的生成方式
        /// </summary>
        public enum Code128Encode
        {
            Code128A,
            Code128B,
            Code128C,//128C长度必须是偶数 128C必须是数字
            EAN128//EAN128长度必须是偶数 EAN128必须是数字
        }

        private static uint m_Height = 40;
        private static Font m_ValueFont = null;


        public static DataTable Code128KeyTable = new DataTable();
        /// <summary>
        /// 高度
        /// </summary>
        public static uint Height { get { return m_Height; } set { m_Height = value; } }


        /// <summary>
        /// 是否显示可见号码  如果为NULL不显示号码
        /// </summary>
        public static Font ValueFont { get { return m_ValueFont; } set { m_ValueFont = value; } }



        private static byte m_Magnify = 0;
        /// <summary>
        /// 放大倍数
        /// </summary>
        public static byte Magnify { get { return m_Magnify; } set { m_Magnify = value; } }
        /// <summary>
        /// 初始化条码生成内置关键参数表
        /// </summary>
        public static void InitCode128()
        {
            Code128KeyTable.Columns.Add("ID");
            Code128KeyTable.Columns.Add("Code128A");
            Code128KeyTable.Columns.Add("Code128B");
            Code128KeyTable.Columns.Add("Code128C");
            Code128KeyTable.Columns.Add("BandCode");
            Code128KeyTable.CaseSensitive = true;
            #region 数据表
            Code128KeyTable.Rows.Add("0", " ", " ", "00", "212222");
            Code128KeyTable.Rows.Add("1", "!", "!", "01", "222122");
            Code128KeyTable.Rows.Add("2", "\"", "\"", "02", "222221");
            Code128KeyTable.Rows.Add("3", "#", "#", "03", "121223");
            Code128KeyTable.Rows.Add("4", "$", "$", "04", "121322");
            Code128KeyTable.Rows.Add("5", "%", "%", "05", "131222");
            Code128KeyTable.Rows.Add("6", "&", "&", "06", "122213");
            Code128KeyTable.Rows.Add("7", "'", "'", "07", "122312");
            Code128KeyTable.Rows.Add("8", "(", "(", "08", "132212");
            Code128KeyTable.Rows.Add("9", ")", ")", "09", "221213");
            Code128KeyTable.Rows.Add("10", "*", "*", "10", "221312");
            Code128KeyTable.Rows.Add("11", "+", "+", "11", "231212");
            Code128KeyTable.Rows.Add("12", ",", ",", "12", "112232");
            Code128KeyTable.Rows.Add("13", "-", "-", "13", "122132");
            Code128KeyTable.Rows.Add("14", ".", ".", "14", "122231");
            Code128KeyTable.Rows.Add("15", "/", "/", "15", "113222");
            Code128KeyTable.Rows.Add("16", "0", "0", "16", "123122");
            Code128KeyTable.Rows.Add("17", "1", "1", "17", "123221");
            Code128KeyTable.Rows.Add("18", "2", "2", "18", "223211");
            Code128KeyTable.Rows.Add("19", "3", "3", "19", "221132");
            Code128KeyTable.Rows.Add("20", "4", "4", "20", "221231");
            Code128KeyTable.Rows.Add("21", "5", "5", "21", "213212");
            Code128KeyTable.Rows.Add("22", "6", "6", "22", "223112");
            Code128KeyTable.Rows.Add("23", "7", "7", "23", "312131");
            Code128KeyTable.Rows.Add("24", "8", "8", "24", "311222");
            Code128KeyTable.Rows.Add("25", "9", "9", "25", "321122");
            Code128KeyTable.Rows.Add("26", ":", ":", "26", "321221");
            Code128KeyTable.Rows.Add("27", ";", ";", "27", "312212");
            Code128KeyTable.Rows.Add("28", "<", "<", "28", "322112");
            Code128KeyTable.Rows.Add("29", "=", "=", "29", "322211");
            Code128KeyTable.Rows.Add("30", ">", ">", "30", "212123");
            Code128KeyTable.Rows.Add("31", "?", "?", "31", "212321");
            Code128KeyTable.Rows.Add("32", "@", "@", "32", "232121");
            Code128KeyTable.Rows.Add("33", "A", "A", "33", "111323");
            Code128KeyTable.Rows.Add("34", "B", "B", "34", "131123");
            Code128KeyTable.Rows.Add("35", "C", "C", "35", "131321");
            Code128KeyTable.Rows.Add("36", "D", "D", "36", "112313");
            Code128KeyTable.Rows.Add("37", "E", "E", "37", "132113");
            Code128KeyTable.Rows.Add("38", "F", "F", "38", "132311");
            Code128KeyTable.Rows.Add("39", "G", "G", "39", "211313");
            Code128KeyTable.Rows.Add("40", "H", "H", "40", "231113");
            Code128KeyTable.Rows.Add("41", "I", "I", "41", "231311");
            Code128KeyTable.Rows.Add("42", "J", "J", "42", "112133");
            Code128KeyTable.Rows.Add("43", "K", "K", "43", "112331");
            Code128KeyTable.Rows.Add("44", "L", "L", "44", "132131");
            Code128KeyTable.Rows.Add("45", "M", "M", "45", "113123");
            Code128KeyTable.Rows.Add("46", "N", "N", "46", "113321");
            Code128KeyTable.Rows.Add("47", "O", "O", "47", "133121");
            Code128KeyTable.Rows.Add("48", "P", "P", "48", "313121");
            Code128KeyTable.Rows.Add("49", "Q", "Q", "49", "211331");
            Code128KeyTable.Rows.Add("50", "R", "R", "50", "231131");
            Code128KeyTable.Rows.Add("51", "S", "S", "51", "213113");
            Code128KeyTable.Rows.Add("52", "T", "T", "52", "213311");
            Code128KeyTable.Rows.Add("53", "U", "U", "53", "213131");
            Code128KeyTable.Rows.Add("54", "V", "V", "54", "311123");
            Code128KeyTable.Rows.Add("55", "W", "W", "55", "311321");
            Code128KeyTable.Rows.Add("56", "X", "X", "56", "331121");
            Code128KeyTable.Rows.Add("57", "Y", "Y", "57", "312113");
            Code128KeyTable.Rows.Add("58", "Z", "Z", "58", "312311");
            Code128KeyTable.Rows.Add("59", "[", "[", "59", "332111");
            Code128KeyTable.Rows.Add("60", "\\", "\\", "60", "314111");
            Code128KeyTable.Rows.Add("61", "]", "]", "61", "221411");
            Code128KeyTable.Rows.Add("62", "^", "^", "62", "431111");
            Code128KeyTable.Rows.Add("63", "_", "_", "63", "111224");
            Code128KeyTable.Rows.Add("64", "NUL", "`", "64", "111422");
            Code128KeyTable.Rows.Add("65", "SOH", "a", "65", "121124");
            Code128KeyTable.Rows.Add("66", "STX", "b", "66", "121421");
            Code128KeyTable.Rows.Add("67", "ETX", "c", "67", "141122");
            Code128KeyTable.Rows.Add("68", "EOT", "d", "68", "141221");
            Code128KeyTable.Rows.Add("69", "ENQ", "e", "69", "112214");
            Code128KeyTable.Rows.Add("70", "ACK", "f", "70", "112412");
            Code128KeyTable.Rows.Add("71", "BEL", "g", "71", "122114");
            Code128KeyTable.Rows.Add("72", "BS", "h", "72", "122411");
            Code128KeyTable.Rows.Add("73", "HT", "i", "73", "142112");
            Code128KeyTable.Rows.Add("74", "LF", "j", "74", "142211");
            Code128KeyTable.Rows.Add("75", "VT", "k", "75", "241211");
            Code128KeyTable.Rows.Add("76", "FF", "I", "76", "221114");
            Code128KeyTable.Rows.Add("77", "CR", "m", "77", "413111");
            Code128KeyTable.Rows.Add("78", "SO", "n", "78", "241112");
            Code128KeyTable.Rows.Add("79", "SI", "o", "79", "134111");
            Code128KeyTable.Rows.Add("80", "DLE", "p", "80", "111242");
            Code128KeyTable.Rows.Add("81", "DC1", "q", "81", "121142");
            Code128KeyTable.Rows.Add("82", "DC2", "r", "82", "121241");
            Code128KeyTable.Rows.Add("83", "DC3", "s", "83", "114212");
            Code128KeyTable.Rows.Add("84", "DC4", "t", "84", "124112");
            Code128KeyTable.Rows.Add("85", "NAK", "u", "85", "124211");
            Code128KeyTable.Rows.Add("86", "SYN", "v", "86", "411212");
            Code128KeyTable.Rows.Add("87", "ETB", "w", "87", "421112");
            Code128KeyTable.Rows.Add("88", "CAN", "x", "88", "421211");
            Code128KeyTable.Rows.Add("89", "EM", "y", "89", "212141");
            Code128KeyTable.Rows.Add("90", "SUB", "z", "90", "214121");
            Code128KeyTable.Rows.Add("91", "ESC", "{", "91", "412121");
            Code128KeyTable.Rows.Add("92", "FS", "|", "92", "111143");
            Code128KeyTable.Rows.Add("93", "GS", "}", "93", "111341");
            Code128KeyTable.Rows.Add("94", "RS", "~", "94", "131141");
            Code128KeyTable.Rows.Add("95", "US", "DEL", "95", "114113");
            Code128KeyTable.Rows.Add("96", "FNC3", "FNC3", "96", "114311");
            Code128KeyTable.Rows.Add("97", "FNC2", "FNC2", "97", "411113");
            Code128KeyTable.Rows.Add("98", "SHIFT", "SHIFT", "98", "411311");
            Code128KeyTable.Rows.Add("99", "CODEC", "CODEC", "99", "113141");
            Code128KeyTable.Rows.Add("100", "CODEB", "FNC4", "CODEB", "114131");
            Code128KeyTable.Rows.Add("101", "FNC4", "CODEA", "CODEA", "311141");
            Code128KeyTable.Rows.Add("102", "FNC1", "FNC1", "FNC1", "411131");
            Code128KeyTable.Rows.Add("103", "StartA", "StartA", "StartA", "211412");
            Code128KeyTable.Rows.Add("104", "StartB", "StartB", "StartB", "211214");
            Code128KeyTable.Rows.Add("105", "StartC", "StartC", "StartC", "211232");
            Code128KeyTable.Rows.Add("106", "Stop", "Stop", "Stop", "2331112");
            #endregion
        }


        /// <summary>
        /// 获取128图形
        /// </summary>
        /// <param name="p_Text">文字</param>
        /// <param name="p_Code">编码</param>      
        /// <returns>图形</returns>
        public static Bitmap GenerateCode128Image(this string p_Text, Code128Encode p_Code)
        {
            string _ViewText = p_Text;
            string _Text = "";
            IList<int> _TextNumb = new List<int>();
            int _Examine = 0;  //首位
            switch (p_Code)
            {
                case Code128Encode.Code128C:
                    _Examine = 105;
                    if (!((p_Text.Length & 1) == 0))
                    {
                        return null;
                    }
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        try
                        {
                            int _CodeNumb128 = Int32.Parse(p_Text.Substring(0, 2));
                        }
                        catch
                        {
                            return null;
                        }
                        _Text += GetValue(p_Code, p_Text.Substring(0, 2), ref _Temp);
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 2);
                    }
                    break;
                case Code128Encode.EAN128:
                    _Examine = 105;
                    if (!((p_Text.Length & 1) == 0))
                    {
                        return null;
                    }
                    _TextNumb.Add(102);
                    _Text += "411131";
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        try
                        {
                            int _CodeNumb128 = Int32.Parse(p_Text.Substring(0, 2));
                        }
                        catch
                        {
                            return null;
                        }
                        _Text += GetValue(Code128Encode.Code128C, p_Text.Substring(0, 2), ref _Temp);
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 2);
                    }
                    break;
                case Code128Encode.Code128A:
                    _Examine = 103;
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        string _ValueCode = GetValue(p_Code, p_Text.Substring(0, 1), ref _Temp);
                        if (_ValueCode.Length == 0)
                        {
                            return null;
                        }
                        _Text += _ValueCode;
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 1);
                    }
                    break;
                default:
                    _Examine = 104;
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        string _ValueCode = GetValue(p_Code, p_Text.Substring(0, 1), ref _Temp);
                        if (_ValueCode.Length == 0)
                        {
                            return null;
                        }
                        _Text += _ValueCode;
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 1);
                    }
                    break;
            }
            if (_TextNumb.Count == 0)
            {
                return null;
            }
            _Text = _Text.Insert(0, GetValue(_Examine)); //获取开始位

            for (int i = 0; i != _TextNumb.Count; i++)
            {
                _Examine += _TextNumb[i] * (i + 1);
            }
            _Examine = _Examine % 103;           //获得严效位
            _Text += GetValue(_Examine);  //获取严效位
            _Text += "2331112"; //结束位
            Bitmap _CodeImage = GetImage(_Text);
            GetViewText(_CodeImage, _ViewText);
            return _CodeImage;
        }

        /// <summary>
        /// 获取128图形
        /// </summary>
        /// <param name="p_Text">文字</param>
        /// <param name="p_Code">编码</param>      
        /// <returns>图形</returns>
        public static Bitmap GenerateCode128Image(this string p_Text, Code128Encode p_Code, out string errormsg)
        {
            string _ViewText = p_Text;
            string _Text = "";
            IList<int> _TextNumb = new List<int>();
            int _Examine = 0;  //首位
            switch (p_Code)
            {
                case Code128Encode.Code128C:
                    _Examine = 105;
                    if (!((p_Text.Length & 1) == 0))
                    {
                        errormsg = "128C长度必须是偶数";
                        return null;
                    }
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        try
                        {
                            int _CodeNumb128 = Int32.Parse(p_Text.Substring(0, 2));
                        }
                        catch
                        {
                            errormsg = "128C必须是数字！";
                            return null;
                        }
                        _Text += GetValue(p_Code, p_Text.Substring(0, 2), ref _Temp);
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 2);
                    }
                    break;
                case Code128Encode.EAN128:
                    _Examine = 105;
                    if (!((p_Text.Length & 1) == 0))
                    {
                        errormsg = "EAN128长度必须是偶数";
                        return null;
                    }
                    _TextNumb.Add(102);
                    _Text += "411131";
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        try
                        {
                            int _CodeNumb128 = Int32.Parse(p_Text.Substring(0, 2));
                        }
                        catch
                        {
                            errormsg = "EAN128必须是数字！";
                            return null;
                        }
                        _Text += GetValue(Code128Encode.Code128C, p_Text.Substring(0, 2), ref _Temp);
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 2);
                    }
                    break;
                case Code128Encode.Code128A:
                    _Examine = 103;
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        string _ValueCode = GetValue(p_Code, p_Text.Substring(0, 1), ref _Temp);
                        if (_ValueCode.Length == 0)
                        {
                            errormsg = "无效的字符集!" + p_Text.Substring(0, 1).ToString();
                            return null;
                        }
                        _Text += _ValueCode;
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 1);
                    }
                    break;
                default:
                    _Examine = 104;
                    while (p_Text.Length != 0)
                    {
                        int _Temp = 0;
                        string _ValueCode = GetValue(p_Code, p_Text.Substring(0, 1), ref _Temp);
                        if (_ValueCode.Length == 0)
                        {
                            errormsg = "无效的字符集!" + p_Text.Substring(0, 1).ToString();
                            return null;
                        }
                        _Text += _ValueCode;
                        _TextNumb.Add(_Temp);
                        p_Text = p_Text.Remove(0, 1);
                    }
                    break;
            }
            if (_TextNumb.Count == 0)
            {
                errormsg = "错误的编码,无数据";
                return null;
            }
            _Text = _Text.Insert(0, GetValue(_Examine)); //获取开始位

            for (int i = 0; i != _TextNumb.Count; i++)
            {
                _Examine += _TextNumb[i] * (i + 1);
            }
            _Examine = _Examine % 103;           //获得严效位
            _Text += GetValue(_Examine);  //获取严效位
            _Text += "2331112"; //结束位
            Bitmap _CodeImage = GetImage(_Text);
            GetViewText(_CodeImage, _ViewText);
            errormsg = "";
            return _CodeImage;
        }


        #region 绘制条码
        /// <summary>
        /// 获取目标对应的数据
        /// </summary>
        /// <param name="p_Code">编码</param>
        /// <param name="p_Value">数值 A b  30</param>
        /// <param name="p_SetID">返回编号</param>
        /// <returns>编码</returns>
        private static string GetValue(Code128Encode p_Code, string p_Value, ref int p_SetID)
        {
            if (Code128KeyTable == null) return "";
            DataRow[] _Row = Code128KeyTable.Select(p_Code.ToString() + "='" + p_Value + "'");
            if (_Row.Length != 1) throw new Exception("错误的编码" + p_Value.ToString());
            p_SetID = Int32.Parse(_Row[0]["ID"].ToString());
            return _Row[0]["BandCode"].ToString();
        }
        /// <summary>
        /// 根据编号获得条纹
        /// </summary>
        /// <param name="p_CodeId"></param>
        /// <returns></returns>
        private static string GetValue(int p_CodeId)
        {
            DataRow[] _Row = Code128KeyTable.Select("ID='" + p_CodeId.ToString() + "'");
            if (_Row.Length != 1) throw new Exception("验效位的编码错误" + p_CodeId.ToString());
            return _Row[0]["BandCode"].ToString();
        }
        /// <summary>
        /// 获得条码图形
        /// </summary>
        /// <param name="p_Text">文字</param>
        /// <returns>图形</returns>
        private static Bitmap GetImage(string p_Text)
        {
            char[] _Value = p_Text.ToCharArray();
            int _Width = 0;
            for (int i = 0; i != _Value.Length; i++)
            {
                _Width += Int32.Parse(_Value[i].ToString()) * (m_Magnify + 1);
            }
            Bitmap _CodeImage = new Bitmap(_Width, (int)m_Height);
            //使用GDI+绘制图像
            Graphics _Garphics = Graphics.FromImage(_CodeImage);
            //Pen _Pen;
            int _LenEx = 0;
            for (int i = 0; i != _Value.Length; i++)
            {
                int _ValueNumb = Int32.Parse(_Value[i].ToString()) * (m_Magnify + 1);  //获取宽和放大系数
                if (!((i & 1) == 0))
                {
                    //_Pen = new Pen(Brushes.White, _ValueNumb);
                    _Garphics.FillRectangle(Brushes.White, new Rectangle(_LenEx, 0, _ValueNumb, (int)m_Height));
                }
                else
                {
                    //_Pen = new Pen(Brushes.Black, _ValueNumb);
                    _Garphics.FillRectangle(Brushes.Black, new Rectangle(_LenEx, 0, _ValueNumb, (int)m_Height));
                }
                //_Garphics.(_Pen, new Point(_LenEx, 0), new Point(_LenEx, m_Height));
                _LenEx += _ValueNumb;
            }
            _Garphics.Dispose();
            return _CodeImage;
        }
        /// <summary>
        /// 显示可见条码文字 如果小于40 不显示文字
        /// </summary>
        /// <param name="p_Bitmap">图形</param>           
        private static void GetViewText(Bitmap p_Bitmap, string p_ViewText)
        {
            if (m_ValueFont == null) return;

            Graphics _Graphics = Graphics.FromImage(p_Bitmap);
            SizeF _DrawSize = _Graphics.MeasureString(p_ViewText, m_ValueFont);
            if (_DrawSize.Height > p_Bitmap.Height - 10 || _DrawSize.Width > p_Bitmap.Width)
            {
                _Graphics.Dispose();
                return;
            }

            int _StarY = p_Bitmap.Height - (int)_DrawSize.Height;
            _Graphics.FillRectangle(Brushes.White, new Rectangle(0, _StarY, p_Bitmap.Width, (int)_DrawSize.Height));
            _Graphics.DrawString(p_ViewText, m_ValueFont, Brushes.Black, 0, _StarY);
        }
        #endregion
    }
}
