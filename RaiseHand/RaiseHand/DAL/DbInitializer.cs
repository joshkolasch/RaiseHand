using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RaiseHand.Models;

namespace RaiseHand.DAL
{
    public class DbInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RaiseHandContext>
    {/*
        protected override void Seed(RaiseHandContext context)
        {
            var numCouches = 8;
            var numPCs = 43;
            var numTrapezoidTables = 7;
            var numWhiteboardTables = 7;
            var numRoundTables = 8;
            var numStudyRoomTables = 3;

            var locations = new List<Location>();

            for(int i = 1; i < numCouches + 1; i++)
            {
                string id = "couch-" + i.ToString();
                string name = "Couch " + i.ToString();
                var location = new Location{ Id = id, Name = name};
                locations.Add(location);
            }

            for (int i = 1; i < numPCs + 1; i++)
            {
                string id = "computer-" + i.ToString();
                string name = "Computer " + i.ToString();
                var location = new Location { Id = id, Name = name };
                locations.Add(location);
            }

            for (int i = 1; i < numTrapezoidTables + 1; i++)
            {
                string id = "trapezoid-table-" + i.ToString();
                string name = "Trapezoid Table " + i.ToString();
                var location = new Location { Id = id, Name = name };
                locations.Add(location);
            }

            for (int i = 1; i < numWhiteboardTables + 1; i++)
            {
                string id = "whiteboard-table-" + i.ToString();
                string name = "Whiteboard Table " + i.ToString();
                var location = new Location { Id = id, Name = name };
                locations.Add(location);
            }

            for (int i = 1; i < numRoundTables + 1; i++)
            {
                string id = "round-table-" + i.ToString();
                string name = "Round Table " + i.ToString();
                var location = new Location { Id = id, Name = name };
                locations.Add(location);
            }

            for (int i = 1; i < numStudyRoomTables + 1; i++)
            {
                string id = "study-room-table-" + i.ToString();
                string name = "Study Room " + i.ToString();
                var location = new Location { Id = id, Name = name };
                locations.Add(location);
            }

            locations.ForEach(x => context.Locations.Add(x));
            context.SaveChanges();

            var reasonsLowered = new List<ReasonLowered>
            {
                new ReasonLowered{Id = 1, Name="StudentSelectedHelped"},
                new ReasonLowered{Id = 2, Name="StudentSelectedNevermind"},
                new ReasonLowered{Id = 3, Name="StudentSelectedUnhelped"},
                new ReasonLowered{Id = 4, Name="TutorSelectedHelped"},
                new ReasonLowered{Id = 5, Name="TutorSelectedNotFound"},
                new ReasonLowered{Id = 6, Name="ClosingTimeout"}
            };

            reasonsLowered.ForEach(x => context.ReasonLowereds.Add(x));
            context.SaveChanges();

            var subjects = new List<Subject>
            {
                new Subject{Id=1, Name="Algebra"},
                new Subject{Id=2, Name="PreCalculus"},
                new Subject{Id=3, Name="Calculus"},
                new Subject{Id=4, Name="Business Calculus"},
                new Subject{Id=5, Name="Developmental Math"},
                new Subject{Id=6, Name="Chemistry"},
                new Subject{Id=7, Name="Biology"},
                new Subject{Id=8, Name="Accounting"},
                new Subject{Id=9, Name="Statistics"},
                new Subject{Id=10, Name="Math In Society"},
                new Subject{Id=11, Name="Physics"}
            };

            subjects.ForEach(x => context.Subjects.Add(x));
            context.SaveChanges();

            var statuses = new List<Status>
            {
                new Status{Id=1, Name = "Active" },
                new Status{Id=2, Name = "Inactive" }
            };

            statuses.ForEach(x => context.Statuses.Add(x));
            context.SaveChanges();


        }
        */
    }
}