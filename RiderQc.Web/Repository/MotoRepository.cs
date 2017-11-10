using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Moto;
using System.Collections.Generic;

namespace RiderQc.Web.Repository
{
    public class MotoRepository : IMotoRepository
    {
        private readonly IMotoDao dao;

        public MotoRepository(IMotoDao _dao)
        {
            dao = _dao;
        }
        
        public bool CreateMoto(MotoViewModel motoViewModel)
        {
            Moto moto = new Moto();
            moto.UserId = motoViewModel.UserId;
            moto.Brand = motoViewModel.Brand;
            moto.Model = motoViewModel.Model;
            moto.Year = motoViewModel.Year;
            moto.Type = motoViewModel.Type;
            return dao.CreateMoto(moto);
        }
        
        public bool EditMoto(MotoViewModel motoViewModel)
        {
            Moto moto = new Moto();
            moto.UserId = motoViewModel.UserId;
            moto.Brand = motoViewModel.Brand;
            moto.Model = motoViewModel.Model;
            moto.Year = motoViewModel.Year;
            moto.Type = motoViewModel.Type;

            return dao.EditMoto(moto);
        }
        
        public bool DeleteMoto(int motoId)
        {
            return dao.DeleteMoto(motoId);
        }

        public Moto GetMoto(int motoId)
        {
            return dao.GetMoto(motoId);
        }

        public List<Moto> GetAllMotos()
        {
            return dao.GetAllMotos();
        }

        public bool UserHasAccess(int motoId, string username)
        {
            if (motoId <= 0 || string.IsNullOrWhiteSpace(username))
            {
                return false;
            }
            return dao.UserHasAccess(motoId, username);
        }
    }
}