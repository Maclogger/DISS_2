using DISS_2.BackEnd.Core;

namespace DISS_2.BackEnd.TopFurniture.CustomEvents;

public class Step1End(int startTime) : Event(startTime)
{
    public override Task Execute(SimCore sim)
    {
        throw new NotImplementedException();
    }
}
