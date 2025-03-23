using DISS_2.BackEnd.Core;

namespace DISS_2.Components;

public interface ISimDelegate
{
    void UpdateUi(SimCore simCore, Event currentEvent);
}

public interface IRepDelegate
{
    void UpdateUi(SimCore simCore);
}
