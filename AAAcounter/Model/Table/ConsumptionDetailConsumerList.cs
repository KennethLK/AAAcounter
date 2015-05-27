/* *
* ClassName: ConsumptionDetailPeopleList
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 11:28:33 PM
* */

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAcounter.Model
{
    public class ConsumptionDetailConsumerList : ITable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public string ChangedProp { get; set; }

        public int ConsumerId { get; set; }

        public int ConsumptionDetailId { get; set; }
    }
}
