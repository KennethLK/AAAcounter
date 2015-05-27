/* *
* ClassName: ConsumerDetail
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 9:09:51 PM
* */

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAcounter.Model
{
    /// <summary>
    /// 个人消费明细
    /// </summary>
    public class ConsumerDetail : ITable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public string ChangedProp { get; set; }

        public string Place { get; set; }

        public string PlaceType { get; set; }

        public double Money { get; set; }

        public int UserId { get; set; }

        public DetailType DetailType { get; set; }

        /// <summary>
        /// 关联的个人Id
        /// </summary>
        public int ConsumerId { get; set; }

        /// <summary>
        /// 关联的单次消费记录
        /// </summary>
        public int ConsumptionId { get; set; }
    }
}
