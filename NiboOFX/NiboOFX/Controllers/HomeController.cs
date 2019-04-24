using NiboOFX.Models.DAO;
using NiboOFX.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NiboOFX.Controllers
{
    public class HomeController : Controller
    {
        public Models.NiboOFXEntities1 context = new Models.NiboOFXEntities1();

        public ActionResult Index()
        {
            List<Models.OFX> list = new List<Models.OFX>();
            
            list = context.OFX.ToList();

            return View(list);
        }

        public ActionResult Save(HttpPostedFileBase OfxFile)
        {
            try
            {
                if (OfxFile.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(OfxFile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Files/OFX"), fileName);
                    OfxFile.SaveAs(path);

                    string xmlPath = NiboTools.SaveXml(path);

                    //Reading OFX and Save in List.
                    OFXList ofx = NiboTools.ReadOfx(xmlPath);

                    context.OFX.AddRange(ofx.OfxList.Select(x => new Models.OFX
                    {
                        CheckNum = x.CheckNum,
                        DtPosted = x.DtPosted,
                        FitId = x.FitId,
                        IdTrnType = x.IdTrnType,
                        Memo = x.Memo,
                        TrNamt = x.TrNamt
                    }).ToList());

                    try
                    {
                        context.SaveChanges();
                        System.IO.File.Delete(xmlPath);
                    }
                    catch(Exception ex)
                    {
                        throw;
                    }
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Message = "Ocorreu uma falha ao enviar o arquivo!";
                return RedirectToAction("Index"); 
            }         
        }
    }
}