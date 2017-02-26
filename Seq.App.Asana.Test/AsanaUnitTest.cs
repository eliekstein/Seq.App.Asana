using Seq.App.Asana;
using System;
using Xunit;

namespace Seq.App.Asana.Test
{
    public class AsanaUnitTest
    {
        #region Properties
        private readonly Authentication authentication;
        #endregion

        #region Constructors

        public AsanaUnitTest()
        {
            this.authentication = new Authentication("0/56d857894dffb9f6dc5f564bcc803348");
        }

        #endregion

        #region Unit Tests

        [Theory]
        [InlineData(typeof(AsanaWorkspace), "280863004129149", "testing inc")]
        [InlineData(typeof(AsanaProject), "280862546009236", "ProjectTest")]
        [InlineData(typeof(AsanaTask), "280868970271023", "testing tesks get")]
        [InlineData(typeof(AsanaUser), "280863015720932", "test")]
        public void ShouldGetResource(Type ResourceType,string resourceId, string resourceName)
        {
            var sut = AsanaWorkspace.Retreive(ResourceType, resourceId, authentication);
            Assert.IsType(ResourceType, sut);
            Assert.Equal(resourceName, sut.name);
        }

        [Theory]
        [InlineData("280868970271023")]
        public void ShouldGetTaskAssignee(string taskId)
        {
            var sut = AsanaTask.Retreive<AsanaTask>(taskId, authentication);

            Assert.Equal("test", sut.assignee.name);
        }
        
        [Fact]
        public void ShouldCreateTask()
        {
            var sut = new AsanaTask
            {
                workspace = new AsanaWorkspace { id = "280863004129149" },
                name = "Test new proj",
                projects = new[] {new AsanaProject { id = "280862546009236" } }
            };

            sut.Create(authentication);
        }
        #endregion
    }
}
