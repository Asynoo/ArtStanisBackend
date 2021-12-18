﻿using ArtStanisProject.Security.Entities;
using ArtStanisProject.Security.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtStanisProject.Security
{

    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        public DbSet<LoginUserEntity> LoginUsers { get; set; }
    }
}