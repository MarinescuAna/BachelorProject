using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.ApplicationLogic.Repository.UOW;
using TeamWork.ApplicationLogic.Service.Models.Interface;
using TeamWork.ApplicationLogic.Service.Utils;
using TeamWork.DataAccess.Domain.Models.Domain;

namespace TeamWork.ApplicationLogic.Service.Models.Implementation
{
    public class ImageServiceImpl : ABaseService,IImageService
    {
        public ImageServiceImpl(IUnitOfWork uow) : base(uow)
        {
        }
        public async Task<Image> GetImageAsync(string userEmail) =>await _unitOfWork.Image.GetItem(u => u.UserId == userEmail); 
        public async Task<bool> InsertImageAsync(Image image)
        {
            _unitOfWork.Image.InsertItem(image);

            return (await _unitOfWork.Commit(Messages.InsertImageAsync))>0;
        }

        public async Task<bool> UpdateImageAsync(Image image)
        {
            await _unitOfWork.Image.UpdateItem(image);

            return (await _unitOfWork.Commit(Messages.UpdateImageAsync)) > 0;
        }
    }
}
