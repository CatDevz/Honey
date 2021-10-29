using Honey.Core.Modrinth.Filters;
using Xunit;

namespace Honey.Core.Tests
{
    public class FilterBuilderTests
    {
        [Fact]
        public void TestFilterBuilder()
        {
            string expectedStatement = "Genre=Action AND (Actors=[Chris, Josh]) OR (Director=Cody)";

            string actualStatement = new FilterBuilder()
                .Filter("Genre", "Action")
                .And(new FilterBuilder()
                    .Filter("Actors", "[Chris, Josh]"))
                .Or(new FilterBuilder()
                    .Filter("Director", "Cody"))
                .BuildStatement();  
            
            Assert.Equal(expectedStatement, actualStatement);
        }

        [Fact]
        public void TestMoreComplexFilterBuilder()
        {
            string expectedStatement = @"
Genre=Action AND 
(Actors=[Chris, Josh] OR (Producer=[Oscar, Henry] AND (StuntMan=[Josh])) 
OR (Director=Cody))
".Replace("\n", "");

            string actualStatement = new FilterBuilder()
                .Filter("Genre", "Action")
                .And(new FilterBuilder()
                    .Filter("Actors", "[Chris, Josh]")
                    .Or(new FilterBuilder()
                        .Filter("Producer", "[Oscar, Henry]")
                        .And(new FilterBuilder()
                            .Filter("StuntMan", "[Josh]")))
                    .Or(new FilterBuilder()
                        .Filter("Director", "Cody"))).BuildStatement();

            Assert.Equal(expectedStatement, actualStatement);
        }

        [Fact]
        public void TestFilterBuilderWithNotStatements()
        {
            string expectedStatement = @"
genre=action AND NOT actor=leo AND (NOT stuntman=james AND NOT (cameraman=billy OR producer=frank))
".Replace("\n", "");

            string actualStatement = new FilterBuilder()
                .Filter("genre", "action")
                .AndNot("actor", "leo")
                .And(new FilterBuilder()
                    .FilterNot("stuntman", "james")
                    .AndNot(new FilterBuilder()
                    .Filter("cameraman", "billy")
                    .Or("producer", "frank")))
                .BuildStatement();

            Assert.Equal(expectedStatement, actualStatement);
        }
    }
}