using Business.Abstract;
using Business.Constants.Messages;
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
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;
        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public IResult Add(Brand brand)
        {
            if (brand.BrandName.Length<3)
            {
                return new ErrorResult(Messages.InvalidNameError);
            }
            _brandDal.Add(brand);
            return new SuccessResult(Messages.Succeed);
        }

        public IResult Delete(Brand brand)
        {
            if (DateTime.Now.Hour ==18)
            {
                return new ErrorResult(Messages.InvalidNameError);
            }
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.Succeed);
        }

        public IDataResult<List<Brand>> GetAll()
        {
            if (DateTime.Now.Hour == 05)
            {
                return new ErrorDataResult<List<Brand>>(Messages.InvalidNameError);
            }
            
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),Messages.Succeed);
        }

        public IDataResult<Brand> GetBrandByBrandId(int brandId)
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<Brand>(Messages.InvalidNameError);
            }
            
            return new SuccessDataResult<Brand>(_brandDal.Get(b=>b.BrandId ==brandId),Messages.Succeed);
        }

        public IResult Update(Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}
