using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GUI.Input.Scripts
{
    public interface IMenuInputScript
    {
        bool Next
        {
            get;
        }

        bool Previous
        {
            set;
        }

        bool Trigger
        {
            get;
        }
    }
}
