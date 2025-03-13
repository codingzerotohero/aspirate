using Aspirate.Commands.Actions.KubeContext;

namespace Aspirate.Commands.Commands.Apply;

public sealed class ApplyCommandHandler(IServiceProvider serviceProvider) : BaseCommandOptionsHandler<ApplyOptions>(serviceProvider)
{
    public override Task<int> HandleAsync(ApplyOptions optionses) =>
        ActionExecutor
            .QueueAction(nameof(SelectKubeContextAction))
            .QueueAction(nameof(ApplyMinikubeMountsAction))
            .QueueAction(nameof(ApplyManifestsToClusterAction))
            .ExecuteCommandsAsync();
}
