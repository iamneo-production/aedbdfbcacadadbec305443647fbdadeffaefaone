using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookStoreDBFirst.Models;

public class FreelancerProjectDbContext : DbContext
{

    public FreelancerProjectDbContext(DbContextOptions<FreelancerProjectDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Freelancer> Freelancers { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
}
