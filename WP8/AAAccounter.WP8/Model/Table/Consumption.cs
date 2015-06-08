/* *
* ClassName: Consumption
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 9:09:21 PM
* */

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAccounter.Model
{
    /// <summary>
    /// 单次消费记录
    /// </summary>
    public class Consumption : ITable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        [Ignore]
        public string ChangedProp { get; set; }

        public string Place { get; set; }

        public PlaceType PlaceType { get; set; }

        public double Money { get; set; }

        public int PeopleCount { get; set; }

        public double CreateDate { get; set; }

        [Ignore]
        public List<ConsumptionItemList> Items { get; set; }

        [Ignore]
        public List<ConsumerModel> People { get; set; }

    }
}
