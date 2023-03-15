using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Conctrete.EfMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colordal;
        public ColorManager(IColorDal colordal)
        {
            _colordal = colordal;
        }

        public IResult Add(Colors colors)
        {
            if (colors.ColorName.Length<3)
            {
                return new ErrorResult(Messages.InvalidNameError);
            }
            _colordal.Add(colors);
            return new SuccessResult(Messages.Succeed);
        }

        public IResult Delete(Colors colors)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            return new SuccessResult(Messages.Succeed);
        }

        public IDataResult<List<Colors>> GetAllColors()
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<List<Colors>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Colors>>(_colordal.GetAll(),Messages.Succeed);
        }

        public IDataResult<Colors> GetColorById(int id)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<Colors>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<Colors>(_colordal.Get(c=>c.Id==id), Messages.Succeed);
        }

        public IResult Update(Colors colors)
        {
            if (colors.ColorName.Length <3 && DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime + Messages.InvalidNameError);
            }
            _colordal.Update(colors);
            return new SuccessResult( Messages.Succeed);
        }
    }
}
