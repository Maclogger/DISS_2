using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;

namespace DISS_2.BackEnd.TicketSelling;

public class TicketSellingSimState : SimState
{
    public int PeopleInQueue { get; set; } = 0;
    public bool IsBusy { get; set; } = false;

    public TicketGenerators Gens { get; set; } = new();
}

public class TicketGenerators
{
    public  UniformGenerator<int> ArrivalGen { get; set; }=
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(20, 40);

    public UniformGenerator<int> OperationDurationGen { get; set; }=
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(10, 30);
}