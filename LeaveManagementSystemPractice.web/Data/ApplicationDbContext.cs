﻿using LeaveManagementSystemPractice.web.Authorization;
using LeaveManagementSystemPractice.web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystemPractice.web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<Period> Periods { get; set; }
}