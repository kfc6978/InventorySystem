using InventeorySystem.Models;
using MySql.Data.MySqlClient;

namespace InventeorySystem.Repositories;

public class MysqlPouductRepository : IProductRepository
// java: implememt interface
// java: extend ParentObj
{
    private readonly string _connectionString;
    //constructor
    public MysqlPouductRepository(string connectionString)
    {
        _connectionString = connectionString;
        IntializeDatabase();
    }

    private void IntializeDatabase()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                string createTableSql = @"
                create table if not exists pouducts(
                    id int primary key auto_increment,
                    name varchar(100) not null,
                    price decimal(10, 2) not null,
                    quantity int not null,
                    status int not null -- 對應enum的整數值
                );";
                using (MySqlCommand cmd = new MySqlCommand(createTableSql, connection))
                {
                    cmd.ExecuteNonQuery();
                }
                Console.WriteLine("Mysql初始化成功或已存在");
            }
            catch (Exception e)
            {
                Console.WriteLine($"初始化Mysql失敗 : {e.Message}");
            }
            
        }
    }

    public List<Product> GetAllProducts()
    {
        List<Product> products = new List<Product>();
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string selectSql = "SELECT * FROM pouducts";
            // 1 box
            // 2 dish
            // 3 phone
            using (MySqlCommand cmd = new MySqlCommand(selectSql, connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                        // reader = 1 box -> reader = 2 dish ...->
                    {
                        //origin way
                        //Product product = new Product(reader.GetInt32("id"),
                        //    reader.GetString("name"),
                        //    reader.GetInt32("price"),
                        //    reader.GetInt32("quantity"));
                        //Product.status = (Product.ProductStatus)reader.GetInt32("status");
                        //products.Add(product);
                        
                        //obj initializer
                        products.Add(new Product(reader.GetInt32("id"),
                            reader.GetString("name"),
                            reader.GetInt32("price"),
                            reader.GetInt32("quantity"))
                        {
                            Status = (Product.ProductStatus)reader.GetInt32("status")
                        });
                    }
                }
            }
        }
        return products;
    }

    public Product GetProductById(int id)
    {
        Product product = null;
        //todo
        
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            string selectSql = "SELECT * FROM products WHERE id = @id";
            // gen by AI
            // 舊方式 -> string selectSql = "SELECT * FROM products WHERE id =" + id
            using (MySqlCommand cmd = new MySqlCommand(selectSql, connection))
            {
                //防止 sql injection
                cmd.Parameters.AddWithValue("@id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        product = new Product(reader.GetInt32("id"),
                            reader.GetString("name"),
                            reader.GetInt32("price"),
                            reader.GetInt32("quantity"))
                        {
                            Status = (Product.ProductStatus)reader.GetInt32("status")
                        };
                    }
                }
            }
        }

        return product;
    }
}