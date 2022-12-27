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
}
