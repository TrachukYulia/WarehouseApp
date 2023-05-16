using AutoMapper;
using DAL.Interfaces;
using BLL.Interfaces;
using BLL.DTO;
using DAL.Repository;
using BLL.Exceptions;
using DAL.Models;
using BLL.Interfaces;
using Ninject;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BLL.Services
{
    public class GoodService: IGoodService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GoodService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<GoodModel> GetAll()
        {
            var goods = _unitOfWork.GoodRepository.GetAll();

            if (goods is null)
                throw new NotFoundException("List is empty");
            return _mapper.Map<IEnumerable<GoodModel>>(goods);
        }

        public GoodModel Get(int id)
        {
            var good = _unitOfWork.GoodRepository.Get(id);

            if (good == null)
            {
                throw new NotFoundException("Object with id {id} not found");
            }

            return _mapper.Map<GoodModel>(good);
        }

        public void Create(GoodsRequestToCreate item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), message: "Object is empty");
            var good = _mapper.Map<Good>(item);
            _unitOfWork.GoodRepository.Create(good);
            _unitOfWork.Save();
        }

        public void Update(GoodsRequest item, int id)
        {
            var good = _unitOfWork.GoodRepository.Get(id);

            if (good == null)
            {
                throw new NotFoundException("Object not found");
            }

            good = _mapper.Map(item, good);
            _unitOfWork.GoodRepository.Update(good);

            var queue = _unitOfWork.QueueRepository.GetAll();

            var  activeOrders =  queue
                .Where(x => _unitOfWork.OrderRepository.Get(x.OrderId).GoodId == id)
                .OrderBy(x => _unitOfWork.OrderRepository.Get(x.OrderId).TimeCreated)
                .ToList();

            foreach (var q in activeOrders)
            {
                UpdateOrder(_unitOfWork.OrderRepository.Get(q.OrderId), q.OrderId);
            }
           
            _unitOfWork.Save();

        }
        public void UpdateOrder(Order item, int id )
        {
            
            var order = _unitOfWork.OrderRepository.Get(id);

            if (order is null)
                throw new NotFoundException("Order not found");

            var good = _unitOfWork.GoodRepository.Get(order.GoodId);
            var queueList = _unitOfWork.QueueRepository.GetAll();

            if (good.Amount - item.Amount >= 0)
            {
                order.StatusOfOrder = StatusOfOrder.Done;
                order.TotalPrice = order.Amount * good.Price;
                good.Amount = good.Amount - item.Amount;
                _unitOfWork.QueueRepository.Delete(
                    queueList
                    .Where(x => x.OrderId == id)
                    .First().Id
                    );
            }
            else
            {
                order.StatusOfOrder = StatusOfOrder.Active;
                order.TotalPrice = 0;
            }
        }
        public void Delete(int id)
        {
            var good =  _unitOfWork.GoodRepository.Get(id);

            if (good is null)
                throw new NotFoundException("Object with id {id} not found");

             _unitOfWork.GoodRepository.Delete(id);
             _unitOfWork.Save();
        }
    }
}
