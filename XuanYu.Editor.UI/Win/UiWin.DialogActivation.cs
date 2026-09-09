using Avalonia.Threading;

namespace XuanYu.Editor.UI;

public partial class UiWin
{
    void QueueCloseDialogActivation(string reason)
    {
        if (!_closePromptActive || _dialogTcs is null || !DialogCard.IsVisible) return;
        CloseProbe("dialog-activation-queued", $"reason={reason} {CloseProbeState()}");
        Dispatcher.UIThread.Post(() => RestoreCloseDialogActivation(reason));
    }

    void RestoreCloseDialogActivation(string reason)
    {
        if (!_closePromptActive || _dialogTcs is null || !DialogCard.IsVisible) return;
        CloseProbe("dialog-activation-before", $"reason={reason} {CloseProbeState()}");
        if (!IsActive) Activate();
        _dialogDefault?.Focus();
        CloseProbe("dialog-activation-after", $"reason={reason} {CloseProbeState()}");
    }
}
