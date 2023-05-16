
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Exceptions;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class QueueService: IQueueService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public QueueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<QueueModel> GetAll()
        {
            var queue = _unitOfWork.QueueRepository.GetAll();
            if (queue is null)
                throw new NotFoundException("List is empty");
            return _mapper.Map<IEnumerable<QueueModel>>(queue);
        }

        public QueueModel Get(int id)
        {
            var queue = _unitOfWork.QueueRepository.Get(id);

            if (queue == null)
            {
                throw new NotFoundException("Object with id {id} not found");
            }

            return _mapper.Map<QueueModel>(queue);
        }

        public void Create(QueueRequest item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), message: "Object is empty");
            var queue = _mapper.Map<Queue>(item);
            _unitOfWork.QueueRepository.Create(queue);
            _unitOfWork.Save();
        }

        public void Update(QueueRequest item, int id)
        {
            var queue = _unitOfWork.QueueRepository.Get(id);

            if (queue == null)
            {
                throw new NotFoundException("Object not found");
            }

            queue = _mapper.Map(item, queue);

            _unitOfWork.QueueRepository.Update(queue);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var queue = _unitOfWork.QueueRepository.Get(id);

            if (queue is null)
                throw new NotFoundException("Object with id {id} not found");

            _unitOfWork.QueueRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}

