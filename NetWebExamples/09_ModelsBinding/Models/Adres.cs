
namespace _09_ModelsBinding.Models
{
    public class Adres
    {
       
        public  string AdresTanim { get; set; }=string.Empty;
        public string? Sehir {  get; set; }//? bir veri tipinin yanında ise allow null yani veri tabanında boş geçilebilir tiki bırakır.
    }
}
