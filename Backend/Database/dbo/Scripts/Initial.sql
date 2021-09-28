-- Deleting all data
TRUNCATE TABLE [UserPasswords]

TRUNCATE TABLE [SeatOrders]

TRUNCATE TABLE [SessionSeats]

TRUNCATE TABLE [CinemaServices]

TRUNCATE TABLE [SeatTypePrices]

TRUNCATE TABLE [CinemaPhotos]

TRUNCATE TABLE [HallPhotos]

TRUNCATE TABLE [Posters]

DELETE FROM [Services]

DELETE FROM [Orders]

DELETE FROM [Seats]

DELETE FROM [Sessions]

DELETE FROM [Halls]

DELETE FROM [Cinemas]

DELETE FROM [Cities]

DELETE FROM [Films]

DELETE FROM [Currencies]

DELETE FROM [Users]

DELETE FROM [UserRoles]

DELETE FROM [SeatTypes]


-- Filling data
DECLARE @TicksPerMinute AS INT = 600000000


INSERT INTO [UserRoles] ([Id], [Name]) 
VALUES 
(0, 'User'),
(1, 'Administrator')


SET IDENTITY_INSERT [Users] ON

INSERT INTO [Users] ([Id], [RoleId], [Email]) 
VALUES 
(1, 0, 'verycoolmail@gmail.com'),
(2, 1, 'newmail@outlook.com'),
(3, 0, 'wow@yandex.by'),
(4, 0, 'mew@yandex.ru')

SET IDENTITY_INSERT [Users] OFF


INSERT INTO [UserPasswords] ([UserId], [PasswordHash], [Salt])
VALUES
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
)


SET IDENTITY_INSERT [Services] ON

INSERT INTO [Services] ([Id], [Name])
VALUES
(1, 'Popcorn'),
(2, 'Cotton candy'),
(3, 'Burger'),
(4, 'Hot-dog'),
(5, 'Pizza'),
(6, 'Fries with baked calamari'),
(7, 'Nachos'),
(8, 'Chock'),
(9, 'Coca-cola'),
(10, 'Pepsi')

SET IDENTITY_INSERT [Services] OFF


SET IDENTITY_INSERT [Cities] ON

INSERT INTO [Cities] ([Id], [Name])
VALUES
(1, 'Minsk'),
(2, 'Vitebsk'),
(3, 'Brest'),
(4, 'Grodno'),
(5, 'Mogilev'),
(6, 'Gomel'),
(7, 'Molodechno'),
(8, 'Orsha') 

SET IDENTITY_INSERT [Cities] OFF


SET IDENTITY_INSERT [Currencies] ON

INSERT INTO [Currencies] ([Id], [Name])
VALUES
(1, 'EUR'),
(2, 'USD'),
(3, 'RUB'),
(4, 'BYN'),
(5, 'UAH'),
(6, 'INR')

SET IDENTITY_INSERT [Currencies] OFF


INSERT INTO [SeatTypes] ([Id], [Name], [ColorRgb])
VALUES
(0, 'Common', '#FFFFFF'),
(1, 'Vip', '#FFD700'),
(2, 'Special', '#C0C0C0')


SET IDENTITY_INSERT [Cinemas] ON

INSERT INTO [Cinemas] ([Id], [CityId], [Name], [Description], [IsDeleted])
VALUES
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
)

SET IDENTITY_INSERT [Cinemas] OFF


INSERT INTO [CinemaPhotos] ([CinemaId], [FileName])
VALUES
(1, '1.jpg'),
(2, '2.jpg'),
(3, '3.jpg'),
(4, '4,jpg'),
(5, '5.jpg')


INSERT INTO [CinemaServices] ([CinemaId], [ServiceId], [Price], [CurrencyId])
VALUES
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
(5, 2, 1.3, 4) 


SET IDENTITY_INSERT [Halls] ON

INSERT INTO [Halls] ([Id], [CinemaId], [Name], [IsDeleted])
VALUES
(1, 1, 'Hall A', 0),
(2, 2, 'Hall Prime', 0),
(3, 3, 'Hall', 0),
(4, 4, 'Hall', 0),
(5, 1, 'hall B', 0)

SET IDENTITY_INSERT [Halls] OFF


INSERT INTO [HallPhotos] ([HallId], [FileName])
VALUES
(1, '1.jpeg'),
(2, '2.jpg'),
(3, '3.jpg'),
(4, '4.jpg'),
(5, '5.jpg')


SET IDENTITY_INSERT [Seats] ON

INSERT INTO [Seats] ([Id], [HallId], [SeatTypeId], [Row], [Place])
VALUES
(1, 1, 0, 1, 1),
(2, 1, 0, 1, 2),
(3, 1, 0, 1, 3),
(4, 1, 0, 1, 4),
(5, 1, 0, 1, 5),
(6, 1, 0, 1, 6),
(7, 1, 0, 1, 7),
(8, 1, 0, 1, 8),
(9, 1, 0, 1, 9),
(10, 1, 0, 1, 10),
(11, 1, 1, 2, 1),
(12, 1, 1, 2, 2),
(13, 1, 1, 2, 3),
(14, 1, 1, 2, 4),
(15, 1, 1, 2, 5),
(16, 1, 1, 2, 6),
(17, 1, 1, 2, 7),
(18, 1, 1, 2, 8),
(19, 1, 1, 2, 9),
(20, 1, 1, 2, 10),
(21, 2, 0, 1, 1),
(22, 2, 0, 1, 2),
(23, 2, 0, 1, 3),
(24, 2, 0, 1, 4),
(25, 2, 0, 1, 5),
(26, 2, 0, 1, 6),
(27, 2, 0, 1, 7),
(28, 2, 0, 1, 8),
(29, 2, 0, 1, 9),
(30, 2, 0, 1, 10),
(31, 2, 1, 2, 1),
(32, 2, 1, 2, 2),
(33, 2, 1, 2, 3),
(34, 2, 1, 2, 4),
(35, 2, 1, 2, 5),
(36, 2, 1, 2, 6),
(37, 2, 1, 2, 7),
(38, 2, 1, 2, 8),
(39, 2, 1, 2, 9),
(40, 2, 1, 2, 10),
(41, 3, 0, 1, 1),
(42, 3, 0, 1, 2),
(43, 3, 0, 1, 3),
(44, 3, 0, 1, 4),
(45, 3, 0, 1, 5),
(46, 3, 0, 2, 1),
(47, 3, 0, 2, 2),
(48, 3, 0, 2, 3),
(49, 3, 0, 2, 4),
(50, 3, 0, 2, 5),
(51, 4, 1, 1, 1),
(52, 4, 1, 1, 2),
(53, 4, 1, 1, 3),
(54, 4, 1, 1, 4),
(55, 4, 1, 1, 5),
(56, 4, 1, 2, 1),
(57, 4, 1, 2, 2),
(58, 4, 1, 2, 3),
(59, 4, 1, 2, 4),
(60, 4, 1, 2, 5),
(61, 4, 0, 3, 1),
(62, 4, 0, 3, 2),
(63, 4, 0, 3, 3),
(64, 4, 0, 3, 4),
(65, 4, 0, 3, 5),
(66, 4, 0, 4, 1),
(67, 4, 0, 4, 2),
(68, 4, 0, 4, 3),
(69, 4, 0, 4, 4),
(70, 4, 0, 4, 5),
(71, 4, 1, 5, 1),
(72, 4, 1, 5, 2),
(73, 4, 1, 5, 3),
(74, 4, 1, 5, 4),
(75, 4, 1, 5, 5),
(76, 5, 1, 1, 1),
(77, 5, 1, 1, 2),
(78, 5, 1, 1, 3),
(79, 5, 1, 1, 4),
(80, 5, 1, 1, 5),
(81, 5, 0, 2, 1),
(82, 5, 0, 2, 2),
(83, 5, 0, 2, 3),
(84, 5, 0, 2, 4),
(85, 5, 0, 2, 5),
(86, 5, 0, 3, 1),
(87, 5, 0, 3, 2),
(88, 5, 0, 3, 3),
(89, 5, 0, 3, 4),
(90, 5, 0, 3, 5),
(91, 5, 1, 4, 1),
(92, 5, 1, 4, 2),
(93, 5, 1, 4, 3),
(94, 5, 1, 4, 4),
(95, 5, 1, 4, 5),
(96, 5, 1, 5, 1),
(97, 5, 1, 5, 2),
(98, 5, 1, 5, 3),
(99, 5, 1, 5, 4),
(100, 5, 1, 5, 5)

SET IDENTITY_INSERT [Seats] OFF


SET IDENTITY_INSERT [Films] ON

INSERT INTO [FIlms] ([Id], [Name], [Description], [ReleaseYear], [DurationInTicks], [IsDeleted])
VALUES
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
)

SET IDENTITY_INSERT [Films] OFF


INSERT INTO [Posters] ([FilmId], [FileName])
VALUES
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
(16, '16.jpg')


SET IDENTITY_INSERT [Sessions] ON

INSERT INTO [Sessions] ([Id], [FilmId], [HallId], [StartDateTime], [FreeSeatsNumber], [IsDeleted])
VALUES
(1, 1, 1, DATEADD(day, 2, GETDATE()), 18, 0),
(2, 1, 1, DATEADD(day, 3, GETDATE()), 18, 0)

SET IDENTITY_INSERT [Sessions] OFF


INSERT INTO [SeatTypePrices] ([SessionId], [SeatTypeId], [Price], [CurrencyId])
VALUES
(1, 0, 5, 4),
(1, 1, 10, 4),
(1, 2, 15, 4),
(2, 0, 5, 4),
(2, 1, 10, 4),
(2, 2, 15, 4)


INSERT INTO [SessionSeats] ([SessionId], [SeatId], [UserId], [Status], [TakenAt])
VALUES
(1, 1, 1, 2, GETDATE()),
(1, 2, 1, 2, GETDATE()),
(2, 3, 2, 2, GETDATE()),
(2, 4, 2, 2, GETDATE()),
(2, 5, 2, 2, GETDATE())

SET IDENTITY_INSERT [Orders] ON


INSERT INTO [Orders] ([Id], [UserId], [SessionId], [Price], [CurrencyId], [RegistratedAt], [IsDeleted])
VALUES
(1, 1, 1, 32, 4, GETDATE(), 0),
(2, 1, 2, 24, 4, GETDATE(), 0)

SET IDENTITY_INSERT [Orders] OFF


INSERT INTO [SeatOrders] ([OrderID], [SeatId])
VALUES
(1, 1),
(1, 2),
(2, 3),
(2, 4),
(2, 5)