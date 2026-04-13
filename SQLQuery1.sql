-- 1. Create the Database
CREATE DATABASE EventEase_St10449059;
GO

USE EventEase_St10449059;
GO

-- 2. Create Venue Table
CREATE TABLE Venues (
    VenueId INT PRIMARY KEY IDENTITY(1,1),
    VenueName NVARCHAR(100) NOT NULL,
    Location NVARCHAR(200) NOT NULL,
    ImageUrl NVARCHAR(MAX) NULL -- For Rubric Point B.3
);

-- 3. Create Event Table (1:Many with Venue)
CREATE TABLE Events (
    EventId INT PRIMARY KEY IDENTITY(1,1),
    EventName NVARCHAR(100) NOT NULL,
    EventDate DATETIME NOT NULL,
    ImageUrl NVARCHAR(MAX) NULL,
    VenueId INT NOT NULL,
    CONSTRAINT FK_Events_Venues FOREIGN KEY (VenueId) REFERENCES Venues(VenueId) ON DELETE CASCADE
);

-- 4. Create Booking Table (1:Many with Event)
CREATE TABLE Bookings (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    AttendeeName NVARCHAR(100) NOT NULL,
    EventId INT NOT NULL,
    CONSTRAINT FK_Bookings_Events FOREIGN KEY (EventId) REFERENCES Events(EventId) ON DELETE CASCADE
);
