using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmountToChineseString
{
    class Program
    {
        public static void Main(string[] args)
        {
            int n = 1033;
            Console.WriteLine($"開頭不補零\t\t      {AmountToString(n, PaddingZeroUnit.NoPadding)}");
            Console.WriteLine($"顯示最高單為「萬」\t{AmountToString(n, PaddingZeroUnit.TenThousand)}");
            Console.WriteLine($"顯示最高單為「拾」\t      {AmountToString(n, PaddingZeroUnit.Ten)}");
        }

        public static string AmountToString(long amount, PaddingZeroUnit paddingZeroUnit)
        {
            int length = amount.ToString().Length - 1;
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("amount", "數值必須為正整數");
            }
            else if (length > 13)
            {
                throw new ArgumentOutOfRangeException("amount", "數值不可超過13位數");
            }

            if (paddingZeroUnit != 0 && length > (int)paddingZeroUnit)
            {
                paddingZeroUnit = (PaddingZeroUnit)length;
            }
            string[] numberTexts = { "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖" };
            string[] typeMappings = { "", "拾", "佰", "仟", "萬", "拾", "佰", "仟", "億", "拾", "佰", "仟", "兆" };
            int loopTimes = paddingZeroUnit == 0 ? length : (int)paddingZeroUnit;
            long pow10, tempNum;
            List<string> result = new List<string>();
            for (int i = loopTimes; i >= 0; i--)
            {
                pow10 = Convert.ToInt64(Math.Pow(10, i));
                tempNum = amount / pow10;
                amount -= tempNum * pow10;
                result.Add(numberTexts[tempNum]);
                if (typeMappings[i] != "")
                {
                    result.Add(typeMappings[i]);
                }
            }
            return string.Join(" ", result);
        }

        /// <summary>
        /// 顯示最高單位，不足補零
        /// </summary>
        public enum PaddingZeroUnit
        {
            /// <summary>
            /// 開頭不補零
            /// </summary>
            NoPadding = 0,
            /// <summary>
            /// 拾
            /// </summary>
            Ten,
            /// <summary>
            /// 佰
            /// </summary>
            Hundred,
            /// <summary>
            /// 仟
            /// </summary>
            Thousand,
            /// <summary>
            /// 萬
            /// </summary>
            TenThousand,
            /// <summary>
            /// 拾萬
            /// </summary>
            HundredThousand,
            /// <summary>
            /// 佰萬
            /// </summary>
            Million,
            /// <summary>
            /// 佰萬
            /// </summary>
            TenMillion,
            /// <summary>
            /// 億
            /// </summary>
            HundredMillion,
            /// <summary>
            /// 拾億
            /// </summary>
            Billion,
            /// <summary>
            /// 佰億
            /// </summary>
            TenBillion,
            /// <summary>
            /// 仟億
            /// </summary>
            HundredBillion,
            /// <summary>
            /// 兆
            /// </summary>
            Trillion
        }
    }
}
