
CREATE TABLE [dbo].[user_articles] (
    [article_title]       VARCHAR (50)  NULL,
    [article_Description] VARCHAR (200) NULL,
    [article_id]          INT           IDENTITY (1, 1) NOT NULL,
    [user_numero]         VARCHAR (50)  NULL,
    [categorie] VARCHAR(50) NOT NULL, 
    PRIMARY KEY CLUSTERED ([article_id] ASC),
    FOREIGN KEY ([user_numero]) REFERENCES [dbo].[publishfan_users] ([user_numero])
);

drop table article_details
CREATE TABLE [dbo].[article_details] (
    [article_id]      INT            NULL,
    [article_text]    VARCHAR (4000) NULL,
    [number_likes]    BIGINT         NULL,
    [number_comments] BIGINT         NULL,
    [number_visits]   BIGINT         NULL,
    FOREIGN KEY ([article_id]) REFERENCES [dbo].[user_articles] ([article_id])
);

CREATE TABLE [dbo].[article_comments] (
    [article_id]   INT            NULL,
    [user_name_ac] VARCHAR (30)   NULL,
    [comment_text] VARCHAR (2000) NULL,
    FOREIGN KEY ([article_id]) REFERENCES [dbo].[user_articles] ([article_id])
);

