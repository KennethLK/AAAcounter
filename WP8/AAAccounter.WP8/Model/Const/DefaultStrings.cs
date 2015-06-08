/* *
* ClassName: DefaultStrings
* Description: 
*
* Author: 李靖
* Date: 5/28/2015 10:53:09 PM
* */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace AAAccounter.Model
{
    public class DefaultConfig
    {
        public const string DEFAULT_PWD = "111111";

        public const string CONFIG_FILE_NAME = "setting.config";

        public const string DATABASE_NAME = "aaacount.db";

        public static readonly UnicodeEncoding ENCODING = UnicodeEncoding.Utf8;
    }
}
