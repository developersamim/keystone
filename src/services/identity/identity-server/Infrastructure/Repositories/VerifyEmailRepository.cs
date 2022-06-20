using System;
using System.Linq.Expressions;
using identity_server.Models;

namespace identity_server.Infrastructure.Repositories;

public class VerifyEmailRepository : IVerifyEmailRepository
{
    private ApplicationDbContext context;

    public VerifyEmailRepository(ApplicationDbContext context)
    {
        this.context = context;
    }

    public VerifyEmail GetVerifyEmail(string userId)
    {
        Expression<Func<VerifyEmail, bool>> predicate = ve => ve.UserId == userId;
        return context.VerifyEmail.FirstOrDefault(predicate);
    }

    public void InsertVerifyEmail(VerifyEmail entity)
    {
        context.VerifyEmail.Add(entity);
    }

    public void Save()
    {
        context.SaveChanges();
    }

    private bool disposed = false;

    public void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
                context.Dispose();
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

