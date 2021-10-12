CREATE TABLE Books
(
	ID INT PRIMARY KEY IDENTITY(1,1),
	Title VARCHAR(MAX),
	Authors VARCHAR(MAX),
	ISBN BIGINT,
	PublicationDate DATETIME,
	IsActive BIT DEFAULT 1,
	CreatedBy INT,
	CreatedDate DateTime,
	ModifiedBy INT,
	ModifiedDate DateTime
)