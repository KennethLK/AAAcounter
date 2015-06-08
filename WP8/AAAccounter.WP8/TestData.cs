/* *
* ClassName: TestData
* Description: 
*
* Author: 李靖
* Date: 6/6/2015 3:44:44 PM
* */

using AAAccounter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAAccounter
{
    public class TestData : List<ConsumerModel>
    {
        public TestData()
        {
            Add(new ConsumerModel {
                Name = "李靖"
            });
            Add(new ConsumerModel
            {
                Name = "田龙"
            });
            Add(new ConsumerModel
            {
                Name = "敬亮"
            });
            Add(new ConsumerModel
            {
                Name = "吴迪"
            });
        }


    }
}
