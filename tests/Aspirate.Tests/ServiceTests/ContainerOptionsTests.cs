using System.Collections.Generic;
using System.Threading.Tasks;
using VerifyTests;
using Xunit;

namespace Aspirate.Tests.ServiceTests;

public class ContainerOptionsTests
{
    [ModuleInitializer]
    internal static void Initialize() =>
        VerifierSettings.NameForParameter<TestContainerOptions>(name => name.Value);

    [Theory]
    [MemberData(nameof(MockContainerParameters))]
    public async Task ContainerParametersFullyPopulated_ShouldPopulateImageCorrectly(TestContainerOptions testOptions)
    {
        // Arrange
        var containerParameters = testOptions.Options;


        // Act
        var fullImageName = containerParameters.ToImageNames(testOptions.Options.ImageName);


        // Assert
        await Verify(fullImageName)
            .UseParameters(testOptions)
            .UseDirectory("VerifyResults");
    }

     public static IEnumerable<object[]> MockContainerParameters =>
        new List<object[]>
        {
            new object[]
            {
                new TestContainerOptions(
                    "FullParameters", CreateContainerParameters("test-registry", "test-repository", "test-image", "test-tag", "test-arg", "test-context")),
            },
            new object[]
            {
                new TestContainerOptions(
                    "RegistryAndPrefixAndImage", CreateContainerParameters("test-registry", "test-repository", "test-image", null, null, null)),
            },
            new object[]
            {
                new TestContainerOptions(
                    "ImageAndTag", CreateContainerParameters(null, null, "test-image", "test-tag", null, null)),
            },
        };

     private static ContainerOptions CreateContainerParameters(string? testRegistry, string? testRepositoryPrefix, string? testImage, string? testTag, string? testBuildArg, string? testBuildContext) =>
         new()
         {
             Registry = testRegistry,
             Prefix = testRepositoryPrefix,
             ImageName = testImage,
             Tags = [testTag],
             BuildArgs = testBuildArg is null ? null : new() { {testBuildArg, testBuildArg} },
             BuildContext = testBuildContext,
         };

     public record TestContainerOptions(string Value, ContainerOptions Options);
}
