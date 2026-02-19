using DreamJourney.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace DreamJourney.Data
{
    public class DreamJourneyDbContext : DbContext
    {
        //Using Microsoft.EntityFrameworkCore for the DbContext
        public DreamJourneyDbContext(DbContextOptions<DreamJourneyDbContext> options)
            : base(options)
        {

        }

        //DbSets of the Models
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripApplication> TripApplications { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<FeatureCategory> FeatureCategories { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<TripFeature> TripFeatures { get; set; }
        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Trip → User (User has many Trips)
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.User)
                .WithMany(u => u.Trips)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            // TripApplication → Trip
            modelBuilder.Entity<TripApplication>()
                .HasOne(ta => ta.Trip)
                .WithMany(t => t.TripApplications)
                .HasForeignKey(ta => ta.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            // TripApplication → User  (NO cascade delete)
            modelBuilder.Entity<TripApplication>()
                .HasOne(ta => ta.User)
                .WithMany(u => u.TripApplications)
                .HasForeignKey(ta => ta.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Category → SubCategory (1 → many)
            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(sc => sc.Category)
                .HasForeignKey(sc => sc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // SubCategory → Trip (1 → many)
            modelBuilder.Entity<SubCategory>()
                .HasMany(sc => sc.Trips)
                .WithOne(t => t.SubCategory)
                .HasForeignKey(t => t.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // TripFeature many-to-many join
            modelBuilder.Entity<TripFeature>()
                .HasKey(tf => new { tf.TripId, tf.FeatureId });

            modelBuilder.Entity<TripFeature>()
                .HasOne(tf => tf.Trip)
                .WithMany(t => t.TripFeatures)
                .HasForeignKey(tf => tf.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TripFeature>()
                .HasOne(tf => tf.Feature)
                .WithMany(f => f.TripFeatures)
                .HasForeignKey(tf => tf.FeatureId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Екскурзии" },
                new Department { Id = 2, Name = "Почивки" },
                new Department { Id = 3, Name = "Приключения" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Градски турове", DepartmentId = 1 },
                new Category { Id = 2, Name = "Културни екскурзии", DepartmentId = 1 },

                new Category { Id = 3, Name = "Морски почивки", DepartmentId = 2 },
                new Category { Id = 4, Name = "Планински почивки", DepartmentId = 2 },

                new Category { Id = 5, Name = "Екстремни", DepartmentId = 3 },
                new Category { Id = 6, Name = "Еко туризъм", DepartmentId = 3 }
            );

            modelBuilder.Entity<SubCategory>().HasData(
                new SubCategory { Id = 1, Name = "Уикенд", CategoryId = 1 },
                new SubCategory { Id = 2, Name = "Седмични", CategoryId = 1 },

                new SubCategory { Id = 3, Name = "Исторически", CategoryId = 2 },
                new SubCategory { Id = 4, Name = "Музеи", CategoryId = 2 },

                new SubCategory { Id = 5, Name = "Лято", CategoryId = 3 },
                new SubCategory { Id = 6, Name = "Зима", CategoryId = 4 },

                new SubCategory { Id = 7, Name = "Рафтинг", CategoryId = 5 },
                new SubCategory { Id = 8, Name = "Пешеходни", CategoryId = 6 }
            );

            modelBuilder.Entity<FeatureCategory>().HasData(
                new FeatureCategory { Id = 1, Name = "Транспорт", SingleSelection = true },
                new FeatureCategory { Id = 2, Name = "Хотел", SingleSelection = true },
                new FeatureCategory { Id = 3, Name = "Хранене", SingleSelection = true },
                new FeatureCategory { Id = 4, Name = "Екстри", SingleSelection = false }
            );  

            modelBuilder.Entity<Feature>().HasData(
                new Feature { Id = 1, Name = "Самолет", FeatureCategoryId = 1 },
                new Feature { Id = 2, Name = "Автобус", FeatureCategoryId = 1 },

                new Feature { Id = 3, Name = "Хотел 2*", FeatureCategoryId = 2 },
                new Feature { Id = 4, Name = "Хотел 3*", FeatureCategoryId = 2 },
                new Feature { Id = 5, Name = "Хотел 4*", FeatureCategoryId = 2 },
                new Feature { Id = 6, Name = "Хотел 5*", FeatureCategoryId = 2 },

                new Feature { Id = 7, Name = "Закуска", FeatureCategoryId = 3 },
                new Feature { Id = 8, Name = "HB", FeatureCategoryId = 3 },
                new Feature { Id = 9, Name = "FB", FeatureCategoryId = 3 },
                new Feature { Id = 10, Name = "All Inclusive", FeatureCategoryId = 3 },

                new Feature { Id = 11, Name = "Wi-Fi", FeatureCategoryId = 4 },
                new Feature { Id = 12, Name = "Трансфер", FeatureCategoryId = 4 },
                new Feature { Id = 13, Name = "Басейн", FeatureCategoryId = 4 },
                new Feature { Id = 14, Name = "Аквапарк", FeatureCategoryId = 4 },
                new Feature { Id = 15, Name = "Фитнес", FeatureCategoryId = 4 },
                new Feature { Id = 16, Name = "СПА", FeatureCategoryId = 4 }
            );

        }


    }
}
