---------------------------------------- CREATE DATABASE [Airport] ----------------------------------------
CREATE DATABASE [Airport]
ON  PRIMARY 
	(
		NAME = N'airportdb',
		FILENAME = N'D:\\airportdb.mdf',
		SIZE = 5MB, 
		MAXSIZE = UNLIMITED,
		FILEGROWTH = 1024KB
	)
LOG ON 
	(
	NAME = N'airportdb_log',
	FILENAME = N'D:\\airportdb_log.ldf',
	SIZE = 1024KB,
	MAXSIZE = 2048GB, 
	FILEGROWTH = 10%
	)

ALTER AUTHORIZATION ON DATABASE::[Airport] TO [NT AUTHORITY\яхярелю];
