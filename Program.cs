// See https://aka.ms/new-console-template for more information

using InventeorySystem.Models;
using InventeorySystem.Repositories;

//Server: mysql所在伺服器位置 (localhost or ip xxx.xxx.xxx.xxx)
//Port: mysql端口(預設3306)
//Database: inventory_db(CREATE DATABASE inventory_db;)
//uid: mysql使用者名稱
//pwd: mysql使用者密碼
const string MYSQL_CONNECTION_STRING = "Server=localhost;Port=3306;Database=inventory_db;Uid=root;Pwd=j6u06j6M3*;";

MysqlPouductRepository pouductRepository = new MysqlPouductRepository(MYSQL_CONNECTION_STRING);