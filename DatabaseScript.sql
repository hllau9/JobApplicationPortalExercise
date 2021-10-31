
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

CREATE TABLE JobPostings (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	JobTitle NVARCHAR(200) NOT NULL,
	JobDescription NVARCHAR(1000) NOT NULL,
	Employer NVARCHAR(200) NOT NULL,
)

CREATE TABLE JobPostingSkillMap (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	JobPostingId INT NOT NULL FOREIGN KEY REFERENCES JobPostings(Id),
	SkillId INT NOT NULL FOREIGN KEY REFERENCES Skills(Id),
	UNIQUE (JobPostingId, SkillId)
)

CREATE TABLE Applications (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	FirstName NVARCHAR(200) NOT NULL,
	LastName NVARCHAR(200) NOT NULL,
	CurrentJobTitle NVARCHAR(200) NOT NULL,
	YearsOfExperience INT NOT NULL,
	PreferredLocation NVARCHAR(200) NOT NULL,
	HeardFromWhere NVARCHAR(200) NOT NULL,
	NoticePeriod NVARCHAR(200) NOT NULL,
	Phone NVARCHAR(200) NOT NULL,
	Email NVARCHAR(200) NOT NULL,
	Address NVARCHAR(1000) NOT NULL,
	ResumeFilePath NVARCHAR(1000) NOT NULL,
	JobPostingId INT NOT NULL FOREIGN KEY REFERENCES JobPostings(Id) ,
	UNIQUE (Email, JobPostingId)
)

CREATE TABLE SkillMap (
	Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	ApplicationId INT NOT NULL FOREIGN KEY REFERENCES Applications(Id),
	SkillId INT NOT NULL FOREIGN KEY REFERENCES Skills(Id),
	UNIQUE (ApplicationId, SkillId)
)


INSERT INTO JobPostings (JobTitle, JobDescription, Employer) VALUES
('Tech Lead', 'superstar tech lead', 'Confidential'),
('Java Developer', 'senior java developer', 'Randstad'),
('DevOps Specialist', 'aws, google cloud, azure, ci/cd', 'PosMalaysia'),
('.Net Developer', 'C#, ASP .Net developer', 'Singtel')

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

DECLARE @skillGroupId INT

SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Web'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('HTML5', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('CSS', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Django', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Node.js', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Laravel', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('React', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('ASP .NET Webforms', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('ASP .NET MVC', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('ASP .NET Core', @skillGroupId)

SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Programming'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('C#', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Java', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('C++', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Python', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('JavaScript', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('PHP', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Objective-C', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Ruby', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Golang', @skillGroupId)

SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Blockchain Technologies'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Bitcoin', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Ripple', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Ethereum', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Monero', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Litecoin', @skillGroupId)

SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Cloud and Distributed Computing'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Azure', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('AWS', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Kubernetes', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Docker', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Google Cloud', @skillGroupId)


SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Machine Learning'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('TensorFlow', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('scikit-learn', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Google Cloud ML Engine', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('AML', @skillGroupId)

SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Operating Systems'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('MS Windows', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Linux', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('MacOS', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('UNIX', @skillGroupId)

SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Network and Information Security'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('OSCP', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('CISSP', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Cisco CCNA', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Certified Ethical Hacker (CEH)', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('CompTIA Security+', @skillGroupId)

SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Virtualization'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('VMware vSphere', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Microsoft Hyper-V', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('QEMU', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Oracle VM VirtualBox', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('XEN', @skillGroupId)

SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'Big Data'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Statistical Analysis', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Data Mining and Modeling', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Database Management', @skillGroupId)

SELECT @skillGroupId = Id FROM SkillGroups WHERE SkillGroupName = 'eCommerce Platforms'
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Magento', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('PrestaShop', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Joomla', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('OpenCart', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('WooCommerce', @skillGroupId)
INSERT INTO Skills (SkillName, SkillGroupId) VALUES ('Shopify', @skillGroupId)

DECLARE @jobPostingId INT
DECLARE @skillId INT

SELECT @jobPostingId = Id FROM JobPostings WHERE JobTitle = 'Tech Lead'
SELECT @skillId = Id FROM Skills WHERE SkillName = 'AWS'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)
SELECT @skillId = Id FROM Skills WHERE SkillName = 'Database Management'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)
SELECT @skillId = Id FROM Skills WHERE SkillName = 'Magento'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)

SELECT @jobPostingId = Id FROM JobPostings WHERE JobTitle = 'Java Developer'
SELECT @skillId = Id FROM Skills WHERE SkillName = 'Java'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)
SELECT @skillId = Id FROM Skills WHERE SkillName = 'Database Management'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)
SELECT @skillId = Id FROM Skills WHERE SkillName = 'Linux'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)

SELECT @jobPostingId = Id FROM JobPostings WHERE JobTitle = 'DevOps Specialist'
SELECT @skillId = Id FROM Skills WHERE SkillName = 'Azure'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)
SELECT @skillId = Id FROM Skills WHERE SkillName = 'Docker'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)

SELECT @jobPostingId = Id FROM JobPostings WHERE JobTitle = '.Net Developer'
SELECT @skillId = Id FROM Skills WHERE SkillName = 'C#'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)
SELECT @skillId = Id FROM Skills WHERE SkillName = 'ASP .NET MVC'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)
SELECT @skillId = Id FROM Skills WHERE SkillName = 'ASP .NET Core'
INSERT INTO JobPostingSkillMap (JobPostingId, SkillId) VALUES (@jobPostingId, @skillId)

