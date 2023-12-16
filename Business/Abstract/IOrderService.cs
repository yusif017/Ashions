using Core.Utilities.Abstract;
using Entities.DTOs.OrderDTO;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IResult CreateOrder(string userId, List<OrderCreateDTO> orderCreateDTO);
        IResult CheckProductQuantity(List<OrderCreateDTO> orderCreateDTOs);
        IResult CheckProductQuantityLimit(List<OrderCreateDTO> orderCreateDTOs);
        IResult UpdateProductQuantity(int PraductId, int productQuantity);
    }
}
