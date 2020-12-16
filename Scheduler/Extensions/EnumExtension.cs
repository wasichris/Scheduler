using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Scheduler.Extensions
{
    /// <summary>
    /// Enum 擴充功能
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 將Enum轉為數字
        /// </summary>
        /// <param name="self">Enum本體</param>
        /// <returns>Enum表示的數字</returns>
        public static int ToIntValue(this Enum self)
        {
            return Convert.ToInt16(self);
        }


        /// <summary>
        /// 取得Enum的描述標籤內容 (Description)
        /// </summary>
        /// <param name="self">Enum本體</param>
        /// <param name="defaultDesc">找無對應定義時所顯示的預設描述</param>
        /// <returns> Enum描述標籤內容 </returns>
        public static string GetDescription(this Enum self, string defaultDesc = "")
        {
            var field = self.GetType().GetRuntimeField(self.ToString());
            var desc = defaultDesc;

            if (field != null)
            {
                desc = field.GetCustomAttributes<DescriptionAttribute>()
                    .FirstOrDefault()?.Description ?? defaultDesc;
            }

            return desc;
        }

    }
}
