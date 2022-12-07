using Google.Protobuf.WellKnownTypes;
using Persistence;
using Persistence.Models;
using Persistence.Services;

namespace UnitTest.Chat;

[TestFixture]
public class JobConfirmationFactoryTest
{
    private static JobConfirmation[] JobConfirmationData=
    {
        
        new JobConfirmation
            {
                Id = 1,
                ChatId = 1,
                SubstituteId = 1,
                EmployerId = 2,
                Status = JobConfirmationStatus.Accepted,
                CreatedAt = DateTime.Now,
                IsTaken = true
            },
        new   JobConfirmation
         {
             Id = -1,
             ChatId = -1,
             SubstituteId = -1,
             EmployerId = -2,
             Status = JobConfirmationStatus.Declined,
             CreatedAt = DateTime.MinValue,
             IsTaken = false   
         },
        new JobConfirmation
            {
                Id = 0,
                ChatId = 0,
                SubstituteId = 0,
                EmployerId = 0,
                Status = JobConfirmationStatus.Unanswered,
                CreatedAt = DateTime.MaxValue,
                IsTaken = false
            }
    };

    [Test, TestCaseSource(nameof(JobConfirmationData))]
    public void ToCreateJobConfirmationResponseTest(JobConfirmation jobConfirmation)
    {
        var result = JobConfirmationFactory.ToCreateJobConfirmationResponse(jobConfirmation);
        
        Assert.AreEqual(jobConfirmation.Id, result.JobConfirmation.Id);
        Assert.AreEqual(jobConfirmation.ChatId, result.JobConfirmation.ChatId);
        Assert.AreEqual(jobConfirmation.SubstituteId, result.JobConfirmation.SubstituteId);
        Assert.AreEqual(jobConfirmation.EmployerId, result.JobConfirmation.EmployerId);
        Assert.AreEqual(jobConfirmation.Status, result.JobConfirmation.Status);
        Assert.That(result.JobConfirmation.CreatedAt, Is.EqualTo(jobConfirmation.CreatedAt.ToTimestamp()));
    }
    
    [Test, TestCaseSource(nameof(JobConfirmationData))]
    public void ToJobConfirmationAsnwerResponseTest(JobConfirmation jobConfirmation)
    {
        var result = JobConfirmationFactory.ToJobConfirmationAnswerResponse(jobConfirmation);
        
        Assert.AreEqual(jobConfirmation.Id, result.JobConfirmation.Id);
        Assert.AreEqual(jobConfirmation.ChatId, result.JobConfirmation.ChatId);
        Assert.AreEqual(jobConfirmation.SubstituteId, result.JobConfirmation.SubstituteId);
        Assert.AreEqual(jobConfirmation.EmployerId, result.JobConfirmation.EmployerId);
        Assert.AreEqual(jobConfirmation.Status, result.JobConfirmation.Status);
        Assert.That(result.JobConfirmation.CreatedAt, Is.EqualTo(jobConfirmation.CreatedAt.ToTimestamp()));
    }
    
    [Test, TestCaseSource(nameof(JobConfirmationData))]
    public void toGetJobConfirmationResponseTest(JobConfirmation jobConfirmation)
    {
        var result = JobConfirmationFactory.ToGetJobConfirmationResponse(jobConfirmation);
        
        Assert.AreEqual(jobConfirmation.Id, result.JobConfirmation.Id);
        Assert.AreEqual(jobConfirmation.ChatId, result.JobConfirmation.ChatId);
        Assert.AreEqual(jobConfirmation.SubstituteId, result.JobConfirmation.SubstituteId);
        Assert.AreEqual(jobConfirmation.EmployerId, result.JobConfirmation.EmployerId);
        Assert.AreEqual(jobConfirmation.Status, result.JobConfirmation.Status);
        Assert.That(result.JobConfirmation.CreatedAt, Is.EqualTo(jobConfirmation.CreatedAt.ToTimestamp()));
    }
    
        
    
    
}