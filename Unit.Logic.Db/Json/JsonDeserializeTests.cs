using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Db.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Db.Dto;

namespace Logic.Db.Json.Tests {
    [TestClass()]
    public class JsonDeserializeTests {
        [TestMethod()]
        public void GeneratorDefaultCategoriesTest() {
            JsonDeserialize<CategoryDto> json = new JsonDeserialize<CategoryDto>("DefaultCategories.json");
            CategoryDto category0 = new CategoryDto() {
                Name = "JUNIOR",
                MinAge = 18,
                MaxAge = 18
            };
            CategoryDto category1 = new CategoryDto() {
                Name = "SENIOR",
                MinAge = 19,
                MaxAge = 34
            };
            CategoryDto category2 = new CategoryDto() {
                Name = "VETERAN_A",
                MinAge = 35,
                MaxAge = 44
            };
            CategoryDto category3 = new CategoryDto() {
                Name = "VETERAN_B",
                MinAge = 45,
                MaxAge = 54
            };
            CategoryDto category4 = new CategoryDto() {
                Name = "VETERAN_C",
                MinAge = 55,
                MaxAge = int.MaxValue
            };

            CategoryDto[] categories = {category0, category1, category2, category3, category4};
            json.Serialize(categories);
            foreach (var category in json.ListJson()) {
                Console.WriteLine($@"{category.Name} {category.MinAge} {category.MaxAge}");
            }
        }
    }
}