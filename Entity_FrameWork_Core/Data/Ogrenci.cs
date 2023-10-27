namespace Entity_FrameWork_Core.Data;

public class Ogrenci{
    public int OgrenciId { get; set; }
    public string? OgrenciAd{ get; set; }
    public string? OgrenciSoyad{ get; set; }

    public string Ad_Soyad { 
        get{
            return this.OgrenciAd + " " + OgrenciSoyad ;
    } 
    }

    public string? Email { get; set; }

    public string? Telefon { get; set; }

    public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

}