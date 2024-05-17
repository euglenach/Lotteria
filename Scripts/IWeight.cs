namespace LotterySystem
{
    public interface IWeight
    {
        int Weight{get;}
    }

    public class DefaultWeight : IWeight
    {
        public int Weight{get;set;}

        public DefaultWeight(int weight)
        {
            this.Weight = weight;
        }
    }
    
    public readonly struct DefaultWeightStruct : IWeight
    {
        public int Weight{get;}

        public DefaultWeightStruct(int weight)
        {
            this.Weight = weight;
        }
    }

    public class WeightProxy<T> : IWeight
    {
        public readonly T Target;
        public int Weight{ get; }

        public WeightProxy(T target, int weight)
        {
            Target = target;
            Weight = weight;
        }
    }
    
    public readonly struct WeightProxyStruct<T> : IWeight
    {
        public readonly T Target;
        public int Weight{ get; }

        public WeightProxyStruct(T target, int weight)
        {
            Target = target;
            Weight = weight;
        }
    }
}
