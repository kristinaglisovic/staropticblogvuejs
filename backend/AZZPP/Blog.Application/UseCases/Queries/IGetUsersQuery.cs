﻿using Blog.Application.UseCases.DTO.Searches;
using Blog.Application.UseCases.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public interface IGetUsersQuery : IQuery<UsersPagedSearch, PagedResponse<UserDTO>>
    {
    }
}
