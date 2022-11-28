using Persistence.Converter.Interfaces;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;

namespace Persistence.Converter;

public class MatchConverter : IMatchConverter
{
    public MatchValidation EmployerConverter(Employer employer, int userId)
    {
        if (employer == null)
            throw new ConverterNullReference("Substitute");
        
        MatchValidation val = CreateMatchValidation(employer.Id);
        
        if (employer.Substitutes == null)
            throw new ConverterNullReference("Gigs");
        
        foreach (Substitute subs in employer.Substitutes)
        {
            if (subs.Id == userId)
            {
                val.IsMatched = true;
            }
        }

        return val;
    }

    public MatchValidation SubstituteConverter(Substitute substitute, int userId)
    {
        if (substitute == null)
            throw new ConverterNullReference("Substitute");
        
        
        MatchValidation val = CreateMatchValidation(substitute.Id);
        
        
        if (substitute.Positions == null)
            throw new ConverterNullReference("Gigs");

            foreach (Gig gig in substitute.Positions )
            {
                if (gig.Employer.Id == userId)
                {
                    val.IsMatched = true;
                }
            }

            return val;
    }

    
    
    public MatchingSubstitutes ConvertSubList(List<Substitute> substitutes)
    {
        if (substitutes == null || substitutes.Count.Equals(0))
            throw new ConverterNullReference("Substitute list");
        
        MatchingSubstitutes subs = new MatchingSubstitutes();

        foreach (var sub in substitutes)
        {
            subs.Substitutes.Add(new SubstituteToBeMatched
            {
                Id = sub.Id
            });
        }

        return subs;
    }
    
    public MatchingGigs ConvertGigList(List<Gig> gigs)
    {
        MatchingGigs gigsGrpc = new MatchingGigs();

        foreach (var gig in gigs)
        {
            gigsGrpc.Gigs.Add(new GigToBeMatched()
            {
                Id = gig.Id
            });
        }

        return gigsGrpc;
    }
    
    
    

    private MatchValidation CreateMatchValidation(int id)
    {
        MatchValidation val = new MatchValidation
        {
            MatchId = id,
            IsMatched = false
        };

        return val;
    }
}