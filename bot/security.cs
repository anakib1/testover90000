using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace bot
{
    class Security
    {
        public string[] list;
        public Security()
            {
            list = File.ReadAllLines(@"E:\doc\cc\list.mmc").ToArray();
        }
        public bool IsBlocked(string text)
        {

            if (list.Contains(text.ToLower()))
                return true;
            else
                return false;
        }
        public string[] AllBlocked()
        {
            return list;
        }
    }
}
