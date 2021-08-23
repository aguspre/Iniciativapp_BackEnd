using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend
{
    public class AplicationDbContext : DbContext 
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Character> CharacterDB { get; set; }
        public DbSet<Combat> CombatDB { get; set; }
        public DbSet<CombatCharacters> CombatCharacters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CombatCharacters>()
                .HasKey(b => b.Id)
                .HasName("PrimaryKey_Id");
            modelBuilder.Entity<CombatCharacters>()
                .HasKey(cc => new { cc.CombatID, cc.CharacterID, cc.Id });
            modelBuilder.Entity<CombatCharacters>()
                .HasOne(cc => cc.Character)
                .WithMany(cc => cc.CombatCharacters)
                .HasForeignKey(cc => cc.CharacterID);
            modelBuilder.Entity<CombatCharacters>()
                .HasOne(cc => cc.Combat)
                .WithMany(cc => cc.CombatCharacters)
                .HasForeignKey(cc => cc.CombatID);
        }

    }
}
