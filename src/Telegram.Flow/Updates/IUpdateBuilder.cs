using Telegram.Bot.Types.Enums;
using Telegram.Flow.Infrastructure;
using Telegram.Flow.Updates.CallbackQueries;
using Telegram.Flow.Updates.EditedMessages;
using Telegram.Flow.Updates.Messages;

namespace Telegram.Flow.Updates;

public interface IUpdateBuilder : 
    IBuilder<IUpdateContext>
{
    internal string? DisplayName { get; set; }
    internal ISet<UpdateType> TargetUpdateTypes { get; }
    
    internal IList<IMessageBuilder> MessageBuilders { get; }
    internal IList<ICallbackQueryBuilder> CallbackQueryBuilders { get; }
    internal IList<IEditedMessageBuilder> EditedMessageBuilders { get; }
}

public interface IUpdateBuilder<TInjected> : 
    IUpdateBuilder, 
    IBuilder<IUpdateContext, TInjected>;
