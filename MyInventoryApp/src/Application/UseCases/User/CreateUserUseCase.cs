using AutoMapper;
using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;
using BCryptNet = BCrypt.Net.BCrypt;


namespace MyInventoryApp.src.Application.UseCases.User
{
    public class CreateUserUseCase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        public CreateUserUseCase(
            IAuthRepository authRepository,
            IMapper mapper
            )
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }
        public async Task<Result<UserDTO>> Execute(UserDTO userDto)
        {
            try
            {

                var PasswordHasher = BCryptNet.HashPassword(userDto.Password);
                var newUser = new Domain.Entities.User(
                    userDto?.Name ?? "a",
                    userDto?.Email ?? "",
                    PasswordHasher
                );

                var createdUser = await _authRepository.CreateUser(newUser);
                if (createdUser == null)
                {
                    return Result<UserDTO>.Failure("Fallo al Crear el usuario");
                }
                userDto.Id = newUser.Id;
                return Result<UserDTO>.Success(userDto);
            }
            catch (Exception ex)
            {
                return Result<UserDTO>.Failure(ex.Message);
            }
        }
    }
}
