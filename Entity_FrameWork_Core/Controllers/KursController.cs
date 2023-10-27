using Entity_FrameWork_Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Entity_FrameWork_Core.Controllers;

public class KursController : Controller
{

    public readonly DataContext _context;
    public KursController(DataContext context)
    {
        _context = context;
    }



    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]

    public async Task<IActionResult> Create(Kurs kurs){
        _context.Kurslar.Add(kurs);
        await _context.SaveChangesAsync();
        return RedirectToAction("index","Home");
    }

    public async Task<IActionResult> Index()
    {
        var kurs = await _context.Kurslar.ToListAsync();
        return View(kurs);
    }

    public async Task<IActionResult> edit(int? id)
    {
        if(id == null){
            return NotFound();
        }
        else
        {
            var kurs = await _context.Kurslar.Include( k => k.KursKayitlari).ThenInclude(k => k.ogrenci).FirstOrDefaultAsync(k => k.KursId == id);
            if(kurs ==null){
                return NotFound();
            }
            return View(kurs);
            
        }

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> edit(int id ,Kurs model){



        if(id != model.KursId){ 
            return NotFound();
        }
        if(ModelState.IsValid)
        {
            try{
                _context.Update(model);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException){
                if(!_context.Kurslar.Any(o => o.KursId == model.KursId)){
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

        var kurslar = await _context.Kurslar.FindAsync(id);

        if(kurslar == null)
        {
            return NotFound();
        }
        return View(kurslar);
    }

    [HttpPost]
    public async Task<IActionResult> delete([FromForm]int id)
    {
        var kurs = await _context.Kurslar.FindAsync(id);
        if(kurs == null){
            return NotFound();
        }
        _context.Kurslar.Remove(kurs);
        await _context.SaveChangesAsync();
        return RedirectToAction("index");
    }

    
}