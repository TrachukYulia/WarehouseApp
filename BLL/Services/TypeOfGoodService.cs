using AutoMapper;
using DAL.Interfaces;
using BLL.Interfaces;
using BLL.DTO;
using DAL.Repository;
using BLL.Exceptions;
using DAL.Models;
using Ninject;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TypeOfGoodService: ITypeOfGoodService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TypeOfGoodService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<TypeOfGoodModel> GetAll()
        {
            var typeOfGoods = _unitOfWork.TypeOfGoodRepository.GetAll();
            return _mapper.Map<IEnumerable<TypeOfGoodModel>>(typeOfGoods);
        }

        public TypeOfGoodModel Get(int id)
        {
            var typeOfGoods = _unitOfWork.TypeOfGoodRepository.Get(id);

            if (typeOfGoods == null)
            {
                throw new NotFoundException("Object with id {id} not found");
            }

            return _mapper.Map<TypeOfGoodModel>(typeOfGoods);
        }

        public void Create(TypeOfGoodRequest item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), message: "Object is empty");
            var typeOfGood = _mapper.Map<TypeOfGood>(item);
            _unitOfWork.TypeOfGoodRepository.Create(typeOfGood);
            _unitOfWork.Save();
        }

        public void Update(TypeOfGoodRequest item, int id)
        {
            var typeOfGood = _unitOfWork.TypeOfGoodRepository.Get(id);

            if (typeOfGood == null)
            {
                throw new NotFoundException("Object not found");
            }

            typeOfGood = _mapper.Map(item, typeOfGood);

            _unitOfWork.TypeOfGoodRepository.Update(typeOfGood);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var typeOfGood = _unitOfWork.TypeOfGoodRepository.Get(id);

            if (typeOfGood is null)
                throw new NotFoundException("Object with id {id} not found");

            _unitOfWork.TypeOfGoodRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
