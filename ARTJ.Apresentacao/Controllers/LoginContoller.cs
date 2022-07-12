using ARTJ.Apresentacao.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ARTJ.Apresentacao.Controllers
{
    [ApiController]
    [Route("v1")]
    public class LoginContoller : ControllerBase
    {
        [HttpPost]//dynamic = tipo para não retornar nada
        [Route("login")]
        public async Task<ActionResult<dynamic>> AutheticateAsync([FromBody] Model.User model)
        {
            var user = Repository.UserRepository.Get(model.UserName, model.Password);
            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha invalidos" });
            }
            var token = TokenService.GenerateToken(user).ToString();// SerializableError for valido gera o token para esse usuário
            user.Password = ""; //Não mostrar a senha
            return new { user = user, token = token};
        }
    }
}
