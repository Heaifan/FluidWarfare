using System;
using System.IO;

namespace XuanYu.World.Tests.UiTokens;

public sealed class UiCloseLifecycleContractTests
{
    static string Read(string rel) => File.ReadAllText(Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..", "XuanYu.Editor.UI", rel));

    [Fact]
    public void Close_confirmation_is_deferred_until_window_close_event_returns()
    {
        var code = Read("Win/UiWin.CloseLifecycle.cs");
        Assert.DoesNotContain("protected override async void OnClosing", code);
        Assert.Contains("e.Cancel = true;", code);
        Assert.Contains("if (_closePromptActive)", code);
        Assert.Contains("closing-prompt-already-active", code);
        Assert.Contains("Dispatcher.UIThread.Post(() =>", code);
        Assert.Contains("_ = ConfirmCloseAsync(vm);", code);
        Assert.Contains("_allowClosing = true;", code);
        Assert.Contains("finally", code);
    }

    [Fact]
    public void Dialog_card_is_above_input_blocking_overlay()
    {
        var axaml = Read("Win/UiWin.axaml");
        Assert.Contains("x:Name=\"DialogOverlay\" ZIndex=\"90\"", axaml);
        Assert.Contains("x:Name=\"DialogCard\" ZIndex=\"100\"", axaml);
    }

    [Fact]
    public void Close_probe_writes_flushable_terminal_trace_for_each_lifecycle_boundary()
    {
        var probe = Read("Win/UiWin.CloseProbe.cs");
        Assert.Contains("[CLOSE-PROBE]", probe);
        Assert.Contains("Console.WriteLine(line);", probe);
        Assert.Contains("Console.Out.Flush();", probe);
        var close = Read("Win/UiWin.CloseLifecycle.cs");
        Assert.Contains("closing-enter", close);
        Assert.Contains("close-dispatch-enter", close);
        Assert.Contains("confirm-after-await-choice", close);
        Assert.Contains("close-before-final", close);
        Assert.Contains("closed", probe);
    }
}
