namespace FormsApp.Models;
public class Repostory{

    private static readonly List<Product> _products = new();
    private static readonly List<Category> _categorys = new();
    static Repostory(){

        _categorys.Add(new Category{CategoryId = 1,CategoryName = "Telefon"});
        _categorys.Add(new Category{CategoryId = 2,CategoryName = "Bilgisayar"});

        _products.Add(new Product{ProductId = 1,Image="1.jpg",Name =  "iphone 14",Price = 50000,CategoryId = 1,IsActve = true});
        _products.Add(new Product{ProductId = 2,Image="2.jpg",Name =  "iphone 15",Price = 60000,CategoryId = 1,IsActve = true});
        _products.Add(new Product{ProductId = 3,Image="3.jpg",Name =  "iphone 16",Price = 70000,CategoryId = 1,IsActve = true});
        _products.Add(new Product{ProductId = 4,Image="4.jpg",Name =  "iphone 17",Price = 80000,CategoryId = 1,IsActve = true});
        _products.Add(new Product{ProductId = 5,Image="5.jpg",Name =  "Macbook Air",Price = 90000,CategoryId = 2,IsActve = true});
        _products.Add(new Product{ProductId = 6,Image="6.jpg",Name =  "Macbook Pro",Price = 100000,CategoryId = 2,IsActve = true});
    }

    public static List<Product> Products{

        get{
            return _products;
        }
    }


    public static void productCreate(Product _entites){

        _products.Add(_entites);

    }

    public static void updateProduct(Product updateProduct){

        var entity = _products.FirstOrDefault(x=>x.ProductId == updateProduct.ProductId);
 
        if(entity !=null){

            entity.Name = updateProduct.Name;
            entity.Image = updateProduct.Image;
            entity.Price = updateProduct.Price;
            entity.IsActve = updateProduct.IsActve;
            entity.CategoryId = updateProduct.CategoryId;
        }
    }

    public static void deleteProduct(Product deleteProduct){

         var entity = _products.FirstOrDefault(x=>x.ProductId == deleteProduct.ProductId);
 
        if(entity !=null){

            _products.Remove(entity);
        }
    }

    public static List<Category> Category{

        get{
            return _categorys;
        }
    }
}