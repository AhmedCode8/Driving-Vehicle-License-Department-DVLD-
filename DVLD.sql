CREATE TABLE [Countries] (
  [CountryID] int PRIMARY KEY IDENTITY(1, 1),
  [CountryName] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [People] (
  [PersonID] int PRIMARY KEY IDENTITY(1, 1),
  [NationalNo] nvarchar(255) UNIQUE NOT NULL,
  [FirstName] nvarchar(255) NOT NULL,
  [SecondName] nvarchar(255) NOT NULL,
  [ThirdName] nvarchar(255),
  [LastName] nvarchar(255) NOT NULL,
  [DateOfBirth] date,
  [Gendor] tinyint NOT NULL,
  [Address] nvarchar(255),
  [Phone] nvarchar(255),
  [Email] nvarchar(255),
  [NationalityCountryID] int NOT NULL,
  [ImagePath] nvarchar(255)
)
GO

CREATE TABLE [Users] (
  [UserID] int PRIMARY KEY IDENTITY(1, 1),
  [PersonID] int NOT NULL,
  [UserName] nvarchar(255) UNIQUE NOT NULL,
  [Password] nvarchar(255) NOT NULL,
  [IsActive] bit DEFAULT (1)
)
GO

CREATE TABLE [Drivers] (
  [DriverID] int PRIMARY KEY IDENTITY(1, 1),
  [PersonID] int NOT NULL,
  [CreatedByUserID] int NOT NULL,
  [CreatedDate] date NOT NULL
)
GO

CREATE TABLE [LicenseClasses] (
  [LicenseClassID] int PRIMARY KEY IDENTITY(1, 1),
  [ClassName] nvarchar(255) UNIQUE NOT NULL,
  [ClassDescription] text,
  [MinimumAllowedAge] tinyint NOT NULL,
  [DefaultValidityLength] tinyint NOT NULL,
  [ClassFees] decimal NOT NULL
)
GO

CREATE TABLE [ApplicationTypes] (
  [ApplicationTypeID] int PRIMARY KEY IDENTITY(1, 1),
  [ApplicationTypeTitle] nvarchar(255) UNIQUE NOT NULL,
  [ApplicationFees] decimal NOT NULL
)
GO

CREATE TABLE [Applications] (
  [ApplicationID] int PRIMARY KEY IDENTITY(1, 1),
  [ApplicantPersonID] int NOT NULL,
  [ApplicationDate] date NOT NULL,
  [ApplicationTypeID] int NOT NULL,
  [ApplicationStatus] tinyint DEFAULT (1),
  [LastStatusDate] date NOT NULL,
  [PaidFees] decimal NOT NULL,
  [CreatedByUserID] int NOT NULL
)
GO

CREATE TABLE [LocalDrivingLicenseApplications] (
  [LocalDrivingLicenseApplicationID] int PRIMARY KEY IDENTITY(1, 1),
  [ApplicationID] int NOT NULL,
  [LicenseClassID] int NOT NULL
)
GO

CREATE TABLE [TestTypes] (
  [TestTypeID] int PRIMARY KEY IDENTITY(1, 1),
  [TestTypeTitle] nvarchar(255) NOT NULL,
  [TestTypeDescription] text NOT NULL,
  [TestTypeFees] decimal NOT NULL
)
GO

CREATE TABLE [TestAppointments] (
  [TestAppointmentID] int PRIMARY KEY IDENTITY(1, 1),
  [TestTypeID] int NOT NULL,
  [LocalDrivingLicenseApplicationID] int NOT NULL,
  [AppointmentDate] date NOT NULL,
  [PaidFees] decimal NOT NULL,
  [CreatedByUserID] int NOT NULL,
  [IsLocked] bit DEFAULT (0)
)
GO

CREATE TABLE [Tests] (
  [TestID] int PRIMARY KEY IDENTITY(1, 1),
  [TestAppointmentID] int NOT NULL,
  [TestResult] bit NOT NULL,
  [Notes] text,
  [CreatedByUserID] int NOT NULL
)
GO

CREATE TABLE [Licenses] (
  [LicenseID] int PRIMARY KEY IDENTITY(1, 1),
  [ApplicationID] int NOT NULL,
  [DriverID] int NOT NULL,
  [LicenseClass] int NOT NULL,
  [IssueDate] date NOT NULL,
  [ExpirationDate] date NOT NULL,
  [Notes] text,
  [PaidFees] decimal NOT NULL,
  [IsActive] bit DEFAULT (1),
  [IssueReason] tinyint DEFAULT (1),
  [CreatedByUserID] int NOT NULL
)
GO

CREATE TABLE [DetainedLicenses] (
  [DetainID] int PRIMARY KEY IDENTITY(1, 1),
  [LicenseID] int NOT NULL,
  [ReleaseApplicationID] int,
  [CreatedByUserID] int NOT NULL,
  [ReleasedByUserID] int,
  [DetainDate] date NOT NULL,
  [FineFees] decimal NOT NULL,
  [IsReleased] bit DEFAULT (0),
  [ReleaseDate] date
)
GO

CREATE TABLE [InternationalLicenses] (
  [InternationalLicenseID] int PRIMARY KEY IDENTITY(1, 1),
  [ApplicationID] int NOT NULL,
  [DriverID] int NOT NULL,
  [CreatedByUserID] int NOT NULL,
  [IssuedUsingLocalLicenseID] int NOT NULL,
  [IssueDate] date NOT NULL,
  [ExpirationDate] date NOT NULL,
  [IsActive] bit DEFAULT (1)
)
GO

ALTER TABLE [Users] ADD FOREIGN KEY ([PersonID]) REFERENCES [People] ([PersonID])
GO

ALTER TABLE [People] ADD FOREIGN KEY ([NationalityCountryID]) REFERENCES [Countries] ([CountryID])
GO

ALTER TABLE [Drivers] ADD FOREIGN KEY ([PersonID]) REFERENCES [People] ([PersonID])
GO

ALTER TABLE [Drivers] ADD FOREIGN KEY ([CreatedByUserID]) REFERENCES [Users] ([UserID])
GO

ALTER TABLE [Applications] ADD FOREIGN KEY ([ApplicantPersonID]) REFERENCES [People] ([PersonID])
GO

ALTER TABLE [Applications] ADD FOREIGN KEY ([ApplicationTypeID]) REFERENCES [ApplicationTypes] ([ApplicationTypeID])
GO

ALTER TABLE [Applications] ADD FOREIGN KEY ([CreatedByUserID]) REFERENCES [Users] ([UserID])
GO

ALTER TABLE [LocalDrivingLicenseApplications] ADD FOREIGN KEY ([ApplicationID]) REFERENCES [Applications] ([ApplicationID])
GO

ALTER TABLE [LocalDrivingLicenseApplications] ADD FOREIGN KEY ([LicenseClassID]) REFERENCES [LicenseClasses] ([LicenseClassID])
GO

ALTER TABLE [TestAppointments] ADD FOREIGN KEY ([TestTypeID]) REFERENCES [TestTypes] ([TestTypeID])
GO

ALTER TABLE [TestAppointments] ADD FOREIGN KEY ([LocalDrivingLicenseApplicationID]) REFERENCES [LocalDrivingLicenseApplications] ([LocalDrivingLicenseApplicationID])
GO

ALTER TABLE [TestAppointments] ADD FOREIGN KEY ([CreatedByUserID]) REFERENCES [Users] ([UserID])
GO

ALTER TABLE [Tests] ADD FOREIGN KEY ([TestAppointmentID]) REFERENCES [TestAppointments] ([TestAppointmentID])
GO

ALTER TABLE [Tests] ADD FOREIGN KEY ([CreatedByUserID]) REFERENCES [Users] ([UserID])
GO

ALTER TABLE [Licenses] ADD FOREIGN KEY ([ApplicationID]) REFERENCES [Applications] ([ApplicationID])
GO

ALTER TABLE [Licenses] ADD FOREIGN KEY ([DriverID]) REFERENCES [Drivers] ([DriverID])
GO

ALTER TABLE [Licenses] ADD FOREIGN KEY ([LicenseClass]) REFERENCES [LicenseClasses] ([LicenseClassID])
GO

ALTER TABLE [Licenses] ADD FOREIGN KEY ([CreatedByUserID]) REFERENCES [Users] ([UserID])
GO

ALTER TABLE [DetainedLicenses] ADD FOREIGN KEY ([LicenseID]) REFERENCES [Licenses] ([LicenseID])
GO

ALTER TABLE [DetainedLicenses] ADD FOREIGN KEY ([CreatedByUserID]) REFERENCES [Users] ([UserID])
GO

ALTER TABLE [DetainedLicenses] ADD FOREIGN KEY ([ReleasedByUserID]) REFERENCES [Users] ([UserID])
GO

ALTER TABLE [DetainedLicenses] ADD FOREIGN KEY ([ReleaseApplicationID]) REFERENCES [Applications] ([ApplicationID])
GO

ALTER TABLE [InternationalLicenses] ADD FOREIGN KEY ([ApplicationID]) REFERENCES [Applications] ([ApplicationID])
GO

ALTER TABLE [InternationalLicenses] ADD FOREIGN KEY ([DriverID]) REFERENCES [Drivers] ([DriverID])
GO

ALTER TABLE [InternationalLicenses] ADD FOREIGN KEY ([IssuedUsingLocalLicenseID]) REFERENCES [Licenses] ([LicenseID])
GO

ALTER TABLE [InternationalLicenses] ADD FOREIGN KEY ([CreatedByUserID]) REFERENCES [Users] ([UserID])
GO

ALTER TABLE [Tests] ADD FOREIGN KEY ([TestAppointmentID]) REFERENCES [Tests] ([TestID])
GO
