using Adobe.Lr.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adobe.Lr.Data
{
    public interface ILightroomRepository
    {
        ICollection<PictureModel> GetAllPictures();
    }
}
