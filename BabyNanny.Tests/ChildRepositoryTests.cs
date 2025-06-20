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
}
