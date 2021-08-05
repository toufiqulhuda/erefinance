using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Erefinance.Data;
using Erefinance.Models;
using System.Security.Cryptography;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace Erefinance.Controllers
{
    public class StatusEntryController : Controller
    {
        private readonly ApplicationDBContext _context;

        public StatusEntryController(ApplicationDBContext context)
        {
            _context = context;
        }

        
        // GET: StatusEntry
        public  ActionResult Index()
        {
            var query = new List<StatusEntryModel>();
            if (HttpContext.Session.GetString("userType").Equals("User")) {

                query = (from se in _context.statusEntry
                         join d in _context.district on se.DistrictName equals d.DistrictCode.ToString()
                         join c in _context.category on se.SMECategory equals c.Id.ToString()
                         join s in _context.sector on se.TypesofSector equals s.Id.ToString()

                         where se.BranchCode.Equals(Convert.ToInt32(HttpContext.Session.GetString("brCode")))
                         orderby se.Id descending
                         select new StatusEntryModel
                         {
                             Id = se.Id,
                             DistrictName = d.DistrictName,
                             SMECategory = c.CategoryName,
                             TypesofSector = s.SectorName,
                             DisbursedFemaleEnterprise = se.DisbursedFemaleEnterprise,
                             DisbursedFemaleAmount = se.DisbursedFemaleAmount,
                             DisbursedMaleEnterprise = se.DisbursedMaleEnterprise,
                             DisbursedMaleAmount = se.DisbursedMaleAmount,
                             ApprovedFemaleEnterprise = se.ApprovedFemaleEnterprise,
                             ApprovedFemaleAmount = se.ApprovedFemaleAmount,
                             ApprovedMaleEnterprise = se.ApprovedMaleEnterprise,
                             ApprovedMaleAmount = se.ApprovedMaleAmount

                         }).ToList();

            }

            else if (HttpContext.Session.GetString("userType").Equals("Admin"))
            {

                query = (from se in _context.statusEntry
                         join d in _context.district on se.DistrictName equals d.DistrictCode.ToString()
                         join c in _context.category on se.SMECategory equals c.Id.ToString()
                         join s in _context.sector on se.TypesofSector equals s.Id.ToString()
                         orderby se.Id descending
                         select new StatusEntryModel
                         {
                             Id = se.Id,
                             DistrictName = d.DistrictName,
                             SMECategory = c.CategoryName,
                             TypesofSector = s.SectorName,
                             DisbursedFemaleEnterprise = se.DisbursedFemaleEnterprise,
                             DisbursedFemaleAmount = se.DisbursedFemaleAmount,
                             DisbursedMaleEnterprise = se.DisbursedMaleEnterprise,
                             DisbursedMaleAmount = se.DisbursedMaleAmount,
                             ApprovedFemaleEnterprise = se.ApprovedFemaleEnterprise,
                             ApprovedFemaleAmount = se.ApprovedFemaleAmount,
                             ApprovedMaleEnterprise = se.ApprovedMaleEnterprise,
                             ApprovedMaleAmount = se.ApprovedMaleAmount

                         }).ToList();

            }


            return View(query);
        }

        public IActionResult Create()
        {

            try {
                List<DistrictModel> districtlist = new List<DistrictModel>();
                List<FundModel> fundlist = new List<FundModel>();
                List<CategoryModel> categorylist = new List<CategoryModel>();
                List<SectorModel> sectorlist = new List<SectorModel>();

                districtlist = (from dist in _context.district select dist).ToList();
                fundlist = (from funds in _context.fund select funds).ToList();
                categorylist = (from categorys in _context.category select categorys).ToList();
                sectorlist = (from sectors in _context.sector select sectors).ToList();

                districtlist.Insert(0, new DistrictModel { DistrictCode = 0, DistrictName = "Select District" });
                fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
                categorylist.Insert(0, new CategoryModel { Id = 0, CategoryName = "Select Category" });
                sectorlist.Insert(0, new SectorModel { Id = 0, SectorName = "Select Sector" });

                ViewBag.distlist = districtlist;
                ViewBag.fundlists = fundlist;
                ViewBag.categorylists = categorylist;
                ViewBag.sectorlists = sectorlist;

            }
            catch (Exception e) {
                //Console.WriteLine(e.Message);
                TempData["errorMsg"] = e.Message;
            }
            
            return View();
        }

        // POST: StatusEntry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(StatusEntryModel statusEntryModel) 
        {
            try 
            {                
                if (ModelState.IsValid)
                {
                    statusEntryModel.BranchName = HttpContext.Session.GetString("brName");
                    statusEntryModel.BranchCode = int.Parse(HttpContext.Session.GetString("brCode"));
                    statusEntryModel.EntryBy = HttpContext.Session.GetString("userId"); 
                    statusEntryModel.EntryDate = DateTime.Now;
                    _context.statusEntry.Add(statusEntryModel);
                    _context.SaveChanges();
                    TempData["errorMsg"] = "Saved Successfully.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    var errors = ModelState
                                    .Where(x => x.Value.Errors.Count > 0)
                                    .Select(x => new { x.Key, x.Value.Errors })
                                    .ToArray();
                }
            }
            catch (Exception e)
            {
                TempData["errorMsg"] = e.Message;
            } 
            return View(statusEntryModel);
        }

        // GET: StatusEntry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusEntryModel = await _context.statusEntry.FindAsync(id);

            if (statusEntryModel == null)
            {
                return NotFound();
            }
            List<DistrictModel> districtlist = new List<DistrictModel>();
            List<FundModel> fundlist = new List<FundModel>();
            List<CategoryModel> categorylist = new List<CategoryModel>();
            List<SectorModel> sectorlist = new List<SectorModel>();

            districtlist = (from dist in _context.district select dist).ToList();
            fundlist = (from funds in _context.fund select funds).ToList();
            categorylist = (from categorys in _context.category select categorys).ToList();
            sectorlist = (from sectors in _context.sector select sectors).ToList();

            districtlist.Insert(0, new DistrictModel { DistrictCode = 0, DistrictName = "Select District" });
            fundlist.Insert(0, new FundModel { FundCode = 0, FundName = "Select Fund" });
            categorylist.Insert(0, new CategoryModel { Id = 0, CategoryName = "Select Category" });
            sectorlist.Insert(0, new SectorModel { Id = 0, SectorName = "Select Sector" });

            ViewBag.distlist = districtlist;
            ViewBag.fundlists = fundlist;
            ViewBag.categorylists = categorylist;
            ViewBag.sectorlists = sectorlist;
            return View(statusEntryModel);
        }

        // POST: StatusEntry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost, AutoValidateAntiforgeryToken]
        public ActionResult Edit(int id, StatusEntryModel statusEntryModel)
        {
            if (id != statusEntryModel.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var aStatusEntryModel = _context.statusEntry.Where(s => s.Id == statusEntryModel.Id).FirstOrDefault();
                    _context.statusEntry.Remove(aStatusEntryModel);
                    
                    statusEntryModel.ReportDate = aStatusEntryModel.ReportDate;
                    statusEntryModel.EntryBy = aStatusEntryModel.EntryBy;
                    statusEntryModel.EntryDate = aStatusEntryModel.EntryDate;
                    statusEntryModel.BranchName = aStatusEntryModel.BranchName;
                    statusEntryModel.BranchCode = aStatusEntryModel.BranchCode;
                    statusEntryModel.UpdateBy = HttpContext.Session.GetString("userId");
                    statusEntryModel.UpdateDate = DateTime.Now;
                    _context.statusEntry.Add(statusEntryModel);
                    _context.Update(statusEntryModel);
                    _context.SaveChanges();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusEntryModelExists(statusEntryModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));

            }

            return RedirectToAction("Index");
        }

        

        // GET: StatusEntry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statusEntryModel = await _context.statusEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusEntryModel == null)
            {
                return NotFound();
            }

            return View(statusEntryModel);
        }

        // POST: StatusEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statusEntryModel = await _context.statusEntry.FindAsync(id);
            _context.statusEntry.Remove(statusEntryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusEntryModelExists(int id)
        {
            return _context.statusEntry.Any(e => e.Id == id);
        }
    }
}
