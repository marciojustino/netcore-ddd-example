namespace DDDExample.Domain.Interfaces.Auth
{
    using System.Threading.Tasks;
    using CSharpFunctionalExtensions;
    using Dtos;
    using Entities;

    public interface IAuthService
    {
        Task<Result<User>> LoginAsync(AuthDto auth);
    }
}