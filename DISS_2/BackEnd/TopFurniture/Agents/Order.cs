using DISS_2.BackEnd.Generators;
using DISS_2.BackEnd.Generators.Uniform;

namespace DISS_2.BackEnd.TopFurniture.Agents;

public abstract class Order
{
    public int TimeArrival { get; set; }
    public int TimeArrivalAtQueue1 { get; set; }
    public int TimeArrivalAtQueue2 { get; set; }
    public int TimeArrivalAtQueue3 { get; set; }
    public int TimeArrivalAtQueue4 { get; set; }

    public int TimeOfStep1Start { get; set; }
    public int TimeOfStep2Start { get; set; }
    public int TimeOfStep3Start { get; set; }
    public int TimeOfStep4Start { get; set; }


    public Location? Location { get; set; } = null;

    protected Order(int timeArrival)
    {
        TimeArrival = timeArrival;
    }

    public static Order CreateRandomOrder(UniformGenerator<double> gen, int simTime)
    {
        double randomNumber = gen.Generate();

        if (randomNumber < Config.ProbOfTable)
        {
            return new Table(simTime);
        }

        if (randomNumber < Config.ProbOfTable + Config.ProbOfChair)
        {
            return new Chair(simTime);
        }

        return new Wardrobe(simTime);
    }

    public override string ToString()
    {
        return $"   Order: Location={Location?.Id} Type={GetType().Name}";
    }
}