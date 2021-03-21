using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqProject
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Category> categories = new List<Category> {
                new Category{CategoryId=1, CategoryName="Bilgisayar"},
                new Category{CategoryId=2, CategoryName="Telefon"}
            };

            List<Product> products = new List<Product> {
                new Product{ProductId=1,CategoryId=1,ProductName="Asus Notebook",QuantityPerUnit="32 GB Ram",UnitPrice=20000,UnitsInStock=5 },
                new Product{ProductId=2,CategoryId=1,ProductName="Lenovo Notebook",QuantityPerUnit="16 GB Ram",UnitPrice=12000,UnitsInStock=4 },
                new Product{ProductId=3,CategoryId=1,ProductName="Monster Notebook",QuantityPerUnit="16 GB Ram",UnitPrice=9000,UnitsInStock=6 },

                new Product{ProductId=4,CategoryId=2,ProductName="Apple Telefon",QuantityPerUnit="Iphone 8 128 GB",UnitPrice=6000,UnitsInStock=2 },
                new Product{ProductId=5,CategoryId=2,ProductName="Xiaomi Telefon",QuantityPerUnit="Redmi Note 9 Pro 6 GB Ram",UnitPrice=3200,UnitsInStock=6 },
            };

            //Console.WriteLine("Standart------------------------");

            //foreach (var product in GetProducts(products))
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            //Console.WriteLine("Linq------------------------");

            //foreach (var product in GetProductsLinq(products))
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            //AnyTest(products);

            //FindTest(products);

            //FindAllTest(products);

            //AscDescTest(products);

            //ClassicAndNewLinq(products);

            var result = from p in products
                         join c in categories on p.CategoryId equals c.CategoryId
                         where p.CategoryId==1 && p.UnitPrice>10000
                         select new ProductDto {ProductId=p.ProductId, CategoryName=c.CategoryName, ProductName=p.ProductName, UnitPrice=p.UnitPrice };
            
            foreach (var productDto in result)
            {
                Console.WriteLine(productDto.ProductId+":"+ productDto.CategoryName+":"+ productDto.ProductName+":"+ productDto.UnitPrice);
            }
        }

        private static void ClassicAndNewLinq(List<Product> products)
        {
            var result = from p in products
                         where p.UnitPrice > 6000 && p.UnitsInStock > 2
                         orderby p.UnitPrice, p.ProductName descending
                         select new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice };

            foreach (var product in result)
            {
                Console.WriteLine(product.ProductName + " : " + product.UnitPrice);
            }
            Console.WriteLine("------------------------------------------");
            var result2 = products.Where(p => p.UnitPrice > 6000 && p.UnitsInStock > 2).OrderBy(p => p.UnitPrice).ThenByDescending(p => p.ProductName).Select(p => new ProductDto { ProductId = p.ProductId, ProductName = p.ProductName, UnitPrice = p.UnitPrice });
            foreach (var product in result2)
            {
                Console.WriteLine(product.ProductName + " : " + product.UnitPrice);
            }
        }

        private static void AscDescTest(List<Product> products)
        {
            //Where: ProductName'nin içinde book olanları göster
            //Orderby: UnitPrice a göre sıralara
            //ThenBy: Sonrada ProductName için sırala 
            var result = products.Where(p => p.ProductName.Contains("book")).OrderBy(p => p.UnitPrice).ThenBy(p => p.ProductName);
            foreach (var product in result)
            {
                Console.WriteLine(product.ProductId + " : " + product.ProductName);
            }
        }

        private static void FindAllTest(List<Product> products)
        {
            var result = products.FindAll(p => p.ProductName.Contains("book"));
            Console.WriteLine(result);
        }

        private static void FindTest(List<Product> products)
        {
            var result = products.Find(p => p.ProductId == 3);
            Console.WriteLine(result.ProductId + " : " + result.ProductName);
        }

        private static void AnyTest(List<Product> products)
        {
            var result = products.Any(p => p.ProductName == "Asus Notebook");
            Console.WriteLine(result);
        }

        static List<Product> GetProducts(List<Product> products)
        {
            List<Product> filteredProduct = new List<Product>();
            foreach (var product in products)
            {
                if (product.UnitPrice > 5000 && product.UnitsInStock > 3)
                {
                    filteredProduct.Add(product);
                }

            }
            return filteredProduct;
        }

        static List<Product> GetProductsLinq(List<Product> products)
        {
            //Fiyatı 5000 üzeri ve 3'ten fazla stoğu olan ürünleri Liste olarak return eder.
            return products.Where(p => p.UnitPrice > 5000 && p.UnitsInStock > 3).ToList();
        }

    }

    class ProductDto
    {
        public int ProductId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }

     class Product
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
    }

     class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
