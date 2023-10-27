using System.ComponentModel.DataAnnotations;

namespace Entity_FrameWork_Core.Data;

public class Kurs
{
    [Required]
    public int KursId { get; set; }

    public string? Baslik { get; set; }

    public int? OgretmenId { get; set; }

    public Ogretmen Ogretmen { get; set; } = null!;

    public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

}