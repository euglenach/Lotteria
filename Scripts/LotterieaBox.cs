using System;
using Random = Unity.Mathematics.Random;

namespace LotterySystem
{
    public struct LotterieaBox
    {
        private Random random;
        
        public LotterieaBox(uint seed)
        {
            random = new Random(seed);
        }

        public LotterieaBox(Random random)
        {
            this.random = random;
        }
        
        public static LotterieaBox CreateFromIndex(uint index)
        {
            return new LotterieaBox(Random.CreateFromIndex(index));
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

        #region GetRandomIndex
        
        public int GetRandomIndex(params int[] weightTable) => GetRandomIndex((ReadOnlySpan<int>)weightTable);
        
        public int GetRandomIndex(ReadOnlySpan<int> weightTable)
        {
            var totalWeight = SumWeight(weightTable);
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
        
        public int GetRandomIndex(params IWeight[] weightTable) => GetRandomIndex((ReadOnlySpan<IWeight>)weightTable);
        
        public int GetRandomIndex<TWeight>(params TWeight[] weightTable) where TWeight : IWeight => GetRandomIndex((ReadOnlySpan<TWeight>)weightTable);
        
        public int GetRandomIndex(ReadOnlySpan<IWeight> weightTable)
        {
            var totalWeight = SumWeight(weightTable);
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
        
        public int GetRandomIndex<TWeight>(ReadOnlySpan<TWeight> weightTable) where TWeight : IWeight
        {
            var totalWeight = SumWeight(weightTable);
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
        
        #endregion GetRandomIndex

        #region WeightRandom
        
        public IWeight WeightRandom(params IWeight[] weightTable) => WeightRandom((ReadOnlySpan<IWeight>)weightTable);
        public TWeight WeightRandom<TWeight>(params TWeight[] weightTable) where TWeight : IWeight => WeightRandom((ReadOnlySpan<TWeight>)weightTable);

        public IWeight WeightRandom(ReadOnlySpan<IWeight> weightTable)
        {
            var index = GetRandomIndex(weightTable);
            return weightTable[index];
        }
        
        public TWeight WeightRandom<TWeight>(ReadOnlySpan<TWeight> weightTable) where TWeight : IWeight
        {
            var index = GetRandomIndex(weightTable);
            return weightTable[index];
        }
        
        public ref IWeight WeightRandomRef(params IWeight[] weightTable)
        {
            var index = GetRandomIndex(weightTable);
            return ref weightTable[index];
        }
        
        public ref TWeight WeightRandomRef<TWeight>(params TWeight[] weightTable) where TWeight : IWeight
        {
            var index = GetRandomIndex(weightTable);
            return ref weightTable[index];
        }
        #endregion

        int SumWeight(ReadOnlySpan<IWeight> table)
        {
            var sum = 0;
            foreach(var weight in table)
            {
                sum += weight.Weight;
            }

            return sum;
        }
        
        int SumWeight<TWeight>(ReadOnlySpan<TWeight> table) where TWeight : IWeight
        {
            var sum = 0;
            foreach(var weight in table)
            {
                sum += weight.Weight;
            }

            return sum;
        }
        
        int SumWeight(ReadOnlySpan<int> table)
        {
            var sum = 0;
            foreach(var weight in table)
            {
                sum += weight;
            }

            return sum;
        }
    }
}
