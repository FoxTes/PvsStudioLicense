namespace PvsStudioLicense.Infrastructure.Services;

using CSharpFunctionalExtensions;
using Domain.Abstractions;
using Domain.Entities;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Scrutor.AspNetCore;

/// <inheritdoc cref="PvsStudioLicense.Domain.Abstractions.IProjectManager" />
public class ProjectManager : IProjectManager, ISingletonLifetime
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectManager"/> class.
    /// </summary>
    /// <param name="contextFactory"><see cref="IDbContextFactory{TContext}"/>.</param>
    public ProjectManager(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;

        CreateDatabase();
    }

    /// <inheritdoc />
    public IEnumerable<Project> GetAll()
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Products
            .AsNoTracking()
            .ToList();
    }

    /// <inheritdoc />
    public Result<Project> Get(string path)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Products
            .AsNoTracking()
            .FirstOrDefault(x => x.Path == path);
    }

    /// <inheritdoc />
    public Result Add(Project project)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Products.Add(project);

        try
        {
            var result = context.SaveChanges();
            return result > 0
                ? Result.Success()
                : Result.Failure("Не удалось добавить проект в базу данных.");
        }
        catch (UniqueConstraintException)
        {
            return Result.Failure("Проект с таким именем уже существует в базе данных.");
        }
        catch (Exception)
        {
            return Result.Failure("Не удалось добавить проект в базу данных.");
        }
    }

    /// <inheritdoc />
    public Result Update(Project project)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Products.Update(project);
        var result = context.SaveChanges();

        return result > 0
            ? Result.Success()
            : Result.Failure("Не удалось обновить проект в базе данных.");
    }

    /// <inheritdoc />
    public Result Delete(Project project)
    {
        using var context = _contextFactory.CreateDbContext();

        var entity = context.Products.Find(project.Id);
        if (entity == null)
            return Result.Failure("Не удалось удалить проект в базе данных.");

        context.Products.Remove(entity);
        context.SaveChanges();

        return Result.Success();
    }

    private void CreateDatabase()
    {
        using var context = _contextFactory.CreateDbContext();
        context.Database.EnsureCreated();
    }
}