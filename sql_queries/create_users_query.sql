﻿CREATE TABLE Users (
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	CreatorName VARCHAR(45) UNIQUE NOT NULL,
	Password VARCHAR(45) UNIQUE NOT NULL,
	FullName VARCHAR(45) NOT NULL,
	DateJoined  DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
);