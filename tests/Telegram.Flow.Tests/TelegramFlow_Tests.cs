using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Flow;
using Telegram.Flow.Extensions;
using Telegram.Flow.Updates;

namespace Telegram.Flow.Tests;

[TestClass]
public class TelegramFlow_Tests
{
    [TestMethod]
    public void TelegramFlow_New_BuildsFlow()
    {
        IUpdateFlow flow = TelegramFlow.New.Build();
        Assert.IsNotNull(flow);
        Assert.IsInstanceOfType(flow, typeof(IUpdateFlow));
    }

    [TestMethod]
    public void UpdateBuilder_WithDisplayName_SetsFlowDisplayName()
    {
        const string name = "My Flow";
        var flow = TelegramFlow
            .New
            .WithDisplayName(name)
            .Build();

        Assert.AreEqual(name, flow.DisplayName);
    }

    [TestMethod]
    public async Task Message_Text_BotCommand_Leading_TriggersAllTasks()
    {
        var flags = new List<string>();

        var flow = TelegramFlow
            .New
            .ForMessage(m => m
                .ForText(t => t
                    .ForBotCommand(c => c
                        .ForExact("echo")
                        .WithAsyncProcessing((Telegram.Flow.Updates.Messages.Texts.BotCommands.IBotCommandContext ctx, CancellationToken ct) =>
                        {
                            flags.Add($"bot:{ctx.Command}:{ctx.Data}:{ctx.IsLeading}");
                            return Task.CompletedTask;
                        }))
                    .WithAsyncProcessing((ctx, ct) =>
                    {
                        flags.Add($"text:{ctx.Text}");
                        return Task.CompletedTask;
                    }))
                .WithAsyncProcessing((ctx, ct) =>
                {
                    flags.Add($"msg:{ctx.Message.Type}");
                    return Task.CompletedTask;
                }))
            .WithAsyncProcessing((ctx, ct) =>
            {
                _ = ctx.Update.Type;
                flags.Add("update");
                return Task.CompletedTask;
            })
            .Build();

        var update = TestHelpers.CreateMessageUpdate("/echo data", (0, 5));

        await flow.ProcessAsync(update, CancellationToken.None);

        CollectionAssert.AreEquivalent(
            new[] { "bot:echo:data:True", "text:/echo data", "msg:Text", "update" },
            flags);
    }

    [TestMethod]
    public async Task Message_Text_BotCommand_NonLeading_NoInline_DoesNotTriggerBotTask()
    {
        var flags = new List<string>();

        var flow = TelegramFlow
            .New
            .ForMessage(m => m
                .ForText(t => t
                    .ForBotCommand(c => c
                        .ForExact("echo")
                        .WithAsyncProcessing((Telegram.Flow.Updates.Messages.Texts.BotCommands.IBotCommandContext ctx, CancellationToken ct) =>
                        {
                            flags.Add($"bot:{ctx.Command}:{ctx.Data}:{ctx.IsLeading}");
                            return Task.CompletedTask;
                        }))
                    .WithAsyncProcessing((ctx, ct) =>
                    {
                        flags.Add($"text:{ctx.Text}");
                        return Task.CompletedTask;
                    }))
                .WithAsyncProcessing((ctx, ct) =>
                {
                    flags.Add($"msg:{ctx.Message.Type}");
                    return Task.CompletedTask;
                }))
            .WithAsyncProcessing((ctx, ct) =>
            {
                _ = ctx.Update.Type;
                flags.Add("update");
                return Task.CompletedTask;
            })
            .Build();

        var text = "hello /echo data";
        var offset = text.IndexOf("/echo", StringComparison.Ordinal);
        var update = TestHelpers.CreateMessageUpdate(text, (offset, 5));

        await flow.ProcessAsync(update, CancellationToken.None);

        // bot-level should NOT be triggered, others should
        CollectionAssert.AreEquivalent(
            new[] { $"text:{text}", "msg:Text", "update" },
            flags);
    }

    [TestMethod]
    public async Task Message_Text_BotCommand_NonLeading_AllowInline_TriggersBotTask()
    {
        var flags = new List<string>();

        var flow = TelegramFlow
            .New
            .ForMessage(m => m
                .ForText(t => t
                    .ForBotCommand(c => c
                        .ForExact("echo")
                        .AllowInline()
                        .WithAsyncProcessing((Telegram.Flow.Updates.Messages.Texts.BotCommands.IBotCommandContext ctx, CancellationToken ct) =>
                        {
                            flags.Add($"bot:{ctx.Command}:{ctx.Data}:{ctx.IsLeading}");
                            return Task.CompletedTask;
                        }))
                    .WithAsyncProcessing((ctx, ct) =>
                    {
                        flags.Add($"text:{ctx.Text}");
                        return Task.CompletedTask;
                    }))
                .WithAsyncProcessing((ctx, ct) =>
                {
                    flags.Add($"msg:{ctx.Message.Type}");
                    return Task.CompletedTask;
                }))
            .WithAsyncProcessing((ctx, ct) =>
            {
                _ = ctx.Update.Type;
                flags.Add("update");
                return Task.CompletedTask;
            })
            .Build();

        var text = "hello /echo data";
        var offset = text.IndexOf("/echo", StringComparison.Ordinal);
        var update = TestHelpers.CreateMessageUpdate(text, (offset, 5));

        await flow.ProcessAsync(update, CancellationToken.None);

        Assert.IsTrue(flags.Contains("update"));
        Assert.IsTrue(flags.Contains($"text:{text}"));
        Assert.IsTrue(flags.Contains("msg:Text"));
        Assert.IsTrue(flags.Contains("bot:echo:data:False"));
    }

    [TestMethod]
    public async Task Message_Text_BotCommand_Leading_Prefix_TriggersBotTask()
    {
        var flags = new List<string>();

        var flow = TelegramFlow
            .New
            .ForMessage(m => m
                .ForText(t => t
                    .ForBotCommand(c => c
                        .ForPrefix("ec")
                        .WithAsyncProcessing((Telegram.Flow.Updates.Messages.Texts.BotCommands.IBotCommandContext ctx, CancellationToken ct) =>
                        {
                            flags.Add($"bot:{ctx.Command}:{ctx.Data}:{ctx.IsLeading}");
                            return Task.CompletedTask;
                        }))
                    .WithAsyncProcessing((ctx, ct) =>
                    {
                        flags.Add($"text:{ctx.Text}");
                        return Task.CompletedTask;
                    }))
                .WithAsyncProcessing((ctx, ct) =>
                {
                    flags.Add($"msg:{ctx.Message.Type}");
                    return Task.CompletedTask;
                }))
            .WithAsyncProcessing((ctx, ct) =>
            {
                _ = ctx.Update.Type;
                flags.Add("update");
                return Task.CompletedTask;
            })
            .Build();

        var update = TestHelpers.CreateMessageUpdate("/echo data", (0, 5));

        await flow.ProcessAsync(update, CancellationToken.None);

        Assert.IsTrue(flags.Contains("bot:echo:data:True"));
    }

    [TestMethod]
    public async Task CallbackQuery_Data_Exact_TriggersDataAndHigherLevels()
    {
        var flags = new List<string>();

        var flow = TelegramFlow
            .New
            .ForCallbackQuery(cb => cb
                .ForData(d => d
                    .ForExact("ping")
                    .WithAsyncProcessing((Telegram.Flow.Updates.CallbackQueries.Data.IDataContext ctx, CancellationToken ct) =>
                    {
                        flags.Add($"data:{ctx.Data}");
                        return Task.CompletedTask;
                    }))
                .WithAsyncProcessing((ctx, ct) =>
                {
                    flags.Add($"cb:{ctx.CallbackQuery.Data}");
                    return Task.CompletedTask;
                }))
            .WithAsyncProcessing((ctx, ct) =>
            {
                _ = ctx.Update;
                flags.Add("update");
                return Task.CompletedTask;
            })
            .Build();

        var update = TestHelpers.CreateUninitialized<Update>();
        var cb = TestHelpers.CreateUninitialized<CallbackQuery>();
        TestHelpers.SetAutoProperty(cb, nameof(CallbackQuery.Data), "ping");
        TestHelpers.SetAutoProperty(update, nameof(Update.CallbackQuery), cb);

        await flow.ProcessAsync(update, CancellationToken.None);

        CollectionAssert.AreEquivalent(new[] { "data:ping", "cb:ping", "update" }, flags);
    }

    [TestMethod]
    public async Task CallbackQuery_Data_Prefix_TriggersDataTask()
    {
        var flags = new List<string>();

        var flow = TelegramFlow
            .New
            .ForCallbackQuery(cb => cb
                .ForData(d => d
                    .ForPrefix("pre")
                    .WithAsyncProcessing((Telegram.Flow.Updates.CallbackQueries.Data.IDataContext ctx, CancellationToken ct) =>
                    {
                        flags.Add($"data:{ctx.Data}");
                        return Task.CompletedTask;
                    }))
                .WithAsyncProcessing((ctx, ct) =>
                {
                    flags.Add($"cb:{ctx.CallbackQuery.Data}");
                    return Task.CompletedTask;
                }))
            .WithAsyncProcessing((ctx, ct) =>
            {
                _ = ctx.Update;
                flags.Add("update");
                return Task.CompletedTask;
            })
            .Build();

        var update = TestHelpers.CreateUninitialized<Update>();
        var cb = TestHelpers.CreateUninitialized<CallbackQuery>();
        TestHelpers.SetAutoProperty(cb, nameof(CallbackQuery.Data), "prefix_payload");
        TestHelpers.SetAutoProperty(update, nameof(Update.CallbackQuery), cb);

        await flow.ProcessAsync(update, CancellationToken.None);

        Assert.IsTrue(flags.Contains("data:prefix_payload"));
        Assert.IsTrue(flags.Contains("cb:prefix_payload"));
        Assert.IsTrue(flags.Contains("update"));
    }

    [TestMethod]
    public async Task Message_Text_BotCommand_Injections_TriggerAll()
    {
        var flags = new List<string>();

        var flow = TelegramFlow
            .New
            .WithInjection<bool>(true)
            .WithAsyncProcessing((ctx, inj, ct) =>
            {
                flags.Add($"updateinj:{inj}");
                return Task.CompletedTask;
            })
            .ForMessage(m => m
                .WithInjection<double>(3.14)
                .WithAsyncProcessing((ctx, inj, ct) =>
                {
                    flags.Add($"msginj:{inj:F2}");
                    return Task.CompletedTask;
                })
                .ForText(t => t
                    .WithInjection<int>(7)
                    .WithAsyncProcessing((ctx, inj, ct) =>
                    {
                        flags.Add($"textinj:{inj}");
                        return Task.CompletedTask;
                    })
                    .ForBotCommand(c => c
                        .ForExact("echo")
                        .WithInjection<string>("ICE")
                        .WithAsyncProcessing((Telegram.Flow.Updates.Messages.Texts.BotCommands.IBotCommandContext ctx, string inj, CancellationToken ct) =>
                        {
                            flags.Add($"botinj:{inj}");
                            return Task.CompletedTask;
                        }))))
            .Build();

        var update = TestHelpers.CreateMessageUpdate("/echo data", (0, 5));
        await flow.ProcessAsync(update, CancellationToken.None);

        Assert.IsTrue(flags.Contains("updateinj:True"));
        Assert.IsTrue(flags.Contains("msginj:3.14"));
        Assert.IsTrue(flags.Contains("textinj:7"));
        Assert.IsTrue(flags.Contains("botinj:ICE"));
    }

    [TestMethod]
    public async Task EditedMessage_Text_TriggersTextEditedAndUpdate()
    {
        var flags = new List<string>();

        var flow = TelegramFlow
            .New
            .ForEditedMessage(em => em
                .ForText(t => t
                    .WithAsyncProcessing((ctx, ct) =>
                    {
                        flags.Add($"text:{ctx.Text}");
                        return Task.CompletedTask;
                    }))
                .WithAsyncProcessing((ctx, ct) =>
                {
                    flags.Add($"emsg");
                    return Task.CompletedTask;
                }))
            .WithAsyncProcessing((ctx, ct) =>
            {
                flags.Add("update");
                return Task.CompletedTask;
            })
            .Build();

        var update = TestHelpers.CreateEditedMessageUpdate("/edit something", (0, 5));
        await flow.ProcessAsync(update, CancellationToken.None);

        CollectionAssert.AreEquivalent(new[] { "text:/edit something", "emsg", "update" }, flags);
    }
}