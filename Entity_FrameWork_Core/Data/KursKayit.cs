namespace Entity_FrameWork_Core.Data;


public class KursKayit
{
    public int KursKayitId { get; set; }

    public int OgrenciId { get; set; }

    public Ogrenci? ogrenci { get; set; }
    public int KursId { get; set; }

    public Kurs? kurs { get; set; }

    public DateTime KayitTarih { get; set; }
}