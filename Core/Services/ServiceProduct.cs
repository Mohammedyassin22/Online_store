using AutoMapper;
using Domain.Contracts;
using ServicesAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceProduct(IUnitOfWork unitOfWork,IMapper mapper) : IServiceProduct
    {
        public IProductServices Services { get; }=new ProductServices(unitOfWork,mapper);
    }
}
