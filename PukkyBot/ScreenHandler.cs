using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PukkyBot
{
    class ScreenHandler
    {

        public static int getBluePixel(int x, int y)
        {
            return blue[x + y * width];
        }
        public static int getRedPixel(int x, int y)
        {
            return red[x + y * width];
        }
        public static int getGreenPixel(int x, int y)
        {
            return green[x + y * width];
        }

    }
}
