using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
public class Methods : MonoBehaviour
{
    public static List<T> CreateList<T>(int capacity) => Enumerable.Repeat(default(T), capacity).ToList();
    public static void UpgradeCheck<T>(List<T> list, int length) where T : new()
    {
        if (list == null) return;

        while (list.Count < length)
        {
            list.Add(new T());
        }
    }
}
