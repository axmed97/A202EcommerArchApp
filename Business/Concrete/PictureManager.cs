using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PictureManager : IPictureService
    {
        private readonly IPictureDAL _pictureDAL;

        public PictureManager(IPictureDAL pictureDAL)
        {
            _pictureDAL = pictureDAL;
        }

        public async Task<IResult> RemoveProductPictureAsync(string url)
        {
            var result = await _pictureDAL.RemovePictureAsync(url);
            if(result)
                return new SuccessResult();

            return new ErrorResult();
        }
    }
}
