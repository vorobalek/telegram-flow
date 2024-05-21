using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;
using Telegram.Flow.Updates.Messages.Texts;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Extensions;

public static class TextMessageUpdateHandlerBuilderExtensions
{
    public static TBuilder ForBotCommand<TBuilder>(
        this TBuilder builder,
        Func<IBotCommandBuilder, IBotCommandBuilder>? action = null)
        where TBuilder : ITextBuilder
    {
        IBotCommandBuilder botCommandBuilder =
            new BotCommandBuilder();
        if (action is not null)
            botCommandBuilder = action(botCommandBuilder);
        builder.BotCommandBuilders.Add(botCommandBuilder);
        return builder;
    }

    public static ITextBuilder WithAsyncProcessing(
        this ITextBuilder builder,
        AsyncProcessingDelegate<ITextContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }

    public static ITextBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this ITextBuilder<TInjected> builder,
        AsyncProcessingDelegate<ITextContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}