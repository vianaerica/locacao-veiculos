using LocacaoVeiculosApi.Infra.Authentication;
using Microsoft.AspNetCore.Mvc;
using LocacaoVeiculosApi.Domain.UseCase.UseServices;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using LocacaoVeiculosApi.Domain.Entities;
using System.Threading.Tasks;
using LocacaoVeiculosApi.Domain.ViewModel;

namespace LocacaoVeiculosApi.Presentation.Controllers
{
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _userService;
        private readonly ILogger<HomeController> _logger;

            [HttpPost]
            [Route("/login")]
            [AllowAnonymous]
            public async Task<ActionResult> Login(UsuarioLogin userLogin){  
                try{
                    return StatusCode(200, await _userService.Login(new Usuario(){
                        CpfMatricula = userLogin.CpfMatricula,
                        Senha = userLogin.Senha
                    }, new Token()));
                }
                catch(UsuarioNotFound err){
                    return StatusCode(401, new {
                        Message = err.Message
                    });
                }
            }

            [HttpPost]
            [Route("/usuario")]
            [AllowAnonymous]
            public async Task<ActionResult> Create(Usuario user){  
                try{
                //await _userService.Save(user);
                return StatusCode(201);
                }
            catch(UsuarioUnico err){
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
            }

        [HttpGet]
        [Route("/usuario")]
        [AllowAnonymous]
        public async Task<Usuario> RetornaTodosUsuarios(){
            return await _userService.RetornaTodosUsuarios();
        }
    }
}