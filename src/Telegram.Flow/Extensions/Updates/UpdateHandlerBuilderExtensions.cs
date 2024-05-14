using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Internals.Updates;
using Telegram.Flow.Internals.Updates.Messages;
using Telegram.Flow.Updates;
using Telegram.Flow.Updates.Messages;
using Telegram.Flow.Internals.Extensions;
using Telegram.Flow.Internals.Updates.CallbackQueries;
using Telegram.Flow.Internals.Updates.EditedMessages;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.EditedMessages;

namespace Telegram.Flow.Extensions;

public static class UpdateHandlerBuilderExtensions
{
    public static IUpdateFlow Build(
        this IUpdateBuilder builder)
    {
        var messageFlows =
            builder.MessageBuilders.Select(messageUpdateHandlerBuilder =>
                messageUpdateHandlerBuilder.Build());

        var callbackQueryFlows =
            builder.CallbackQueryBuilders.Select(callbackQueryUpdateHandlerBuilder =>
                callbackQueryUpdateHandlerBuilder.Build());

        var editedMessageFlows =
            builder.EditedMessageBuilders.Select(editedMessageUpdateHandlerBuilder =>
                editedMessageUpdateHandlerBuilder.Build());
        
        return new UpdateFlow(
            builder.TargetUpdateTypes,
            messageFlows,
            callbackQueryFlows,
            editedMessageFlows,
            builder.Tasks,
            builder.DisplayName);
    }

    public static IUpdateFlow Build<TInjected>(
        this IUpdateBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var messageFlows =
            builder.MessageBuilders.Select(messageUpdateHandlerBuilder =>
                messageUpdateHandlerBuilder.Build<TInjected>(serviceProvider));

        var callbackQueryFlows =
            builder.CallbackQueryBuilders.Select(callbackQueryUpdateHandlerBuilder =>
                callbackQueryUpdateHandlerBuilder.Build<TInjected>(serviceProvider));

        var editedMessageFlows =
            builder.EditedMessageBuilders.Select(editedMessageUpdateHandlerBuilder =>
                editedMessageUpdateHandlerBuilder.Build<TInjected>(serviceProvider));
        
        if (builder is IUpdateBuilder<TInjected> injectedBuilder)
            return new UpdateFlow<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                injectedBuilder.InjectedTasks,
                injectedBuilder.TargetUpdateTypes,
                messageFlows,
                callbackQueryFlows,
                editedMessageFlows,
                injectedBuilder.Tasks,
                builder.DisplayName);
        
        return new UpdateFlow(
            builder.TargetUpdateTypes,
            messageFlows,
            callbackQueryFlows,
            editedMessageFlows,
            builder.Tasks,
            builder.DisplayName);
    }

    public static IUpdateBuilder WithDisplayName(
        this IUpdateBuilder builder,
        string displayName)
    {
        builder.DisplayName = displayName;
        return builder;
    }

    public static IUpdateBuilder ForMessage(
        this IUpdateBuilder builder,
        Func<IMessageBuilder, IMessageBuilder>? action = null)
    {
        builder.TargetUpdateTypes.Add(UpdateType.Message);
        IMessageBuilder messageBuilder = new MessageBuilder();
        if (action is not null)
            messageBuilder = action(messageBuilder);
        builder.MessageBuilders.Add(messageBuilder);
        return builder;
    }

    public static IUpdateBuilder ForEditedMessage(
        this IUpdateBuilder builder,
        Func<IEditedMessageBuilder, IEditedMessageBuilder>? action = null)
    {
        builder.TargetUpdateTypes.Add(UpdateType.EditedMessage);
        IEditedMessageBuilder editedMessageBuilder = new EditedMessageBuilder();
        if (action is not null)
            editedMessageBuilder = action(editedMessageBuilder);
        builder.EditedMessageBuilders.Add(editedMessageBuilder);
        return builder;
    }

    public static IUpdateBuilder ForCallbackQuery(
        this IUpdateBuilder builder,
        Func<ICallbackQueryBuilder, ICallbackQueryBuilder>? action = null)
    {
        builder.TargetUpdateTypes.Add(UpdateType.CallbackQuery);
        ICallbackQueryBuilder callbackQueryBuilder = new CallbackQueryBuilder();
        if (action is not null)
            callbackQueryBuilder = action(callbackQueryBuilder);
        builder.CallbackQueryBuilders.Add(callbackQueryBuilder);
        return builder;
    }
    
    public static IUpdateBuilder<TInjected> WithInjection<TInjected>(
        this IUpdateBuilder builder)
    {
        return new UpdateBuilder<TInjected>(builder);
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