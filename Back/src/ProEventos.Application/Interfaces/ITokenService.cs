using ProEventos.Application.Dtos;
using System.Threading.Tasks;

namespace ProEventos.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserUpdateDto userUpdateDto);
    }
}
