using Core.DataAccess.SQLServer.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFOxsarDAL : EFRepositoryBase<PraductOxsar, AppDbContext>, IOxsarDAL
    {
        public async Task<bool> Add(PraductOxsar praductOxsar)
        {
            using AppDbContext context = new();

            try
            {
                PraductOxsar praduct = new()
                {

                    Name = praductOxsar.Name,


                };

                await context.PraductOxsars.AddAsync(praduct);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<PraductOxsar> GetAll()
        {
            using var context = new AppDbContext();
            return context.PraductOxsars
                .Select(x => new PraductOxsar
                {
                    Name = x.Name,
                    Id = x.Id

                }).ToList();
        }
    }
}
