using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;

namespace XuanYu.Editor.UI;

public partial class UiWin
{
    protected override void OnClosing(WindowClosingEventArgs e)
    {
        var vm = DataContext as UiVm;
        CloseProbe("closing-enter", $"cancel={e.Cancel} dirty={vm?.IsSceneDirty ?? false} {CloseProbeState()}");
        if (_allowClosing || vm is null || !vm.IsSceneDirty)
        {
            vm?.CancelInteractionFromWindowClosing();
            CloseProbe("closing-pass-through", $"cancel={e.Cancel} {CloseProbeState()}");
            base.OnClosing(e);
            CloseProbe("closing-base-return", $"cancel={e.Cancel} {CloseProbeState()}");
            return;
        }
        e.Cancel = true;
        CloseProbe("closing-cancelled", $"cancel={e.Cancel} {CloseProbeState()}");
        if (_closePromptActive)
        {
            CloseProbe("closing-prompt-already-active", CloseProbeState());
            return;
        }
        _closePromptActive = true;
        CloseProbe("closing-prompt-start", CloseProbeState());
        Dispatcher.UIThread.Post(() =>
        {
            CloseProbe("close-dispatch-enter", CloseProbeState());
            _ = ConfirmCloseAsync(vm);
            CloseProbe("close-dispatch-return", CloseProbeState());
        });
        CloseProbe("closing-return", $"cancel={e.Cancel} {CloseProbeState()}");
    }

    async Task ConfirmCloseAsync(UiVm vm)
    {
        CloseProbe("confirm-enter", $"dirty={vm.IsSceneDirty} {CloseProbeState()}");
        try
        {
            CloseProbe("confirm-before-await-choice", CloseProbeState());
            var proceed = await ConfirmUnsavedBeforeContinue(vm);
            CloseProbe("confirm-after-await-choice", $"proceed={proceed} {CloseProbeState()}");
            if (!proceed) return;
            vm.CancelInteractionFromWindowClosing();
            _allowClosing = true;
            CloseProbe("close-before-final", CloseProbeState());
            Close();
            CloseProbe("close-after-final", CloseProbeState());
        }
        catch (Exception ex)
        {
            CloseProbe("confirm-exception", $"type={ex.GetType().Name} message={ex.Message}");
            Debug.WriteLine($"[WindowClose] confirmation failed: {ex}");
            CompleteDialog("cancel");
        }
        finally
        {
            CloseProbe("confirm-finally-before-reset", CloseProbeState());
            _closePromptActive = false;
            CloseProbe("confirm-finally-after-reset", CloseProbeState());
        }
    }
}
