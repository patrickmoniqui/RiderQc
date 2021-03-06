﻿using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Moto;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface IMotoDao
    {
        bool CreateMoto(Moto moto);
        bool EditMoto(Moto moto);
        bool DeleteMoto(int motoId);
        bool UserHasAccess(int motoId, string username);
        Moto GetMoto(int motoId);
        List<Moto> GetAllMotos();
    }
}
