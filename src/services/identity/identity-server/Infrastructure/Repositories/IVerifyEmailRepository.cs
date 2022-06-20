using System;
using identity_server.Models;

namespace identity_server.Infrastructure.Repositories;

public interface IVerifyEmailRepository : IDisposable
{
    void InsertVerifyEmail(VerifyEmail entity);
    VerifyEmail GetVerifyEmail(string userId);
    void Save();
}

