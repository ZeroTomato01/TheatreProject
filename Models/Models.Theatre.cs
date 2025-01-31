using Newtonsoft.Json;

namespace TheatreProject.Models
{
    //note, these classes aren't automatically converted and used in frontend.
    //so make sure you change front-end definitions too if you change these here
    public class Customer //note
    {
        public int? CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public List<int>? ReservationIds {get; set;}

    }

    public class Reservation
    {
        public int ReservationId { get; set; }

        public int AmountOfTickets { get; set; }

        public bool Used { get; set; }

        public Customer? Customer { get; set; }

        public int? TheatreShowDateId { get; set; }
    }

    public class TheatreShowDate
    {
        public int TheatreShowDateId { get; set; }

        public DateTime DateAndTime { get; set; } //"MM-dd-yyyy HH:mm"

        public List<int>? ReservationIds { get; set; }

        public TheatreShow? TheatreShow { get; set; }

        public int? TheatreShowId {get; set;}

    }

    public class TheatreShow
    {
        public int TheatreShowId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public List<int>? TheatreShowDateIds { get; set; }
        public int VenueId { get; set; } // Foreign key
        public Venue? Venue { get; set; } // Navigation property
    }

public class Venue
{
    public int VenueId { get; set; }
    public string? Name { get; set; }
    public int Capacity { get; set; }
    public List<int>? TheatreShowIds { get; set; } // Navigation property
}

    // public class TheatreShow
    // {
    //     public int TheatreShowId { get; set; }

    //     public string? Title { get; set; }

    //     public string? Description { get; set; }

    //     public double Price { get; set; }

    //     public List<TheatreShowDate>? theatreShowDates { get; set; }

    //     public Venue? Venue { get; set; }

    // }

    // public class Venue
    // {
    //     public int VenueId { get; set; }

    //     public string? Name { get; set; }

    //     public int Capacity { get; set; }

    //     public List<TheatreShow>? TheatreShows { get; set; }
    // }
}