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
        private readonly IUnitOfWork _unitOfWork;
        public CreateUserUseCase(
            IAuthRepository authRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork
            )
        {
            _authRepository = authRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Execute(UserDTO userDto)
        {

            try
            {
                var currentUser = await _authRepository.ExistEmail(userDto.Email);
                if (currentUser is not null || currentUser != null)
                    return Result<string>.Failure("El correo ya esta registrado");

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
                    return Result<string>.Failure("Fallo al Crear el usuario");
                }
                userDto.Id = newUser.Id;
                return Result<string>.Success("Usuario Creado");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return Result<string>.Failure("Error al crear el usuario");
            }
        }
    }
}
