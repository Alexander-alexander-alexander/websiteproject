using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
  public class HomeController : Controller
  {
    // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
    string connectionString = "Server=172.16.160.21;Port=3306;Database=;Uid=;Pwd=;";

    public IActionResult Index()
    {
      List<Product> products = new List<Product>();
      // uncomment deze regel om producten uit je database toe te voegen
      // products = GetProducts();

      return View(products);
    }

    private List<Product> GetProducts()
    {
      List<Product> products = new List<Product>();

      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("select * from product", conn);

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            Product p = new Product
            {
              Id = Convert.ToInt32(reader["Id"]),
              Naam = reader["Naam"].ToString(),
              Calorieen = float.Parse(reader["calorieen"].ToString()),
              Formaat = reader["Naam"].ToString(),
              Gewicht = Convert.ToInt32(reader["Naam"].ToString()),
              Prijs = Decimal.Parse(reader["Naam"].ToString())
            };
            products.Add(p);
          }
        }
      }

      return products;
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}
