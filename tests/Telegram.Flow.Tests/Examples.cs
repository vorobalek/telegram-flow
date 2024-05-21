using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Flow.Extensions;

namespace Telegram.Flow.Tests;

public class Examples
{
    public async Task Example()
    {
        var processedUpdates = new List<Update>();
        var builder = TelegramFlow
            .New
            .ForMessage(message => message
                .ForText(text => text
                    .ForBotCommand(
                        botCommand => botCommand
                            .ForExact("echo")
                            .WithInjection<ITelegramBotClient>(new TelegramBotClient("asd"))
                            .WithAsyncProcessing(async (context, client, cancellationToken) =>
                            {
                                await client
                                    .SendTextMessageAsync(
                                        context.Update.Message.From.Id,
                                        "echo hi",
                                        cancellationToken: cancellationToken);
                            }))
                    .ForBotCommand(
                        botCommand => botCommand
                            .ForExact("echoinline")
                            .ForPrefix("echoinline_")
                            .AllowInline()
                            .WithInjection<ITelegramBotClient>(new TelegramBotClient("asd"))
                            .WithAsyncProcessing(async (context, client, cancellationToken) =>
                            {
                                await client
                                    .SendTextMessageAsync(
                                        context.Update.Message.From.Id,
                                        "echo hi",
                                        cancellationToken: cancellationToken);
                            }))
                    .WithInjection<ITelegramBotClient>(new TelegramBotClient("asd"))
                    .WithAsyncProcessing(async (context, client, cancellationToken) =>
                    {
                        await client
                            .SendTextMessageAsync(
                                context.Update.Message.From.Id,
                                "echo hi",
                                cancellationToken: cancellationToken);
                    }))
                .WithInjection<ITelegramBotClient>(new TelegramBotClient("asd"))
                .WithAsyncProcessing(async (context, client, cancellationToken) =>
                {
                    await client
                        .SendTextMessageAsync(
                            context.Update.Message.From.Id,
                            "echo hi",
                            cancellationToken: cancellationToken);
                }))
            .WithAsyncProcessing((context, cancellationToken) =>
            {
                processedUpdates.Add(context.Update);
                return Task.CompletedTask;
            })
            .WithInjection<ITelegramBotClient>(new TelegramBotClient("asd"))
            .WithAsyncProcessing(async (context, client, cancellationToken) =>
            {
                await client
                    .SendTextMessageAsync(
                        context.Update.Message.From.Id,
                        "echo hi",
                        cancellationToken: cancellationToken);
            });

        var handler = builder.Build();
    }
}