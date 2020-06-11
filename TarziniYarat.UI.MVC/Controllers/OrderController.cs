using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TarziniYarat.BusinessLogic.Abstract;
using TarziniYarat.Model;
using TarziniYarat.UI.MVC.Filtres;

namespace TarziniYarat.UI.MVC.Controllers
{
    [CustomAuthorize(Roles = "Admin,Uye,Modelist")]
    public class OrderController : Controller
    {
        IShipperService _shipperService;

        public OrderController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }
        public ActionResult PersonDetail()
        {
            GetShipperToDLL();
            return View();
        }

        private void GetShipperToDLL()
        {
            List<Shipper> shippers = new List<Shipper>();
            foreach (Shipper item in _shipperService.GetAll())
            {
                shippers.Add(new Shipper { CompanyName = item.CompanyName, ShipperID = item.ShipperID });
            }
            ViewBag.Shippers = shippers;
        }
    }
}