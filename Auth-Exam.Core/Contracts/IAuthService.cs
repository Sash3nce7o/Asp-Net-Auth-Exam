using System.Threading.Tasks;
using Auth_Exam.Core.Models.Auth;
using Auth_Exam.Core.Models.HTTP;
namespace Auth_Exam.Core.Contracts
{
    public interface IAuthService
    {
        Task<Response<LoginResponseViewModel>> Login(LoginFormViewModel model);
    }
}