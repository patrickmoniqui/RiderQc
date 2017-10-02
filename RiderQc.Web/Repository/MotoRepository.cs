using System.Collections.Generic;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Moto;
using RiderQc.Web.DAL.Interface;
using System;

namespace RiderQc.Web.Repository
{
    public class MotoRepository : IMotoRepository
    {
        private readonly IMotoDao dao;
        //THIS IS HARDCODED BEFORE ADDING AUTHENTIFICATION, TESTING PURPOSES -Benjamin Lam
        public int UserHardCoded = 7;

        public MotoRepository(IMotoDao _dao)
        {
            dao = _dao;
        }
        public bool CreateMoto(MotoViewModel motoViewModel)
        {
            Moto moto = new Moto(motoViewModel);
            moto.UserId = UserHardCoded;
            return dao.CreateMoto(moto);
        }

        public bool EditMoto(MotoViewModel motoViewModel)
        {
            Moto moto = new Moto(motoViewModel, motoViewModel.MotoId);
            moto.UserId = UserHardCoded;
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
    }
}