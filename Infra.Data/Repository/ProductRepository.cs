using Domain.Entities;
using Domain.Repositories;

namespace Infra.Data.Repository;

public class ProductRepository(IUnitOfWork unitOfWork) : BaseRepository<Product>(unitOfWork), IProductRepository
{

}