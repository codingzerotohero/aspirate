namespace Aspirate.Commands.Options;

public sealed class SpecifiedResourcesOption : BaseOption<string?>
{
    private static readonly string[] _aliases =
    [
        "-sr",
        "--specify-resources"
    ];

    private SpecifiedResourcesOption() : base(_aliases, "ASPIRATE_SPECIFY_RESOURCES", null)
    {
        Name = nameof(ISpecifiedResourcesOptions.SpecifiedResources);
        Description = "Specified which resources to apply/build/generate/run non interactively";
        Arity = ArgumentArity.OneOrMore;
        IsRequired = false;
    }

    public static SpecifiedResourcesOption Instance { get; } = new();

    public override bool IsSecret => false;
}
