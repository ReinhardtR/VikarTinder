using System.Collections;
using Persistence;
using Persistence.Converter;
using Persistence.Converter.Interfaces;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace UnitTest;

[TestFixture]
public class ConverterTest
{
    private IMatchConverter _converter;
    private List<Gig> _gigsForTest;
    readonly int idOnSubMatch = 1;
    bool matchMatchedYou = false;
    readonly int userId = 2;
    
    [SetUp]
    public void SetUp()
    {
        _converter = new MatchConverter();
        _gigsForTest = new List<Gig>
        {
            new(new Employer
            {
                Id = 1
            }),
            new(new Employer
            {
                Id = 2
            }),
            new(new Employer
            {
                Id = 3
            }),
            new(new Employer
            {
                Id = 4
            }),
            new(new Employer
            {
                Id = 5
            })
        };
    }
    
    //Substitute converter tests
    //Testing for employer making matches
    //Creation of match validations
    
    [Test, Description("Testing for creating matchValidation with null gigs list")]
    public void GigsListIsNull()
    {
        Assert.Catch<ConverterNullReference>(() => _converter.SubstituteConverter(new Substitute { Id = 1 }, userId));
    }

    [Test, Description("Testing for throwing if substitute is null")]
    public void SubstiuteIsNull()
    {
        Assert.Catch<ConverterNullReference>(() => _converter.SubstituteConverter(null, userId));
    }

    [Test, Description("Testing for creating matchValidation with no current match")]
    public void MatchValidationWithNoCurrentMatchForEmployer()
    {
        int notMatchingUserId = 99;
        
        MatchValidation val = _converter.SubstituteConverter(new Substitute { Id = idOnSubMatch , Positions = _gigsForTest}, notMatchingUserId);

        Assert.That(idOnSubMatch, Is.EqualTo(val.MatchId));
        Assert.That(matchMatchedYou, Is.EqualTo(val.IsMatched));
    }

    
    [Test, Description("Testing for creating matchValidation with existing match")]
    public void MatchValidationWithCurrentMatchForEmployer()
    {
        bool expectedMatch = true;

        MatchValidation val = _converter.SubstituteConverter(new Substitute { Id = idOnSubMatch , Positions = _gigsForTest}, userId);

        Assert.That(idOnSubMatch, Is.EqualTo(val.MatchId));
        Assert.That(expectedMatch, Is.EqualTo(val.IsMatched));
    }
    
}