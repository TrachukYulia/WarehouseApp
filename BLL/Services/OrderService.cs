using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class OrderService: IOrderService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<OrderModel> GetAll()
        {
            var orders = _unitOfWork.OrderRepository.GetAll();
            if (orders is null)
                throw new NotFoundException("List is empty");
            return _mapper.Map<IEnumerable<OrderModel>>(orders);
        }

        public OrderModel Get(int id)
        {
            var order = _unitOfWork.OrderRepository.Get(id);

            if (order == null)
            {
                throw new NotFoundException("Object with id ${id} not found");
            }
            return _mapper.Map<OrderModel>(order);
        }

        public void Create(OrderRequest item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), message: "Object is empty");
            var order = _mapper.Map<Order>(item);

            var good = _unitOfWork.GoodRepository.Get(item.GoodId);
            var customer = _unitOfWork.CustomerRepository.Get(item.CustomerId);

            if (good is null)
                throw new NotFoundException("Good not found");

            if (customer is null)
                throw new NotFoundException("Customer not found");

            if (good.Amount - item.Amount >= 0)
            {
                _unitOfWork.OrderRepository.Create(order);
                order.StatusOfOrder = StatusOfOrder.Done;
                order.TotalPrice = order.Amount * good.Price;
                good.Amount = good.Amount - item.Amount;

            }
            else
            {
                order.StatusOfOrder = StatusOfOrder.Active;
                order.TotalPrice = 0;
                QueueOrderRequest queueRequest = new QueueOrderRequest()
                {
                    OrderId = order.Id,
                    Orders = order
                };

                var queue = _mapper.Map<Queue>(queueRequest);

                _unitOfWork.QueueRepository.Create(queue);
            }

            _unitOfWork.Save();
        }

        public void Update(OrderGoodRequest item, int id)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), message: "Object is empty");

            var order = _unitOfWork.OrderRepository.Get(id);

            if (order is null)
                throw new NotFoundException("Order not found");

            if (order.StatusOfOrder == StatusOfOrder.Done)
                throw new ArgumentException("Order is done");

            var good = _unitOfWork.GoodRepository.Get(order.GoodId);
            var queueList = _unitOfWork.QueueRepository.GetAll();

            if (good.Amount - item.Amount >= 0)
            {
                order = _mapper.Map(item, order);
                order.StatusOfOrder = StatusOfOrder.Done;
                order.TotalPrice = order.Amount * good.Price;
                order.Amount = good.Amount - item.Amount;
                _unitOfWork.QueueRepository.Delete(
                    queueList
                   .First(x => x.OrderId == id).Id
                    );
            }
            else
            {
                order.Amount = item.Amount;
                order.StatusOfOrder = StatusOfOrder.Active;
                order.TotalPrice = 0;
            }

            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var order = _unitOfWork.OrderRepository.Get(id);

            if (order is null)
                throw new NotFoundException("Object with id {id} not found");

            _unitOfWork.OrderRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
