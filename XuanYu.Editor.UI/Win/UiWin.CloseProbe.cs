using System;
using System.Diagnostics;
using System.Threading;
using Avalonia.Threading;

namespace XuanYu.Editor.UI;

public partial class UiWin
{
    long _closeProbeSequence;

    void RegisterCloseProbes()
    {
        Closed += (_, _) => CloseProbe("closed", CloseProbeState());
        Activated += (_, _) => CloseProbe("activated", CloseProbeState());
        Deactivated += (_, _) =>
        {
            CloseProbe("deactivated", CloseProbeState());
            (DataContext as UiVm)?.CancelInteractionFromWindowDeactivated();
            CloseProbe("deactivated-after-cancel", CloseProbeState());
            QueueCloseDialogActivation("deactivated");
        };
    }

    void CloseProbe(string stage, string details = "")
    {
        var line = $"[CLOSE-PROBE] {DateTimeOffset.Now:O} "
            + $"seq={Interlocked.Increment(ref _closeProbeSequence)} "
            + $"tid={Environment.CurrentManagedThreadId} "
            + $"ui={Dispatcher.UIThread.CheckAccess()} stage={stage} {details}";
        Console.WriteLine(line);
        Console.Out.Flush();
        Debug.WriteLine(line);
    }

    string CloseProbeState() =>
        $"allow={_allowClosing} prompt={_closePromptActive} "
        + $"dialog={_dialogTcs is not null} overlay={DialogOverlay.IsVisible} "
        + $"card={DialogCard.IsVisible} active={IsActive} visible={IsVisible} "
        + $"focus={FocusManager?.GetFocusedElement()?.GetType().Name ?? "none"}";
}
