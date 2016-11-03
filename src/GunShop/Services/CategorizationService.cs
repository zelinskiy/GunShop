using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GunShop.Data;
using GunShop.Models;
using Microsoft.Extensions.Logging;
using GunShop.Models.Interfaces;

namespace GunShop.Services
{
    public class CategorizationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public CategorizationService(
            ILoggerFactory loggerFactory,
            ApplicationDbContext context)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<ChartService>();
        }

        public void AddCommodityTypeToCategory(
            CommodityType ct, 
            Category cat, 
            IEnumerable<CharacteristicValue> charvals)
        {
            var charsToFill = _context
                .Characteristics
                .Where(c => c.CategoryId == cat.Id)
                .ToArray();
            foreach(var c in charsToFill)
            {
                var charval = charvals.FirstOrDefault(cv => cv.CharacteristicId == c.Id);
                if (charval != null)
                {
                    SetCharacteristicValue(charval);
                }
                else
                {
                    throw new ArgumentException($"Characteristic {c.Name}({c.Id}) value not filled");
                }
            }
            var newConnection = new CommodityTypeInCathegory
            {
                CategoryId = cat.Id,
                CommodityTypeId = ct.Id
            };
            _context.CommoditiesTypesInCathegories.Add(newConnection);
            _context.SaveChanges();
        }

        public void RemoveCommodityTypeFromCategory(ICommodityType ct, ICategory cat)
        {
            var oldConnection =
                _context.CommoditiesTypesInCathegories.FirstOrDefault(
                    conn => conn.CommodityTypeId == ct.Id && conn.CategoryId == cat.Id);
            if (oldConnection == null)
            {
                throw new ArgumentException($"Commodity {ct.Id} not in Category {cat.Id}");
            }
            var oldCharVals = _context
                .CharacteristicValues
                .Where(cv =>
                    cv.CommodityTypeId == ct.Id
                    && cv.CharacteristicId == cat.Id)
                .ToArray();

            _context.CharacteristicValues.RemoveRange(oldCharVals);
            _context.CommoditiesTypesInCathegories.Remove(oldConnection);
            _context.SaveChanges();
        }

        public void AddCharacteristicToCategory(ICategory cat, Characteristic charact)
        {
            charact.CategoryId = cat.Id;
            if(_context.Characteristics.Count(ch=>ch.Id == charact.Id) > 0)
            {
                _context.Characteristics.Update(charact);
            }
            else
            {
                _context.Characteristics.Add(charact);
            }            
            _context.SaveChanges();
        }

        public void AddCategory(
            ICategory cat, 
            IEnumerable<Characteristic> characteristics)
        {
            _context.Categories.Add(new Category
            {
                MasterCategoryId = cat.MasterCategoryId,
                Name = cat.Name
            });
            _context.SaveChanges();
            foreach(var c in characteristics)
            {
                AddCharacteristicToCategory(cat, c);
            }

        }

        public void AddCategory(Category cat)
        {
            AddCategory(cat, Enumerable.Empty<Characteristic>());
        }

        public void SetCharacteristicValue(
            Characteristic charact, 
            CommodityType ct, 
            string value)
        {
            var oldVal = _context.CharacteristicValues
                .FirstOrDefault(cv => 
                    cv.CharacteristicId == charact.Id
                    && cv.CommodityTypeId == ct.Id);

            var possvals = PossibleValsFromString(charact.AvailableValues);
            if (!possvals.Contains(value))
            {
                throw new ArgumentException($"Value {value} not allowed for " +
                                    "characteristic {charact.Name}({charact.Id})");
            }

            if (oldVal == null)
            {
                _context.CharacteristicValues.Add(new CharacteristicValue
                {
                    CharacteristicId = charact.Id,
                    CommodityTypeId = ct.Id,
                    Value = value
                });
            }
            else
            {
                oldVal.Value = value;
                _context.CharacteristicValues.Update(oldVal);
            }
            _context.SaveChanges();
        }

        public void SetCharacteristicValue(CharacteristicValue charval)
        {
            SetCharacteristicValue(
                new Characteristic { Id = charval.CharacteristicId },
                new CommodityType { Id = charval.CommodityTypeId },
                charval.Value);
        }

        public static IEnumerable<string> PossibleValsFromString(string possvals)
        {
            return possvals.Split(';');
        }

        public static string PossibleValsToString(IEnumerable<string> possvals)
        {
            return string.Join(";", possvals);
        }

    }
}
