using InvitationManagerAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace InvitationManagerAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Person> Persons => Set<Person>();
        public DbSet<User> User => Set<User>();
        public DbSet<CalendarBookings> Bookings => Set<CalendarBookings>();
    }
}
