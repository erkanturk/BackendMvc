using _16_Web_Api.DataContext;
using _16_Web_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _16_Web_Api.Controllers
{
    [Route("api/[controller]/[action]")]//Api endpoint rotasını belirttik
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {//Pagination bir sayfaya kaç adet ürün yükleneceğini ve sistem optimizasyonu için en önemli yapılardan biridir.
            if (_context.Products.Count() > 0)
            {
                var totalCount = await _context.Products.CountAsync();
                var products = await _context.Products
                    .Skip((page - 1) * pageSize)//Sayfa numarasına göre atanması gereken ürün sayısını hesaplar
                    .Take(pageSize)//Belirli sayıda ürün alır
                    .ToListAsync();//Ürünleri asenkron bir şekilde listeler.
                Response.Headers.Add("X-Total-Count", totalCount.ToString());//ürün sayısını header kısmına ekle
                Response.Headers.Add("X-Page", page.ToString());//Mevcut sayfa numarasını header a ekle
                Response.Headers.Add("X-Page-Size", pageSize.ToString());//Sayfa başına düşen ürün miktarını header a ekle

                return products;

            }
            return NotFound("Ürün bulunamadı");//404 sonucu
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product is null)
            {
                return NotFound($"ID: {id} olan ürün bulunamadı");
            }
            return product;//status ok 200
        }
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ürün eklenirken bir hata oluştu: {ex.Message}");
            }
        }
        [HttpPut]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            var products = _context.Products.FindAsync(id);
            if (id == null || id == 0)
            {
                return BadRequest("İd bulunamadı");
            }
            if (id != product.Id)
            {
                return BadRequest("Gönderilen değerde ürün bulunamadı");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
                {
                    return NotFound($"Id: {id} olan ürün bulunamadı");
                }
                else
                {
                    return StatusCode(500, $"Bir hata oluştu eş zamanlılık çatışması sonra tekrar deneyin");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Bir hata oluştu" + ex.Message);
            }
            return NoContent();//204 kodunu dönder
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound($"Id: {id} olan ürün bulunamadı");
                }
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return Ok($"ID: {id} olan ürün başarı ile silindi");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Ürün silinirken hata meydana geldi " + ex.Message);
            }
        }
        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
