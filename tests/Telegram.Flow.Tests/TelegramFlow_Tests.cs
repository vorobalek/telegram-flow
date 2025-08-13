using System.Threading;
using System.Threading.Tasks;
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
}