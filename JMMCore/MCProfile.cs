using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class MCProfile
    {
        public MCProfile(string path, string name)
        {
            Path = path;
            Name = name;
        }

        public string Path
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
