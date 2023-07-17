using Business.Abstract;
using Business.Constants.Messages;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        ITokenHelper _tokenHelper;
        IUserService _userService;
        public AuthManager(ITokenHelper tokenHelper,IUserService userService)
        {
            _tokenHelper = tokenHelper;
            _userService = userService;
        }
        
        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(UserMessages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
            {
                return new ErrorDataResult<User>(userToCheck.Data, UserMessages.PasswordError);
            }
            return new SuccessDataResult<User>(userToCheck.Data, Messages.Succeed);

        }

        public IDataResult<AccessToken> Register
            (UserForRegisterDto userForRegisterDto,string password)
        {
            var result = BusinessRules.Run(CheckIfTheMailAlreadyUsed(userForRegisterDto.Email));
            if (result != null)
            {
                return new ErrorDataResult<AccessToken>(result.Message);
            }
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password,out passwordHash,out passwordSalt);
            User user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user); // user vaalidator 
            return new SuccessDataResult<AccessToken>(_tokenHelper.CreateToken
                (user,_userService.GetOperationClaims(user).Data),UserMessages.UserRegistered);

        }
        public IResult UserExists(string email) 
        {
            var user = _userService.GetByMail(email);
            if (user != null)
            {
                return new ErrorResult(UserMessages.UserAlreadyExists);
            }
            return new SuccessResult(UserMessages.Succeed);
        }
        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetOperationClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, AccessTokenMessages.TokenCreated);

        }
        private IResult CheckIfTheMailAlreadyUsed(string mail)
        {
            var result = _userService.GetByMail(mail).Data;
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult(UserMessages.MailAlreadyExists);
        }
    }
}
