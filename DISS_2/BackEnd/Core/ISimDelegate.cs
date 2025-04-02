namespace DISS_2.BackEnd.Core;

public interface ISimDelegate
{
    Task UpdateUi(SimCore simCore, Event currentEvent);
}

public interface IRepDelegate
{
    Task UpdateUi(SimCore simCore);
}
