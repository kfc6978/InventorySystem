using InventeorySystem.Models;
using MySql.Data.MySqlClient;

namespace InventeorySystem.Repositories;

public class MysqlPouductRepository : IProductRepository
{
    private readonly string _connectionString;
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
                Console.WriteLine("Mysql初始化失敗成功或已存在");
            }
            catch (Exception e)
            {
                Console.WriteLine($"初始化Mysql失敗 : {e.Message}");
            }
            
        }
    }

    public List<Product> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Product GetProductById(int id)
    {
        throw new NotImplementedException();
    }
}