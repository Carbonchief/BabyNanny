using System;
using System.IO;
using System.Linq;
using BabyNanny.Data;
using BabyNanny.Models;
using Xunit;

public class ChildRepositoryTests
{
    [Fact]
    public void AddChild_SavesChild()
    {
        var repo = new BabyNannyRepository(Path.GetTempFileName());
        repo.Init();
        var child = repo.AddChild(new Child { Name = "Test" });
        Assert.True(child.Id > 0);
    }

    [Fact]
    public void UpdateChild_ChangesName()
    {
        var repo = new BabyNannyRepository(Path.GetTempFileName());
        repo.Init();
        var child = repo.AddChild(new Child { Name = "Old" });
        child.Name = "New";
        repo.UpdateChild(child);
        var list = repo.GetChildren();
        Assert.Equal("New", list!.First().Name);
    }

    [Fact]
    public void DeleteChild_RemovesChild()
    {
        var repo = new BabyNannyRepository(Path.GetTempFileName());
        repo.Init();
        var child = repo.AddChild(new Child { Name = "Test" });
        repo.DeleteChild(child.Id);
        var list = repo.GetChildren();
        Assert.Empty(list!);
    }

    [Fact]
    public void GetChildren_ReturnsAll()
    {
        var path = Path.GetTempFileName();
        var repo = new BabyNannyRepository(path);
        repo.Init();
        repo.AddChild(new Child { Name = "A" });
        repo.AddChild(new Child { Name = "B" });
        var list = repo.GetChildren();
        Assert.Equal(2, list!.Count);
    }
}
