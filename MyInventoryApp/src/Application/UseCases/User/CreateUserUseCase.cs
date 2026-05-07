using MyInventoryApp.src.Application.DTOs;
using MyInventoryApp.src.Application.Results;
using MyInventoryApp.src.Domain.Interfaces;
using BCryptNet = BCrypt.Net.BCrypt;


namespace MyInventoryApp.src.Application.UseCases.User
{
    public class CreateUserUseCase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserUseCase(
            IAuthRepository authRepository,
            IUnitOfWork unitOfWork
            )
        {
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<UserDTO>> Execute(UserDTO userDto)
        {
            if(userDto.Email is null)
                return Result<UserDTO>.Failure("El correo es requerido");
            try
            {

                var existUser = await _authRepository.ExistEmail(userDto.Email);
                if (existUser)
                    return Result<UserDTO>.Failure("El correo ya esta registrado");

                await _unitOfWork.BeginTransactionAsync();
                var PasswordHasher = BCryptNet.HashPassword(userDto.Password);
                var newUser = new Domain.Entities.User(
                    userDto?.Name ?? "",
                    userDto?.Email ?? "",
                    PasswordHasher
                );

                var createdUser = await _authRepository.CreateUser(newUser);

                await _unitOfWork.CommitAsync();

                if (createdUser == null)
                {
                    return Result<UserDTO>.Failure("Fallo al Crear el usuario");
                }
                userDto.Id = newUser.Id;
                return Result<UserDTO>.Success(userDto);
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                return Result<UserDTO>.Failure("Error al crear el usuario");
            }
        }
    }
}
