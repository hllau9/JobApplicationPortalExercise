

CREATE TABLE SkillGroups (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	SkillGroupName NVARCHAR(200) NOT NULL UNIQUE
)

CREATE TABLE Skills (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	SkillName NVARCHAR(200) NOT NULL,
	SkillGroupId INT NOT NULL FOREIGN KEY REFERENCES SkillGroups(Id) ON DELETE CASCADE,
	UNIQUE (SkillName, SkillGroupId)
)

CREATE TABLE Applicants (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(200) NOT NULL,
	LastName NVARCHAR(200) NOT NULL,
	JobTitle NVARCHAR(200) NOT NULL,
	YearsOfExperience INT NOT NULL,
	PreferredLocation NVARCHAR(200) NOT NULL,
	HeardFromWhere NVARCHAR(200) NOT NULL,
	NoticePeriod NVARCHAR(200) NOT NULL,
	Phone NVARCHAR(200) NOT NULL,
	Email NVARCHAR(200) NOT NULL UNIQUE,
	Address NVARCHAR(1000) NOT NULL,
	ResumeFilePath NVARCHAR(1000) NOT NULL
)

CREATE TABLE SkillMap (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ApplicantId INT NOT NULL FOREIGN KEY REFERENCES Applicants(Id),
	SkillId INT NOT NULL FOREIGN KEY REFERENCES Skills(Id),
	UNIQUE (ApplicantId, SkillId)
)

INSERT INTO SkillGroups (SkillGroupName) VALUES
('Web'),
('Programming'),
('Blockchain Technologies'),
('Cloud and Distributed Computing'),
('Machine Learning'),
('Operating Systems'),
('Network and Information Security'),
('Virtualization'),
('Big Data'),
('eCommerce Platforms')





DECLARE @SkillGroupId INT

SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Web'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('HTML5', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('CSS', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Django', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Node.js', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Laravel', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('React', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('ASP .NET Webforms', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('ASP .NET MVC', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('ASP .NET Core', @SkillGroupId)

SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Programming'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('C#', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Java', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('C++', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Python', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('JavaScript', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('PHP', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Objective-C', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Ruby', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Golang', @SkillGroupId)

SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Blockchain Technologies'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Bitcoin', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Ripple', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Ethereum', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Monero', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Litecoin', @SkillGroupId)

SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Cloud and Distributed Computing'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Azure', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('AWS', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Kubernetes', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Docker', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Google Cloud', @SkillGroupId)


SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Machine Learning'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('TensorFlow', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('scikit-learn', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Google Cloud ML Engine', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('AML', @SkillGroupId)

SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Operating Systems'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('MS Windows', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Linux', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('MacOS', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('UNIX', @SkillGroupId)

SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Network and Information Security'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('OSCP', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('CISSP', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Cisco CCNA', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Certified Ethical Hacker (CEH)', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('CompTIA Security+', @SkillGroupId)

SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Virtualization'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('VMware vSphere', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Microsoft Hyper-V', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('QEMU', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Oracle VM VirtualBox', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('XEN', @SkillGroupId)

SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Big Data'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Statistical Analysis', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Data Mining and Modeling', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Database Management', @SkillGroupId)

SELECT @SkillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'eCommerce Platforms'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Magento', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('PrestaShop', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Joomla', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('OpenCart', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('WooCommerce', @SkillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Shopify', @SkillGroupId)



