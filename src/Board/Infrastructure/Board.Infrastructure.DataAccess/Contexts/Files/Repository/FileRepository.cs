using Board.Application.AppData.Contexts.Files.Repositories;
using Board.Contracts.File;
using Board.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Board.Infrastructure.DataAccess.Contexts.Files.Repository;

public class FileRepository : IFileRepository
{
    private readonly IRepository<Domain.Files.File> _repository;
    public FileRepository(IRepository<Domain.Files.File> repository)
    {
        _repository = repository;
    }


    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var file = await _repository.GetByIdAsync(id, cancellationToken);
        if (file == null)
        {
            return;
        }

        await _repository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task<Board.Domain.Files.File> DownloadAsync(Guid id, CancellationToken cancellationToken)
    {
        var fileModel = await _repository.GetAll(cancellationToken).Where(x => x.Id == id).FirstOrDefaultAsync();
        return fileModel;
    }

    public Task<Board.Domain.Files.File> GetInfoByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken).Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Guid> UploadAsync(Domain.Files.File model, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(model, cancellationToken);
        return model.Id;
    }
}
