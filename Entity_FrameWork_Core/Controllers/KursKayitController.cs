using Entity_FrameWork_Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Entity_FrameWork_Core.Controllers;


public class KursKayitController : Controller
{
    private readonly DataContext _context;
    public KursKayitController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var Kurskayitlari = await _context.KursKayitlari.Include(x => x.ogrenci).Include(x => x.kurs).ToListAsync();

        return View(Kurskayitlari);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Ogrenciler = new SelectList( await _context.Ogrenciler.ToListAsync() , "OgrenciId" , "Ad_Soyad");
        ViewBag.Kurslar = new SelectList( await _context.Kurslar.ToListAsync() , "KursId" , "Baslik");

        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(KursKayit model){
        model.KayitTarih = DateTime.Now;
        _context.KursKayitlari.Add(model);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }



}