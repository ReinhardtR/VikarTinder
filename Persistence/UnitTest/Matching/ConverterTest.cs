using System.Collections;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Persistence;
using Persistence.Converter;
using Persistence.Converter.Interfaces;
using Persistence.Dto;
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
    
    //ConvertSublist & ConvertGigList
    
    [Test, Description("Conversion of null SubList or null GigList should catch")]
    public void GigAndSubListNull()
    {
        Assert.Catch<ConverterNullReference>(() => _converter.ConvertGigList(null));
        Assert.Catch<ConverterNullReference>(() => _converter.ConvertSubList(null));
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.GigListConversion)), Description(
         "Testing boundaries for gigList conversion")]
    public int BoundaryBehaviors(List<Gig> gigs)
    {
        return _converter.ConvertGigList(gigs).Gigs.Count;
    }
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.SubListConversion)), Description(
         "Testing boundaries for gigList conversion")]
    public int BoundaryBehaviors(List<Substitute> substitutes)
    {
        return _converter.ConvertSubList(substitutes).Substitutes.Count;
    }
    
    
    //ConvertToValidation
    
    [Test, Description("Null argument")]
    public void NullTestValidationConversion()
    {
        Assert.Catch<ConverterNullReference>(() =>_converter.ConvertToValidation(null));
    }
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConversionToValidation)), Description(
         "Testing for null and boundray")]
    public MatchValidation ValidationConversion(IdsForMatchDto dto)
    {

        return _converter.ConvertToValidation(dto);
    }
    
    
    //CreateToBeMatchedDto
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.IdToTobematchedDtoConversion)),Description(
         "Testing boundaries for conversion of ids to dto")]
    public List<dynamic> IdToDtoConversion(int currentUser, int matchUser, bool wantsToMatch)
    {
        ToBeMatchedDto dto = _converter.CreateToBeMatchedDto(currentUser, matchUser, wantsToMatch);

        return new List<dynamic>() { dto.UserId, dto.MatchId ,dto.WantsToMatch};
    }
    
    

}

public class DataClass
{
    public static IEnumerable GigListConversion
    {
        get
        {
            DataClass dataClass = new DataClass();
            
            yield return new TestCaseData(new List<Gig>()).
                Returns(0);
            yield return new TestCaseData(dataClass.CreateGigList(1))
                .Returns(1);
            yield return new TestCaseData(dataClass.CreateGigList(100))
                .Returns(100);
        }
        
    }

    public static IEnumerable SubListConversion
    {
        get
        {
            DataClass dataClass = new DataClass();
            
            yield return new TestCaseData(new List<Substitute>()).
                Returns(0);
            yield return new TestCaseData(dataClass.CreateSubList(1))
                .Returns(1);
            yield return new TestCaseData(dataClass.CreateSubList(100))
                .Returns(100);
        }
    }

    public static IEnumerable ConversionToValidation
    {
        get
        {
            yield return new TestCaseData(new IdsForMatchDto())
                .Returns(new MatchValidation());
            yield return new TestCaseData(new IdsForMatchDto
            {
                EmployerId = 1,
                GigId = 1,
                SustituteId = 1,
                WasAMatch = true
            }).Returns(new MatchValidation
            {
                EmployerId = 1,
                GigId = 1,
                SubstituteId = 1,
                IsMatched = true
            });
            yield return new TestCaseData(new IdsForMatchDto
            {
                EmployerId = Int32.MinValue,
                GigId = Int32.MinValue,
                SustituteId = Int32.MinValue,
            }).Returns(new MatchValidation
            {
                EmployerId = Int32.MinValue,
                GigId = Int32.MinValue,
                SubstituteId = Int32.MinValue
            });
            yield return new TestCaseData(new IdsForMatchDto
            {
                EmployerId = Int32.MaxValue,
                GigId = Int32.MaxValue,
                SustituteId = Int32.MaxValue,
                WasAMatch = true
            }).Returns(new MatchValidation
            {
                EmployerId = Int32.MaxValue,
                GigId = Int32.MaxValue,
                SubstituteId = Int32.MaxValue,
                IsMatched = true
            });
        }
        
    }

    public static IEnumerable IdToTobematchedDtoConversion
    {
        get
        {
            yield return new TestCaseData(1, 1, true)
                .Returns(new List<dynamic>{1,1,true});
            yield return new TestCaseData(Int32.MinValue, Int32.MinValue, false)
                .Returns(new List<dynamic> { Int32.MinValue, Int32.MinValue, false });
            yield return new TestCaseData(Int32.MaxValue, Int32.MaxValue, true)
                .Returns(new List<dynamic> { Int32.MaxValue, Int32.MaxValue, true });
        }
    }

    public List<Gig> CreateGigList(int amount)
    {
        List<Gig> gigs = new List<Gig>();
        for (int i = 0; i < amount; i++)
        {
            gigs.Add(new Gig(null)
            {
                Id = i
            });
        }

        return gigs;
    }
    public List<Substitute> CreateSubList(int amount)
    {
        List<Substitute> substitutes = new List<Substitute>();
        for (int i = 0; i < amount; i++)
        {
            substitutes.Add(new Substitute
            {
                Id = i
            });
        }

        return substitutes;
    }
    

}