using ObjectDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sqo;

namespace ObjectDatabase
{
    public class Generator
    {
        private Siaqodb db;

        public Generator(Siaqodb db)
        {
            this.db = db;
        }
        public void LoadData()
        {
            var people = RandomPeople();
            var companies = RandomCompanies();
            foreach (var person in people)
            {
                db.StoreObject(person);
            }
            foreach (var company in companies)
            {
                db.StoreObject(company);
            }
            for (int i = 0; i < 100; i++)
            {
                db.StoreObject(RandomHome());
            }

            var addr = new Address() { Street = "Six", BuildingNumber = "6", City = "Lake" };
            var comp = new Company() { Name = "forSix", PhoneNumber = "998774663", Headquaters = addr };
            var own = new Owner() { CompanyOwner = comp, Share = 70 };
            var ppl = new List<Occupant>(){new Occupant { PersonOccupant = new Person() { Name = "Kaja", Surname = "Test", PhoneNumber = "890234234"} },
                                               new Occupant{ PersonOccupant = new Person() { Name = "Anna", Surname = "Lato", PhoneNumber = "890234234"}} };

            var forSix = new Home()
            {
                Owners = new List<Owner> { own },
                Address = addr,
                Rooms = new List<Room>() { new Room() { Name = "sypialnia", Furniture = new List<Furniture>() { new Furniture() { Name = "komoda" } }, Windows = new List<Window>() { new Window() { Side = "wschód" } } } },
                Occupants = ppl
            };
            db.StoreObject(forSix);
        }

        public string RandName()
        {
            var names = new string[] { "Liam", "Noah", "Oliver", "Elijah", "William", "James", "Benjamin", "Lucas", "Henry", "Alexander",
                                       "Olivia", "Emma", "Ava", "Charlotte", "Sophia", "Amelia", "Isabella", "Mia", "Evelyn", "Harper"};

            Random rand = new Random();

            return names[rand.Next(0, names.Length - 1)];
        }
        public string RandSurame()
        {
            var surnames = new string[] {   "Aitken", "Macleod","Alexander", "Marshall","Allan", "Martin","Anderson", "Mcdonald","Bell", "Mcgregor","Black", "Mcintosh",
                                            "Boyle", "Mcintyre","Brown", "Mckay", "Bruce", "Mckenzie","Burns", "Mclean","Cameron", "Mcmillan","Campbell", "Millar",
                                            "Christie", "Miller","Clark", "Milne","Craig", "Mitchell","Crawford", "Moore","Cunningham", "Morrison","Davidson", "Muir",
                                            "Dickson", "Munro","Docherty", "Murphy","Donaldson", "Murray","Douglas", "Paterson","Duncan", "Reid","Ferguson", "Reilly",
                                            "Findlay", "Ritchie","Fleming", "Robertson","Gibson", "Scott","Gordon", "Shaw","Graham", "Simpson","Grant", "Sinclair",
                                            "Gray", "Smith","Hamilton", "Stevenson","Henderson", "Stewart","Hill", "Sutherland","Johnstone", "Wallace","Jones", "Watson",
                                            "Kelly", "Watt","Macdonald", "Wilson","Mackay", "Wood","Mackenzie", "Wright","Maclean", "Young" };

            Random rand = new Random();

            return surnames[rand.Next(0, surnames.Length - 1)];
        }


        public string RandomStreet()
        {
            var surnames = new string[] { "Hollybush Hills", "Sea View Down", "Clement Lea", "Torrington Knoll", "Horseguards", "Westbury Bridge", "Yew Brook",
                                          "Multi Way","Hermitage Hills","Lyndale Approach","March Gait","Clark Garth", "Bardwell Avenue","Walnut Tree Down",
                                          "Tilley Street","Michael Foot Avenue", "Stoney Chase","Buxton Vale","College Garth","Netherfield Poplars" };

            Random rand = new Random();

            return surnames[rand.Next(0, surnames.Length - 1)];
        }
        public string RandomCity()
        {
            var cities = new string[] {"Mifflintown","Haw River","Moreauville","Barling","New Bedford","Mountain View","West Columbia","South Webster","Lake Mary",
                                       "North Grosvenor Dale","East Globe","Alder","Crowley Lake","Ben Avon","Mehlville","Rexburg","Gering","Sewickley","Shongaloo",
                                       "Onset","Oconto Falls","West Alexander","Centreville","South Bethany","Seymour"};
            Random rand = new Random();

            return cities[rand.Next(0, cities.Length - 1)];
        }
        public string RandomNumber(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        public string RandomEmail(string name, string surname)
        {
            var digs = RandomNumber(3);
            return $"{name}.{surname}{digs}@email.com";
        }
        public string RandomZipCode()
        {
            return $"{RandomNumber(2)}-{RandomNumber(3)}";
        }
        public List<Person> RandomPeople()
        {
            var people = new List<Person>();
            for (int i = 0; i < 1000; i++)
            {
                var name = RandName();
                var surname = RandSurame();
                people.Add(new Person() { Name = name, Surname = surname, Email = RandomEmail(name.ToLower(), surname.ToLower()), PhoneNumber = RandomNumber(9) });
            }
            return people;
        }
        public Address RandomAddress()
        {
            return new Address()
            {
                City = RandomCity(),
                Country = "United States",
                ApartmentNumber = RandomNumber(2),
                BuildingNumber = RandomNumber(3),
                ZIPCode = RandomZipCode(),
                Street = RandomStreet()
            };
        }
        public Address RandomAddress(List<Address> addresses)
        {
            Random rand = new Random();

            return addresses[rand.Next(0, addresses.Count() - 1)];
        }
        public List<Window> RandomWindows(double roomArea)
        {
            var random = new Random();
            var windows = new List<Window>();
            var side = new string[] { "północ", "południe", "wschód", "zachód" };
            if (roomArea < 6)
            {
                windows.Add(new Window() { Area = roomArea / 9, Side = side[random.Next(0, side.Count() - 1)] });
            }
            else
                for (int i = 0; i < random.Next(1, 3); i++)
                {
                    windows.Add(new Window() { Area = roomArea / 9, Side = side[random.Next(0, side.Count() - 1)] });
                }

            return windows;
        }
        public Door RandomDoor()
        {
            var random = new Random();
            var door = new Door();
            var types = new string[] { "prawe", "lewe", "balkonowe", "łazienkowe" };
            door.Type = types[random.Next(0, types.Count() - 1)];
            return door;
        }
        public List<Furniture> RandomFurnitures(Room room)
        {
            var random = new Random();
            var furniture = new List<Furniture>();
            for (int i = 0; i < 4; i++)
            {
                furniture.Add(RandomFurniture(room));
            }
            return furniture;
        }
        public Furniture RandomFurniture(Room room)
        {
            string roomType = room.Name;
            var random = new Random();
            var furniture = new Furniture();

            var names = new List<string>();
            var kitchenFurniture = new List<string> { "piekarnik", "zlew", "lodówka", "zmywarka" };
            var loundryFurniture = new List<string> { "pralka", "suszarka", "zlew" };
            var toiletFurniture = new List<string> { "toaleta", "zlew" };
            var bathroomFurniture = new List<string> { "prysznic", "wanna" };
            var bedroomFurniture = new List<string> { "łóżko", "komoda", "szafa" };
            var livingrommFurniture = new List<string> { "kanapa", "stół", "regał", "witryna" };
            var wardrobeFurniture = new List<string> { "szafa", "szafa parowa", "lustro" };
            var storeroomFurniture = new List<string> { "regał", "lodówka", "zamrażalnik" };
            bathroomFurniture.AddRange(loundryFurniture);
            bathroomFurniture.AddRange(toiletFurniture);

            var roomDict = new Dictionary<string, List<string>>() { { "kuchnia", kitchenFurniture}, { "łazienka", bathroomFurniture},
                                                                    { "toaleta", toiletFurniture }, { "salon", livingrommFurniture},
                                                                    { "sypialnia", bedroomFurniture }, { "gościnny", bedroomFurniture},
                                                                    { "garderoba", wardrobeFurniture}, { "spiżarnia", storeroomFurniture},
                                                                    { "pralnia", loundryFurniture } };

            foreach (var f in roomDict[room.Name].ToList())
            {
                names = CheckFurnitureToRemove(roomDict[room.Name], f, room, 2);
                names = CheckFurnitureToAdd(roomDict[room.Name], f, room);
            }

            furniture.Name = names[random.Next(0, names.Count() - 1)];

            return furniture;
        }
        public Room RandomRoom(Home home)
        {
            var room = new Room();

            var random = new Random();
            var types = new List<string> { "kuchnia", "łazienka", "toaleta", "salon", "sypialnia", "gościnny", "garderoba", "spiżarnia", "pralnia", "garaż" };
            var mustHave = new List<string> { "kuchnia", "łazienka", "salon", "sypialnia" };
            int rooms = 0;

            if (home.Rooms != null)
            {
                rooms = home.Rooms.ToList().Count();
            }

            foreach (var type in types.ToList())
            {
                types = CheckRoomToRemove(types, type, home, 2);
            }

            if (rooms <= mustHave.Count())
                foreach (var type in mustHave.ToList())
                {
                    types = CheckRoomToAdd(mustHave, type, home);
                }

            room.Name = types[random.Next(0, types.Count() - 1)];
            room.Area = random.Next(5, 25);
            room.Doors = new List<Door>() { RandomDoor() };
            if (random.Next(0, 1) == 0) { room.Doors.Add(RandomDoor()); }
            room.Windows = new List<Window>();
            room.Windows.AddRange(RandomWindows(room.Area));
            room.Furniture = new List<Furniture>();
            room.Furniture.AddRange(RandomFurnitures(room));
            return room;
        }
        private List<string> CheckRoomToRemove(List<string> typesToAdd, string toCheck, Home home, int noMoreThan)
        {
            var typesChecked = typesToAdd;
            if (home.Rooms != null)
                if (home.Rooms.Where(x => x.Name == toCheck).Count() >= noMoreThan)
                {
                    typesChecked.Remove(toCheck);
                }

            return typesChecked;
        }
        private List<string> CheckRoomToAdd(List<string> typesToAdd, string toCheck, Home home)
        {
            var typesChecked = typesToAdd;
            if (home.Rooms != null)
                if (home.Rooms.Where(x => x.Name == toCheck).Count() == 0)
                {
                    typesChecked.Clear();
                    typesChecked.Add(toCheck);
                }
            return typesChecked;
        }
        private List<string> CheckFurnitureToRemove(List<string> typesToAdd, string toCheck, Room room, int noMoreThan)
        {
            var typesChecked = typesToAdd;
            if (room.Furniture != null)
                if (room.Furniture.Where(x => x.Name == toCheck).Count() >= noMoreThan)
                {
                    typesChecked.Remove(toCheck);
                }

            return typesChecked;
        }
        private List<string> CheckFurnitureToAdd(List<string> typesToAdd, string toCheck, Room room)
        {
            var typesChecked = typesToAdd;
            if (room.Furniture != null)
                if (room.Furniture.Where(x => x.Name == toCheck).Count() == 0)
                {
                    typesChecked.Clear();
                    typesChecked.Add(toCheck);
                }

            return typesChecked;
        }
        public List<Company> RandomCompanies()
        {
            var companies = new List<Company>();
            for (int i = 0; i < 64; i++)
            {
                var name = RandomCompanyName();
                companies.Add(new Company()
                {
                    Name = name,
                    PhoneNumber = RandomNumber(9),
                    Headquaters = RandomAddress(),
                    Email = $"{name}@email.com",
                });
            }

            return companies;
        }
        public string RandomCompanyName()
        {
            var random = new Random();
            var names = new List<string>{ "Caratch", "Caromni", "Stepegg", "Caraipi", "Netelectra", "Cafesea", "Cafefire",
                          "Woodtap","Reelectra","Cafejar","Cafemirror","Electra", "The Car Group", "Bestofstep",
                          "Enginecafe", "Nuelectra","Carer","Softdude","Woodcell","Targetwood","Cardecu","Carroch",
                          "Stephq","Sweetwiki","Hubwood","Wowstep","Rreget","Telesweet","Woodoffers","Steploop",
                          "Cabinsoft","Electraall","Carceag	","Softjunky","Ranchsoft","Woodrace","Titanicpower",
                          "Trycup","Goldalpha","Zippyhigh","Leaderhigh", "Herowild","Timeneo","Vipever","Funvita",
                          "Nextwavefashion","Safetyvita","Vitaprofessional","Surfacefashion", "Hugecake","Morenova",
                          "Joyprofessional","Availgold","Primalteam","Winnerelite","Rightelite","Meelite","Dayneo",
                          "Novagenius","Onlynova", "Firstwiz","Pitchlook","Maximateam","Royalprimary"};

            var name = names[random.Next(0, names.Count() - 1)];
            while (db.LoadAll<Company>().Any(x => x.Name == name))
            {
                names.Remove(name);
                name = names[random.Next(0, names.Count() - 1)];
            }
            return name;
        }
        public List<Owner> RandomOwners()
        {
            var random = new Random();
            var owners = new List<Owner>();
            var people = db.LoadAll<Person>().ToList();
            var companies = db.LoadAll<Company>().ToList();
            int newObjectShare = 0;
            var avaliableShare = 10;

            while (avaliableShare != 0)
            {
                if (avaliableShare <= 3)
                {
                    newObjectShare = avaliableShare;
                }
                else
                {
                    newObjectShare = random.Next(1, avaliableShare);
                }

                if (random.Next(0, 2) == 0)
                {
                    owners.Add(new Owner() { PersonOwner = people[random.Next(0, people.Count() - 1)], Share = newObjectShare * 10 });
                }
                else
                {
                    owners.Add(new Owner() { CompanyOwner = companies[random.Next(0, companies.Count() - 1)], Share = newObjectShare * 10 });
                }

                avaliableShare -= newObjectShare;
            }

            return owners;
        }
        public List<Occupant> RandomOccupants()
        {
            var random = new Random();
            var homes = db.LoadAll<Home>().ToList();
            var people = db.LoadAll<Person>().ToList();

            var alreadyOccupants = new List<Occupant>();
            var occupants = new List<Occupant>();

            foreach (var home in homes)
            {
                if (home.Occupants != null)
                    alreadyOccupants.AddRange(home.Occupants);
            }

            for (int i = 0; i < random.Next(1, 3); i++)
            {
                var randomPerson = people[random.Next(0, people.Count() - 1)];
                while (alreadyOccupants.Count(x => x.PersonOccupant == randomPerson) >= 3)
                {
                    people.Remove(randomPerson);
                    randomPerson = people[random.Next(0, people.Count() - 1)];
                }
                occupants.Add(new Occupant() { PersonOccupant = randomPerson });
            }

            return occupants;
        }
        public Home RandomHome()
        {
            var home = new Home();
            home.Address = RandomAddress();
            home.Occupants = RandomOccupants();
            home.Owners = RandomOwners();
            home.Rooms = new List<Room>();
            var random = new Random();

            for (int i = 0; i < random.Next(4, 18); i++)
            {
                home.Rooms.Add(RandomRoom(home));
            }

            return home;
        }
    }
}
