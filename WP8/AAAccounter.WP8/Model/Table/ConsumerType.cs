﻿/* *
* ClassName: ConsumerType
* Description: 
*
* Author: 李靖
* Date: 5/28/2015 11:07:57 PM
* */

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAccounter.Model.Table
{
    public class ConsumerType : ITable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        [Ignore]
        public string ChangedProp { get; set; }

        public string Name { get; set; }
    }
}
