using System;
using System.Collections.Generic;
using System.Text;

namespace F2M6PROG
{
    public abstract class SCP
    {
        public  string Name { get; private set; }
        

        protected SCP(string name)
        {
            this.Name = name;
        }
        public abstract void Description();
        
        

        
    }
}
