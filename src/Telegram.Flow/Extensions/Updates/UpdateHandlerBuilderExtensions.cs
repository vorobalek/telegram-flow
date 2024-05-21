using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates.CallbackQueries;
using Telegram.Flow.Internals.Updates.EditedMessages;
using Telegram.Flow.Internals.Updates.Messages;
using Telegram.Flow.Updates;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Extensions;

public static class UpdateHandlerBuilderExtensions
{
    public static TBuilder WithDisplayName<TBuilder>(
        this TBuilder builder,
        string displayName)
        where TBuilder : IUpdateBuilder
    {
        builder.DisplayName = displayName;
        return builder;
    }

    public static TBuilder ForMessage<TBuilder>(
        this TBuilder builder,
        Func<IMessageBuilder, IMessageBuilder>? action = null)
        where TBuilder : IUpdateBuilder
    {
        builder.TargetUpdateTypes.Add(UpdateType.Message);
        IMessageBuilder messageBuilder = new MessageBuilder();
        if (action is not null)
            messageBuilder = action(messageBuilder);
        builder.MessageBuilders.Add(messageBuilder);
        return builder;
    }

    public static TBuilder ForEditedMessage<TBuilder>(
        this TBuilder builder,
        Func<IEditedMessageBuilder, IEditedMessageBuilder>? action = null)
        where TBuilder : IUpdateBuilder
    {
        builder.TargetUpdateTypes.Add(UpdateType.EditedMessage);
        IEditedMessageBuilder editedMessageBuilder = new EditedMessageBuilder();
        if (action is not null)
            editedMessageBuilder = action(editedMessageBuilder);
        builder.EditedMessageBuilders.Add(editedMessageBuilder);
        return builder;
    }

    public static TBuilder ForCallbackQuery<TBuilder>(
        this TBuilder builder,
        Func<ICallbackQueryBuilder, ICallbackQueryBuilder>? action = null)
        where TBuilder : IUpdateBuilder
    {
        builder.TargetUpdateTypes.Add(UpdateType.CallbackQuery);
        ICallbackQueryBuilder callbackQueryBuilder = new CallbackQueryBuilder();
        if (action is not null)
            callbackQueryBuilder = action(callbackQueryBuilder);
        builder.CallbackQueryBuilders.Add(callbackQueryBuilder);
        return builder;
    }

    public static IUpdateBuilder WithAsyncProcessing(
        this IUpdateBuilder builder,
        AsyncProcessingDelegate<IUpdateContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }

    public static IUpdateBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IUpdateBuilder<TInjected> builder,
        AsyncProcessingDelegate<IUpdateContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}