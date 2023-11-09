using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IOrderDAL : IRepositoryBase<Order>
    {
        void OrderAddRange(List<Order> orders);
    }
}
