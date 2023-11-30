

using System;
using System.Collections.Generic;

public static class RandomExtensions
{

    private static Random rng = new Random();

    //Mezclar elementos de un List aleatoriamente
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
