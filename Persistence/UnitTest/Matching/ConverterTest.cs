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
    private List<Substitute> _substitutesForTest;
    
    readonly int idOnSubMatch = 1;
    bool matchMatchedYou = false;
    readonly int userId = 2;
    public int listSize = 5;
    
    [SetUp]
    public void SetUp()
    {
        _converter = new MatchConverter();
        _gigsForTest = new List<Gig>();
        _substitutesForTest = new List<Substitute>();
        
        for (int i = 0; i < listSize; i++)
        {
            _gigsForTest.Add(new Gig(new Employer
            {
                Id = i
            }));
            
            _substitutesForTest.Add(new Substitute
            {
                Id = i
            });
        }
        
    }
    
    //Substitute converter tests
    //Testing for employer making matches
    //Creation of match validations
    
    [Test, Description("Testing for creating matchValidation with null gigs list")]
    public void GigsListIsNull()
    {
        Assert.Catch<ConverterNullReference>(() => _converter.SubstituteConverter(new Substitute() { Id = 1 }, userId));
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
        
        MatchValidation val = _converter.SubstituteConverter(new Substitute() { Id = idOnSubMatch , Positions = _gigsForTest}, notMatchingUserId);

        Assert.That(idOnSubMatch, Is.EqualTo(val.MatchId));
        Assert.That(matchMatchedYou, Is.EqualTo(val.IsMatched));
    }

    
    [Test, Description("Testing for creating matchValidation with existing match")]
    public void MatchValidationWithCurrentMatchForEmployer()
    {
        bool expectedMatch = true;

        MatchValidation val = _converter.SubstituteConverter(new Substitute() { Id = idOnSubMatch , Positions = _gigsForTest}, userId);

        Assert.That(idOnSubMatch, Is.EqualTo(val.MatchId));
        Assert.That(expectedMatch, Is.EqualTo(val.IsMatched));
    }
    
    
    //ConvertSubList Test
    //Conversion from List of domain objects Substitute to message MatchingSubstitutes

    [Test, Description("Giving a null list to convert should catch a ConverterNullReference")]
    public void ConvertWithNullList()
    {
        Assert.Catch<ConverterNullReference>(() => _converter.ConvertSubList(null));
    }

    [Test, Description("Giving an empty list as argument should also catch a nullReference")]
    public void ConvertListWithNoELements()
    {
        Assert.Catch<ConverterNullReference>((() => _converter.ConvertSubList(new List<Substitute>())));
    }

    [Test, Description("Given a list of substitutes with 5 elements they should all be correctly converted")]
    public void ConvertListWithFiveElements()
    {
        MatchingSubstitutes convertedSubs = _converter.ConvertSubList(_substitutesForTest);
        
        Assert.That(listSize, Is.EqualTo(convertedSubs.Substitutes.Count));

        int index = 0;
        foreach (var sub in convertedSubs.Substitutes)
        {
            Assert.That(index, Is.EqualTo(sub.Id));
            index++;
        }
    }
    
    
    
}