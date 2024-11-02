
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Core.Autentication;
using Dto.Catalog;
using EntityFrameworkCore.Repository;

namespace ApplicationServices.Catalogs.Other
{
    public class OtherService : IOtherService
    {
        private readonly IMapper _mapper;
        IRepository<int, UserType> _repositoryUserType;

        public OtherService(IMapper mapper,
                            IRepository<int, UserType> repositoryUserType
                            )
        {
            _mapper = mapper;
            _repositoryUserType = repositoryUserType;
        }
        public async Task<List<UserTypeDto>> getUserTypes()
        {
            List<UserType> result = await _repositoryUserType.GetAll().ToListAsync();
            List<UserTypeDto> _mappedDtos = _mapper.Map<List<UserTypeDto>>(result);
            return _mappedDtos;
        }
    }
}
