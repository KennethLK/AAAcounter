/* *
* ClassName: ConsumerDetailItemList
* Description: 
*
* Author: 李靖
* Date: 5/28/2015 10:34:55 PM
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
    /// 个人单列消费清单
    /// </summary>
    public class ConsumerDetailItemList : ITable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        [Ignore]
        public string ChangedProp { get; set; }

        public int ConsumerDetailId { get; set; }

        public string Item { get; set; }

        public double Price { get; set; }
    }
}
