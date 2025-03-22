using DISS_2.BackEnd.Core;

namespace DISS_2.Components;

public interface IDelegate
{
    void UpdateUI(SimState simState);
}