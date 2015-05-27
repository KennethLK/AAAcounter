﻿/* *
* ClassName: ConsumptionDetailList
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 11:25:09 PM
* */

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAcounter.Model
{
    public class ConsumptionDetaiItemlList : ITable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public string ChangedProp { get; set; }

        public int ConsumptionDetailId { get; set; }
        
        public string Item { get; set; }

        public double Price { get; set; }
    }
}
