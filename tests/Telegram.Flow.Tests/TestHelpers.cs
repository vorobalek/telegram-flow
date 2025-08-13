using System.Reflection;
using System.Runtime.Serialization;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Flow.Tests;

internal static class TestHelpers
{
    public static T CreateUninitialized<T>() where T : class
    {
        return (T)FormatterServices.GetUninitializedObject(typeof(T));
    }

    public static void SetAutoProperty(object obj, string propertyName, object? value)
    {
        var field = obj.GetType().GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
        if (field == null)
            throw new MissingFieldException(obj.GetType().FullName, $"<{propertyName}>k__BackingField");
        field.SetValue(obj, value);
    }

    public static Update CreateMessageUpdate(string text, (int offset, int length) command)
    {
        var update = CreateUninitialized<Update>();
        var message = CreateUninitialized<Message>();

        SetAutoProperty(update, nameof(Update.Message), message);

        SetAutoProperty(message, nameof(Message.Text), text);
        SetAutoProperty(message, nameof(Message.Entities), new[]
        {
            InitializeEntity(command.offset, command.length, MessageEntityType.BotCommand)
        });

        return update;
    }

    public static Update CreateEditedMessageUpdate(string text, (int offset, int length) command)
    {
        var update = CreateUninitialized<Update>();
        var message = CreateUninitialized<Message>();

        SetAutoProperty(update, nameof(Update.EditedMessage), message);

        SetAutoProperty(message, nameof(Message.Text), text);
        SetAutoProperty(message, nameof(Message.Entities), new[]
        {
            InitializeEntity(command.offset, command.length, MessageEntityType.BotCommand)
        });

        return update;
    }

    private static MessageEntity InitializeEntity(int offset, int length, MessageEntityType type)
    {
        var entity = CreateUninitialized<MessageEntity>();
        SetAutoProperty(entity, nameof(MessageEntity.Offset), offset);
        SetAutoProperty(entity, nameof(MessageEntity.Length), length);
        SetAutoProperty(entity, nameof(MessageEntity.Type), type);
        return entity;
    }
}