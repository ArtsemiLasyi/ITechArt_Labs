DECLARE @TicksPerMinute AS BIGINT = 600000000

MERGE INTO [UserRoles]
USING (VALUES 
    (0, 'User'),
    (1, 'Administrator')) AS [NewUserRoles] ([Id], [Name])
ON [UserRoles].[Id] = [NewUserRoles].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [Name]) 
    VALUES ([NewUserRoles].[Id], [NewUserRoles].[Name]);

SET IDENTITY_INSERT [Users] ON

MERGE INTO [Users]
USING (VALUES
    (1, 0, 'verycoolmail@gmail.com'),
    (2, 1, 'newmail@outlook.com'),
    (3, 0, 'wow@yandex.by'),
    (4, 0, 'mew@yandex.ru')) AS [NewUsers] ([Id], [RoleId], [Email])
ON [Users].[Id] = [Users].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [RoleId], [Email])
    VALUES ([NewUsers].[Id], [NewUsers].[RoleId], [NewUsers].[Email]);

SET IDENTITY_INSERT [Users] OFF


MERGE INTO [UserPasswords]
USING (VALUES
    (
        1,
        0x8379261691E01541E10B061D7F636F2897BB27FEE2B65E9AA28DF6C036C935DD,
        0x22DD05383F37277F0DD7CF8CEE996EC0073208AE
    ),
    (
        2,
        0x8379261691E01541E10B061D7F636F2897BB27FEE2B65E9AA28DF6C036C935DD,
        0x22DD05383F37277F0DD7CF8CEE996EC0073208AE
    ),
    (
        3,
        0x8379261691E01541E10B061D7F636F2897BB27FEE2B65E9AA28DF6C036C935DD,
        0x22DD05383F37277F0DD7CF8CEE996EC0073208AE),
    (
        4,
        0x8379261691E01541E10B061D7F636F2897BB27FEE2B65E9AA28DF6C036C935DD,
        0x22DD05383F37277F0DD7CF8CEE996EC0073208AE
    )) AS [NewUserPasswords] ([UserId], [PasswordHash], [Salt])
ON [NewUserPasswords].[UserId] = [UserPasswords].[UserId]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([UserId], [PasswordHash], [Salt])
    VALUES (
        [NewUserPasswords].[UserId],
        [NewUserPasswords].[PasswordHash],
        [NewUserPasswords].[Salt]
    );


SET IDENTITY_INSERT [Services] ON

MERGE INTO [Services]
USING (VALUES
    (1, 'Popcorn'),
    (2, 'Cotton candy'),
    (3, 'Burger'),
    (4, 'Hot-dog'),
    (5, 'Pizza'),
    (6, 'Fries with baked calamari'),
    (7, 'Nachos'),
    (8, 'Chock'),
    (9, 'Coca-cola'),
    (10, 'Pepsi')) AS [NewServices] ([Id], [Name])
ON [Services].[Id] = [Services].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [Name])
    VALUES ([NewServices].[Id], [NewServices].[Name]);

SET IDENTITY_INSERT [Services] OFF


SET IDENTITY_INSERT [Cities] ON

MERGE INTO [Cities]
USING (VALUES
    (1, 'Minsk'),
    (2, 'Vitebsk'),
    (3, 'Brest'),
    (4, 'Grodno'),
    (5, 'Mogilev'),
    (6, 'Gomel'),
    (7, 'Molodechno'),
    (8, 'Orsha')) AS [NewCities] ([Id], [Name])
ON [Cities].[Id] = [Cities].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [Name])
    VALUES ([NewCities].[Id], [NewCities].[Name]);

SET IDENTITY_INSERT [Cities] OFF


SET IDENTITY_INSERT [Currencies] ON

MERGE INTO [Currencies]
USING (VALUES
    (1, 'EUR'),
    (2, 'USD'),
    (3, 'RUB'),
    (4, 'BYN'),
    (5, 'UAH'),
    (6, 'INR')) AS [NewCurrencies] ([Id], [Name])
ON [Currencies].[Id] = [Currencies].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [Name])
    VALUES ([NewCurrencies].[Id], [NewCurrencies].[Name]);

SET IDENTITY_INSERT [Currencies] OFF


SET IDENTITY_INSERT [SeatTypes] ON

MERGE INTO [SeatTypes]
USING (VALUES
    (1, 'Common', '#FFFFFF'),
    (2, 'Vip', '#FFD700'),
    (3, 'Special', '#C0C0C0')) AS [NewSeatTypes] ([Id], [Name], [ColorRgb])
ON [SeatTypes].[Id] = [SeatTypes].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [Name], [ColorRgb])
    VALUES (
        [NewSeatTypes].[Id],
        [NewSeatTypes].[Name],
        [NewSeatTypes].[ColorRgb]
    );

SET IDENTITY_INSERT [SeatTypes] OFF

SET IDENTITY_INSERT [Cinemas] ON

MERGE INTO [Cinemas]
USING (VALUES
    (
        1,
        1,
        'Mir',
        'Mir is a large-format cinema in Minsk. It opened on December 29, 1958. One of the oldest cinemas in Minsk.'
            + ' September 8, 1978 in this cinema for the first time in Belarus was shown a stereo film',
        0
    ),
    (
        2,
        1,
        'October',
        '"October" is a large-format cinema in Minsk, which has the largest cinema hall in Belarus.'
            + ' Located on Nezavisimosti Avenue, 73. It was designed by the architect V. I. Malyshev.',
        0
    ),
    (
        3,
        1,
        'Belarus',
        'Cinema, located at the address: the city of Minsk, Romanovskaya Sloboda street, house 28.'
            + ' It was reopened on September 12, 2008 on the site of the former cinema of the same name, built in 1961.'
            + ' The first modern multiplex in Belarus (5 halls). President of Belarus Alexander Lukashenko visited the new cinema on the opening day',
        0
    ),
    (
        4,
        3,
        'Mir',
        'Address: Brest, st. Pushkinskaya, 7',
        0
    ),
    (
        5,
        7,
        'Rodina',
        'Address: Molodechno, st. Pritytskogo, 19',
        0
    )) AS [NewCinemas] ([Id], [CityId], [Name], [Description], [IsDeleted])
ON [Cinemas].[Id] = [Cinemas].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [CityId], [Name], [Description], [IsDeleted])
    VALUES (
        [NewCinemas].[Id],
        [NewCinemas].[CityId],
        [NewCinemas].[Name],
        [NewCinemas].[Description],
        [NewCinemas].[IsDeleted]
    );

SET IDENTITY_INSERT [Cinemas] OFF


MERGE INTO [CinemaPhotos]
USING (VALUES
    (1, '1.jpg'),
    (2, '2.jpg'),
    (3, '3.jpg'),
    (4, '4,jpg'),
    (5, '5.jpg')) AS [NewCinemaPhotos] ([CinemaId], [FileName])
ON [CinemaPhotos].[CinemaId] = [CinemaPhotos].[CinemaId]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([CinemaId], [FileName])
    VALUES ([NewCinemaPhotos].[CinemaId], [NewCinemaPhotos].[FileName]);


MERGE INTO [CinemaServices]
USING (VALUES 
    (1, 3, 2.6, 4), 
    (1, 4, 2.72, 4), 
    (1, 10, 2.3, 4), 
    (1, 1, 1.3, 4), 
    (1, 7, 1.9, 4), 
    (1, 8, 6.3, 4), 
    (1, 9, 3.3, 4), 
    (1, 2, 1.3, 4),
    (2, 3, 2.6, 4), 
    (2, 4, 2.72, 4), 
    (2, 10, 2.3, 4), 
    (2, 1, 1.3, 4), 
    (2, 7, 1.9, 4), 
    (2, 8, 6.3, 4), 
    (2, 9, 3.3, 4), 
    (2, 2, 1.3, 4),
    (3, 3, 2.6, 4), 
    (3, 4, 2.72, 4), 
    (3, 10, 2.3, 4), 
    (3, 1, 1.3, 4), 
    (3, 7, 1.9, 4), 
    (3, 8, 6.3, 4), 
    (3, 9, 3.3, 4), 
    (3, 2, 1.3, 4),
    (4, 3, 2.6, 4), 
    (4, 4, 2.72, 4), 
    (4, 10, 2.3, 4), 
    (4, 1, 1.3, 4), 
    (4, 7, 1.9, 4), 
    (4, 8, 6.3, 4), 
    (4, 9, 3.3, 4), 
    (4, 2, 1.3, 4),
    (5, 3, 2.6, 4), 
    (5, 4, 2.72, 4), 
    (5, 10, 2.3, 4), 
    (5, 1, 1.3, 4), 
    (5, 7, 1.9, 4), 
    (5, 8, 6.3, 4), 
    (5, 9, 3.3, 4), 
    (5, 2, 1.3, 4)) AS [NewCinemaServices] ([CinemaId], [ServiceId], [Price], [CurrencyId])
ON [CinemaServices].[CinemaId] = [NewCinemaServices].[CinemaId] 
    AND [CinemaServices].[ServiceId] = [NewCinemaServices].[ServiceId] 
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([CinemaId], [ServiceId], [Price], [CurrencyId]) 
    VALUES (
        [NewCinemaServices].[CinemaId],
        [NewCinemaServices].[ServiceId],
        [NewCinemaServices].[Price],
        [NewCinemaServices].[CurrencyId]
    );


SET IDENTITY_INSERT [Halls] ON

MERGE INTO [Halls]
USING (VALUES 
    (1, 1, 'Hall A', 0),
    (2, 2, 'Hall Prime', 0),
    (3, 3, 'Hall', 0),
    (4, 4, 'Hall', 0),
    (5, 1, 'hall B', 0)) AS [NewHalls] ([Id], [CinemaId], [Name], [IsDeleted])
ON [Halls].[Id] = [NewHalls].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [CinemaId], [Name], [IsDeleted]) 
    VALUES (
        [NewHalls].[Id], 
        [NewHalls].[CinemaId], 
        [NewHalls].[Name],
        [NewHalls].[IsDeleted]
    );

SET IDENTITY_INSERT [Halls] OFF


MERGE INTO [HallPhotos]
USING (VALUES 
    (1, '1.jpeg'),
    (2, '2.jpg'),
    (3, '3.jpg'),
    (4, '4.jpg'),
    (5, '5.jpg')) AS [NewHallPhotos] ([HallId], [FileName])
ON [HallPhotos].[HallId] = [NewHallPhotos].[HallId]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([HallId], [FileName]) 
    VALUES (
        [NewHallPhotos].[HallId],  
        [NewHallPhotos].[FileName]
    );


SET IDENTITY_INSERT [Seats] ON

MERGE INTO [Seats]
USING (VALUES 
    (1, 1, 1, 1, 1, 0),
    (2, 1, 1, 1, 2, 0),
    (3, 1, 1, 1, 3, 0),
    (4, 1, 1, 1, 4, 0),
    (5, 1, 1, 1, 5, 0),
    (6, 1, 1, 1, 6, 0),
    (7, 1, 1, 1, 7, 0),
    (8, 1, 1, 1, 8, 0),
    (9, 1, 1, 1, 9, 0),
    (10, 1, 1, 1, 10, 0),
    (11, 1, 2, 2, 1, 0),
    (12, 1, 2, 2, 2, 0),
    (13, 1, 2, 2, 3, 0),
    (14, 1, 2, 2, 4, 0),
    (15, 1, 2, 2, 5, 0),
    (16, 1, 2, 2, 6, 0),
    (17, 1, 2, 2, 7, 0),
    (18, 1, 2, 2, 8, 0),
    (19, 1, 2, 2, 9, 0),
    (20, 1, 2, 2, 10, 0),
    (21, 2, 1, 1, 1, 0),
    (22, 2, 1, 1, 2, 0),
    (23, 2, 1, 1, 3, 0),
    (24, 2, 1, 1, 4, 0),
    (25, 2, 1, 1, 5, 0),
    (26, 2, 1, 1, 6, 0),
    (27, 2, 1, 1, 7, 0),
    (28, 2, 1, 1, 8, 0),
    (29, 2, 1, 1, 9, 0),
    (30, 2, 1, 1, 10, 0),
    (31, 2, 2, 2, 1, 0),
    (32, 2, 2, 2, 2, 0),
    (33, 2, 2, 2, 3, 0),
    (34, 2, 2, 2, 4, 0),
    (35, 2, 2, 2, 5, 0),
    (36, 2, 2, 2, 6, 0),
    (37, 2, 2, 2, 7, 0),
    (38, 2, 2, 2, 8, 0),
    (39, 2, 2, 2, 9, 0),
    (40, 2, 2, 2, 10, 0),
    (41, 3, 1, 1, 1, 0),
    (42, 3, 1, 1, 2, 0),
    (43, 3, 1, 1, 3, 0),
    (44, 3, 1, 1, 4, 0),
    (45, 3, 1, 1, 5, 0),
    (46, 3, 1, 2, 1, 0),
    (47, 3, 1, 2, 2, 0),
    (48, 3, 1, 2, 3, 0),
    (49, 3, 1, 2, 4, 0),
    (50, 3, 1, 2, 5, 0),
    (51, 4, 2, 1, 1, 0),
    (52, 4, 2, 1, 2, 0),
    (53, 4, 2, 1, 3, 0),
    (54, 4, 2, 1, 4, 0),
    (55, 4, 2, 1, 5, 0),
    (56, 4, 2, 2, 1, 0),
    (57, 4, 2, 2, 2, 0),
    (58, 4, 2, 2, 3, 0),
    (59, 4, 2, 2, 4, 0),
    (60, 4, 2, 2, 5, 0),
    (61, 4, 1, 3, 1, 0),
    (62, 4, 1, 3, 2, 0),
    (63, 4, 1, 3, 3, 0),
    (64, 4, 1, 3, 4, 0),
    (65, 4, 1, 3, 5, 0),
    (66, 4, 1, 4, 1, 0),
    (67, 4, 1, 4, 2, 0),
    (68, 4, 1, 4, 3, 0),
    (69, 4, 1, 4, 4, 0),
    (70, 4, 1, 4, 5, 0),
    (71, 4, 2, 5, 1, 0),
    (72, 4, 2, 5, 2, 0),
    (73, 4, 2, 5, 3, 0),
    (74, 4, 2, 5, 4, 0),
    (75, 4, 2, 5, 5, 0),
    (76, 5, 2, 1, 1, 0),
    (77, 5, 2, 1, 2, 0),
    (78, 5, 2, 1, 3, 0),
    (79, 5, 2, 1, 4, 0),
    (80, 5, 2, 1, 5, 0),
    (81, 5, 1, 2, 1, 0),
    (82, 5, 1, 2, 2, 0),
    (83, 5, 1, 2, 3, 0),
    (84, 5, 1, 2, 4, 0),
    (85, 5, 1, 2, 5, 0),
    (86, 5, 3, 3, 1, 0),
    (87, 5, 3, 3, 2, 0),
    (88, 5, 3, 3, 3, 0),
    (89, 5, 3, 3, 4, 0),
    (90, 5, 3, 3, 5, 0),
    (91, 5, 3, 4, 1, 0),
    (92, 5, 3, 4, 2, 0),
    (93, 5, 3, 4, 3, 0),
    (94, 5, 3, 4, 4, 0),
    (95, 5, 3, 4, 5, 0),
    (96, 5, 1, 5, 1, 0),
    (97, 5, 1, 5, 2, 0),
    (98, 5, 1, 5, 3, 0),
    (99, 5, 1, 5, 4, 0),
    (100, 5, 1, 5, 5, 0)) AS [NewSeats] ([Id], [HallId], [SeatTypeId], [Row], [Place], [IsDeleted])
ON [Seats].[Id] = [NewSeats].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [HallId], [SeatTypeId], [Row], [Place], [IsDeleted]) 
    VALUES (
        [NewSeats].[Id], 
        [NewSeats].[HallId], 
        [NewSeats].[SeatTypeId],
        [NewSeats].[Row],
        [NewSeats].[Place],
		[NewSeats].[IsDeleted]
    );

SET IDENTITY_INSERT [Seats] OFF


SET IDENTITY_INSERT [Films] ON

MERGE INTO [Films]
USING (VALUES
    (
        1, 
        'Her cherished desire',
        'She is a young dreamer who loves to draw and read books, but she has never enjoyed life outside her home.' 
            + ' He is a student-oceanologist, for whom the main thing is study. ' 
            + 'Their fateful meeting will give each of them impressions that they will remember forever, and will fulfill their most cherished desire.', 
        2020, 
        99*@TicksPerMinute,
        0
    ),
    (
        2,
        'Time Is Up',
        'An accident will force "Vivien" and Royan to come to a stop and reclaim their lives, one minute at the time,'
            + ' and finally start living in a present that perhaps will prove to be more exciting than any predefined.',
        2021,
        80*@TicksPerMinute,
        0
    ),
    (
        3,
        'Demonic',
        'A young woman unleashes terrifying demons when supernatural forces at the root of a decades-old rift '
            + 'between mother and daughter are ruthlessly revealed.',
        2021,
        104*@TicksPerMinute,
        0
    ),
    (
        4,
        'Ivan Denisovich',
        'Ivan Shukhov, an ordinary soldier, according to the laws of wartime after the German captivity is in his native country as a prisoner of Sch-854.' 
            + ' Among the snows, along with other convicts, Shukhov is building a future giant of the space industry.'
            + ' Despite injustice, hunger, humiliation and fear, even in the hard labor camp,'
            + ' Ivan Denisovich retains the amazing properties of the Russian character.'
            + ' Hard-working and extremely honest, open to the world, but having learned to survive even on the brink of death,'
            + ' he combines folk wisdom and boundless faith in miracles. Ivan Denisovich is serving the last days of 10 years of imprisonment ...',
        2021,
        105*@TicksPerMinute,
        0
    ),
    (
        5,
        'Back in the future',
        'Back to the Future is an American science fiction/comedy movie directed by Robert Zemeckis and released in 1985.'
            + ' It is about a young man named Marty McFly who accidentally travels into the past and jeopardizes his own future existence.',
        1985,
        116*@TicksPerMinute,
        0
    ),
    (
        6,
        'Coco',
        'The story follows a 12-year-old boy named Miguel who is accidentally transported to the Land of the Dead,'
            + ' where he seeks the help of his deceased musician great-great-grandfather to return him to his family among the living'
            + ' and to reverse his family''s ban on music.',
        2017,
        105*@TicksPerMinute,
        0
    ),
    (
        7,
        'Fight club',
        'Fight Club is a 1999 American film directed by David Fincher and starring Brad Pitt, Edward Norton, and Helena Bonham Carter.'
            + '... He forms a "fight club" with soap salesman Tyler Durden (Pitt), and becomes embroiled in a relationship with a destitute woman,'
            + ' Marla Singer (Bonham Carter).',
        1999,
        139*@TicksPerMinute,
        0
    ),
    (
        8,
        'Forrest Gump',
        'Forrest Gump is a simple man with a low I.Q. but good intentions. He is running through childhood with his best and only friend Jenny.'
            + ' His ''mama'' teaches him the ways of life and leaves him to choose his destiny.',
        1994,
        142*@TicksPerMinute,
        0
    ),
    (
        9,
        'Gentlemen of Fortune',
        'A Gentleman of fortune was a term which was used during the Golden Age of Piracy. It usually referred to a successful pirate or a notorious adventurer.',
        1971,
        84*@TicksPerMinute,
        0
    ),
    (
        10,
        'Inception',
        'The practice of entering dreams and planting an idea in someone''s head. ...'
            + ' The Architect: The person who constructs the dream world inside the mind of the Dreamer.'
            + 'In the final dream of Inception, Ariadne (as played by Ellen Page) is the architect.'
            + ' The Dreamer: The person whose dream you''re actually in.',
        2010,
        148*@TicksPerMinute,
        0
    ),
    (
        11,
        'Interstellar',
        'Interstellar is a 2014 epic science fiction drama film co-written, directed and produced by Christopher Nolan.'
            + ' ... Set in a dystopian future where humanity is struggling to survive, the film follows a group of astronauts'
            + ' who travel through a wormhole near Saturn in search of a new home for humanity.',
        2014,
        169*@TicksPerMinute,
        0
    ),
    (
        12,
        'Intouchables',
        'Based on a true story, Intouchables tells how a millionaire left quadriplegic after an accident – played French arthouse cinema star,'
            + ' Francois Cluzet – hires the unlikely home-help, Driss, who is from the poor suburbs and just out of prison.',
        2011,
        112*@TicksPerMinute,
        0
    ),
    (
        13,
        'Ivan Vasilievich Changes Profession',
        'Ivan Vasilievich Changes Profession (Russian: Иван Васильевич меняет профессию, romanized: Ivan Vasilyevich menyayet professiyu)'
            + ' is a Soviet comic science fiction film directed by Leonid Gaidai in June 1973.'
            + ' In the United States the film has sometimes been sold under the title Ivan Vasilievich: Back to the Future.', 
        1973,
        88*@TicksPerMinute,
        0
    ),
    (
        14,
        'Klaus',
        'A selfish postman and a reclusive toymaker form an unlikely friendship, delivering joy to a cold, dark town that desperately needs it.', 
        2019,
        96*@TicksPerMinute,
        0
    ),
    (
        15,
        'Knockin'' on Heaven''s Door',
        'Two terminally ill patients escape from a hospital, steal a car and rush towards the sea.'
            + ' Two terminally ill patients escape from a hospital, steal a car and rush towards the sea.'
            + ' Two terminally ill patients escape from a hospital, steal a car and rush towards the sea.', 
        1997,
        87*@TicksPerMinute,
        0
    ),
    (
        16,
        'Lock, Stock and Two Smoking Barrels',
        'The story is a heist involving a self-confident young card sharp who loses £500,000 to a powerful crime lord in a rigged game of three-card brag.'
            + ' To pay off his debts, he and his friends decide to rob a small-time gang who happen to be operating out of the flat next door.', 
        1998,
        107*@TicksPerMinute,
        0
    )) AS [NewFilms] (
        [Id],
        [Name],
        [Description],
        [ReleaseYear],
        [DurationInTicks],
        [IsDeleted])
ON [Films].[Id] = [NewFilms].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [Name], [Description], [ReleaseYear], [DurationInTicks], [IsDeleted]) 
    VALUES (
        [NewFilms].[Id],
        [NewFilms].[Name],
        [NewFilms].[Description],
        [NewFilms].[ReleaseYear],
        [NewFilms].[DurationInTicks],
        [NewFilms].[IsDeleted]
    );

SET IDENTITY_INSERT [Films] OFF


MERGE INTO [Posters]
USING (VALUES 
    (1, '1.jpg'),
    (2, '2.jpg'),
    (3, '3.jpg'),
    (4, '4.jpg'),
    (5, '5.jpg'),
    (6, '6.jpg'),
    (7, '7.jpg'),
    (8, '8.jpg'),
    (9, '9.jpg'),
    (10, '10.jpg'),
    (11, '11.jpg'),
    (12, '12.jpg'),
    (13, '13.jpg'),
    (14, '14.jpg'),
    (15, '15.jpg'),
    (16, '16.jpg')) AS [NewPosters] ([FilmId], [FileName])
ON [Posters].[FilmId] = [NewPosters].[FilmId]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([FilmId], [FileName]) 
    VALUES (
        [NewPosters].[FilmId],  
        [NewPosters].[FileName]
    );


SET IDENTITY_INSERT [Sessions] ON

MERGE INTO [Sessions]
USING (VALUES 
    (1, 1, 1, DATEADD(day, 2, GETDATE()), 18, 0),
    (2, 1, 1, DATEADD(day, 3, GETDATE()), 18, 0)) AS [NewSessions] (
        [Id],
        [FilmId],
        [HallId],
        [StartDateTime],
        [FreeSeatsNumber],
        [IsDeleted])
ON [Sessions].[Id] = [NewSessions].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [FilmId], [HallId], [StartDateTime], [FreeSeatsNumber], [IsDeleted]) 
    VALUES (
        [NewSessions].[Id],  
        [NewSessions].[FilmId],
        [NewSessions].[HallId],
        [NewSessions].[StartDateTime],
        [NewSessions].[FreeSeatsNumber],
        [NewSessions].[IsDeleted]
    );

SET IDENTITY_INSERT [Sessions] OFF


MERGE INTO [SeatTypePrices]
USING (VALUES 
    (1, 1, 5, 4),
    (1, 2, 10, 4),
    (1, 3, 15, 4),
    (2, 1, 5, 4),
    (2, 2, 10, 4),
    (2, 3, 15, 4)) AS [NewSeatTypePrices] ([SessionId], [SeatTypeId], [Price], [CurrencyId])
ON [SeatTypePrices].[SessionId] = [NewSeatTypePrices].[SessionId] 
    AND [SeatTypePrices].[SeatTypeId] = [NewSeatTypePrices].[SeatTypeId] 
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([SessionId], [SeatTypeId], [Price], [CurrencyId]) 
    VALUES (
        [NewSeatTypePrices].[SessionId],
        [NewSeatTypePrices].[SeatTypeid],
        [NewSeatTypePrices].[Price],
        [NewSeatTypePrices].[CurrencyId]
    );


MERGE INTO [SessionSeats]
USING (VALUES 
    (1, 1, 1, 2, GETDATE()),
    (1, 2, 1, 2, GETDATE()),
    (2, 3, 2, 2, GETDATE()),
    (2, 4, 2, 2, GETDATE()),
    (2, 5, 2, 2, GETDATE())) AS [NewSessionSeats] ([SessionId], [SeatId], [UserId], [Status], [TakenAt])
ON [SessionSeats].[SessionId] = [NewSessionSeats].[SessionId] 
    AND [SessionSeats].[SeatId] = [NewSessionSeats].[SeatId] 
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([SessionId], [SeatId], [UserId], [Status], [TakenAt]) 
    VALUES (
        [NewSessionSeats].[SessionId],
        [NewSessionSeats].[SeatId],
        [NewSessionSeats].[UserId],
        [NewSessionSeats].[Status],
        [NewSessionSeats].[TakenAt]
    );


SET IDENTITY_INSERT [Orders] ON

MERGE INTO [Orders]
USING (VALUES 
    (1, 1, 1, 32, 4, GETDATE(), 0),
    (2, 1, 2, 24, 4, GETDATE(), 0)) AS [NewOrders] (
        [Id], 
        [UserId],
        [SessionId],
        [Price],
        [CurrencyId],
        [RegistratedAt],
        [IsDeleted])
ON [Orders].[Id] = [NewOrders].[Id]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Id], [UserId], [SessionId], [Price], [CurrencyId], [RegistratedAt], [IsDeleted]) 
    VALUES (
        [NewOrders].[Id],
        [NewOrders].[UserId],
        [NewOrders].[SessionId],
        [NewOrders].[Price],
        [NewOrders].[CurrencyId],
        [NewOrders].[RegistratedAt],
        [NewOrders].[IsDeleted]
    );

SET IDENTITY_INSERT [Orders] OFF


MERGE INTO [SeatOrders]
USING (VALUES 
    (1, 1),
    (1, 2),
    (2, 3),
    (2, 4),
    (2, 5)) AS [NewSeatOrders] ([OrderId], [SeatId])
ON [SeatOrders].[OrderId] = [NewSeatOrders].[OrderId] 
    AND [SeatOrders].[SeatId] = [NewSeatOrders].[SeatId] 
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([OrderId], [SeatId]) 
    VALUES ([NewSeatOrders].[OrderId], [NewSeatOrders].[Seatid]);