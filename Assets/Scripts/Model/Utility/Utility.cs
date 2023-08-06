using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InheritanceFiveCrowns
{
    public static class Utility
    {
        public static Random RandomNumberGenerator = new();

        public static void Swap<T>(ref T val1, ref T val2)
        {
            (val1, val2) = (val2, val1);
        }
        
        public static IEnumerator WaitForFrames(Action callback, int numOfFrames = 1)
        {
            for (int i = 0; i < numOfFrames; i++)
                yield return null;
            callback();
        }

        public static int ChooseRandom(int min, int max)
        {
            // Min inclusive, max exclusive
            return RandomNumberGenerator.Next(min, max);
        }

        public static T ChooseRandom<T>(List<T> list)
        {
            return list[ChooseRandom(0, list.Count)];
        }

        public static T RemoveRandomElement<T>(List<T> list)
        {
            int index = ChooseRandom(0, list.Count);
            return RemoveElement(list, index);
        }

        public static void Shuffle<T>(ref List<T> list)
        {
            List<T> shuffledList = new List<T>();
            while (list.Count > 0)
            {
                var element = RemoveRandomElement(list);
                shuffledList.Add(element);
            }

            list = shuffledList;
        }

        public static void SwapIndex<T>(List<T> list, int index1, int index2)
        {
            (list[index1], list[index2]) = (list[index2], list[index1]);
        }

        public static List<T> GetNewPopulatedList<T>(int count, T obj)
        {
            return PopulateList(new List<T>(count), count, obj);
        }

        public static List<T> PopulateList<T>(List<T> list, int count, T obj)
        {
            for (int i = 0; i < count; i++)
            {
                list.Add(obj);
            }

            return list;
        }

        public static T RemoveFirstElement<T>(List<T> list)
            => RemoveElement(list, 0);

        public static T RemoveElement<T>(List<T> list, int index)
        {
            var e = list[index];
            list.RemoveAt(index);
            return e;
        }

        public static bool HasNull<T>(List<T> list)
            => list.Any(item => item == null);

        public static bool HasNull<T, T1>(List<T> list, Func<T, T1> get)
            => list.Any(item => get(item) == null);

        public static bool IsEmpty<T>(List<T> list)
            => list is not { Count: > 0 };

        public static int GetNumberOfNotNull<T>(List<T> list)
            => list.Count(item => item != null);
    }
}