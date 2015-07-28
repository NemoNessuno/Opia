

using System.Collections.Generic;
using UnityEngine;
class RandomizationHelper<T>
{
    public static List<T> ChooseRandomly(List<T> items, int amountOfChosen)
    {
        var result = new List<T>();

        while (result.Count < amountOfChosen)
        {
            int index = Random.Range(0, items.Count);
            var chosen = items[index];

            result.Add(chosen);
            items.Remove(chosen);
        }

        return result;
    }
}
