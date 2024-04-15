using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Updates;

public interface IUpdateHandlerBuilder : 
    IAsyncProcessingBuilder<IUpdateHandlerContext>
{
    internal string? DisplayName { get; set; }
    internal ISet<UpdateType> TargetUpdateTypes { get; }
    
    internal IList<IMessageUpdateHandlerBuilder> MessageUpdateHandlerBuilders { get; }
    internal IList<ICallbackQueryUpdateHandlerBuilder> CallbackQueryUpdateHandlerBuilders { get; }
    internal IList<IEditedMessageUpdateHandlerBuilder> EditedMessageUpdateHandlerBuilders { get; }
}

public interface IUpdateHandlerBuilder<TInjected> : 
    IUpdateHandlerBuilder, 
    IAsyncProcessingBuilder<IUpdateHandlerContext, TInjected>;
