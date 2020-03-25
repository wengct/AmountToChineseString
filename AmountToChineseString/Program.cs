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
            int n = 107000001;
            Console.WriteLine(AmountToString(n, PaddingZeroUnit.開頭不補零));
        }

        public static string AmountToString(long amount, PaddingZeroUnit paddingZeroUnit)
        {
            string result = string.Empty;
            int length = amount.ToString().Length - 1;
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException("number", "數值必需為正整數");
            }
            else if ((int)paddingZeroUnit != 0 && length > (int)paddingZeroUnit)
            {
                throw new ArgumentOutOfRangeException("PaddingZeroUnit", "限定最高單位低於傳入金額");
            }
            else if (length > 13)
            {
                throw new ArgumentOutOfRangeException("number", "數值超過 13 位數");
            }

            string[] numberTexts = { "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖" };
            string[] typeMappings = { "", "拾", "佰", "仟", "萬", "拾", "佰", "仟", "億", "拾", "佰", "仟", "兆" };
            int loopTimes = (int)paddingZeroUnit == 0 ? length : (int)paddingZeroUnit;
            for (int i = loopTimes; i >= 0; i--)
            {
                long temp = amount / Convert.ToInt64(Math.Pow(10, i));
                amount -= temp * Convert.ToInt64(Math.Pow(10, i));
                result += " " + numberTexts[temp] + " " + typeMappings[i];
            }
            return result;
        }

        public enum PaddingZeroUnit
        {
            開頭不補零 = 0,
            拾 = 1,
            佰,
            仟,
            萬,
            拾萬,
            佰萬,
            仟萬,
            億,
            拾億,
            佰億,
            仟億,
            兆
        }
    }
}
