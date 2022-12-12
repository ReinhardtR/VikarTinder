using System.Collections;
using Persistence;
using Persistence.Dto;
using Persistence.Dto.Administration;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;
using Persistence.Services.Factories;
using static System.String;

namespace UnitTest.Administration;

public class ConverterTestAdministration
{
    
    //Null tests of all methods
    [Test, Description("Testing all methods creating a User Response catches FactoryNullReference with given null as parameter")]
    public void UserResponseWithNullArgument()
    {
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateSubstituteUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateEmployerUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.UpdateSubstituteUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.UpdateEmployerUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateLoginUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateGetUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateDeleteUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeEmployerDomainObject(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeSubstituteDomainObject(null));

    }
    [Test, Description("Testing that creating a UserResponse catches a FactoryNullReference when given an object with no arguments")]
    public void UserResponseWithEmptyArgumentsInParameter()
    {
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateSubstituteUserResponse(
            new Substitute()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateEmployerUserResponse(
            new Employer()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.UpdateSubstituteUserResponse(
            new Substitute()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.UpdateEmployerUserResponse(
            new Employer()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateLoginUserResponse(
            new Substitute()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateLoginUserResponse(
            new Employer()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateGetUserResponse(
            new Substitute()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateGetUserResponse(
            new Employer()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateDeleteUserResponse(
            new DeleteUserDto()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeSubstituteDomainObject(
            new UpdateUserRequest()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeEmployerDomainObject(
            new UpdateUserRequest()));
    }
    
    [Test, Description("Testing that creating a UserResponse catches a FactoryNullReference when given an object with some empty arguments")]
    public void UserResponseWithNullArgumentsInParameter()
    {
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateSubstituteUserResponse(
            new Substitute
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                Bio = "I am test!"
            }));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateEmployerUserResponse(
            new Employer
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                Title = "Testman",
                WorkPlace = "TestPlace"
            }));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.UpdateSubstituteUserResponse(
            new Substitute
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                Bio = "I am test!"
            }));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.UpdateEmployerUserResponse(
            new Employer
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                Title = "Testman",
                WorkPlace = "TestPlace"
            }));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateLoginUserResponse(
            new Substitute
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                Bio = "I am test!"
            }));
        Assert.Catch<FactoryException>(() => AuthServiceFactory.CreateLoginUserResponse(
            new Employer
                {
                    Id = 3,
                    FirstName = "Test",
                    LastName = "Tester",
                    PasswordHash = "1 2 3 4",
                    Email = "MailTest@mail.com",
                    WorkPlace = "TestPlace"
                }));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateGetUserResponse(
            new Substitute
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                Bio = "I am test!"
            }));
        Assert.Catch<FactoryNullReference>((() => AuthServiceFactory.CreateGetUserResponse(
            new Employer
            {
                Id = 3,
                FirstName = "Test",
                LastName = "Tester",
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                WorkPlace = "TestPlace"
            })));
        
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateDeleteUserResponse(
            new DeleteUserDto
            {
                Validation = false,
                Role = DaoRequestType.Substitute
            }));

        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeSubstituteDomainObject(
            new UpdateUserRequest
            {
                User = new UserObject
                {
                    UserData = new UserData
                    {
                        FirstName = "Testman",
                        Email = "TestMail@Test.com",
                        Sub = new SubstituteObject
                        {
                            Address = "Testroad 1"
                        }
                    }
                }
            }));

        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeSubstituteDomainObject(
            new UpdateUserRequest
            {
                User = new UserObject
                {
                    Id = 1,
                    UserData = new UserData()
                    {
                        FirstName = "Testman",
                        LastName = "Tester",
                        PasswordHash = "123",
                        Email = "TestMail@Test.com",
                        Emp = new EmployerObject
                        {
                            Title = "Test CEO",
                            Workplace = "Testland"
                        }
                    }
                }
            }));
        
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeEmployerDomainObject(
            new UpdateUserRequest
            {
                User = new UserObject
                {
                    UserData = new UserData
                    {
                        FirstName = "Testman",
                        Email = "TestMail@Test.com",
                        Emp = new EmployerObject
                        {
                            Title = "Test CEO"
                        }
                    }
                }
            }));

        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeEmployerDomainObject(
            new UpdateUserRequest
            {
                User = new UserObject
                {
                    Id = 1,
                    UserData = new UserData()
                    {
                        FirstName = "Testman",
                        LastName = "Tester",
                        PasswordHash = "123",
                        Email = "TestMail@Test.com",
                        Sub = new SubstituteObject
                        {
                            Address = "Testroad 1",
                            Age = 18,
                            Bio = "I love testing"
                        }
                    }
                }
            }));
    }


    //Success tests of conversion to UserResponse from DomainObject
    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertToResponseAsSubstitute))
        ,Description("Testing creation of User Response as substitute - boundary testing ")]
    public List<dynamic> CreateUserResponseAsSubstitute(Substitute substitute)
    {
        CreateUserResponse response = AuthServiceFactory.CreateSubstituteUserResponse(substitute);
        
        return new List<dynamic> {
           response.User.Id,
           response.User.UserData.FirstName,
           response.User.UserData.LastName,
           response.User.UserData.PasswordHash,
           response.User.UserData.Email,
           response.User.UserData.Sub.Address,
           response.User.UserData.Sub.Age,
           response.User.UserData.Sub.Bio
        };
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertToResponseAsEmployer)),
    Description("Testing creation of User Response as employer - boundary testing")]
    public List<dynamic> CreateUserResponseAsEmployer(Employer employer)
    {
        CreateUserResponse response = AuthServiceFactory.CreateEmployerUserResponse(employer);

        return new List<dynamic>
        {
            response.User.Id,
            response.User.UserData.FirstName,
            response.User.UserData.LastName,
            response.User.UserData.PasswordHash,
            response.User.UserData.Email,
            response.User.UserData.Emp.Title,
            response.User.UserData.Emp.Workplace
        };
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertFromUser)),
    Description("Testing for CreateLoginUserResponse method")]
    public List<dynamic> CreateLoginUserResponseFromUserDto(User user)
    {
        LoginUserResponse response = AuthServiceFactory.CreateLoginUserResponse(user);

        var testDataResponse = response.User.UserData.RoleCase == UserData.RoleOneofCase.Sub
            ? new List<dynamic>
            {
                response.User.Id,
                response.User.UserData.FirstName,
                response.User.UserData.LastName,
                response.User.UserData.PasswordHash,
                response.User.UserData.Email,
                response.User.UserData.Sub.Address,
                response.User.UserData.Sub.Age,
                response.User.UserData.Sub.Bio
            }
            : new List<dynamic>
            {
                response.User.Id,
                response.User.UserData.FirstName,
                response.User.UserData.LastName,
                response.User.UserData.PasswordHash,
                response.User.UserData.Email,
                response.User.UserData.Emp.Title,
                response.User.UserData.Emp.Workplace
            };

        return testDataResponse;
    }
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertFromUser)),
     Description("Testing for CreateGetUserResponse method")]
    public List<dynamic> CreateGetUserResponseFromUserDto(User dto)
    {
        GetUserResponse response = AuthServiceFactory.CreateGetUserResponse(dto);

        var testDataResponse = response.User.UserData.RoleCase == UserData.RoleOneofCase.Sub
            ? new List<dynamic>
            {
                response.User.Id,
                response.User.UserData.FirstName,
                response.User.UserData.LastName,
                response.User.UserData.PasswordHash,
                response.User.UserData.Email,
                response.User.UserData.Sub.Address,
                response.User.UserData.Sub.Age,
                response.User.UserData.Sub.Bio
            }
            : new List<dynamic>
            {
                response.User.Id,
                response.User.UserData.FirstName,
                response.User.UserData.LastName,
                response.User.UserData.PasswordHash,
                response.User.UserData.Email,
                response.User.UserData.Emp.Title,
                response.User.UserData.Emp.Workplace
            };

        return testDataResponse;
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertFromDeleteUserDto)),
    Description("Testing success scenarios of creating a DeleteUserResponse")]
    public List<dynamic> CreateDeleteUserResponse(DeleteUserDto dto)
    {
        DeleteUserResponse response = AuthServiceFactory.CreateDeleteUserResponse(dto);

        return new List<dynamic>
        {
            response.Validation,
            response.User.Id,
            response.User.Role
        };
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertToSubstituteDomainObject))
    ,Description("Testing conversion from UpdateUserRequest to Substitute object success scenarios")]
    public List<dynamic> MakeSubstituteDomainObject(UpdateUserRequest updateUserRequest)
    {
        Substitute substitute = AuthServiceFactory.MakeSubstituteDomainObject(updateUserRequest);

        return new List<dynamic>
        {
            substitute.Id,
            substitute.FirstName,
            substitute.LastName,
            substitute.PasswordHash,
            substitute.Email,
            substitute.Address,
            substitute.Age,
            substitute.Bio
        };
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertToEmployerDomainObject))
    ,Description("Testing conversion from UpdateUserRequest to Employer object success scenarios")]
    public List<dynamic> MakeEmployerDomainObject(UpdateUserRequest updateUserRequest)
    {
        Employer employer = AuthServiceFactory.MakeEmployerDomainObject(updateUserRequest);

        return new List<dynamic>
        {
            employer.Id,
            employer.FirstName,
            employer.LastName,
            employer.PasswordHash,
            employer.Email,
            employer.Title,
            employer.WorkPlace
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
                    FirstName = "Test",
                    LastName = "Tester",
                    PasswordHash = "123",
                    Email = "Testmail@test.com",
                    Address = "Testroad 1",
                    Age = 19,
                    Bio = "I am test man, pick me!"
                }).Returns(
                    new List<dynamic>{1,"Test","Tester","123","Testmail@test.com","Testroad 1", 19, "I am test man, pick me!"});
                
                yield return new TestCaseData(new Substitute
                {
                    Id = Int32.MinValue,
                    FirstName = Empty,
                    LastName = Empty,
                    PasswordHash = Empty,
                    Email = Empty,
                    Address = Empty,
                    Age = Int32.MinValue,
                    Bio = Empty
                }).Returns(
                    new List<dynamic>{Int32.MinValue,Empty,Empty,Empty,Empty,Empty, Int32.MinValue, Empty});
                
            }
        }

        public static IEnumerable ConvertToResponseAsEmployer
        {
            get
            {
                yield return new TestCaseData(new Employer
                {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Tester",
                    PasswordHash = "123",
                    Email = "Testmail@test.com",
                    Title = "Test CEO",
                    WorkPlace = "Testland"
                }).Returns(
                    new List<dynamic>{1,"Test","Tester","123","Testmail@test.com","Test CEO","Testland"});
                
                yield return new TestCaseData(new Employer()
                {
                    Id = Int32.MinValue,
                    FirstName = Empty,
                    LastName = Empty,
                    PasswordHash = Empty,
                    Email = Empty,
                    Title = Empty,
                    WorkPlace = Empty
                }).Returns(
                    new List<dynamic>{Int32.MinValue,Empty,Empty,Empty,Empty,Empty, Empty});
            }
        }

        public static IEnumerable ConvertFromUser
        {
            get
            {
                yield return new TestCaseData(new Substitute
                {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Tester",
                    PasswordHash = "123",
                    Email = "Testmail@test.com",
                    Address = "Testroad 1",
                    Age = 19,
                    Bio = "I am test man, pick me!"
                }).Returns(
                    new List<dynamic> { 1, "Test", "Tester", "123", "Testmail@test.com", "Testroad 1", 19, "I am test man, pick me!" });
                
                yield return new TestCaseData(new Employer
                {
                    
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Tester",
                    PasswordHash = "123",
                    Email = "Testmail@test.com",
                    Title = "Test CEO",
                    WorkPlace = "Testland"
                }).Returns(new List<dynamic>{1,"Test","Tester","123","Testmail@test.com","Test CEO","Testland"});
                
                yield return new TestCaseData(new Substitute
                {
                    Id = Int32.MinValue,
                    FirstName = Empty,
                    LastName = Empty,
                    PasswordHash = Empty,
                    Email = Empty,
                    Address = Empty,
                    Age = Int32.MinValue,
                    Bio = Empty
                }).Returns(
                    new List<dynamic>{Int32.MinValue,Empty,Empty,Empty,Empty,Empty, Int32.MinValue, Empty});
                
                yield return new TestCaseData(new Employer
                {
                    Id = Int32.MinValue,
                    FirstName = Empty,
                    LastName = Empty,
                    PasswordHash = Empty,
                    Email = Empty,
                    Title = Empty,
                    WorkPlace = Empty
                }).Returns(
                    new List<dynamic>{Int32.MinValue,Empty,Empty,Empty,Empty,Empty, Empty});
            }
        }

        public static IEnumerable ConvertFromDeleteUserDto
        {
            get
            {
                yield return new TestCaseData(new DeleteUserDto
                {
                    Validation = true,
                    Id = 1,
                    Role = DaoRequestType.Substitute
                }).Returns(new List<dynamic> {true,1, GetUserParams.Types.Role.Substitute});

                yield return new TestCaseData(new DeleteUserDto
                {
                    Validation = true,
                    Id = 1,
                    Role = DaoRequestType.Employer
                }).Returns(new List<dynamic> { true, 1, GetUserParams.Types.Role.Employer });
                
                yield return new TestCaseData(new DeleteUserDto
                {
                    Validation = false,
                    Id = Int32.MinValue,
                    Role = DaoRequestType.Substitute
                }).Returns(new List<dynamic> { false, Int32.MinValue,GetUserParams.Types.Role.Substitute });
                
                yield return new TestCaseData(new DeleteUserDto
                {
                    Validation = false,
                    Id = Int32.MinValue,
                    Role = DaoRequestType.Employer
                }).Returns(new List<dynamic> { false, Int32.MinValue, GetUserParams.Types.Role.Employer });
            }
        }

        public static IEnumerable ConvertToSubstituteDomainObject
        {
            get
            {
                yield return new TestCaseData(new UpdateUserRequest
                {
                    User = new UserObject
                    {
                        Id = 1,
                        UserData = new UserData
                        {
                            FirstName = "Testman",
                            LastName = "Tester",
                            PasswordHash = "123",
                            Email = "Testmail@Test.com",
                            Sub = new SubstituteObject
                            {
                                Address = "Testroad 1",
                                Age = 18,
                                Bio = "I am testman!!!"
                            }
                        }
                    }
                }).Returns(new List<dynamic>
                    { 1, "Testman", "Tester", "123", "Testmail@Test.com", "Testroad 1", 18, "I am testman!!!" });
                
                
                yield return new TestCaseData(new UpdateUserRequest
                {
                    User = new UserObject
                    {
                        Id = Int32.MinValue,
                        UserData = new UserData
                        {
                            FirstName = Empty,
                            LastName = Empty,
                            PasswordHash = Empty,
                            Email = Empty,
                            Sub = new SubstituteObject
                            {
                                Address = Empty,
                                Age = Int32.MinValue,
                                Bio = Empty
                            }
                        }
                    }
                }).Returns(new List<dynamic>
                    { Int32.MinValue, Empty, Empty, Empty, Empty, Empty, Int32.MinValue, Empty });
            }
        }

        public static IEnumerable ConvertToEmployerDomainObject
        {
            get
            {
                yield return new TestCaseData(new UpdateUserRequest
                {
                    User = new UserObject
                    {
                        Id = 1,
                        UserData = new UserData
                        {
                            FirstName = "Testman",
                            LastName = "Tester",
                            PasswordHash = "123",
                            Email = "Testmail@Test.com",
                            Emp = new EmployerObject
                            {
                                Title = "Test CEO",
                                Workplace = "Testland"
                            }
                        }
                    }
                }).Returns(new List<dynamic>
                    { 1, "Testman", "Tester", "123", "Testmail@Test.com","Test CEO","Testland"});
                
                yield return new TestCaseData(new UpdateUserRequest
                {
                    User = new UserObject
                    {
                        Id = Int32.MinValue,
                        UserData = new UserData
                        {
                            FirstName = Empty,
                            LastName = Empty,
                            PasswordHash = Empty,
                            Email = Empty,
                            Emp = new EmployerObject
                            {
                                Title = Empty,
                                Workplace = Empty
                            }
                        }
                    }
                }).Returns(new List<dynamic>
                    { Int32.MinValue, Empty, Empty, Empty, Empty,Empty,Empty});
            }
        }
    }
}
