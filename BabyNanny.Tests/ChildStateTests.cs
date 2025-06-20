using BabyNanny.Data;
using BabyNanny.Models;
using Xunit;

public class ChildStateTests
{
    [Fact]
    public void AddOrUpdateChild_AddsNewChild()
    {
        var state = new ChildState();
        state.AddOrUpdateChild(new Child { Id = 1, Name = "A" });
        Assert.Single(state.Children);
        Assert.Equal("A", state.Children[0].Name);
    }

    [Fact]
    public void AddOrUpdateChild_UpdatesExisting()
    {
        var state = new ChildState();
        state.AddOrUpdateChild(new Child { Id = 1, Name = "A" });
        state.AddOrUpdateChild(new Child { Id = 1, Name = "B" });
        Assert.Single(state.Children);
        Assert.Equal("B", state.Children[0].Name);
    }

    [Fact]
    public void AddOrUpdateChild_SelectsChildWhenFlagTrue()
    {
        var state = new ChildState();
        var child = new Child { Id = 2, Name = "Child" };
        state.AddOrUpdateChild(child, select: true);
        Assert.Equal(child, state.SelectedChild);
    }
}
