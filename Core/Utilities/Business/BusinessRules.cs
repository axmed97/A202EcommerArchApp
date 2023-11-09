using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public static class BusinessRules
    {
        public static IResult Check(params IResult[] logics)
        {
            foreach (var item in logics)
            {
                if (!item.Success)
                    return new ErrorResult();
            }
            return new SuccessResult();
        }
    }
}
