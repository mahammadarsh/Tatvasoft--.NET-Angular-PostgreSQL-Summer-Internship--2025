﻿using Mission.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.IRepositories
{
    public interface ILoginRepository
    {
        LoginUserResponseModel LoginUser(LoginUserRequestModel model);
        Task<string> RegisterUser(RegisterUserRequestModel registerUserRequest);
        UserResponseModel LoginUserDetailById(int id);
        Task<bool> LoginUserProfileUpdate(AddUserDetailsRequestModel requestModel);

        Task<UserProfileDetailResponseModel> GetUserProfileDetailById(int userId);

    }
}
