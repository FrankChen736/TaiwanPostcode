using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postcode
{
    /// <summary>
    /// 目標郵遞區號
    /// </summary>
   public class TargetPostcode
    {
        /// <summary>
        /// 城市
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// 鄉,鎮,區
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 路名
        /// </summary>
        public string road { get; set; }
    }
}
