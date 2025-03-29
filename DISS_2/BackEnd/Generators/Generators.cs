using DISS_2.BackEnd.Generators.Empiric;
using DISS_2.BackEnd.Generators.Exponential;
using DISS_2.BackEnd.Generators.Triangular;
using DISS_2.BackEnd.Generators.Uniform;

namespace DISS_2.BackEnd.Generators;

public class Generators
{
    // BASIC Testers
    public UniformGenerator<int>? UniformDiscreteGenerator { get; set; }
    public UniformGenerator<double>? UniformRealGenerator { get; set; }
    public EmpiricGenerator<int>? EmpiricDiscreteGenerator { get; set; }
    public EmpiricGenerator<double>?  EmpiricRealGenerator { get; set; }

    public TriangularIntGenerator? TriangularIntGenerator { get; set; }
    public TriangularRealGenerator? TriangularRealGenerator { get; set; }
    public ExponentialGenerator? ExponentialGenerator { get; set; }
}