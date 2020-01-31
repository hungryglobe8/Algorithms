using System;
using System.Collections.Generic;
using System.Text;

namespace GalaxyQuest
{
    interface IReader
    {
        void ReadInput();
        int GetDiameter();
        IList<Coordinate> GetCoordinates();
    }
}
