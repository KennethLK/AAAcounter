using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAcounter.Model
{
    /// <summary>
    /// 数据库表的基类，每个类都必须有Id
    /// </summary>
    public interface ITable
    {
        int Id { get; set; }

        string ChangedProp { get; set; }
    }
}
