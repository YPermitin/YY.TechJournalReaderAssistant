using Xunit;
using YY.TechJournalReaderAssistant.Helpers;
using YY.TechJournalReaderAssistant.Tests.TestData;

namespace YY.TechJournalReaderAssistant.Tests.Helpers
{
    public class StringExtensionsTests
    {
        [Fact]
        public void Test_GetQueryHash()
        {
            string queryTJ = ClearSQLQuery.QueryFromTechJournal;
            string queryXE = ClearSQLQuery.QueryFromSQLServerExtendedEvents;

            string hashQueryTJ = queryTJ.GetQueryHash();
            string hashQueryXE = queryXE.GetQueryHash(true);

            Assert.Equal(hashQueryTJ, hashQueryXE);
        }
    }
}
