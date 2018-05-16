using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RaiseHand.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Configuration;

namespace RaiseHand.DAL
{
    public class RaiseHandContext : DbContext
    {
        /*public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["testDB"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["testuser"];
            string password = appConfig["rdstestuser112233"];
            string hostname = appConfig["mssqltestdb.c6my6oezsr9z.us-east-2.rds.amazonaws.com"];
            string port = appConfig["1433"];

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }*/
        /*
        public RaiseHandContext():base(GetRDSConnectionString())
        {
        }

        public static RaiseHandContext Create()
        {
            return new RaiseHandContext();
        }


         
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ReasonLowered> ReasonLowereds { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        */
        
    }

    
}