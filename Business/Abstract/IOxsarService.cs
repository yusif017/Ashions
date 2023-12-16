using Core.Utilities.Abstract;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IOxsarService
    {
        IResult Add(PraductOxsar praductOxsar);
        IResultData<List<PraductOxsar>> GetAll();

    }
}
