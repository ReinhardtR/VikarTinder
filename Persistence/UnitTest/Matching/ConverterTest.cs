using System.Collections;
using System.Collections.Generic;
using Persistence;
using Persistence.Converter;
using Persistence.Dto;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace UnitTest;

[TestFixture]
public class ConverterTest
{
    // ConvertSublist & ConvertGigList
    
    [Test, Description("Conversion of null SubList or null GigList should catch")]
    public void GigAndSubListNull()
    {
        Assert.Catch<ConverterNullReference>(() => MatchConverter.ConvertGigList(null));
        Assert.Catch<ConverterNullReference>(() => MatchConverter.ConvertSubList(null));
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.GigListConversion)), Description(
         "Testing boundaries for gigList conversion")]
    public int BoundaryBehaviors(List<Gig> gigs)
    {
        return MatchConverter.ConvertGigList(gigs).Gigs.Count;
    }
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.SubListConversion)), Description(
         "Testing boundaries for gigList conversion")]
    public int BoundaryBehaviors(List<Substitute> substitutes)
    {
        return MatchConverter.ConvertSubList(substitutes).Substitutes.Count;
    }
    
    
    // ConvertToValidation
    
    [Test, Description("Null argument")]
    public void NullTestValidationConversion()
    {
        Assert.Catch<ConverterNullReference>(() =>MatchConverter.ConvertToValidation(null));
    }
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConversionToValidation)), Description(
         "Testing for null and boundray")]
    public MatchValidation ValidationConversion(IdsForMatchDto dto)
    {

        return MatchConverter.ConvertToValidation(dto);
    }
    
    
    // CreateToBeMatchedDto
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.IdToToBeMatchedDtoConversion)),Description(
         "Testing boundaries for conversion of ids to dto")]
    public List<dynamic> IdToDtoConversion(int currentUser, int matchUser, bool wantsToMatch)
    {
        ToBeMatchedDto dto = MatchConverter.CreateToBeMatchedDto(currentUser, matchUser, wantsToMatch);

        return new List<dynamic>() { dto.UserId, dto.MatchId ,dto.WantsToMatch};
    }
}

public class DataClass
{
    public static IEnumerable GigListConversion
    {
        get
        {
            yield return new TestCaseData(new List<Gig>()).
                Returns(0);
            yield return new TestCaseData(CreateGigList(1))
                .Returns(1);
            yield return new TestCaseData(CreateGigList(100))
                .Returns(100);
        }
    }

    public static IEnumerable SubListConversion
    {
        get
        {
            yield return new TestCaseData(new List<Substitute>()).
                Returns(0);
            yield return new TestCaseData(CreateSubList(1))
                .Returns(1);
            yield return new TestCaseData(CreateSubList(100))
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
                EmployerId = int.MinValue,
                GigId = int.MinValue,
                SustituteId = int.MinValue,
            }).Returns(new MatchValidation
            {
                EmployerId = int.MinValue,
                GigId = int.MinValue,
                SubstituteId = int.MinValue
            });
            yield return new TestCaseData(new IdsForMatchDto
            {
                EmployerId = int.MaxValue,
                GigId = int.MaxValue,
                SustituteId = int.MaxValue,
                WasAMatch = true
            }).Returns(new MatchValidation
            {
                EmployerId = int.MaxValue,
                GigId = int.MaxValue,
                SubstituteId = int.MaxValue,
                IsMatched = true
            });
        }
        
    }

    public static IEnumerable IdToToBeMatchedDtoConversion
    {
        get
        {
            yield return new TestCaseData(1, 1, true)
                .Returns(new List<dynamic>{1,1,true});
            yield return new TestCaseData(int.MinValue, int.MinValue, false)
                .Returns(new List<dynamic> { int.MinValue, int.MinValue, false });
            yield return new TestCaseData(int.MaxValue, int.MaxValue, true)
                .Returns(new List<dynamic> { int.MaxValue, int.MaxValue, true });
        }
    }

    private static List<Gig> CreateGigList(int amount)
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
    
    private static List<Substitute> CreateSubList(int amount)
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