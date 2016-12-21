using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Xml.Serialization;
using GunShop.Models;
using GunShop.Data;

namespace GunShop.Controllers
{
    public class FileController : Controller
    {
        private IHostingEnvironment hostingEnvironment;
        private Random random;
        private ApplicationDbContext _context;

        public FileController(IHostingEnvironment _hostingEnvironment,
            ApplicationDbContext context)
        {
            hostingEnvironment = _hostingEnvironment;
            random = new Random();
            _context = context;
        }

        private async Task<string> addFile(IFormFile file)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int len = 10;
            var filename = file.FileName;
            var extension = filename.Split('.').Last();
            filename = filename.Replace(extension, "");
            filename = new string(Enumerable.Repeat(chars, len)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            filename = filename + "." + extension;
            var uploads = Path.Combine(hostingEnvironment.WebRootPath, "temp");
            var filepath = Path.Combine(uploads, filename);
            using (var fileStream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filepath;
        }

        public async Task<IActionResult> AddManufacturersXML(IFormFile file)
        {
            var res = LoadXML<Manufacturer>(await addFile(file));
            _context.Manufacturers.AddRange(res);
            await _context.SaveChangesAsync();
            return Content(res.Count.ToString());
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile file)
        {
            return Content(await addFile(file));
        }


        public static void SaveXML<T>(string filePath, List<T> elems)
        {
            XmlSerializer writer = new XmlSerializer(typeof(List<T>));
            using (var file = new StreamWriter(new FileStream(filePath, FileMode.Open)))
            {
                writer.Serialize(file, elems);
            }
        }

        public static List<T> LoadXML<T>(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
            {
                throw new ArgumentException("File not found!");
            }
            XmlSerializer writer = new XmlSerializer(typeof(List<T>));
            using (var file = new StreamReader(new FileStream(filePath, FileMode.Open)))
            {
                return (List<T>)writer.Deserialize(file);
            }
        }
    }
}