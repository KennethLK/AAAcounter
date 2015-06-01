/* *
* ClassName: Place
* Description: 
*
* Author: 李靖
* Date: 5/28/2015 11:07:41 PM
* */

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAcounter.Model
{
    public class Place : ITable
    {
        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        [Ignore]
        public string ChangedProp { get; set; }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitute { get; set; }

        public string City { get; set; }
    }
}
