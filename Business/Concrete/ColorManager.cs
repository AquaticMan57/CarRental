using Business.Abstract;
using Business.BusinessAspect;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performances;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
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
        //[SecuredOperation("add,admin")]
        //[CacheRemoveAspect("IColorsService.Get")]
        //[ValidationAspect(typeof(ColorsValidator))]
        //[PerformanceAspect(10)]


        public IResult Add(Colors colors)
        {
            
            _colordal.Add(colors);
            return new SuccessResult(Messages.Succeed);
        }
        [SecuredOperation("delete,admin")]
        [CacheRemoveAspect("IColorsService.Get")]
        [ValidationAspect(typeof(ColorsValidator))]
        [PerformanceAspect(10)]


        public IResult Delete(Colors colors)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            return new SuccessResult(Messages.Succeed);
        }
        //[SecuredOperation("list,admin")]
        //[CacheAspect]
        [PerformanceAspect(10)]


        public IDataResult<List<Colors>> GetAll()
        {
            
            
            return new SuccessDataResult<List<Colors>>(_colordal.GetAll(),Messages.Succeed);
        }
        [SecuredOperation("list,admin")]
        [CacheAspect]
        [PerformanceAspect(10)]


        public IDataResult<Colors> GetColorById(int id)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<Colors>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<Colors>(_colordal.Get(c=>c.ColorId ==id), Messages.Succeed);
        }

        public IResult Transaction(Colors colors)
        {
            throw new NotImplementedException();
        }

        [ValidationAspect(typeof(ColorsValidator))]
        [SecuredOperation("update,admin")]
        [CacheRemoveAspect("IColorsService.Get")]
        [PerformanceAspect(10)]


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
