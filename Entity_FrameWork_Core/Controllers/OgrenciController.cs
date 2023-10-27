using Entity_FrameWork_Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entity_FrameWork_Core.Controllers;

public class OgrenciController : Controller
{
    private readonly DataContext _context;

    public OgrenciController(DataContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(){

        
        return View( await _context.Ogrenciler.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Create(Ogrenci model)
    {
        _context.Ogrenciler.Add(model);
         await _context.SaveChangesAsync();
        return RedirectToAction("Index","ogrenci");
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

    [HttpGet]
    public async Task<IActionResult> delete(int? id)
    {
        if(id == null){
            return NotFound();
        }

        var ogrenci = await _context.Ogrenciler.FindAsync(id);

        if(ogrenci == null)
        {
            return NotFound();
        }
        return View(ogrenci);
    }

    [HttpPost]
    public async Task<IActionResult> delete([FromForm]int id)
    {
        var ogrenci = await _context.Ogrenciler.FindAsync(id);
        if(ogrenci == null){
            return NotFound();
        }
        _context.Ogrenciler.Remove(ogrenci);
        await _context.SaveChangesAsync();
        return RedirectToAction("index");
    }

}