using System.Linq;
using LocacaoVeiculosApi.Infrastructure.Repositories;
using LocacaoVeiculosApi.Domain.Entities;
using LocacaoVeiculosApi.Domain.Services.Communication;
using System.Threading.Tasks;
using LocacaoVeiculosApi.Presentation.ViewModel;
using LocacaoVeiculosApi.Domain.Authentication;

namespace LocacaoVeiculosApi.Services
{
    public class LoginService
    {
        private readonly EntityRepository<Usuario> _usuarioRepository;

        public LoginService(EntityRepository<Usuario> usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<DtoResponse<UsuarioJwt>> Logar(UsuarioLogin usuarioLogin, IToken token)
        {
            var usuarios = await _usuarioRepository.Filter(x => x.CpfMatricula == usuarioLogin.CpfMatricula);
            if (usuarios == null || usuarios.Count() == 0)
            {
                return new DtoResponse<UsuarioJwt>("Usuário/Senha Inválido");
            }
            var usuario = usuarios.First();
            if (usuario.Senha != usuarioLogin.Senha)
            {
                return new DtoResponse<UsuarioJwt>("Usuário/Senha Inválido");
            }
            return new DtoResponse<UsuarioJwt>(new UsuarioJwt()
            {
                id = usuario.Id,
                Nome = usuario.Nome,
                login = usuario.CpfMatricula,
                TipoUsuario = usuario.TipoUsuario.ToString(),
                Token = token.GerarToken(usuario)
            });
        }

        public async Task<DtoResponse<ClienteJwt>> LogarCliente(ClienteLogin usuarioLogin, IToken token)
        {
            var usuarios = await _usuarioRepository.Filter(x => x.CpfMatricula == usuarioLogin.Cpf);

            if (usuarios == null || usuarios.Count() == 0)
            {
                return new DtoResponse<ClienteJwt>("Usuário/Senha Inválido");
            }

            var usuario = usuarios.First();
            if (usuario.Senha != usuarioLogin.Senha)
            {
                return new DtoResponse<ClienteJwt>("Usuário/Senha Inválido");
            }

            return new DtoResponse<ClienteJwt>(new ClienteJwt()
            {
                id = usuario.Id,
                Nome = usuario.Nome,
                Cpf = usuario.CpfMatricula,
                TipoUsuario = usuario.TipoUsuario.ToString(),
                Token = token.GerarToken(usuario)
            });
        }

         public async Task<DtoResponse<OperadorJwt>> LogarOperador(OperadorLogin usuarioLogin, IToken token)
        {
            var usuarios = await _usuarioRepository.Filter(x => x.CpfMatricula == usuarioLogin.Matricula);

            if (usuarios == null || usuarios.Count() == 0)
            {
                return new DtoResponse<OperadorJwt>("Usuário/Senha Inválido");
            }

            var usuario = usuarios.First();
            if (usuario.Senha != usuarioLogin.Senha)
            {
                return new DtoResponse<OperadorJwt>("Usuário/Senha Inválido");
            }

            return new DtoResponse<OperadorJwt>(new OperadorJwt()
            {
                id = usuario.Id,
                Nome = usuario.Nome,
                Matricula = usuario.CpfMatricula,
                TipoUsuario = usuario.TipoUsuario.ToString(),
                Token = token.GerarToken(usuario)
            });
        }
    }
}