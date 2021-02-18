using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LocacaoVeiculosApi.Domain.Entities;
using LocacaoVeiculosApi.Domain.Entities.Enums;
using LocacaoVeiculosApi.Presentation.ViewModel;
using LocacaoVeiculosApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LocacaoVeiculosApi.Extensions;


namespace LocacaoVeiculosApi.Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly EntityService<Usuario> _usuarioService;
        private readonly IMapper _mapper;

        public ClienteController(EntityService<Usuario> usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [HttpGet]
        // [Route("/cliente")]
        //[Authorize(Roles = "Cliente, Operador")]
        [AllowAnonymous]
        public async Task<IEnumerable<UsuarioCompleto>> Index()
        {
            var clientes = await _usuarioService.ListAsync();
            return _mapper.Map<IEnumerable<Usuario>, IEnumerable<UsuarioCompleto>>(clientes);
        }

        [HttpGet("{id}")]
        // [Route("/cliente/{id}")]
        //[Authorize(Roles = "Cliente, Operador")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _usuarioService.GetAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(_mapper.Map<Usuario, UsuarioCompleto>((Usuario)result.Entity));
        }

        [HttpPost]
        [Route("/users")]
        [Route("/cliente")]
        //[Authorize(Roles = "Operador")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreateClienteDto resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
        
            var cliente = _mapper.Map<CreateClienteDto, Cliente>(resource);
            var usuario = new Usuario()
            {
                Nome = cliente.Nome,
                Senha = cliente.Senha,
                CpfMatricula = cliente.Cpf,
                DataNascimento = cliente.DataNascimento,
                TipoUsuario = TipoUsuario.Cliente,
                Endereco = new Endereco()
                {
                    Cep = resource.Cep,
                    Cidade = resource.Cidade,
                    Complemento = resource.Complemento,
                    Estado = resource.Estado,
                    Logradouro = resource.Logradouro,
                    Numero = resource.Numero,
                }
            };
            var result = await _usuarioService.CreateAsync(usuario);
        
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return StatusCode(201);
        }

        [HttpPut]
        [Route("/cliente/{id}")]
        [Authorize(Roles = "Operador")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateClienteDto resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
        
            var cliente = _mapper.Map<CreateClienteDto, Cliente>(resource);
             var usuario = new Usuario()
            {
                Nome = cliente.Nome,
                Senha = cliente.Senha,
                CpfMatricula = cliente.Cpf,
                DataNascimento = cliente.DataNascimento,
                TipoUsuario = TipoUsuario.Cliente,
                Endereco = new Endereco()
                {
                    Cep = resource.Cep,
                    Cidade = resource.Cidade,
                    Complemento = resource.Complemento,
                    Estado = resource.Estado,
                    Logradouro = resource.Logradouro,
                    Numero = resource.Numero,
                }
            };
            var result = await _usuarioService.UpdateAsync(id, cliente);
            
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return StatusCode(204);
        }

        [HttpDelete]
        [Route("/cliente/{id}")]
        [Authorize(Roles = "Operador")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _usuarioService.DeleteAsync(id);
        
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
        
            return StatusCode(204);
        }
        
    }
}