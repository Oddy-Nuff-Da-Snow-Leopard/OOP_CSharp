using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L12
{
    interface IMovable
    {
        void Move();
    }

    interface IAdd
    {
        void AddFuel(ushort value);
        void AddOil(ushort value);
    }
}
