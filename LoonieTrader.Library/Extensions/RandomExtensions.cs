//using System;

//namespace LoonieTrader.Library.Extensions;

//public static class RandomExtensions
//{
//    public static decimal NextDecimal(this Random r, decimal min, decimal max)
//    {
//        decimal randomDecimal = (decimal)r.NextDouble();

//        var p1 = randomDecimal * (max - min) + min;
//        return p1;
//    }

//    public static double NextDouble(this Random r, double min, double max)
//    {
//        var randomDouble = r.NextDouble() * (max - min) + min;
//        return randomDouble;
//    }
//}