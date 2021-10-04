using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutFloat.WPF.Helper
{
    public class Math
    {
        public static long Map(long x, long in_min, long in_max, long out_min, long out_max) =>
            (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

        public static double Map(double x, double in_min, double in_max, double out_min, double out_max) =>
            (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;

        public static float Map(float x, float in_min, float in_max, float out_min, float out_max) =>
            (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
