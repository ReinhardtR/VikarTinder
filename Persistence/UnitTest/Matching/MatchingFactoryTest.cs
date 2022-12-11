using System.Collections;
using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;
using Persistence;
using Persistence.Converter;
using Persistence.Dto;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace UnitTest.Matching;

[TestFixture]
public class MatchingFactoryTest
{
    // ConvertSublist & ConvertGigList
    
    [Test, Description("Giving null as argument should throw")]
    public void NullArguments()
    {
        Assert.Catch<FactoryNullReference>(() => MatchFactory.ConvertGigList(null));
        Assert.Catch<FactoryNullReference>(() => MatchFactory.ConvertSubList(null));
        Assert.Catch<FactoryNullReference>(() => MatchFactory.ConvertToValidation(null));
    }

    [Test, Description("Giving empty object as argument should throw")]
    public void EmptyObject()
    {
        Assert.Catch<FactoryNullReference>(() => MatchFactory.ConvertToValidation(new IdsForMatchDto()));
    }

    [Test, Description("Arguments will some null parameters should throw")]
    public void NullParametersInArguments()
    {
        Assert.Catch<FactoryNullReference>(() => MatchFactory.ConvertGigList(
            new List<Gig>
            {
                new Gig
                {
                    Id = 1
                },
                new Gig
                {
                },
                new Gig
                {
                    Id = 3
                }
            }));
        Assert.Catch<FactoryNullReference>(() => MatchFactory.ConvertSubList(
            new List<Substitute>
            {
                new Substitute
                {
                    Id = 1
                },
                new Substitute
                {

                },
                new Substitute
                {
                    Id = 3
                }
            }));
        Assert.Catch<FactoryNullReference>(() => MatchFactory.ConvertToValidation(
            new IdsForMatchDto
            {
                EmployerId = 1,
                GigId = 2,
            }));
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.GigListConversion)), Description(
         "Testing boundaries for gigList conversion")]
    public List<int> BoundaryBehaviors(List<Gig> gigs)
    {
        GigsForMatchingResponse responseTest = MatchFactory.ConvertGigList(gigs);

        List<int> idsToReturn = new List<int>();
        for (int i = 0; i < responseTest.Gigs.Count; i++)
        {
            idsToReturn.Add(responseTest.Gigs[i].Id);
        }

        return idsToReturn;
    }
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.SubListConversion)), Description(
         "Testing boundaries for gigList conversion")]
    public List<int> BoundaryBehaviors(List<Substitute> substitutes)
    {
        SubstitutesForMatchingResponse responseTest = MatchFactory.ConvertSubList(substitutes);

        List<int> idsToReturn = new List<int>();
        for (int i = 0; i < responseTest.Substitutes.Count; i++)
        {
            idsToReturn.Add(responseTest.Substitutes[i].Id);
        }

        return idsToReturn;
    }
    
    // ConvertToValidation
    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConversionToValidation)), Description(
         "Testing for null and boundray")]
    public MatchValidationResponse ValidationConversion(IdsForMatchDto dto)
    {
        return MatchFactory.ConvertToValidation(dto);
    }
    
    
    // CreateToBeMatchedDto
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.IdToToBeMatchedDtoConversion)),Description(
         "Testing boundaries for conversion of ids to dto")]
    public List<dynamic> IdToDtoConversion(int currentUser, int matchUser, bool wantsToMatch)
    {
        ToBeMatchedDto dto = MatchFactory.CreateToBeMatchedDto(currentUser, matchUser, wantsToMatch);

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
                Returns(IdsOnList(0));
            yield return new TestCaseData(CreateGigList(1))
                .Returns(IdsOnList(1));
            yield return new TestCaseData(CreateGigList(100))
                .Returns(IdsOnList(100));
        }
    }

    public static IEnumerable SubListConversion
    {
        get
        {
            yield return new TestCaseData(new List<Substitute>()).
                Returns(IdsOnList(0));
            yield return new TestCaseData(CreateSubList(1))
                .Returns(IdsOnList(1));
            yield return new TestCaseData(CreateSubList(100))
                .Returns(IdsOnList(100));
        }
    }

    public static IEnumerable ConversionToValidation
    {
        get
        {
            yield return new TestCaseData(new IdsForMatchDto
            {
                EmployerId = 1,
                GigId = 1,
                SubstituteId = 1,
                WasAMatch = true
            }).Returns(new MatchValidationResponse
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
                SubstituteId = int.MinValue,
            }).Returns(new MatchValidationResponse
            {
                EmployerId = int.MinValue,
                GigId = int.MinValue,
                SubstituteId = int.MinValue
            });
            yield return new TestCaseData(new IdsForMatchDto
            {
                EmployerId = int.MaxValue,
                GigId = int.MaxValue,
                SubstituteId = int.MaxValue,
                WasAMatch = true
            }).Returns(new MatchValidationResponse
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
                .Returns(new List<dynamic>{1, 1, true});
            yield return new TestCaseData(int.MinValue, int.MinValue, false)
                .Returns(new List<dynamic> { int.MinValue, int.MinValue, false });
            yield return new TestCaseData(int.MaxValue, int.MaxValue, true)
                .Returns(new List<dynamic> { int.MaxValue, int.MaxValue, true });
        }
    }

    private static List<Gig> CreateGigList(int amount)
    {
        List<Gig> gigs = new List<Gig>();
        for (int i = 1; i <= amount; i++)
        {
            gigs.Add(new Gig()
            {
                Id = i
            });
        }

        return gigs;
    }
    
    private static List<Substitute> CreateSubList(int amount)
    {
        List<Substitute> substitutes = new List<Substitute>();
        for (int i = 1; i <= amount; i++)
        {
            substitutes.Add(new Substitute
            {
                Id = i
            });
        }

        return substitutes;
    }

    private static List<int> IdsOnList(int amount)
    {
        List<int> expectedIds = new List<int>();
        
        for (int i = 1; i <= amount; i++)
        {
            expectedIds.Add(i);
        }

        return expectedIds;
    }
}