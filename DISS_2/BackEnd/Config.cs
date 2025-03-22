using DISS_2.BackEnd.Core;

namespace DISS_2.BackEnd;

public class Config
{
    public const int MaxPrintValuesInTextArea = 1000;

    private const int PointsInGraph = 1000;
    private const double PercentToCutFromBeggingOfTheChart = 5;
    public const string FloatFormat = "F2";
    public const double Tolerance = 0.00001;
    public const string TesterOutputDir = "Output";

    private int GetNthPointToDraw(int replicationCount)
    {
        return Math.Max(1, replicationCount / PointsInGraph);
    }

    public Task<bool> ShouldBePrintedToGraph(int currentReplication, int replicationCount)
    {
        int startPrintIndex = (int)(replicationCount * (PercentToCutFromBeggingOfTheChart / 100.0));

        int nthPoint = GetNthPointToDraw(replicationCount);
        return Task.FromResult(currentReplication >= startPrintIndex &&
                               currentReplication % nthPoint == 0);
    }

    public const bool PrintCompleteReplication = false;
    public const string StrategiesDirectory = "Strategies";
    public const double AbsorbersDailyStorageCostPerUnit = 0.2; // eur
    public const double BrakePadsDailyStorageCostPerUnit = 0.3; // eur
    public const double LightsDailyStorageCostPerUnit = 0.25; // eur
    public const double FinePerUnit = 0.3; // eur
    public const int Fps = 10;
}