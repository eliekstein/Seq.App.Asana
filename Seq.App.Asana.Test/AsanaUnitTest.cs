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
            this.authentication = new Authentication("0/3e7abde9b7de18d2845f2f511090f59e");
        }

        #endregion

        #region Unit Tests

        [Theory]
        [InlineData("41019370781762")]
        public void ShouldGetWorkspace(string workspaceId)
        {
            var sut = AsanaWorkspace.Retreive<AsanaWorkspace>(workspaceId, authentication);

            Assert.Equal("jafgifts.com", sut.name);
        }
        [Theory]
        [InlineData("187522734289958")]
        public void ShouldGetProject(string projectId)
        {
            var sut = AsanaProject.Retreive<AsanaProject>(projectId, authentication);

            Assert.Equal("EasyPost Implementation", sut.name);
        }
        [Theory]
        [InlineData("224577326177397")]
        public void ShouldGetTask(string taskId)
        {
            var sut = AsanaTask.Retreive<AsanaTask>(taskId, authentication);

            Assert.Equal("Manifest print out", sut.name);
            Assert.Equal("Eli Ekstein", sut.assignee.name);
        }

        [Theory]
        [InlineData("41019368881164")]
        public void ShouldGetUser(string userId)
        {
            var sut = AsanaUser.Retreive<AsanaUser>(userId, authentication);

            Assert.Equal("Eli Ekstein", sut.name);
        }

        [Fact]
        public void ShouldCreateTask()
        {
            var sut = new AsanaTask
            {
                workspace = new AsanaWorkspace { id = "41019370781762" },
                name = "Test new proj",
                projects = new[] {new AsanaProject { id = "187522734289958" } }
            };

            sut.Create(authentication);
        }
        #endregion
    }
}
