using System;
using System.IO;
using BabyNanny.Data;
using BabyNanny.Models;
using Xunit;

namespace BabyNanny.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void GetActions_FiltersByChild()
        {
            var path = Path.GetTempFileName();
            try
            {
                var repo = new BabyNannyRepository(path);
                repo.Init();
                var c1 = repo.AddChild(new Child { Name = "A" });
                var c2 = repo.AddChild(new Child { Name = "B" });
                repo.AddAction(new BabyAction { Type = 0, Started = DateTime.Now, ChildId = c1.Id });
                repo.AddAction(new BabyAction { Type = 0, Started = DateTime.Now, ChildId = c2.Id });

                var filtered = repo.GetActions(c1.Id);
                Assert.All(filtered, a => Assert.Equal(c1.Id, a.ChildId));
            }
            finally
            {
                File.Delete(path);
            }
        }
    }
}
