using AutoMapper;
using BLL.DTO;
using BLL.Exceptions;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CustomerService:ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<CustomerModel> GetAll()
        {
            var customers = _unitOfWork.CustomerRepository.GetAll();
            if (customers is null)
                throw new NotFoundException("List is empty");
            return _mapper.Map<IEnumerable<CustomerModel>>(customers);
        }

        public CustomerModel Get(int id)
        {
            var customer = _unitOfWork.CustomerRepository.Get(id);

            if (customer == null)
            {
                throw new NotFoundException("Object with id {id} not found");
            }

            return _mapper.Map<CustomerModel>(customer);
        }

        public void Create(CustomerRequest item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), message: "Object is empty");
            var customer = _mapper.Map<Customer>(item);
            _unitOfWork.CustomerRepository.Create(customer);
            _unitOfWork.Save();
        }

        public void Update(CustomerRequest item, int id)
        {
            var customer = _unitOfWork.CustomerRepository.Get(id);

            if (customer == null)
            {
                throw new NotFoundException("Object not found");
            }

            customer = _mapper.Map(item, customer);

            _unitOfWork.CustomerRepository.Update(customer);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var customer = _unitOfWork.CustomerRepository.Get(id);

            if (customer is null)
                throw new NotFoundException("Object with id {id} not found");

            _unitOfWork.CustomerRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
