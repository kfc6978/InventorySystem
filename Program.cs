// See https://aka.ms/new-console-template for more information

using System.Net.NetworkInformation;
using InventeorySystem.Models;
using InventeorySystem.Repositories;

//Server: mysql所在伺服器位置 (localhost or ip xxx.xxx.xxx.xxx)
//Port: mysql端口(預設3306)
//Database: inventory_db(CREATE DATABASE inventory_db;)
//uid: mysql使用者名稱
//pwd: mysql使用者密碼
const string MYSQL_CONNECTION_STRING = "Server=localhost;Port=3306;Database=inventory_db;Uid=root;Pwd=j6u06j6M3*;";

MysqlProductRepository productRepository = new MysqlProductRepository(MYSQL_CONNECTION_STRING);

RunMenu();

void RunMenu()
{
    while (true)
    {
        DisplayMeanu();
        string input = Console.ReadLine();
        switch (input)
        {
            case "1": GetAllProduct(); 
                break;
            case "2": SerchProduct(); 
                break;
            case "3": AddProduct();
                break;
            case "0": 
                Console.WriteLine("Goodbye~");
                return;
        }
    }
}

void DisplayMeanu()
{
    Console.WriteLine("Welcome to the Inventeory System");
    Console.WriteLine("What would you like to do?");
    Console.WriteLine("1. 查看所有產品");
    Console.WriteLine("2. 查詢商品");
    Console.WriteLine("3. 新增產品");
    Console.WriteLine("0. 離開");
}

void GetAllProduct()
{
    Console.WriteLine("\n--- 所有產品列表 ---");
    var products= productRepository.GetAllProducts();
    if (products.Any())
    {
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("ID | Name | Price | Quantity | Status");
        Console.WriteLine("-------------------------------------");
        foreach (var product in products)
        {
            Console.WriteLine(product);
        }
        Console.WriteLine("---------------------------------------");
    }
}

void SerchProduct()
{
    Console.WriteLine("輸入欲查詢的產品編號");
     int input = ReadIntLine();
     var product = productRepository.GetProductById(input);
    
    //接try-catch
    //string input = Console.ReadLine();
    //var product = productRepository.GetProductById(ReadInt(input));
    //Convert 轉換型別
    
    if (product != null)
    {
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("ID | Name | Price | Quantity | Status");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine(product);
        Console.WriteLine("-------------------------------------");
    }
}

void AddProduct()
{
    Console.WriteLine("輸入產品名稱");
    string name = Console.ReadLine();
    Console.WriteLine("請輸入產品價格");
    decimal price = ReadDecimalLine();
    Console.WriteLine("請輸入產品數量");
    int quantity = ReadIntLine();
    productRepository.AddProduct(name, price, quantity);
}

//try-catch不管輸入什麼都會再重新搜尋一次
//int ReadInt(string input)
//{
//    try
//    {
//        return Convert.ToInt32(input);
//    }
//    catch (FormatException e)
//    {
//        Console.WriteLine("請輸入有效數字");
//        return 0;
//    }
//}

int ReadIntLine(int defaultValue = 0 )
{
    while (true)
        //因為使用while，所以都一定會走進來
    {
        String input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input) && defaultValue != 0)
            //IsNy110rWhiteSpace和IsNu110rSpace有什麼不一樣
        {
            return defaultValue;
        }
        //string paring to int
        if (int.TryParse(input, out int value))
        {
            return value;
        }
        else
        {
            Console.WriteLine("請輸入有效數字"); 
        }
    }
}

decimal ReadDecimalLine(decimal defaultValue = 0.0m )
{
    while (true)
        //因為使用while，所以都一定會走進來
    {
        String input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input) && defaultValue != 0.0m)
        {
            return defaultValue;
        }
        //string paring to int
        if (decimal.TryParse(input, out decimal value))
        {
            return value;
        }
        else
        {
            Console.WriteLine("請輸入有效數字"); 
        }
    }
}