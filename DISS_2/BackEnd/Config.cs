using DISS_2.BackEnd.Core;

namespace DISS_2.BackEnd;

public class Config
{
    public const int MaxPrintValuesInTextArea = 1000;
    private const int PointsInGraph = 1000;
    private const double PercentToCutFromBeggingOfTheChart = 5;
    public const string FloatFormat = "F2";
    public const string StatisticFloatFormat = "F6";
    public const double Tolerance = 0.00001;
    public const string TesterOutputDir = "Output";
    public static int MaxLogCount { get; set; } = 1000;
}