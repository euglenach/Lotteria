using System;
using System.Linq;
using Random = Unity.Mathematics.Random;

namespace LotterySystem
{
    public struct LotteriaBox
    {
        private Random random;
        
        public LotteriaBox(uint seed)
        {
            random = new Random(seed);
        }

        public LotteriaBox(Random random)
        {
            this.random = random;
        }
        
        public static LotteriaBox CreateFromIndex(uint index)
        {
            return new LotteriaBox(Random.CreateFromIndex(index));
        }

        public void InitState(uint seed = 0x6E624EB7u)
        {
            random.InitState(seed);
        }

        public bool True(float trueProbability, RandomType randomType = RandomType.Ratio)
        {
            if(trueProbability <= 0) { return false;}

            return randomType switch
            {
                RandomType.Ratio => random.NextFloat() <= trueProbability,
                RandomType.Percent => random.NextFloat(0f, 100f) <= trueProbability,
                _ => throw new ArgumentOutOfRangeException(nameof(randomType), randomType, null)
            };
        }
    
        public bool False(float falseProbability, RandomType randomType = RandomType.Ratio)
        {
            return !True(falseProbability, randomType);
        }

        public T Select<T>(params T[] source)
        {
            var count = source.Length;
            var index = random.NextInt(0, count);
            return source[index];
        }
        
        public T Select<T>(T item1, T item2)
        {
            return True(0.5f)? item1 : item2;
        }
        
        public ref T Select<T>(ref T[] source)
        {
            var count = source.Length;
            var index = random.NextInt(0, count);
            return ref source[index];
        }

        public ref T Select<T>(ref T item1, ref T item2)
        {
            return ref True(0.5f)? ref item1 : ref item2;
        }
        public int GetRandomIndex(params int[] weightTable)
        {
            var totalWeight = weightTable.Sum();
            var value = random.NextInt(1, totalWeight + 1);
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
        
        public ref int WeightRandom(ref int[] weightTable)
        {
            var totalWeight = weightTable.Sum();
            var value = random.NextInt(1, totalWeight + 1);
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
        
        public int GetRandomIndex(params IWeight[] weightTable)
        {
            var totalWeight = weightTable.Sum(w => w.Weight);
            var value = random.NextInt(1, totalWeight + 1);
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
        
        public ref IWeight WeightRandom(ref IWeight[] weightTable)
        {
            var totalWeight = weightTable.Sum(w => w.Weight);
            var value = random.NextInt(1, totalWeight + 1);
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
}
