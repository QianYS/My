using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyProject.Common
{
    /// <summary>
    /// 身份证号码帮助类
    /// </summary>
    public class IdentityCardHelper
    {
        #region 验证身份证 
        /// <summary>
        /// 验证身份证号码是否标准(15位18位通用)
        /// </summary>
        /// <param name="Id">身份证号码</param>
        /// <returns></returns>
        public static bool CheckIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = CheckIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证18位号码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证  
            }

            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }

            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证  
            }
            return true;//符合GB11643-1999标准  
        }

        private static int DivRem(int a, int b, out int result)
        {
            result = a % b;
            return (a / b);
        }

        /// <summary>
        /// 验证15位号码
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证  
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证  
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证  
            }
            return true;//符合15位身份证标准  
        }
        #endregion

        #region 身份证号码转换
        /// <summary>
        /// 转化身份证位数
        /// </summary>
        /// <param name="edittime"></param>
        /// <returns></returns>
        public static string IdCardTransfer(string identitycard)
        {
            string retStr = string.Empty;
            switch (identitycard.Length)
            {
                case 15:
                    retStr = IdCard15To18(identitycard);
                    break;
                case 18:
                    retStr = IdCard18To15(identitycard);
                    break;
                default:
                    break;
            }
            return retStr;
        }
        /// <summary>
        /// 身份证15位转18位
        /// </summary>
        /// <param name="identitycard"></param>
        /// <returns></returns>
        public static string IdCard15To18(string identitycard)
        {
            string retStr = string.Empty;
            if (identitycard.Length == 15 && Regex.IsMatch(identitycard, @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$"))
            {
                string str = identitycard.Substring(6, 1) + identitycard.Substring(7, 1);
                if (int.Parse(str) < 10)
                {
                    retStr = identitycard.Substring(0, 6) + "20" + identitycard.Substring(6);
                }
                else
                {
                    retStr = identitycard.Substring(0, 6) + "19" + identitycard.Substring(6);
                }
                char[] endNum = { '1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2' };//身份证最后一位数组
                int[] ratio = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
                int total = 0;
                for (int i = 0; i < 17; i++)
                {
                    total += int.Parse(retStr[i].ToString()) * ratio[i];//乘系数之后求和
                }
                total %= 11;//除以11取余
                retStr += endNum[total].ToString();//17位号码加上 余数所代表的下标在尾数数组endNum中的
            }
            return retStr;
        }
        /// <summary>
        /// 身份证15位转18位
        /// </summary>
        /// <param name="identitycard"></param>
        /// <returns></returns>
        public static string IdCard18To15(string identitycard)
        {
            string retStr = string.Empty;
            if (identitycard.Length == 18 && Regex.IsMatch(identitycard, @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])(\d{3})(\d|[X]|[x])$"))
            {
                retStr = identitycard.Substring(0, 6) + identitycard.Substring(8, 9);
            }
            return retStr;
        }
        #endregion
    }
}
