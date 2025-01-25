using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NoSQL.Data;
using System;
using System.Linq;
namespace NoSQL.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new NoSQLContext(serviceProvider.GetRequiredService<DbContextOptions<NoSQLContext>>()))
            {
                if (context.Roles.Any())
                {
                   
                }
                else
                {
                    var roles = new[] {
                         new Microsoft.AspNetCore.Identity.IdentityRole("Admin"){ NormalizedName="ADMIN"},
                         new Microsoft.AspNetCore.Identity.IdentityRole("Customer"){ NormalizedName="CUSTOMER"},
                         new Microsoft.AspNetCore.Identity.IdentityRole("Moderator"){ NormalizedName="CUSTOMER"}
                    };
                    foreach (var role in roles)
                    {
                        context.Roles.Add(role);
                    }
                }
                if (!context.Book.Any())
                {
                    var books = new[]
                    {
                        new Book {
                            opis=" ",
                            tytul="Przetwarzanie języka naturalnego w praktyce. Przewodnik po budowie rzeczywistych systemów NLP",
                            autor="Sowmya Vajjala, Bodhisattwa Majumder, Anuj Gupta, Harshit Surana",
                            cena=109.00m,
                            isbn="978-83-832-2727-6, 9788383227276",
                            DataWydanie=DateTime.Today,
                            gatunek="Informatyka"
                        },
                        new Book
                        {
                            opis=" ",
                            tytul="Matematyka w uczeniu maszynowym",
                            autor="Marc Peter Deisenroth, A. Aldo Faisal, Cheng Soon Ong",
                            cena=129.00m,
                            isbn="978-83-283-8459-0, 9788328384590",
                            DataWydanie=DateTime.Today,
                            gatunek="Informatyka"
                        },
                        new Book
                        {
                            opis=" ",
                            tytul="Czysty kod. Podręcznik dobrego programisty",
                            autor="Robert C. Martin",
                            cena=79.00m,
                            isbn="978-83-832-2344-5, 9788383223445",
                            DataWydanie=DateTime.Today,
                            gatunek="Informatyka"
                        },
                        new Book
                        {
                            opis=" ",
                            tytul="Uczenie maszynowe z użyciem Scikit-Learn, Keras i TensorFlow. Wydanie III",
                            autor="Aurélien Géron",
                            cena=179.00m,
                            isbn="978-83-832-2423-7, 9788383224237",
                            DataWydanie=DateTime.Today,
                            gatunek="Informatyka"
                        },
                        new Book
                        {
                            opis=" ",
                            tytul="Data science od podstaw. Analiza danych w Pythonie. Wydanie II",
                            autor="Joel Grus",
                            cena=79.00m,
                            isbn="978-83-832-2131-1, 9788383221311",
                            DataWydanie=DateTime.Today,
                            gatunek="Informatyka"
                        },
                        new Book
                        {
                            opis=" ",
                            tytul="Deep learning z TensorFlow 2 i Keras dla zaawansowanych. Sieci GAN i VAE, deep RL, uczenie nienadzorowane, wykrywanie i segmentacja obiektów i nie tylko. Wydanie II",
                            autor="Rowel Atienza",
                            cena=89.00m,
                            isbn="978-83-283-8883-3, 9788328388833",
                            DataWydanie=DateTime.Today,
                            gatunek="Informatyka"
                        },
                        new Book
                        {
                            opis=" ",
                            tytul="Analityk danych. Przewodnik po data science, statystyce i uczeniu maszynowym",
                            autor="Alex J. Gutman, Jordan Goldmeier",
                            cena=69.00m,
                            isbn="978-83-289-0215-2, 9788328902152",
                            DataWydanie=DateTime.Today,
                            gatunek="Informatyka"
                        },
                        new Book
                        {
                            opis=" ",
                            tytul="Podstawy matematyki w data science. Algebra liniowa, rachunek prawdopodobieństwa i statystyka",
                            autor="Thomas Nield",
                            cena=69.00m,
                            isbn="978-83-832-2013-0, 9788383220130",
                            DataWydanie=DateTime.Today,
                            gatunek="Informatyka"
                        },
                    };
                    foreach (Book book in books)
                    {
                        context.Book.Add(book);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
