﻿using System;
using System.Linq;
using Random = UnityEngine.Random;

namespace LotterySystem
{
    /// <summary>
    /// 抽選系
    /// </summary>
    public static class Lotteria
    {
        private static LotteriaBox core = new((uint)Random.Range(0, 0x6E624EB7u + 1));
        
        public static void InitState(uint seed = 0x6E624EB7u)
        {
            core.InitState(seed);
        }

        public static bool True(float trueProbability, RandomType randomType = RandomType.Ratio) =>
            core.True(trueProbability, randomType);

        public static bool False(float falseProbability, RandomType randomType = RandomType.Ratio) =>
            core.False(falseProbability, randomType);

        public static T Select<T>(params T[] source) => core.Select(source);

        public static T Select<T>(T item1, T item2) => core.Select(item1, item2);

        public static ref T Select<T>(ref T[] source) => ref core.Select(ref source);

        public static ref T Select<T>(ref T item1, ref T item2) => ref core.Select(ref item1, ref item2);

        public static int GetRandomIndex(params int[] weightTable) => core.GetRandomIndex(weightTable);

        public static ref int WeightRandom(ref int[] weightTable) => ref core.WeightRandom(ref weightTable);

        public static int GetRandomIndex(params IWeight[] weightTable) => core.GetRandomIndex(weightTable);

        public static ref IWeight WeightRandom(ref IWeight[] weightTable) => ref core.WeightRandom(ref weightTable);
    }
}
