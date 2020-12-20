﻿using Bank.Service.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Service.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<Account, Guid>
    {
    }
}
