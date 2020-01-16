using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_Reu.Models
{
    public static class DbInitializer
    {
        public static void Initialize(ReuContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Flight.Any())
            {
                return;   // DB has been seeded
            }

            var flights = new Flight[]
            {
                new Flight{Name="DTW - CDG",Depart="Détroit",Arrivee="Charles de Gaulle"},
                new Flight{Name="CDG - DTW",Depart="Charles de Gaulle",Arrivee="Détroit"},
                new Flight{Name="DTW - CDG",Depart="Détroit",Arrivee="Charles de Gaulle"},
                new Flight{Name="DTW - CDG",Depart="Détroit",Arrivee="Charles de Gaulle"},
                new Flight{Name="CDG - JFK",Depart="Charles de Gaulle",Arrivee="John Fitzgerald Keneddy"},
                new Flight{Name="JFK - CDG",Depart="John Fitzegerald Kennedy",Arrivee="Charles de Gaulle"}
            };
            foreach (Flight f in flights)
            {
                context.Flight.Add(f);
            }
            context.SaveChanges();

            var classes = new Classe[]
            {
                new Classe{FlightId=1,Name="1ère classe",NbSiege=15,Price=2000},
                new Classe{FlightId=1,Name="1ère classe",NbSiege=15,Price=4000},
                new Classe{FlightId=1,Name="Éco",NbSiege=15,Price=500},
                new Classe{FlightId=1,Name="Éco",NbSiege=15,Price=700},

                new Classe{FlightId=2,Name="1ère classe",NbSiege=15,Price=2000},
                new Classe{FlightId=2,Name="1ère classe",NbSiege=15,Price=4000},
                new Classe{FlightId=2,Name="Éco",NbSiege=15,Price=500},
                new Classe{FlightId=2,Name="Éco",NbSiege=15,Price=700},

                new Classe{FlightId=3,Name="1ère classe",NbSiege=5,Price=800},
                new Classe{FlightId=3,Name="1ère classe",NbSiege=5,Price=3200},
                new Classe{FlightId=3,Name="Éco",NbSiege=75,Price=100},
                new Classe{FlightId=3,Name="Éco",NbSiege=75,Price=300},

                new Classe{FlightId=4,Name="1ère classe",NbSiege=5,Price=800},
                new Classe{FlightId=4,Name="1ère classe",NbSiege=5,Price=3200},
                new Classe{FlightId=4,Name="Éco",NbSiege=75,Price=100},
                new Classe{FlightId=4,Name="Éco",NbSiege=75,Price=300},

                new Classe{FlightId=5,Name="1ère classe",NbSiege=20,Price=4000},
                new Classe{FlightId=5,Name="1ère classe",NbSiege=20,Price=6000},
                new Classe{FlightId=5,Name="Éco",NbSiege=150,Price=700},
                new Classe{FlightId=5,Name="Éco",NbSiege=150,Price=900},

                new Classe{FlightId=6,Name="1ère classe",NbSiege=20,Price=4000},
                new Classe{FlightId=6,Name="1ère classe",NbSiege=20,Price=6000},
                new Classe{FlightId=6,Name="Éco",NbSiege=150,Price=700},
                new Classe{FlightId=6,Name="Éco",NbSiege=150,Price=900}
            };
            foreach (Classe c in classes)
            {
                context.Classes.Add(c);
            }
            context.SaveChanges();
        }
    }
}
