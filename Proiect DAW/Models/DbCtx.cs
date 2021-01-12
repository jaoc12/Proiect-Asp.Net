using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Proiect_DAW.Models
{
    public class DbCtx : DbContext
    {
        public DbCtx() : base("DbConnectionString")
        {
            Database.SetInitializer<DbCtx>(new Initp());
        }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Person> People { get; set; }

        public DbSet<ContactInfo> ContactInfos { get; set; }

        public DbSet<Job> Jobs { get; set; }

        public DbSet<Studio> Studios { get; set; }

        public DbSet<Credit> Credits { get; set; }

        public DbSet<Award> Awards { get; set; }

        public DbSet<Review> Reviews { get; set; }
    }

    public class Initp : DropCreateDatabaseAlways<DbCtx>
    {
        protected override void Seed(DbCtx context)
        {
            Award award1 = new Award
            {
                Name = "Best Picture",
                Description = "This award goes to the producers of the film and is the only category in which every member of the Academy is eligible to submit a nomination and vote on the final ballot."
            };

            Award award2 = new Award
            {
                Name = "Best Original Screenplay",
                Description = "The Academy Award for Best Original Screenplay is the Academy Award for the best screenplay not based upon previously published material."
            };

            context.Awards.Add(award1);
            context.Awards.Add(award2);

            context.Studios.Add(new Studio
            {
                StudioId = 1,
                Name = "Walt Disney Studios",
                FoundingDate = new DateTime(1923, 10, 16),
                CEO = "Bob Chapek"
            });

            context.Studios.Add(new Studio
            {
                StudioId = 2,
                Name = "Warner Media",
                FoundingDate = new DateTime(1923, 4, 4),
                CEO = "Jason Kilar"
            });

            Job jobDirector = new Job
            {
                Type = "Director",
                Description = "A film director controls a film's artistic and dramatic aspects and visualizes the screenplay while guiding the technical crew and actors in the fulfilment of that vision."
            };
            Job jobActor = new Job
            {
                Type = "Actor",
                Description = "An actor is a person who portrays a character in a performance."
            };

            context.Jobs.Add(jobDirector);
            context.Jobs.Add(jobActor);

            ContactInfo contact1 = new ContactInfo
            {
                ContactEmail = "tarantino@email.com",
                ContactPhone = "0734567890"
            };

            ContactInfo contact2 = new ContactInfo
            {
                ContactEmail = "robert@email.com",
                ContactPhone = "0787654321"
            };

            context.ContactInfos.Add(contact1);
            context.ContactInfos.Add(contact2);

            Person person1 = new Person
            {
                Name = "Taratino",
                ContactInfo = contact1,
                Birthday = new DateTime(1975, 10, 16)
            };

            Person person2 = new Person
            {
                Name = "Robert",
                ContactInfo = contact2,
                Birthday = new DateTime(1969, 12, 28)
            };

            context.People.Add(person1);
            context.People.Add(person2);

            context.Movies.Add(new Movie
            {
                MovieId = 1,
                Title = "Star Wars",
                StudioId = 1,
                Awards = new List<Award> { award1},
                Description = "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader.",
                Credits = new List<Credit>
                {
                    new Credit
                    {
                        JobId = 1,
                        PersonId = 1
                    },
                    new Credit
                    {
                        JobId = 2,
                        PersonId = 2
                    }
                }
            });
            context.Movies.Add(new Movie
            {
                MovieId = 2,
                Title = "Avengers",
                StudioId = 2,
                Awards = new List<Award> { award2 },
                Description = "Earth's mightiest heroes must come together and learn to fight as a team if they are going to stop the mischievous Loki and his alien army from enslaving humanity.",
                Credits = new List<Credit>
                {
                    new Credit
                    {
                        JobId = 2,
                        PersonId = 1
                    },
                    new Credit
                    {
                        JobId = 1,
                        PersonId = 2
                    }
                }
            });

            Review review1 = new Review
            {
                Content = "Good space movie!",
                Rating = (decimal)8.3,
                MovieId = 1,
                AuthorEmail = "critic@critic.com"
            };

            Review review2 = new Review
            {
                Content = "Good super hero movie!",
                Rating = (decimal)8.1,
                MovieId = 2,
                AuthorEmail = "critic@critic.com"
            };

            context.Reviews.Add(review1);
            context.Reviews.Add(review2);
            base.Seed(context);
        }
    }
}