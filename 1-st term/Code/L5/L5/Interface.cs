
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    interface ISwitch
    {
        bool Lights { set; get; }
        void SwitchLights();

        bool Music { set; get; }
        void SwitchMusic();
    }

    interface IAdd
    {
        void AddFuel();
        void AddOil();
    }

    interface Ismth1
    {
        void Move();
        void StartEngine();
    }

    interface Ismth2
    {
        void Move();
        void StartEngine();
    }
}