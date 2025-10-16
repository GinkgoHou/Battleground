using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;
public static class CardShuffle
{
   // private static readonly System.Random _random = new System.Random();

    public static void Shuffle<T>(this List<T> list)
    {
        var n = list.Count;

        while (n-- > 1)
        {

            var index = UnityEngine.Random.Range(0,n + 1);
            var value = list[index];
            list[index] = list[n];
            list[n] = value;

        }
    }

}


public class A
{
    private int a = 0;
    private List<int> _list = new List<int>();

    public A()
    {
        this.a = 2;
        _list.Shuffle();
        CardShuffle.Shuffle(_list);

    }

    public int GetAvalue()
    {
        return a;
    }

}
