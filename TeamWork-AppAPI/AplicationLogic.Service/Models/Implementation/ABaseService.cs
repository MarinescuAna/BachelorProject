using TeamWork.ApplicationLogic.Repository.UOW;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
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
