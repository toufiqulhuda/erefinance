using Microsoft.AspNetCore.Mvc;
using Erefinance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Erefinance.Data;
using Microsoft.EntityFrameworkCore;

namespace Erefinance.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ReportsController(ApplicationDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult DisbursmentTarget()
        {

            return View();
        }
        public ActionResult DistrictWise()
        {
            try
            {
                List<DistrictModel> districtlist = new List<DistrictModel>();
                List<FundModel> fundlist = new List<FundModel>();
                districtlist = (from dist in _context.district select dist).ToList();
                fundlist = (from funds in _context.fund select funds).ToList();
                districtlist.Insert(0, new DistrictModel { DistrictCode = 0, DistrictName = "Select District" });
                fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
                ViewBag.distlist = districtlist;
                ViewBag.fundlists = fundlist;
            }
            catch (Exception e)
            {
                TempData["errorMsg"] = e.Message;
            }
            return View();
        }

        [HttpPost]
        
        public ActionResult DistrictWise(string district_id_list, string fund_id_list, DateTime start_date, DateTime end_date) {

            List<DistrictModel> districtlist = new List<DistrictModel>();
            List<FundModel> fundlist = new List<FundModel>();
            districtlist = (from dist in _context.district select dist).ToList();
            fundlist = (from funds in _context.fund select funds).ToList();
            districtlist.Insert(0, new DistrictModel { DistrictCode = 0, DistrictName = "Select District" });
            fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
            ViewBag.distlist = districtlist;
            ViewBag.fundlists = fundlist;

            //ViewBag.districtName = district_id_list;
            ViewBag.fundName = fundlist.Find(m => (m.FundCode == Convert.ToInt32(fund_id_list))).FundName;
            ViewBag.startDate = start_date.Date.ToString("dd-MMM-yy");
            ViewBag.endDate = end_date.Date.ToString("dd-MMM-yy");
            ViewBag.reportPrintDate = DateTime.Now.ToString("dd-MMM-yyyy, h:m tt");


            var query = (from se in _context.statusEntry
                         join f in _context.fund on se.FundName equals f.FundCode.ToString()
                         join d in _context.district on se.DistrictName equals d.DistrictCode.ToString()
                         where se.DistrictName.Contains(district_id_list)
                         where se.FundName.Contains(fund_id_list)
                         where se.ReportDate.Date >= start_date.Date
                         where se.ReportDate.Date <= end_date.Date
                         //group se by se.DistrictName into g
                         //from y in g
                         select new StatusEntryModel
                         {
                             //se.DisbursedFemaleEnterprise,
                             //se.DisbursedMaleEnterprise,
                             //se.DisbursedFemaleAmount,
                             //se.DisbursedMaleAmount,
                             //se.ApprovedFemaleEnterprise,
                             //se.ApprovedMaleEnterprise,
                             //se.ApprovedFemaleAmount,
                             //se.ApprovedMaleAmount,                     
                             DisbursedFemaleEnterprise = se.DisbursedFemaleEnterprise,
                             DisbursedMaleEnterprise = se.DisbursedMaleEnterprise,
                             DisbursedFemaleAmount = se.DisbursedFemaleAmount,
                             DisbursedMaleAmount = se.DisbursedMaleAmount,
                             ApprovedFemaleEnterprise = se.ApprovedFemaleEnterprise,
                             ApprovedMaleEnterprise = se.ApprovedMaleEnterprise,
                             ApprovedFemaleAmount = se.ApprovedFemaleAmount,
                             ApprovedMaleAmount = se.ApprovedMaleAmount,
                             DistrictName = d.DistrictName
                             //FundName = f.FundName
                             //TotalDisbEnterprise = g.Sum(x => x.DisbursedFemaleEnterprise),
                             //SL = g.Count(),
                             //TotalDisbEnterprise = g.Sum(x => x.DisbursedFemaleEnterprise + x.DisbursedMaleEnterprise),
                             //TotalDisbAmount = g.Sum(x => x.DisbursedFemaleAmount + x.DisbursedMaleAmount),
                             //TotalApprEnterprise = g.Sum(x => x.ApprovedFemaleEnterprise + x.ApprovedMaleEnterprise),
                             //TotalApprAmount = g.Sum(x => x.ApprovedFemaleAmount + x.ApprovedMaleAmount),
                             //DistrictName = y.DistrictName

                         }).ToList();


            //ViewBag.DistRpt = query;
            //Console.WriteLine("Value: " + query);
            //return  Content("Hello, " + HttpContext.Request.Form["district_id_list"] + ". You are " + HttpContext.Request.Form["fund_id_list"] + " years old!" + HttpContext.Request.Form["start_date"] +"" + HttpContext.Request.Form["end_date"]);
            return View(query);
        }
        public ActionResult SmeCategoryWise()
        {
            List<FundModel> fundlist = new List<FundModel>();
            fundlist = (from funds in _context.fund select funds).ToList();
            fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
            ViewBag.fundlists = fundlist;

            List<CategoryModel> categorylist = new List<CategoryModel>();
            categorylist = (from categorys in _context.category select categorys).ToList();
            categorylist.Insert(0, new CategoryModel { Id = 0, CategoryName = "Select Category" });
            ViewBag.categorylists = categorylist;

            return View();
        }

        [HttpPost]
        public ActionResult SmeCategoryWise(string smecat_id_list, string fund_id_list, DateTime start_date, DateTime end_date)
        {
            List<FundModel> fundlist = new List<FundModel>();
            fundlist = (from funds in _context.fund select funds).ToList();
            fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
            ViewBag.fundlists = fundlist;

            List<CategoryModel> categorylist = new List<CategoryModel>();
            categorylist = (from categorys in _context.category select categorys).ToList();
            categorylist.Insert(0, new CategoryModel { Id = 0, CategoryName = "Select Category" });
            ViewBag.categorylists = categorylist;

            ViewBag.fundName = fundlist.Find(m => (m.FundCode == Convert.ToInt32(fund_id_list))).FundName;
            ViewBag.startDate = start_date.Date.ToString("dd-MMM-yy");
            ViewBag.endDate = end_date.Date.ToString("dd-MMM-yy");
            ViewBag.reportPrintDate = DateTime.Now.ToString("dd-MMM-yyyy, h:m tt");

            var query = (from se in _context.statusEntry
                         join f in _context.fund on se.FundName equals f.FundCode.ToString()
                         join c in _context.category on se.SMECategory equals c.Id.ToString()
                         where se.SMECategory.Contains(smecat_id_list)
                         where se.FundName.Contains(fund_id_list)
                         where se.ReportDate.Date >= start_date.Date
                         where se.ReportDate.Date <= end_date.Date
                         //group se by se.DistrictName into g
                         //from y in g
                         select new StatusEntryModel
                         {
                             //se.DisbursedFemaleEnterprise,
                             //se.DisbursedMaleEnterprise,
                             //se.DisbursedFemaleAmount,
                             //se.DisbursedMaleAmount,
                             //se.ApprovedFemaleEnterprise,
                             //se.ApprovedMaleEnterprise,
                             //se.ApprovedFemaleAmount,
                             //se.ApprovedMaleAmount,                     
                             DisbursedFemaleEnterprise = se.DisbursedFemaleEnterprise,
                             DisbursedMaleEnterprise = se.DisbursedMaleEnterprise,
                             DisbursedFemaleAmount = se.DisbursedFemaleAmount,
                             DisbursedMaleAmount = se.DisbursedMaleAmount,
                             ApprovedFemaleEnterprise = se.ApprovedFemaleEnterprise,
                             ApprovedMaleEnterprise = se.ApprovedMaleEnterprise,
                             ApprovedFemaleAmount = se.ApprovedFemaleAmount,
                             ApprovedMaleAmount = se.ApprovedMaleAmount,
                             SMECategory = c.CategoryName
                             //FundName = f.FundName
                             //TotalDisbEnterprise = g.Sum(x => x.DisbursedFemaleEnterprise),
                             //SL = g.Count(),
                             //TotalDisbEnterprise = g.Sum(x => x.DisbursedFemaleEnterprise + x.DisbursedMaleEnterprise),
                             //TotalDisbAmount = g.Sum(x => x.DisbursedFemaleAmount + x.DisbursedMaleAmount),
                             //TotalApprEnterprise = g.Sum(x => x.ApprovedFemaleEnterprise + x.ApprovedMaleEnterprise),
                             //TotalApprAmount = g.Sum(x => x.ApprovedFemaleAmount + x.ApprovedMaleAmount),
                             //DistrictName = y.DistrictName

                         }).ToList();

            return View(query);
        }


        public ActionResult SectorWise()
        {
            List<SectorModel> sectorlist = new List<SectorModel>();
            sectorlist = (from sectors in _context.sector select sectors).ToList();
            sectorlist.Insert(0, new SectorModel { Id = 0, SectorName = "Select Sector" });
            ViewBag.sectorlists = sectorlist;

            List<FundModel> fundlist = new List<FundModel>();
            fundlist = (from funds in _context.fund select funds).ToList();
            fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
            ViewBag.fundlists = fundlist;


            return View();
        }

        [HttpPost]
        public ActionResult SectorWise(string sector_id_list, string fund_id_list, DateTime start_date, DateTime end_date)
        {
            List<SectorModel> sectorlist = new List<SectorModel>();
            sectorlist = (from sectors in _context.sector select sectors).ToList();
            sectorlist.Insert(0, new SectorModel { Id = 0, SectorName = "Select Sector" });
            ViewBag.sectorlists = sectorlist;

            List<FundModel> fundlist = new List<FundModel>();
            fundlist = (from funds in _context.fund select funds).ToList();
            fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
            ViewBag.fundlists = fundlist;

            ViewBag.fundName = fundlist.Find(m => (m.FundCode == Convert.ToInt32(fund_id_list))).FundName;
            ViewBag.startDate = start_date.Date.ToString("dd-MMM-yy");
            ViewBag.endDate = end_date.Date.ToString("dd-MMM-yy");
            ViewBag.reportPrintDate = DateTime.Now.ToString("dd-MMM-yyyy, h:m tt");

            var query = (from se in _context.statusEntry
                         join f in _context.fund on se.FundName equals f.FundCode.ToString()
                         join s in _context.sector on se.TypesofSector equals s.Id.ToString()
                         where se.TypesofSector.Contains(sector_id_list)
                         where se.FundName.Contains(fund_id_list)
                         where se.ReportDate.Date >= start_date.Date
                         where se.ReportDate.Date <= end_date.Date
                         //group se by se.DistrictName into g
                         //from y in g
                         select new StatusEntryModel
                         {
                             //se.DisbursedFemaleEnterprise,
                             //se.DisbursedMaleEnterprise,
                             //se.DisbursedFemaleAmount,
                             //se.DisbursedMaleAmount,
                             //se.ApprovedFemaleEnterprise,
                             //se.ApprovedMaleEnterprise,
                             //se.ApprovedFemaleAmount,
                             //se.ApprovedMaleAmount,                     
                             DisbursedFemaleEnterprise = se.DisbursedFemaleEnterprise,
                             DisbursedMaleEnterprise = se.DisbursedMaleEnterprise,
                             DisbursedFemaleAmount = se.DisbursedFemaleAmount,
                             DisbursedMaleAmount = se.DisbursedMaleAmount,
                             ApprovedFemaleEnterprise = se.ApprovedFemaleEnterprise,
                             ApprovedMaleEnterprise = se.ApprovedMaleEnterprise,
                             ApprovedFemaleAmount = se.ApprovedFemaleAmount,
                             ApprovedMaleAmount = se.ApprovedMaleAmount,
                             TypesofSector = s.SectorName
                             //FundName = f.FundName
                             //TotalDisbEnterprise = g.Sum(x => x.DisbursedFemaleEnterprise),
                             //SL = g.Count(),
                             //TotalDisbEnterprise = g.Sum(x => x.DisbursedFemaleEnterprise + x.DisbursedMaleEnterprise),
                             //TotalDisbAmount = g.Sum(x => x.DisbursedFemaleAmount + x.DisbursedMaleAmount),
                             //TotalApprEnterprise = g.Sum(x => x.ApprovedFemaleEnterprise + x.ApprovedMaleEnterprise),
                             //TotalApprAmount = g.Sum(x => x.ApprovedFemaleAmount + x.ApprovedMaleAmount),
                             //DistrictName = y.DistrictName

                         }).ToList();

            return View(query);
        }
        public ActionResult GenderWise()
        {
            List<FundModel> fundlist = new List<FundModel>();
            fundlist = (from funds in _context.fund select funds).ToList();
            fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
            ViewBag.fundlists = fundlist;

            return View();
        }
        [HttpPost]
        public ActionResult GenderWise(string fund_id_list, DateTime start_date, DateTime end_date)
        {
            List<FundModel> fundlist = new List<FundModel>();
            fundlist = (from funds in _context.fund select funds).ToList();
            fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
            ViewBag.fundlists = fundlist;

            ViewBag.fundName = fundlist.Find(m => (m.FundCode == Convert.ToInt32(fund_id_list))).FundName;
            ViewBag.startDate = start_date.Date.ToString("dd-MMM-yy");
            ViewBag.endDate = end_date.Date.ToString("dd-MMM-yy");
            ViewBag.reportPrintDate = DateTime.Now.ToString("dd-MMM-yyyy, h:m tt");

            var query = (from se in _context.statusEntry
                         join f in _context.fund on se.FundName equals f.FundCode.ToString()
                         //join s in _context.sector on se.TypesofSector equals s.Id.ToString()
                         //where se.TypesofSector.Contains(sector_id_list)
                         where se.FundName.Contains(fund_id_list)
                         where se.ReportDate.Date >= start_date.Date
                         where se.ReportDate.Date <= end_date.Date
                         //group se by se.DistrictName into g
                         //from y in g
                         select new StatusEntryModel
                         {
                             //se.DisbursedFemaleEnterprise,
                             //se.DisbursedMaleEnterprise,
                             //se.DisbursedFemaleAmount,
                             //se.DisbursedMaleAmount,
                             //se.ApprovedFemaleEnterprise,
                             //se.ApprovedMaleEnterprise,
                             //se.ApprovedFemaleAmount,
                             //se.ApprovedMaleAmount,                     
                             DisbursedFemaleEnterprise = se.DisbursedFemaleEnterprise,
                             DisbursedMaleEnterprise = se.DisbursedMaleEnterprise,
                             DisbursedFemaleAmount = se.DisbursedFemaleAmount,
                             DisbursedMaleAmount = se.DisbursedMaleAmount,
                             ApprovedFemaleEnterprise = se.ApprovedFemaleEnterprise,
                             ApprovedMaleEnterprise = se.ApprovedMaleEnterprise,
                             ApprovedFemaleAmount = se.ApprovedFemaleAmount,
                             ApprovedMaleAmount = se.ApprovedMaleAmount
                             //TypesofSector = s.SectorName
                             //FundName = f.FundName
                             //TotalDisbEnterprise = g.Sum(x => x.DisbursedFemaleEnterprise),
                             //SL = g.Count(),
                             //TotalDisbEnterprise = g.Sum(x => x.DisbursedFemaleEnterprise + x.DisbursedMaleEnterprise),
                             //TotalDisbAmount = g.Sum(x => x.DisbursedFemaleAmount + x.DisbursedMaleAmount),
                             //TotalApprEnterprise = g.Sum(x => x.ApprovedFemaleEnterprise + x.ApprovedMaleEnterprise),
                             //TotalApprAmount = g.Sum(x => x.ApprovedFemaleAmount + x.ApprovedMaleAmount),
                             //DistrictName = y.DistrictName

                         }).ToList();


            return View(query);
        }
    }
}
