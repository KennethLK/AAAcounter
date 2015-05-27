﻿/* *
* ClassName: ConsumerModel
* Description: 
*
* Author: 李靖
* Date: 5/26/2015 8:53:13 PM
* */

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAcounter.Model
{
    public class ConsumerModel : ITable
    {
        public ConsumerModel()
        {
            CreateDate = DateTime.Now.GetMili1970();
        }

        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        public string ChangedProp { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public double Balance { get; set; }

        public double Spending { get; set; }

        public double Saving { get; set; }

        public double CreateDate { get; set; }
    }
}