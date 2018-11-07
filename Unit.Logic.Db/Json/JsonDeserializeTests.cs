using System;
using Logic.Db.Dto;
using Logic.Db.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Unit.Logic.Db.Json {
    [TestClass()]
    public class JsonDeserializeTests {
        [TestMethod()]
        public void GeneratorDefaultCategoriesTest() {
            JsonDeserialize<AbsoluteCategory> json = new JsonDeserialize<AbsoluteCategory>("DefaultCategories.json");
            string[] categorias = {"JUNIOR", "SENIOR", "VETERAN_A", "VETERAN_B", "VETERAN_C"};
            int[] agem = {18, 19, 35, 45, 55};
            int[] ageM = {18, 34, 44, 54, int.MaxValue};
            AbsoluteCategory[] categories = new AbsoluteCategory[categorias.Length];
            for (int i = 0; i < categories.Length; i++) {
                string str = categorias[i];

                AbsoluteCategory absoluteCategory = new AbsoluteCategory() {
                    Id = 1,
                    Name = str,
                    CategoryM = new CategoryDto() {
                        Id = 1,
                        Name = $"{str}_M",
                        MinAge = agem[i],
                        MaxAge = ageM[i],
                        Gender = "M"
                    },
                    CategoryF = new CategoryDto() {
                        Id = 1,
                        Name = $"{str}_F",
                        MinAge = agem[i],
                        MaxAge = ageM[i],
                        Gender = "F"
                    },
                };

                categories[i] = absoluteCategory;
            }

            json.Serialize(categories);
            foreach (var category in json.ListJson()) {
                if (category != null)
                    Console.WriteLine($@"{category.Name} {category.CategoryF.Name} {category.CategoryM.Name}");
            }
        }
    }
}