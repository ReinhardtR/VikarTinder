using Persistence.Logic;

namespace UnitTest;

[TestFixture]
public class MatchAdapterTest
{
    private MatchAdapter _matchAdapter;

    [SetUp]
    public void SetUp()
    {
        _matchAdapter = new MatchAdapter(null);
    }

    [Test]
    public void canConvertProtoToMatch()
    {
    }
}