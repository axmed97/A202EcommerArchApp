using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLServer
{
    public class EFPictureDAL : EFRepositoryBase<Picture, AppDbContext>, IPictureDAL
    {
        public async Task<bool> RemovePictureAsync(string url)
        {
            using var context = new AppDbContext();
            var result = await context.Pictures.FirstOrDefaultAsync(x => x.PhotoUrl == url);
            context.Pictures.Remove(result);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
