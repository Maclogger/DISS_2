using DISS_2.BackEnd.Core;

namespace DISS_2.BackEnd.TicketSelling;

public class TicketSellingSimState : SimState
{
    public int PeopleInQueue { get; set; }= 0;
    public bool IsBusy { get; set; } = false;
}