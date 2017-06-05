using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebSpaServices.Models
{
    /// <summary>
    /// Инициализатор нашей тестовой БД с животными
    /// </summary>
    public class AnimalsDbInitializer : DropCreateDatabaseAlways<AnimalsContext>
    {
        protected override void Seed(AnimalsContext db)
        {
            // Skins
            Skin skin1 = new Skin { SkinColor = "Белый" };
            Skin skin2 = new Skin { SkinColor = "Бурый" };
            Skin skin3 = new Skin { SkinColor = "Коричневый" };
            Skin skin4 = new Skin { SkinColor = "Красный" };
            Skin skin5 = new Skin { SkinColor = "Желтый" };
            Skin skin6 = new Skin { SkinColor = "Серый" };
            Skin skin7 = new Skin { SkinColor = "Черный" };
            db.Skins.AddRange(new List<Skin> { skin1, skin2, skin3, skin4, skin5, skin6, skin7 });

            // Kinds
            Kind kind1 = new Kind { AnimalKind = "Амфибии" };
            Kind kind2 = new Kind { AnimalKind = "Беспозвоночные " };
            Kind kind3 = new Kind { AnimalKind = "Млекопитающие" };
            Kind kind4 = new Kind { AnimalKind = "Птицы" };
            Kind kind5 = new Kind { AnimalKind = "Рептилии" };
            Kind kind6 = new Kind { AnimalKind = "Рыбы" };
            db.Kinds.AddRange(new List<Kind> { kind1, kind2, kind3, kind4, kind5, kind6 });


            // Locations
            Location location1 = new Location { LocationName = "Выставка рептилий" };
            Location location2 = new Location { LocationName = "Дом птиц" };     
            Location location3 = new Location { LocationName = "Ночной мир" };
            Location location4 = new Location { LocationName = "Обезьяны" };
            Location location5 = new Location { LocationName = "Остров зверей" };
            Location location6 = new Location { LocationName = "Слоны" };
            db.Locations.AddRange(new List<Location> { location1, location2, location3, location4, location5, location6 });

            // Regions
            Region region1 = new Region { RegionName = "Австралия и Океания" };
            Region region2 = new Region { RegionName = "Азия" };
            Region region3 = new Region { RegionName = "Америка Северная" };
            Region region4 = new Region { RegionName = "Америка Южная" };
            Region region5 = new Region { RegionName = "Арктика" };
            Region region6 = new Region { RegionName = "Африка" };
            Region region7 = new Region { RegionName = "Европа" };
            db.Regions.AddRange(new List<Region> { region1, region2, region3, region4, region5, region6, region7 });
            db.SaveChanges();



            // Animals
            Animal animal1 = new Animal { AnimalName = "Слоны", Skin = skin1, Kind = kind3, Location = location5, 
                Regions = new List<Region>() {region6, region7} };
            Animal animal2 = new Animal { AnimalName = "Шимпанзе", Skin = skin6, Kind = kind3, Location = location6, 
                Regions = new List<Region>() { region6, region1 } };
            Animal animal3 = new Animal { AnimalName = "Зебры", Skin = skin7, Kind = kind3, Location = location4, 
                Regions = new List<Region>() {region1, region3} };
            Animal animal4 = new Animal { AnimalName = "Жирафы", Skin = skin4, Kind = kind3, Location = location4,
                Regions = new List<Region>() { region5, region6 } };
            Animal animal5 = new Animal { AnimalName = "Лебеди", Skin = skin2, Kind = kind4, Location = location3,
                Regions = new List<Region>() { region2, region3, region5 } };
            Animal animal6 = new Animal { AnimalName = "Фламинго", Skin = skin5, Kind = kind4, Location = location3,
                Regions = new List<Region>() { region6, region7 } };
            Animal animal7 = new Animal { AnimalName = "Пингвины", Skin = skin3, Kind = kind4, Location = location4,
                Regions = new List<Region>() { region2, region3, region5 } };
            Animal animal8 = new Animal { AnimalName = "Кенгуру", Skin = skin4, Kind = kind3, Location = location4,
                Regions = new List<Region>() { region6, region1 } };
            Animal animal9 = new Animal { AnimalName = "Крокодилы", Skin = skin3, Kind = kind5, Location = location1,
                Regions = new List<Region>() { region1, region3, region5 } };
            Animal animal10 = new Animal { AnimalName = "Ламы", Skin = skin1, Kind = kind3, Location = location5,
                Regions = new List<Region>() { region1, region3 } };
            db.Animals.AddRange(new List<Animal> { animal1, animal2, animal3, animal4, animal5, animal6, animal7, animal8, animal9, animal10 });


            Animal animal11 = new Animal { AnimalName = "Филины", Skin = skin6, Kind = kind4, Location = location6, 
                Regions = new List<Region>() { region1, region3 } };
            Animal animal12 = new Animal { AnimalName = "Фазаны", Skin = skin7, Kind = kind4, Location = location4, 
                Regions = new List<Region>() { region2, region3, region5 } };
            Animal animal13 = new Animal { AnimalName = "Бобры", Skin = skin4, Kind = kind3, Location = location4, 
                Regions = new List<Region>() { region5, region6 } };
            Animal animal14 = new Animal { AnimalName = "Волки", Skin = skin2, Kind = kind3, Location = location3, 
                Regions = new List<Region>() { region1, region2 } };
            Animal animal15 = new Animal { AnimalName = "Лисы", Skin = skin5, Kind = kind3, Location = location3, 
                Regions = new List<Region>() { region1, region4, region5 } };
            Animal animal16 = new Animal { AnimalName = "Тюлени", Skin = skin3, Kind = kind1, Location = location4, 
                Regions = new List<Region>() { region5, region7 } };
            Animal animal17 = new Animal { AnimalName = "Сурки", Skin = skin4, Kind = kind3, Location = location4, 
                Regions = new List<Region>() { region1, region7 } };
            Animal animal18 = new Animal { AnimalName = "Журавли", Skin = skin3, Kind = kind4, Location = location1, 
                Regions = new List<Region>() { region6, region7 } };
            Animal animal19 = new Animal { AnimalName = "Попугаи", Skin = skin3, Kind = kind4, Location = location1, 
                Regions = new List<Region>() { region1, region3, region5 } };
            Animal animal20 = new Animal { AnimalName = "Макаки", Skin = skin1, Kind = kind3, Location = location5, 
                Regions = new List<Region>() { region1, region3 } };
            db.Animals.AddRange(new List<Animal> { animal11, animal12, animal13, animal14, animal15, animal16, animal17, animal18, animal19, animal20 });


            Animal animal21 = new Animal { AnimalName = "Еноты", Skin = skin6, Kind = kind3, Location = location6, 
                Regions = new List<Region>() { region5, region7 } };
            Animal animal22 = new Animal { AnimalName = "Козы", Skin = skin7, Kind = kind3, Location = location4, 
                Regions = new List<Region>() { region2, region3 } };
            Animal animal23 = new Animal { AnimalName = "Антилопы", Skin = skin4, Kind = kind3, Location = location4, 
                Regions = new List<Region>() { region4, region7 } };
            Animal animal24 = new Animal { AnimalName = "Рыси", Skin = skin2, Kind = kind3, Location = location3, 
                Regions = new List<Region>() { region1, region6 } };
            Animal animal25 = new Animal { AnimalName = "Тапиры", Skin = skin5, Kind = kind3, Location = location3, 
                Regions = new List<Region>() { region1, region3, region5 } };
            Animal animal26 = new Animal { AnimalName = "Гепарды", Skin = skin3, Kind = kind3, Location = location4, 
                Regions = new List<Region>() { region1, region3, region5 } };
            Animal animal27 = new Animal { AnimalName = "Ягуары", Skin = skin4, Kind = kind3, Location = location4, 
                Regions = new List<Region>() {region5, region3} };
            Animal animal28 = new Animal { AnimalName = "Ленивцы", Skin = skin3, Kind = kind3, Location = location1, 
                Regions = new List<Region>() {region7, region2} };
            Animal animal29 = new Animal { AnimalName = "Медведи", Skin = skin3, Kind = kind3, Location = location1, 
                Regions = new List<Region>() {region4, region6} };
            Animal animal30 = new Animal { AnimalName = "Павлины", Skin = skin3, Kind = kind4, Location = location2, 
                Regions = new List<Region>() {region5, region7} };
            db.Animals.AddRange(new List<Animal> { animal21, animal22, animal23, animal24, animal25, animal26, animal27, animal28, animal29, animal30 });

            db.SaveChanges();

            base.Seed(db);
        }
    }
}