using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.DAOs;
using Persistence.DAOs.Interfaces;
using Persistence.Models;

namespace UnitTest;

[TestFixture]
public class EfcMatchTesting
{
    private DatabaseContext _databaseContext;
    private IMatchDao _dao;
    
    [SetUp]
    public void AddingSubstitute()
    {
        _dao = new MatchDao(_databaseContext);
    }

    [Test]
    public void addSubstitute()
    {
        _databaseContext.ChangeTracker.Clear();
        _databaseContext.Substitutes.Add(new Substitute());
        _databaseContext.SaveChanges();
    }

}