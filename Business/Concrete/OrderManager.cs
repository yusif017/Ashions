
using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Abstract;
using Core.Utilities.Concrete.ErrorResult;
using Core.Utilities.Concrete.SuccessResult;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.OrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDAL _orderDAL;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public OrderManager(IOrderDAL orderDAL, IMapper mapper, IProductService productService)
        {
            _orderDAL = orderDAL;
            _mapper = mapper;
            _productService = productService;
        }

        public IResult CreateOrder(string userId, List<OrderCreateDTO> orderCreateDTO)
        {

                var map = _mapper.Map<List<Order>>(orderCreateDTO);
                _orderDAL.AddRange(map);
                return new SuccessResult();
           
            return new ErrorResult(Messages.ProductNotFound);
            
        }

       
        public IResult CheckProductQuantity(List<OrderCreateDTO> orderCreateDTOs)
        {
            foreach (var item in orderCreateDTOs)
            {
                var result = _productService.GetProductQuantityById(item.ProductId);

                if(result.Data == 0)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }

        public IResult CheckProductQuantityLimit(List<OrderCreateDTO> orderCreateDTOs)
        {
            foreach (var item in orderCreateDTOs)
            {
                if (item.Quantity > 10)
                {
                    return new ErrorResult();
                }
            }
            return new SuccessResult();
        }

        public IResult UpdateProductQuantity(int PraductId, int productQuantity)
{
    try
    {
        // Veritabanından belirli bir ürünü alın
        var product = _productService.GetProductById(PraductId);

        if (product != null)
        {
            // Yeni miktarı kontrol et (örneğin, negatif miktarlar izin verilmeyebilir)
            if (productQuantity < 0)
            {
                return new ErrorResult("Geçersiz miktar. Miktar negatif olamaz.");
            }

            // Ürün miktarını güncelle
            product.Quantity = productQuantity;

            // Değişiklikleri veritabanına kaydet
            _productService.UpdateProduct(product);

            return new SuccessResult("Ürün miktarı başarıyla güncellendi.");
        }
        else
        {
            return new ErrorResult("Ürün bulunamadı.");
        }
    }
    catch (Exception ex)
    {
        return new ErrorResult("Ürün miktarı güncellenirken bir hata oluştu: " + ex.Message);
    }
}

    }
}
