using Business.Abstract;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
   
    public class OxsarManager : IOxsarService
    {
        private readonly IOxsarDAL _oxsarDAL;

        public OxsarManager(IOxsarDAL oxsarDAL)
        {
            _oxsarDAL = oxsarDAL;
        }

        public IResult Add(PraductOxsar praductOxsar)
        {
            try
            {
                _oxsarDAL.Add(praductOxsar);
                return new SuccessResult("Praduct Added Successfully!");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);
            }
        }

        public IResultData<List<PraductOxsar>> GetAll()
        {
            var result = _oxsarDAL.GetAll();
            return new SuccessDataResult<List<PraductOxsar>>(result, "All Categories");
        }
    }
}
