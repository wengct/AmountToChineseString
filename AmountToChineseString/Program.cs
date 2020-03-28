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
            long n;
            Console.WriteLine($"{0.ToString().PadLeft(16)}={AmountToString(0, AmountUnit.Trillion)}");
            for (int i = 1; i <= 13; i++)
            {
                string s = GetRandomString(i);
                n = Convert.ToInt64(s);
                Console.WriteLine($"{n.ToString().PadLeft(16)}={AmountToString(n, AmountUnit.Trillion)}");
            }
        }

        /// <summary>
        /// 金額轉國字
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="amountUnit"></param>
        /// <returns></returns>
        public static string AmountToString(long amount, AmountUnit amountUnit)
        {
            int length = amount.ToString().Length - 1;
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", "數值必須為正整數");
            }
            else if (length > 13)
            {
                throw new ArgumentOutOfRangeException("amount", "數值不可超過13位數");
            }

            if (amountUnit == 0 || length > (int)amountUnit)
            {
                amountUnit = (AmountUnit)length;
            }
            string[] numberTexts = { "零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖" };
            string[] typeMappings = { "", "拾", "佰", "仟", "萬", "拾", "佰", "仟", "億", "拾", "佰", "仟", "兆" };
            long pow10, tempNum;
            List<string> result = new List<string>();
            for (int i = (int)amountUnit; i >= 0; i--)
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
        public enum AmountUnit
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

        /// <summary>
        /// 隨機取得數字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            var str = "0123456789";
            var next = new Random(Guid.NewGuid().GetHashCode());
            var builder = new StringBuilder();
            char temp;
            for (var i = 0; i < length; i++)
            {
                temp = str[next.Next(0, str.Length)];
                if (i == 0 && temp == '0')
                {
                    i = -1;
                    continue;
                }
                builder.Append(temp);
            }
            return builder.ToString();
        }
    }
}
