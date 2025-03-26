namespace DISS_2.BackEnd.Core;

public interface ISimDelegate
{
    void UpdateUi(SimCore simCore, Event currentEvent);
}

public interface IRepDelegate
{
    void UpdateUi(SimCore simCore);
}
