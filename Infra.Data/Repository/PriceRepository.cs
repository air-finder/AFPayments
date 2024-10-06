using Domain.Entities;
using Domain.Repositories;

namespace Infra.Data.Repository;

public class PriceRepository(IUnitOfWork unitOfWork) : BaseRepository<Price>(unitOfWork), IPriceRepository
{

}