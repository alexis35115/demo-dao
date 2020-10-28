using demoObjetAccesDonnees.AccesDonnees;
using MiniProfiler.Integrations;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace demoObjetAccesDonnees
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialiser une connexion à la base de données
            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost,1401",
                UserID = "sa",
                Password = "Pewpew123!",
                InitialCatalog = "TSQL2012"
            };

            var profiler = CustomDbProfiler.Current;
            using IDbConnection connexion = ProfiledDbConnectionFactory.New(new SqlServerDbConnectionFactory(builder.ConnectionString), profiler);

            IEmployeDao employeDao = new EmployeDao(connexion);

            // Créer un employé
            var nouveauEmploye = new Employe
            {
                lastname = "lastname",
                firstname = "firstname",
                title = "title",
                titleofcourtesy = "titleofcourtesy",
                birthdate = DateTime.Now,
                hiredate = DateTime.Now,
                address = "adress",
                city = "city",
                region = "region",
                postalcode = "postalcode",
                country = "country",
                phone = "(555) 555-5555",
                mgrid = 1
            };

            var idEmployeCree = employeDao.Creer(nouveauEmploye);
            Console.WriteLine($"Identifiant de l'employé créé est {idEmployeCree}");

            // Récupérer l'employé créé
            var employeCree = employeDao.Lire(idEmployeCree);

            // Modifier l'employé
            employeCree.address = "69 rue principale";
            int nombreEmployeModifie = employeDao.MettreAJour(employeCree);
            Console.WriteLine($"Nombre d'employé modifié est de {nombreEmployeModifie}");

            // Supprimer l'employé
            int nombreEmployeSupprime = employeDao.Supprimer(employeCree.empid);
            Console.WriteLine($"Nombre d'employé supprimé est de {nombreEmployeSupprime}");

            File.WriteAllText("SqlScripts.txt", profiler.GetCommands());

            // Faire en sorte que la console reste ouvert
            Console.WriteLine("Press a key to exit..");
            Console.ReadLine();
        }
    }
}
