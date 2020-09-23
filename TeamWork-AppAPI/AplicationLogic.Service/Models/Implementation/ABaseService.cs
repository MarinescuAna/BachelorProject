using AplicationLogic.Repository.UOW;
using System;
using System.Collections.Generic;
using System.Text;

namespace AplicationLogic.Service.Models.Implementation
{
    public abstract class ABaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        public ABaseService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
    }
}
