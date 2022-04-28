namespace PvsStudioLicense.Infrastructure.Services;

using Domain.Abstractions;
using Domain.Entities;
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
    public Project Get(string path)
    {
        using var context = _contextFactory.CreateDbContext();
        return context.Products
            .AsNoTracking()
            .FirstOrDefault(x => x.Path == path);
    }

    /// <inheritdoc />
    public void Add(Project project)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Products.Add(project);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Delete(Project project)
    {
        using var context = _contextFactory.CreateDbContext();
        context.Products.Remove(project);
        context.SaveChanges();
    }

    private void CreateDatabase()
    {
        using var context = _contextFactory.CreateDbContext();
        context.Database.EnsureCreated();
    }
}