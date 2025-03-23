using DISS_2.BackEnd.Core;
using DISS_2.BackEnd.Generators.Uniform;
using DISS_2.BackEnd.TicketSelling.Agents;

namespace DISS_2.BackEnd.TicketSelling;

public class TicketSellingSimState : SimState
{
    public Queue<Customer> CustomerQueue { get; set; } = new();
    public bool IsBusy { get; set; } = false;

    public TicketGenerators Gens { get; set; } = new();
}

public class TicketGenerators
{
    public  UniformGenerator<int> ArrivalGen { get; set; }=
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(10, 20);

    public UniformGenerator<int> OperationDurationGen { get; set; }=
        UniformGeneratorFactory.CreateDiscreteUniformGenerator(10, 30);
}