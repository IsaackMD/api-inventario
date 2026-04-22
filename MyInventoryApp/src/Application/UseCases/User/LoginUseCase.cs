using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;
using MyInventoryApp.src.Infraestructure.Service.Jwt;
using BCryptNet = BCrypt.Net.BCrypt;

namespace MyInventoryApp.src.Application.UseCases.User
{
    public class LoginUseCase
    {
        private readonly IAuthRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly TokenService _tokenService;

        public LoginUseCase(IAuthRepository userRepository, IMapper mapper,
            TokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        public async Task<Result<ResponseLoginDTO>> Execute(UserLoginDTO authUserDTO)
        {
            var currentUser = await _userRepository.LoginAsync(authUserDTO?.Email ?? "");
            if (currentUser == null)
            {
                return Result<ResponseLoginDTO>.Failure("Usuario no encontrado");
            }

            var isPasswordValid = BCryptNet.Verify(authUserDTO?.Password ?? "", currentUser.PasswordHash);

            if (!isPasswordValid)
            {
                return Result<ResponseLoginDTO>.Failure("Contraseña incorrecta");
            }
            var token = _tokenService.GenerateToken(currentUser.Id.ToString(), currentUser.Email, currentUser.Name);

            var userResponse = _mapper.Map<AuthUserDTO>(currentUser);
            var response = new ResponseLoginDTO
            {
                Token = token,
                user = userResponse

            };
            return Result<ResponseLoginDTO>.Success(response);

        }
    }
}
