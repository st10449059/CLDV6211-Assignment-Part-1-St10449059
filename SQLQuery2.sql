-- 1. Create Venue Table
-- Represents the physical locations available for hosting events.
CREATE TABLE Venues (
    -- Primary Key: Unique identifier for each venue record.
    VenueId INT PRIMARY KEY IDENTITY(1,1),
    
    -- Data fields for venue identification and geolocation.
    VenueName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(200) NOT NULL,
    
    -- Stores the path or URL for visual assets (Microsoft, 2023).
    ImageUrl NVARCHAR(MAX) NULL
);

-- 2. Create Event Table (1:Many Relationship with Venues)
-- Defines specific scheduled occurrences linked to a hosting venue.
CREATE TABLE Events (
    EventId INT PRIMARY KEY IDENTITY(1,1),
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATETIME NOT NULL,
    ImageUrl NVARCHAR(MAX) NULL,
    
    /* * Foreign Key:
     * Establishes a formal link to the Venues table. This prevents 'orphaned' 
     * events and ensures every event is mapped to a valid location (Elmasri & Navathe, 2017).
     */
    VenueId INT NOT NULL,
    -- ADDED: ON DELETE CASCADE to allow deleting Venues that have Events
    FOREIGN KEY (VenueId) REFERENCES Venues(VenueId) ON DELETE CASCADE
);

-- 3. Create Booking Table (1:Many Relationship with Events)
-- Tracks attendee registration for specific events within the system.
CREATE TABLE Bookings (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    AttendeeName NVARCHAR(100) NOT NULL,
    
    /* * Referential Integrity:
     * Links the attendee to a specific event record. Maintaining these constraints 
     * is vital for accurate data reporting and analysis (Connolly & Begg, 2015).
     */
    EventId INT NOT NULL,
    -- ADDED: ON DELETE CASCADE to allow deleting Events that have Bookings
    FOREIGN KEY (EventId) REFERENCES Events(EventId) ON DELETE CASCADE
);