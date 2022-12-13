using System.Collections;
using System.Text;
using Google.Protobuf.WellKnownTypes;
using Persistence;
using Persistence.Dto;
using Persistence.Dto.Auth;
using Persistence.Exceptions.ConverterExceptions;
using Persistence.Models;
using Persistence.Services.Factories;

namespace UnitTest.Administration;

public class ConverterTestAdministration
{
    
    //Null tests of all methods
    [Test, Description("Testing all factory Methods catches FactoryNullReference with given null as parameter")]
    public void UserResponseWithNullArgument()
    {
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUpdateUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateLoginUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateGetUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateDeleteUserResponse(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeEmployerDomainObject(null));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeSubstituteDomainObject(null));

    }
    [Test, Description("Testing that creating a UserResponse catches a FactoryNullReference when given an object with no arguments")]
    public void UserResponseWithEmptyArgumentsInParameter()
    {
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUserResponse(
            new Substitute()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUserResponse(
            new Employer()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUpdateUserResponse(
            new Substitute()));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUpdateUserResponse(
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
    
    [Test, Description("Testing that all factory methods catches a FactoryNullReference when given an object with some empty fields")]
    public void UserResponseWithNullArgumentsInParameter()
    {
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUserResponse(
            new Substitute
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                Bio = "I am test!"
            }));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUserResponse(
            new Employer
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                Title = "Testman",
                WorkPlace = "TestPlace"
            }));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUpdateUserResponse(
            new Substitute
            {
                Id = 3,
                PasswordHash = "1 2 3 4",
                Email = "MailTest@mail.com",
                Bio = "I am test!"
            }));
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateUpdateUserResponse(
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
    }

    [Test, Description("Testing for giving an empty id from request will throw as planned")]
    public void DomainObjectAndDeleteUserResponse()
    {
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.CreateDeleteUserResponse(
            new DeleteUserDto
            {
                Validation = false,
                Role = DaoRequestType.Substitute
            }));

        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeSubstituteDomainObject(
            new UpdateUserRequest
            {
                User = new UserInfo
                {
                    FirstName = "Testman",
                    Sub = new SubstituteObject
                        {
                            Address = "Testroad 1"
                        }
                }
            }));

        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeSubstituteDomainObject(
            new UpdateUserRequest
            {
                User = new UserInfo
                {
                    FirstName = "Testman",
                    LastName = "Tester",
                    Emp = new EmployerObject
                    {
                        Title = "Test CEO",
                        Workplace = "Testland" 
                    }
                }
            }));
        
        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeEmployerDomainObject(
            new UpdateUserRequest
            {
                User = new UserInfo
                {
                    FirstName = "Testman",
                    LastName = "SDFSFD",
                    Emp = new EmployerObject
                    {
                        Title = "Test CEO"
                    }
                }
            }));

        Assert.Catch<FactoryNullReference>(() => AuthServiceFactory.MakeEmployerDomainObject(
            new UpdateUserRequest
            {
                User = new UserInfo
                {
                    FirstName = "Testman",
                    LastName = "Tester",
                    Sub = new SubstituteObject
                    {
                        Address = "Testroad 1",
                        Bio = "I love testing"
                    }
                }
            }));
    }


    //Success tests of conversion to UserResponse from DomainObject
    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertFromUserFullData)),
     Description("boundary testing - CreateUserResponse")]
    public List<dynamic> CreateUserResponse(User user)
    {
        CreateUserResponse response = AuthServiceFactory.CreateUserResponse(user);

        var testDataResponse = response.User.UserData.RoleCase == UserData.RoleOneofCase.Sub
            ? new List<dynamic>
            {
                response.User.Id,
                response.User.UserData.FirstName,
                response.User.UserData.LastName,
                response.User.UserData.PasswordHash,
                response.User.UserData.Salt,
                response.User.UserData.Email,
                response.User.UserData.Sub.Address,
                response.User.UserData.Sub.BirthDate.ToDateTime(),
                response.User.UserData.Sub.Bio
            }:new List<dynamic>
            {
                response.User.Id,
                response.User.UserData.FirstName,
                response.User.UserData.LastName,
                response.User.UserData.PasswordHash,
                response.User.UserData.Salt,
                response.User.UserData.Email,
                response.User.UserData.Emp.Title,
                response.User.UserData.Emp.Workplace
            };
        return testDataResponse;
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertFromUserFullData)),
    Description("boundary testing - CreateLoginUserResponse")]
    public List<dynamic> CreateLoginUserResponse(User user)
    {
        LoginUserResponse response = AuthServiceFactory.CreateLoginUserResponse(user);

        var testDataResponse = response.User.UserData.RoleCase == UserData.RoleOneofCase.Sub
            ? new List<dynamic>
            {
                response.User.Id,
                response.User.UserData.FirstName,
                response.User.UserData.LastName,
                response.User.UserData.PasswordHash,
                response.User.UserData.Salt,
                response.User.UserData.Email,
                response.User.UserData.Sub.Address,
                response.User.UserData.Sub.BirthDate.ToDateTime(),
                response.User.UserData.Sub.Bio
            }
            : new List<dynamic>
            {
                response.User.Id,
                response.User.UserData.FirstName,
                response.User.UserData.LastName,
                response.User.UserData.PasswordHash,
                response.User.UserData.Salt,
                response.User.UserData.Email,
                response.User.UserData.Emp.Title,
                response.User.UserData.Emp.Workplace
            };

        return testDataResponse;
    }
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertFromUserModifiableData)),
     Description("boundary testing - CreateUpdateUserResponse")]
    public List<dynamic> CreateUpdateUserResponse(User user)
    {
        UpdateUserResponse response = AuthServiceFactory.CreateUpdateUserResponse(user);

        var testDataResponse = response.User.RoleCase == UserInfo.RoleOneofCase.Sub
            ? new List<dynamic>
            {
                response.User.FirstName,
                response.User.LastName,
                response.User.Sub.Address,
                response.User.Sub.BirthDate.ToDateTime(),
                response.User.Sub.Bio
            }
            : new List<dynamic>
            {
                response.User.FirstName,
                response.User.LastName,
                response.User.Emp.Title,
                response.User.Emp.Workplace
            };

        return testDataResponse;
    }
    
    
    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertFromUserModifiableData)),
     Description("boundary testing - CreateGetUserResponse")]
    public List<dynamic> CreateGetUserResponse(User user)
    {
        GetUserResponse response = AuthServiceFactory.CreateGetUserResponse(user);

        var testDataResponse = response.User.RoleCase == UserInfo.RoleOneofCase.Sub
            ? new List<dynamic>
            {
                response.User.FirstName,
                response.User.LastName,
                response.User.Sub.Address,
                response.User.Sub.BirthDate.ToDateTime(),
                response.User.Sub.Bio
            }
            : new List<dynamic>
            {
                response.User.FirstName,
                response.User.LastName,
                response.User.Emp.Title,
                response.User.Emp.Workplace
            };

        return testDataResponse;
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertFromDeleteUserDto)),
    Description("boundary testing - CreateDeleteUserResponse")]
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
    ,Description("boundary testing - CreateSubstituteDomainObject")]
    public List<dynamic> MakeSubstituteDomainObject(UpdateUserRequest updateUserRequest)
    {
        Substitute substitute = AuthServiceFactory.MakeSubstituteDomainObject(updateUserRequest);

        return new List<dynamic>
        {
            substitute.FirstName,
            substitute.LastName,
            substitute.Address,
            substitute.BirthDate,
            substitute.Bio
        };
    }

    [TestCaseSource(typeof(DataClass), nameof(DataClass.ConvertToEmployerDomainObject))
    ,Description("boundary testing - CreateEmployerDomainObject")]
    public List<dynamic> MakeEmployerDomainObject(UpdateUserRequest updateUserRequest)
    {
        Employer employer = AuthServiceFactory.MakeEmployerDomainObject(updateUserRequest);

        return new List<dynamic>
        {
            employer.FirstName,
            employer.LastName,
            employer.Title,
            employer.WorkPlace
        };
    }

    private class DataClass
    {
        private static readonly string _longString = ExpandString("Test",10000);
        public static IEnumerable ConvertFromUserModifiableData
        {
            get
            {
                yield return new TestCaseData(new Substitute
                {
                    FirstName = "Test",
                    LastName = "Tester",
                    Address = "Testroad 1",
                    BirthDate = new DateTime(2000,01,01),
                    Bio = "I am test man, pick me!"
                }).Returns(
                    new List<dynamic>{"Test","Tester","Testroad 1", new DateTime(2000,01,01).ToUniversalTime(), "I am test man, pick me!"});
                
                yield return new TestCaseData(new Employer
                {
                    FirstName = "Test",
                    LastName = "Tester",
                    Title = "Test CEO",
                    WorkPlace = "Testland"
                }).Returns(
                    new List<dynamic>{"Test","Tester","Test CEO","Testland"});
                
                yield return new TestCaseData(new Substitute
                {
                    FirstName = String.Empty,
                    LastName = String.Empty,
                    Address = String.Empty,
                    BirthDate = new DateTime(1,01,01),
                    Bio = String.Empty
                }).Returns(
                    new List<dynamic>{String.Empty,String.Empty,String.Empty, new DateTime(1,01,01).ToUniversalTime(), String.Empty});
                
                yield return new TestCaseData(new Employer()
                {
                    FirstName = String.Empty,
                    LastName = String.Empty,
                    Title = String.Empty,
                    WorkPlace = String.Empty
                }).Returns(
                    new List<dynamic>{String.Empty,String.Empty,String.Empty, String.Empty});
                
                yield return new TestCaseData(new Substitute
                {
                    FirstName = _longString,
                    LastName = _longString,
                    Address = _longString,
                    BirthDate = new DateTime(9999,12,31),
                    Bio = _longString
                }).Returns(
                    new List<dynamic>{_longString,_longString,_longString, new DateTime(9999,12,31).ToUniversalTime(), _longString});
                
                yield return new TestCaseData(new Employer()
                {
                    FirstName = _longString,
                    LastName = _longString,
                    Title = _longString,
                    WorkPlace = _longString
                }).Returns(
                    new List<dynamic>{_longString,_longString,_longString,_longString});
            }
        }

        public static IEnumerable ConvertFromUserFullData
        {
            get
            {
                yield return new TestCaseData(new Substitute
                {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Tester",
                    PasswordHash = "123",
                    Salt = "TestSalt",
                    Email = "Testmail@test.com",
                    Address = "Testroad 1",
                    BirthDate = new DateTime(2000,01,01),
                    Bio = "I am test man, pick me!"
                }).Returns(
                    new List<dynamic> {1,"Test","Tester","123","TestSalt","Testmail@test.com", "Testroad 1", new DateTime(2000,01,01).ToUniversalTime(), "I am test man, pick me!" });
                
                yield return new TestCaseData(new Employer
                {
                    
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Tester",
                    PasswordHash = "123",
                    Salt = "TestSalt",
                    Email = "Testmail@test.com",
                    Title = "Test CEO",
                    WorkPlace = "Testland"
                }).Returns(new List<dynamic>{1,"Test","Tester","123","TestSalt","Testmail@test.com","Test CEO","Testland"});
                
                yield return new TestCaseData(new Substitute
                {
                    Id = Int32.MinValue,
                    FirstName = String.Empty,
                    LastName = String.Empty,
                    PasswordHash = String.Empty,
                    Salt = String.Empty,
                    Email = String.Empty,
                    Address = String.Empty,
                    BirthDate = new DateTime(1,01,01),
                    Bio = String.Empty
                }).Returns(
                    new List<dynamic>{Int32.MinValue,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty, new DateTime(1,01,01).ToUniversalTime(), String.Empty});
                
                yield return new TestCaseData(new Employer
                {
                    Id = Int32.MinValue,
                    FirstName = String.Empty,
                    LastName = String.Empty,
                    PasswordHash = String.Empty,
                    Salt = String.Empty,
                    Email = String.Empty,
                    Title = String.Empty,
                    WorkPlace = String.Empty
                }).Returns(
                    new List<dynamic>{Int32.MinValue,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty,String.Empty, String.Empty});
                
                yield return new TestCaseData(new Substitute
                {
                    Id = Int32.MaxValue,
                    FirstName = _longString,
                    LastName = _longString,
                    PasswordHash = _longString,
                    Salt = _longString,
                    Email = _longString,
                    Address = _longString,
                    BirthDate = new DateTime(9999,12,31),
                    Bio = _longString
                }).Returns(
                    new List<dynamic>{Int32.MaxValue,_longString,_longString,_longString,_longString,_longString,_longString, new DateTime(9999,12,31).ToUniversalTime(), _longString});
                
                yield return new TestCaseData(new Employer
                {
                    Id = Int32.MaxValue,
                    FirstName = _longString,
                    LastName = _longString,
                    PasswordHash = _longString,
                    Salt = _longString,
                    Email = _longString,
                    Title = _longString,
                    WorkPlace = _longString
                }).Returns(
                    new List<dynamic>{Int32.MaxValue,_longString,_longString,_longString,_longString,_longString,_longString, _longString});
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
                
                yield return new TestCaseData(new DeleteUserDto
                {
                    Validation = true,
                    Id = Int32.MaxValue,
                    Role = DaoRequestType.Substitute
                }).Returns(new List<dynamic> { true, Int32.MaxValue, GetUserParams.Types.Role.Substitute });
                
                yield return new TestCaseData(new DeleteUserDto
                {
                    Validation = true,
                    Id = Int32.MaxValue,
                    Role = DaoRequestType.Employer
                }).Returns(new List<dynamic> { true, Int32.MaxValue, GetUserParams.Types.Role.Employer });
            }
        }

        public static IEnumerable ConvertToSubstituteDomainObject
        {
            get
            {
                yield return new TestCaseData(new UpdateUserRequest
                {
                    Id = 1,
                    User = new UserInfo
                    {
                        FirstName = "Testman",
                        LastName = "Tester",
                        Sub = new SubstituteObject
                        {
                            Address = "Testroad 1",
                            BirthDate = Timestamp.FromDateTime(new DateTime(1999,01,01).ToUniversalTime()),
                            Bio = "I am testman!!!"
                        }
                    }
                }).Returns(new List<dynamic>
                    { "Testman", "Tester", "Testroad 1", new DateTime(1999,01,01).ToUniversalTime(), "I am testman!!!" });
                
                
                yield return new TestCaseData(new UpdateUserRequest
                {
                    Id = Int32.MinValue,
                    User = new UserInfo
                    {
                        FirstName = String.Empty,
                        LastName = String.Empty,
                        Sub = new SubstituteObject
                        {
                            Address = String.Empty,
                            BirthDate = Timestamp.FromDateTime(new DateTime(1,01,01).ToUniversalTime()),
                            Bio = String.Empty
                        }
                    }
                }).Returns(new List<dynamic>
                    {String.Empty, String.Empty, String.Empty, new DateTime(1,01,01).ToUniversalTime(), String.Empty });
               
                yield return new TestCaseData(new UpdateUserRequest
                {
                    Id = Int32.MaxValue,
                    User = new UserInfo
                    {
                        FirstName = _longString,
                        LastName = _longString,
                        Sub = new SubstituteObject
                        {
                            Address = _longString,
                            BirthDate = Timestamp.FromDateTime(new DateTime(9999,12,31).ToUniversalTime()),
                            Bio = _longString
                        }
                    }
                }).Returns(new List<dynamic>
                    {_longString, _longString, _longString, new DateTime(9999,12,31).ToUniversalTime(), _longString });
            }
        }

        public static IEnumerable ConvertToEmployerDomainObject
        {
            get
            {
                yield return new TestCaseData(new UpdateUserRequest
                {
                    Id = 1,
                    User = new UserInfo
                    {
                        FirstName = "Testman",
                        LastName = "Tester",
                        Emp = new EmployerObject
                        {
                            Title = "Test CEO",
                            Workplace = "Testland"
                        }
                    }
                }).Returns(new List<dynamic>
                    {"Testman", "Tester","Test CEO","Testland"});
                
                yield return new TestCaseData(new UpdateUserRequest
                {
                    Id = Int32.MinValue,
                    User = new UserInfo
                    {
                        FirstName = String.Empty,
                        LastName = String.Empty,
                        Emp = new EmployerObject
                        {
                            Title = String.Empty,
                            Workplace = String.Empty
                        }
                    }
                }).Returns(new List<dynamic>
                    {String.Empty, String.Empty,String.Empty,String.Empty});
                
                yield return new TestCaseData(new UpdateUserRequest
                {
                    Id = Int32.MaxValue,
                    User = new UserInfo
                    {
                        FirstName = _longString,
                        LastName = _longString,
                        Emp = new EmployerObject
                        {
                                Title = _longString,
                                Workplace = _longString
                        }
                    }
                }).Returns(new List<dynamic>
                    {_longString, _longString, _longString, _longString });
            }
        }
        
        private static string ExpandString(string str, int length)
        {
            if (length <= str.Length) return str.Substring(0, length);
            var result = new StringBuilder(str);
            for (var i = str.Length; i < length; i++)
            {
                result.Append(str[i % str.Length]);
            }
            return result.ToString();
        }
    }
}
