using DISS_2.BackEnd.Generators;
using DISS_2.BackEnd.Generators.Empiric;
using DISS_2.BackEnd.Generators.Exponential;
using DISS_2.BackEnd.Generators.Triangular;
using DISS_2.BackEnd.Generators.Uniform;
using DISS_2.BackEnd.TopFurniture.Agents;

namespace DISS_2.BackEnd.TopFurniture;

public class FurnitureGenerators
{
    public UniformGenerator<double> OrderTypeGen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(0.0, 1.0);

    public ExponentialGenerator ArrivalGen { get; } =
        new((double)1 / (30 * 60)); // 2 order per hour -> 30 min interval

    private EmpiricGenerator<double> Table1Gen { get; } =
        EmpiricGeneratorFactory.CreateRealGenerator([
            (10 * 60, 25 * 60, 0.6),
            (25 * 60, 50 * 60, 0.4),
        ]);

    private UniformGenerator<double> Table2Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(200 * 60, 610 * 60);

    private UniformGenerator<double> Table3Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(30 * 60, 60 * 60);


    private UniformGenerator<double> Chair1Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(12 * 60, 16 * 60);

    private UniformGenerator<double> Chair2Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(210 * 60, 540 * 60);

    private UniformGenerator<double> Chair3Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(14 * 60, 24 * 60);


    private UniformGenerator<double> Wardrobe1Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(15 * 60, 80 * 60);

    private UniformGenerator<double> Wardrobe2Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(600 * 60, 700 * 60);

    private UniformGenerator<double> Wardrobe3Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(35 * 60, 75 * 60);

    private UniformGenerator<double> Wardrobe4Gen { get; } = UniformGeneratorFactory
        .CreateRealUniformGenerator(15 * 60, 25 * 60);

    public TriangularRealGenerator WarehouseTravelTimeGen { get; } =
        new TriangularRealGenerator(60, 120, 480);

    public TriangularRealGenerator WarehouseMaterialPrepTimeGen { get; } =
        new TriangularRealGenerator(300, 500, 900);

    public TriangularRealGenerator InterLocationTravelTimeGen { get; } =
        new TriangularRealGenerator(120, 150, 500);


    public Generator<double> GetStepGenerator(Order order, int step)
    {
        if (order is Chair)
        {
            switch (step)
            {
                case 1: return Chair1Gen;
                case 2: return Chair2Gen;
                case 3: return Chair3Gen;
            }
        }

        if (order is Table)
        {
            switch (step)
            {
                case 1: return Table1Gen;
                case 2: return Table2Gen;
                case 3: return Table3Gen;
            }
        }

        if (order is Wardrobe)
        {
            switch (step)
            {
                case 1: return Wardrobe1Gen;
                case 2: return Wardrobe2Gen;
                case 3: return Wardrobe3Gen;
                case 4: return Wardrobe4Gen;
            }
        }

        throw new Exception($"Combination of Order and technological step is not supported!");
    }
}