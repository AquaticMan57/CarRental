using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performances;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.BusinessRules;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        //[SecuredOperation("add,admin")]
        //[CacheRemoveAspect("IBrandService.Get")]
        [ValidationAspect(typeof(BrandValidator))]
        [PerformanceAspect(10)]

        public IResult Add(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandNameExists(brand.BrandName));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            if (brand.BrandName.Length<3)
            {
                return new ErrorResult(Messages.InvalidNameError);
            }
            _brandDal.Add(brand);
            return new SuccessResult(Messages.Succeed);
        }
        //[SecuredOperation("delete,admin")]
        //[CacheRemoveAspect("IBrandService.Get")]
        [PerformanceAspect(10)]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Delete(Brand brand)
        {
            
            if (DateTime.Now.Hour ==18)
            {
                return new ErrorResult(Messages.InvalidNameError);
            }
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.Succeed);
        }
        [PerformanceAspect(10)]
        public IResult DeleteById(int id)
        {
            IResult result = BusinessRules.Run(CheckIfBrandExists(id));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            return new SuccessResult(Messages.Succeed);

        }

        //[SecuredOperation("list,admin")]
        //[CacheAspect]
        [PerformanceAspect(10)]
        [ValidationAspect(typeof(BrandValidator))]
        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorDataResult<List<Brand>>(Messages.InvalidNameError);
            }
            
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.Succeed);
        }

        //[SecuredOperation("list,admin")]
        [PerformanceAspect(10)]
        //[CacheAspect]
        [ValidationAspect(typeof(BrandValidator))]
        public IDataResult<Brand> GetBrandByBrandId(int brandId)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<Brand>(Messages.InvalidNameError);
            }
            
            return new SuccessDataResult<Brand>(_brandDal.Get(b=>b.BrandId ==brandId),Messages.Succeed);
        }


        public IResult Transaction(Brand brand)
        {
            throw new NotImplementedException();
        }

        //[SecuredOperation("update,admin")]
        //[CacheRemoveAspect("IBrandService.Get")]
        [PerformanceAspect(10)]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
            IResult result = BusinessRules.Run(CheckIfBrandNameExists(brand.BrandName));
            if (result != null)
            {
                return new ErrorResult(result.Message);
            }
            return new SuccessResult(Messages.Succeed);
        }

        private IResult CheckIfBrandNameExists(string brandName)
        {
            var result = _brandDal.Get(b=>b.BrandName== brandName);
            if (result != null)
            {
                return new ErrorResult(BrandMessages.BrandNameExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfBrandExists(int id)
        {
            var result = _brandDal.Get(b=>b.BrandId==id);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult(BrandMessages.BrandNotExists);
        }
    }
}
