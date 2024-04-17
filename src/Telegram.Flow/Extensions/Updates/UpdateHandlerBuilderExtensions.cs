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
    public static IUpdateHandler Build(
        this IUpdateHandlerBuilder builder)
    {
        var messageUpdateHandlers =
            builder.MessageUpdateHandlerBuilders.Select(messageUpdateHandlerBuilder =>
                messageUpdateHandlerBuilder.Build());

        var callbackQueryUpdateHandlers =
            builder.CallbackQueryUpdateHandlerBuilders.Select(callbackQueryUpdateHandlerBuilder =>
                callbackQueryUpdateHandlerBuilder.Build());

        var editedMessageUpdateHandlers =
            builder.EditedMessageUpdateHandlerBuilders.Select(editedMessageUpdateHandlerBuilder =>
                editedMessageUpdateHandlerBuilder.Build());
        
        return new UpdateHandler(
            builder.TargetUpdateTypes,
            builder.ProcessingTasks,
            messageUpdateHandlers,
            callbackQueryUpdateHandlers,
            editedMessageUpdateHandlers,
            builder.DisplayName);
    }

    public static IUpdateHandler Build<TInjected>(
        this IUpdateHandlerBuilder builder,
        IServiceProvider serviceProvider) where TInjected : notnull
    {
        var messageUpdateHandlers =
            builder.MessageUpdateHandlerBuilders.Select(messageUpdateHandlerBuilder =>
                messageUpdateHandlerBuilder.Build<TInjected>(serviceProvider));

        var callbackQueryUpdateHandlers =
            builder.CallbackQueryUpdateHandlerBuilders.Select(callbackQueryUpdateHandlerBuilder =>
                callbackQueryUpdateHandlerBuilder.Build<TInjected>(serviceProvider));

        var editedMessageUpdateHandlers =
            builder.EditedMessageUpdateHandlerBuilders.Select(editedMessageUpdateHandlerBuilder =>
                editedMessageUpdateHandlerBuilder.Build<TInjected>(serviceProvider));
        
        if (builder is IUpdateHandlerBuilder<TInjected> tInjectedBuilder)
            return new UpdateHandler<TInjected>(
                serviceProvider.GetRequiredService<TInjected>(),
                tInjectedBuilder.InjectedProcessingTasks,
                tInjectedBuilder.TargetUpdateTypes,
                tInjectedBuilder.ProcessingTasks,
                messageUpdateHandlers,
                callbackQueryUpdateHandlers,
                editedMessageUpdateHandlers,
                builder.DisplayName);
        
        return new UpdateHandler(
            builder.TargetUpdateTypes,
            builder.ProcessingTasks,
            messageUpdateHandlers,
            callbackQueryUpdateHandlers,
            editedMessageUpdateHandlers,
            builder.DisplayName);
    }

    public static IUpdateHandlerBuilder WithDisplayName(
        this IUpdateHandlerBuilder builder,
        string displayName)
    {
        builder.DisplayName = displayName;
        return builder;
    }

    public static IUpdateHandlerBuilder ForMessage(
        this IUpdateHandlerBuilder builder,
        Func<IMessageUpdateHandlerBuilder, IMessageUpdateHandlerBuilder>? action = null)
    {
        builder.TargetUpdateTypes.Add(UpdateType.Message);
        IMessageUpdateHandlerBuilder messageUpdateHandlerBuilder = new MessageUpdateHandlerBuilder();
        if (action is not null)
            messageUpdateHandlerBuilder = action(messageUpdateHandlerBuilder);
        builder.MessageUpdateHandlerBuilders.Add(messageUpdateHandlerBuilder);
        return builder;
    }

    public static IUpdateHandlerBuilder ForEditedMessage(
        this IUpdateHandlerBuilder builder,
        Func<IEditedMessageUpdateHandlerBuilder, IEditedMessageUpdateHandlerBuilder>? action = null)
    {
        builder.TargetUpdateTypes.Add(UpdateType.EditedMessage);
        IEditedMessageUpdateHandlerBuilder editedMessageUpdateHandlerBuilder = new EditedMessageUpdateHandlerBuilder();
        if (action is not null)
            editedMessageUpdateHandlerBuilder = action(editedMessageUpdateHandlerBuilder);
        builder.EditedMessageUpdateHandlerBuilders.Add(editedMessageUpdateHandlerBuilder);
        return builder;
    }

    public static IUpdateHandlerBuilder ForCallbackQuery(
        this IUpdateHandlerBuilder builder,
        Func<ICallbackQueryUpdateHandlerBuilder, ICallbackQueryUpdateHandlerBuilder>? action = null)
    {
        builder.TargetUpdateTypes.Add(UpdateType.CallbackQuery);
        ICallbackQueryUpdateHandlerBuilder callbackQueryUpdateHandlerBuilder = new CallbackQueryUpdateHandlerBuilder();
        if (action is not null)
            callbackQueryUpdateHandlerBuilder = action(callbackQueryUpdateHandlerBuilder);
        builder.CallbackQueryUpdateHandlerBuilders.Add(callbackQueryUpdateHandlerBuilder);
        return builder;
    }
    
    public static IUpdateHandlerBuilder<TInjected> WithInjection<TInjected>(
        this IUpdateHandlerBuilder builder)
    {
        return new UpdateHandlerBuilder<TInjected>(builder);
    }

    public static IUpdateHandlerBuilder WithAsyncProcessing(
        this IUpdateHandlerBuilder builder,
        AsyncProcessingDelegate<IUpdateHandlerContext> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
    
    public static IUpdateHandlerBuilder<TInjected> WithAsyncProcessing<TInjected>(
        this IUpdateHandlerBuilder<TInjected> builder,
        AsyncProcessingDelegate<IUpdateHandlerContext, TInjected> func)
    {
        return builder.WithAsyncProcessingInternal(func);
    }
}