namespace GraduationProjectWebApi
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelReservation> HotelReservations { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<AirLine> AirLines { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightBooking> FlightBookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OffersBooking> OffersBookings { get; set; }
        public DbSet<Location> Locations { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Flight>()
                .HasOne(x=>x.DepartureLocation)
                .WithMany(x=>x.DepartureFlights)
                .HasForeignKey(x=>x.DepartureLocationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Flight>()
                .HasOne(x => x.DestinationLocation)
                .WithMany(x=>x.DestinationFlights)
                .HasForeignKey(x => x.DestinationLocationId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Flight>()
                .HasOne(x=>x.Admin)
                .WithMany(x=>x.Flights)
                .OnDelete(DeleteBehavior.NoAction);
                
            modelBuilder.Entity<Admin>()
                .HasMany(x=>x.Flights)
                .WithOne(x=>x.Admin)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
