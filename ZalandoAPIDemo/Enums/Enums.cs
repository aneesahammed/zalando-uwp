using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZalandoAPIDemo.Enums
{

    [Flags]
    public enum CustomFilter
    {
        All,
        Men,
        Women,
        Kids,
    }

    [Flags]
    public enum Gender
    {
        All,
        Male,
        Female,
    }

    [Flags]
    public enum AgeGroup
    {
        Babies = 1,
        Kids = 2,
        Teen = 4,
        Adult = 8,
    }

    [Flags]
    public enum Color
    {
        Black = 1,
        Brown = 2,
        Beige = 4,
        Gray = 8,
        White = 16,
        Blue = 32,
        Petrol = 64,
        Turquoise = 128,
        Green = 256,
        Olive = 512,
        Yellow = 1024,
        Orange = 2048,
        Red = 4096,
        Pink = 8192,
        Purple = 16384,
        Gold = 32768,
        Silver = 65536,
        Multicolored = 131072,
    }    
}
