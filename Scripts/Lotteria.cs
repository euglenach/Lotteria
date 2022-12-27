using System;
using System.Linq;
using Random = UnityEngine.Random;

namespace LotterySystem
{
    public static class Lotteria
    {
        public static bool True(float trueProbability, RandomType randomType = RandomType.Ratio)
        {
            if(trueProbability <= 0) { return false;}

            return randomType switch
            {
                RandomType.Ratio => Random.value <= trueProbability,
                RandomType.Percent => Random.Range(0f, 100f) <= trueProbability,
                _ => throw new ArgumentOutOfRangeException(nameof(randomType), randomType, null)
            };
        }
    
        public static bool False(float falseProbability, RandomType randomType = RandomType.Ratio)
        {
            return !True(falseProbability, randomType);
        }

        public static T Select<T>(params T[] source)
        {
            var count = source.Length;
            var index = Random.Range(0, count);
            return source[index];
        }
        
        public static T Select<T>(T item1, T item2)
        {
            return True(0.5f)? item1 : item2;
        }
        
        public static ref T Select<T>(ref T[] source)
        {
            var count = source.Length;
            var index = Random.Range(0, count);
            return ref source[index];
        }

        public static ref T Select<T>(ref T item1, ref T item2)
        {
            return ref True(0.5f)? ref item1 : ref item2;
        }
        public static int GetRandomIndex(params int[] weightTable)
        {
            var totalWeight = weightTable.Sum();
            var value = Random.Range(1, totalWeight + 1);
            var retIndex = -1;
            for (var i = 0; i < weightTable.Length; ++i)
            {
                if (weightTable[i] >= value)
                {
                    retIndex = i;
                    break;
                }
                value -= weightTable[i];
            }
            return retIndex;
        }
        
        public static ref int WeightRandom(ref int[] weightTable)
        {
            var totalWeight = weightTable.Sum();
            var value = Random.Range(1, totalWeight + 1);
            var retIndex = -1;
            for (var i = 0; i < weightTable.Length; ++i)
            {
                if (weightTable[i] >= value)
                {
                    retIndex = i;
                    break;
                }
                value -= weightTable[i];
            }
            return ref weightTable[retIndex];
        }
        
        public static int GetRandomIndex(params IWeight[] weightTable)
        {
            var totalWeight = weightTable.Sum(w => w.Weight);
            var value = Random.Range(1, totalWeight + 1);
            var retIndex = -1;
            for (var i = 0; i < weightTable.Length; ++i)
            {
                if (weightTable[i].Weight >= value)
                {
                    retIndex = i;
                    break;
                }
                value -= weightTable[i].Weight;
            }
            return retIndex;
        }
        
        public static ref IWeight WeightRandom(ref IWeight[] weightTable)
        {
            var totalWeight = weightTable.Sum(w => w.Weight);
            var value = Random.Range(1, totalWeight + 1);
            var retIndex = -1;
            for (var i = 0; i < weightTable.Length; ++i)
            {
                if (weightTable[i].Weight >= value)
                {
                    retIndex = i;
                    break;
                }
                value -= weightTable[i].Weight;
            }
            return ref weightTable[retIndex];
        }
    }
    
    public enum RandomType
    {
        /// <summary>
        /// 割合 0~1
        /// </summary>
        Ratio,
        
        /// <summary>
        /// 百分率 〇〇%
        /// </summary>
        Percent,
    }
}
