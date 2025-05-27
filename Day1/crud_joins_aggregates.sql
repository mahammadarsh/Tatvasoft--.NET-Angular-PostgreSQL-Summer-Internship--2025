
DROP TABLE IF EXISTS UserSkills, UserDetail, "User", MissionTheme, MissionSkill, MissionApplication, Missions, City, Country;


CREATE TABLE Country (
    Id SERIAL PRIMARY KEY,
    CountryName VARCHAR(100)
);

CREATE TABLE City (
    Id SERIAL PRIMARY KEY,
    CountryId INT REFERENCES Country(Id),
    CityName VARCHAR(100)
);

CREATE TABLE MissionSkill (
    Id SERIAL PRIMARY KEY,
    SkillName VARCHAR(100),
    Status VARCHAR(50)
);

CREATE TABLE MissionTheme (
    Id SERIAL PRIMARY KEY,
    ThemeName VARCHAR(100),
    Status VARCHAR(50)
);

CREATE TABLE Missions (
    Id SERIAL PRIMARY KEY,
    MissionTitle VARCHAR(200),
    MissionDescription TEXT,
    MissionOrganisationName VARCHAR(100),
    MissionOrganisationDetail TEXT,
    CountryId INT REFERENCES Country(Id),
    CityId INT REFERENCES City(Id),
    StartDate DATE,
    EndDate DATE,
    MissionType VARCHAR(50),
    TotalSheets INT,
    RegistrationDeadLine DATE,
    MissionThemeId VARCHAR(100),
    MissionSkillId VARCHAR(100),
    MissionImages TEXT,
    MissionDocuments TEXT,
    MissionAvilability VARCHAR(100),
    MissionVideoUrl TEXT
);

CREATE TABLE "User" (
    Id SERIAL PRIMARY KEY,
    FirstName VARCHAR(100),
    LastName VARCHAR(100),
    PhoneNumber VARCHAR(20),
    EmailAddress VARCHAR(100),
    UserType VARCHAR(50),
    Password VARCHAR(100)
);

CREATE TABLE UserDetail (
    Id SERIAL PRIMARY KEY,
    UserId INT REFERENCES "User"(Id),
    Name VARCHAR(100),
    Surname VARCHAR(100),
    EmployeeId VARCHAR(50),
    Manager VARCHAR(100),
    Title VARCHAR(100),
    Department VARCHAR(100),
    MyProfile TEXT,
    WhyIVolunteer TEXT,
    CountryId INT REFERENCES Country(Id),
    CityId INT REFERENCES City(Id),
    Avilability VARCHAR(100),
    LinkdInUrl TEXT,
    MySkills TEXT,
    UserImage TEXT,
    Status BOOLEAN
);

CREATE TABLE MissionApplication (
    Id SERIAL PRIMARY KEY,
    MissionId INT REFERENCES Missions(Id),
    UserId INT REFERENCES "User"(Id),
    AppliedDate TIMESTAMP,
    Status BOOLEAN,
    Sheet INT
);

CREATE TABLE UserSkills (
    Id SERIAL PRIMARY KEY,
    Skill VARCHAR(100),
    UserId INT REFERENCES "User"(Id)
);


INSERT INTO Country (CountryName) VALUES ('India'), ('USA');
INSERT INTO City (CountryId, CityName) VALUES (1, 'Mumbai'), (1, 'Delhi'), (2, 'New York');

INSERT INTO "User" (FirstName, LastName, PhoneNumber, EmailAddress, UserType, Password)
VALUES ('Arsh', 'Shaikh', '1234567890', 'arsh@example.com', 'Volunteer', 'pass123');

INSERT INTO UserDetail (UserId, Name, Surname, EmployeeId, Manager, Title, Department, MyProfile, WhyIVolunteer, CountryId, CityId, Avilability, LinkdInUrl, MySkills, UserImage, Status)
VALUES (1, 'Arsh', 'Shaikh', 'E001', 'Mr. Manager', 'Developer', 'IT', 'My profile text', 'I love volunteering', 1, 1, 'Weekends', 'linkedin.com/arsh', 'Coding', 'arsh.jpg', TRUE);

INSERT INTO MissionSkill (SkillName, Status) VALUES ('Coding', 'Active');
INSERT INTO MissionTheme (ThemeName, Status) VALUES ('Environment', 'Active');

INSERT INTO Missions (MissionTitle, MissionDescription, MissionOrganisationName, MissionOrganisationDetail, CountryId, CityId, StartDate, EndDate, MissionType, TotalSheets, RegistrationDeadLine, MissionThemeId, MissionSkillId, MissionImages, MissionDocuments, MissionAvilability, MissionVideoUrl)
VALUES ('Tree Plantation', 'Planting trees in city parks', 'GreenOrg', 'We care about nature', 1, 1, '2025-06-01', '2025-06-10', 'Short Term', 10, '2025-05-30', '1', '1', 'trees.jpg', 'doc.pdf', 'Weekdays', 'video.com/tree');

INSERT INTO MissionApplication (MissionId, UserId, AppliedDate, Status, Sheet)
VALUES (1, 1, CURRENT_TIMESTAMP, TRUE, 1);

INSERT INTO UserSkills (Skill, UserId) VALUES ('Coding', 1);


UPDATE "User"
SET PhoneNumber = '9876543210'
WHERE Id = 1;

-- DELETE a mission application
DELETE FROM MissionApplication
WHERE Id = 1;

-- SELECT all users
SELECT * FROM "User";


SELECT u.FirstName, u.LastName, c.CityName, co.CountryName
FROM "User" u
JOIN UserDetail ud ON u.Id = ud.UserId
JOIN City c ON ud.CityId = c.Id
JOIN Country co ON ud.CountryId = co.Id;


SELECT c.CityName, COUNT(*) AS TotalUsers
FROM UserDetail ud
JOIN City c ON ud.CityId = c.Id
GROUP BY c.CityName;


SELECT co.CountryName, COUNT(*) AS TotalMissions
FROM Missions m
JOIN Country co ON m.CountryId = co.Id
GROUP BY co.CountryName;


SELECT m.MissionTitle, COUNT(ma.Id) AS ApplicationCount
FROM Missions m
LEFT JOIN MissionApplication ma ON m.Id = ma.MissionId
GROUP BY m.MissionTitle;

