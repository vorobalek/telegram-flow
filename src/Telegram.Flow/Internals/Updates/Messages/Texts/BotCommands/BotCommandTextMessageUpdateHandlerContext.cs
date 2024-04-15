using Telegram.Bot.Types;
using Telegram.Flow.Updates.Messages.Texts.BotCommands;

namespace Telegram.Flow.Internals.Updates.Messages.Texts.BotCommands;

internal record BotCommandTextMessageUpdateHandlerContext(
    Update Update, 
    Message Message, 
    string Text, 
    string Command, 
    string Data, 
    bool IsLeading) : IBotCommandTextMessageUpdateHandlerContext;