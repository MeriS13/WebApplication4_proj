using Board.Application.AppData.Contexts.Files.Repositories;
using Board.Contracts.File;
using Board.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.Files.Repository;

/// <inheritdoc cref="IFileRepository"/>

public class FileRepository : IFileRepository
{
    private readonly IRepository<Domain.Files.File> _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public FileRepository(IRepository<Domain.Files.File> repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var file = await _repository.GetByIdAsync(id, cancellationToken);
        if (file == null)
        {
            return;
        }

        await _repository.DeleteByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    public async Task<Board.Domain.Files.File> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        var fileModel = await _repository.GetAll(cancellationToken).Where(x => x.Id == id).FirstOrDefaultAsync();
        return fileModel;
    }

    /// <inheritdoc/>
    public Task<Board.Domain.Files.File> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public async Task<Guid> UploadAsync(Domain.Files.File model, CancellationToken cancellationToken)
    {
        
        await _repository.AddAsync(model, cancellationToken);
        return model.Id;
    }

    /// <inheritdoc/>
    public IQueryable<Domain.Files.File> GetAllByPostId(Guid postId, CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken).Include(u => u.Post).Where(u => u.PostId == postId);
    }
}
