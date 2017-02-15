using Seq.App.Asana;
using Xunit;

namespace Seq.App.Asana.Test
{
    public class AsanaUnitTest
    {
        [Fact]
        public void ShouldGetWorkspace()
        {
            AsanaWorkspace sut = new AsanaWorkspace().Retreive< AsanaWorkspace>("41019370781762", new Authentication("0/2d8427f251f20af1861db5e69cdf5e9d")) as AsanaWorkspace;

            Assert.Equal("jafgifts.com", sut.name);

        }
        [Fact]
        public void ShouldGetProject()
        {

            var sut = new AsanaProject().Retreive<AsanaProject>("187522734289958", new Authentication("0/2d8427f251f20af1861db5e69cdf5e9d"));

            Assert.Equal("EasyPost Implementation", sut.name);


        }
        [Fact]
        public void ShouldGetTask()
        {

            var sut = new AsanaTask().Retreive<AsanaTask>("224577326177397", new Authentication("0/2d8427f251f20af1861db5e69cdf5e9d"));

            Assert.Equal("Manifest print out", sut.name);
            Assert.Equal("Eli Ekstein", sut.assignee.name);
        }
    }
}
