CREATE TABLE [dbo].[Persones] (
    [id]          INT           IDENTITY (1, 1) NOT NULL,
    [secondName]  VARCHAR (55)  NOT NULL,
    [firstName]   VARCHAR (55)  NOT NULL,
    [lastName]    VARCHAR (55)  NULL,
    [phoneNumber] VARCHAR (55)  NULL,
    [email]       VARCHAR (100) NOT NULL
);

