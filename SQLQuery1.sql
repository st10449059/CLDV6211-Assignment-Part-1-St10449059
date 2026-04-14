-- 1. Create the Database Container
-- This command initializes the physical storage for the system metadata and user data.
CREATE DATABASE EventEase_St10449059;
GO

-- Switch context to the newly created database for table creation
USE EventEase_St10449059;
GO

-- 2. Create Venue Table
-- Defines the physical locations where events take place.
CREATE TABLE Venues (
    -- Primary Key with Identity incrementing for unique record identification
    VenueId INT PRIMARY KEY IDENTITY(1,1),
    
    -- Mandatory fields for Venue identification
    VenueName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(200) NOT NULL,
    
    -- Nullable field to store external image paths/URLs (Microsoft, 2023).
    -- This supports the rubric requirement for placeholder image integration.
    ImageUrl NVARCHAR(MAX) NULL 
);

-- 3. Create Event Table (1:Many relationship with Venue)
-- Each event is hosted by exactly one venue, while a venue can host multiple events.
CREATE TABLE Events (
    EventId INT PRIMARY KEY IDENTITY(1,1),
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATETIME NOT NULL,
    ImageUrl NVARCHAR(MAX) NULL,
    
    -- Foreign Key column to link the event to a specific venue
    VenueId INT NOT NULL,
    
    -- Relational Constraint: Ensures every Event points to a valid Venue.
    -- ON DELETE CASCADE ensures orphaned events are removed if a venue is deleted (Elmasri & Navathe, 2017).
    CONSTRAINT FK_Events_Venues FOREIGN KEY (VenueId) 
        REFERENCES Venues(VenueId) ON DELETE CASCADE
);

-- 4. Create Booking Table (1:Many relationship with Event)
-- Records attendance for specific events, maintaining referential integrity (Connolly & Begg, 2015).
CREATE TABLE Bookings (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    AttendeeName NVARCHAR(100) NOT NULL,
    
    -- Foreign Key column to link the booking to a specific scheduled event
    EventId INT NOT NULL,
    
    -- Relational Constraint: Links the attendee to the specific event record.
    CONSTRAINT FK_Bookings_Events FOREIGN KEY (EventId) 
        REFERENCES Events(EventId) ON DELETE CASCADE
);
