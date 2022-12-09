using System.Collections;
using Persistence;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;
using Persistence.Services.Factories;

namespace UnitTest.Administration;

public class ConverterTestAdministration
{
    [Test]
    public void CreateUserResponseWithNullArgument()
    {
        Assert.Catch<FactoryNullReference>(() => AdministrationFactory.CreateSubstiuteUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AdministrationFactory.CreateEmployerUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AdministrationFactory.UpdateSubstituteUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AdministrationFactory.UpdateEmployerUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AdministrationFactory.LoginSubstituteUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AdministrationFactory.LoginEmployerUserResponse(null));
    }

    [Test]
    public void CreateUserResponseWithNullArgumentsInParameter()
    {
        Assert.Catch<FactoryNullReference>(() => AdministrationFactory.CreateSubstiuteUserResponse(
            new Substitute
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
            }));
        Assert.Catch<FactoryNullReference>();
    }
    
    [Test]
    public void smth()
    {
        AdministrationFactory.CreateSubstiuteUserResponse(new Substitute());
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertToResponseAsSubstitute))]
    public List<dynamic> CreateUserResponseAsSubstitute(Substitute substitute)
    {
        CreateUserResponse response = AdministrationFactory.CreateSubstiuteUserResponse(substitute);
        
        return new List<dynamic> {
           response.User.Id,
           response.User.UserData.Name,
           response.User.UserData.PasswordHash,
           response.User.UserData.Email,
           response.User.UserData.Sub.Address,
           response.User.UserData.Sub.Age,
           response.User.UserData.Sub.Bio
        };
    }

    public class DataClass
    {
        public static IEnumerable ConvertToResponseAsSubstitute
        {
            get
            {
                yield return new TestCaseData(new Substitute
                {
                    Id = 1,
                    Name = "Test",
                    PasswordHash = "123",
                    Email = "Testmail@test.com",
                    Address = "Testroad 1",
                    Age = 19,
                    Bio = "I am test man, pick me!"
                }).Returns(
                    new List<dynamic>{1,"Test","123","Testmail@test.com","Testroad 1", 19, "I am test man, pick me!"});
                
                yield return new TestCaseData(new Substitute
                {
                    Id = Int32.MinValue,
                    Name = String.Empty,
                    PasswordHash = String.Empty,
                    Email = String.Empty,
                    Address = String.Empty,
                    Age = Int32.MinValue,
                    Bio = String.Empty
                }).Returns(
                    new List<dynamic>{Int32.MinValue,String.Empty,String.Empty,String.Empty,String.Empty, Int32.MinValue, String.Empty});
                
                yield return new TestCaseData(new Substitute
                {
                    Id = 3,
                    PasswordHash = "1 2 3 4",
                    Email = "MailTest@mail.com",
                }).Returns(
                    new List<dynamic>{2,"1 2 3 4","MailTest@mail.com"});
                
                //TODO: hvordan tester man max values for string?



                //Substitute, False, Enum: Sub
                //Employer, False, Enum: Emp

                //Boundaries

                //Skal stadig bare transfer dem ??
                //Substitute, True, Enum : Emp
                //Employer, True, Enum: Sub
            }
        }

        public static IEnumerable ConvertToResponseAsEmployer
        {
            get
            {
                yield break;
            }
        }
    }
}
