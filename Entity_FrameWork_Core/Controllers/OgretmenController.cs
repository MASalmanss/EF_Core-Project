using Entity_FrameWork_Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Entity_FrameWork_Core.Controllers;

public class OgretmenController : Controller
{
    private readonly DataContext _context;
    public OgretmenController(DataContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> Index()
    {
        return View(await _context.Ogretmenler.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Ogretmen model)
    {
        _context.Ogretmenler.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }




    [HttpGet]
    public async Task<IActionResult> edit(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        else
        {
            var ogr = await _context.
                                    Ogrenciler.
                                    Include(o =>o.KursKayitlari).ThenInclude( o => o.kurs).FirstOrDefaultAsync(o => o.OgrenciId == id);
                                    
                    

            if (ogr == null){

                return NotFound();
            }
            else
            {
                return View(ogr);
            }
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> edit(int id ,Ogrenci model){



        if(id != model.OgrenciId){ 
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            try{
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException){
                if(!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId)){
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("index");
        }
        return View(model);
    }
}