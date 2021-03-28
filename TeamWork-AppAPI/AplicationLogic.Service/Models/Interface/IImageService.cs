using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamWork.DataAccess.Domain.Models.Domain;

namespace TeamWork.ApplicationLogic.Service.Models.Interface
{
    public interface IImageService
    {
        Task<Image> GetImageAsync(string userEmail);
        Task<bool> InsertImageAsync(Image image);
        Task<bool> UpdateImageAsync(Image image);
    }
}
