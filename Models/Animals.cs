using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebSpaServices.Models
{
    /// <summary>
    /// Модель: Животные
    /// </summary>
    public class Animal
    {
        public int AnimalId { get; set; }

        public string AnimalName { get; set; }

        public int SkinId { get; set; }
        public Skin Skin { get; set; }

        public int KindId { get; set; }
        public Kind Kind { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public ICollection<Region> Regions { get; set; }
        public Animal()
        {
            Regions = new List<Region>();
        }

        public override string ToString()
        {
            return string.Format("Animal (Id={0}, AnimalName={1}, SkinId={2}, KindId={3}, LocationId={4}, Regions.Id={5})",
                AnimalId, AnimalName, SkinId, KindId, LocationId, string.Join(",", Regions.Select(r => r.Id)));
        }
    }

    /// <summary>
    /// Модель, используемая для фильтрации животных
    /// </summary>
    public class AnimalsFilter
    {
        public int SkinId { get; set; }

        public int KindId { get; set; }

        public ICollection<int> RegIds { get; set; }

        public AnimalsFilter()
        {
            RegIds = new List<int>();
        }

        public override string ToString()
        {
            return string.Format("AnimalsFilter (SkinId={0}, KindId={1}, RegionsIds={2})", SkinId, KindId, string.Join(",", RegIds));
        }
    }

    /// <summary>
    /// Модель: Животные - "облегченная" версия - только идентификаторы - для уменьшения трафика
    /// </summary>
    public class AnimalLight
    {
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }

        public int SkinId { get; set; }

        public int KindId { get; set; }

        public int LocationId { get; set; }

        public ICollection<int> RegIds { get; set; }

        public AnimalLight()
        {
            RegIds = new List<int>();
        }

        public override string ToString()
        {
            return string.Format("AnimalLight (Id={0}, AnimalName={1}, SkinId={2}, KindId={3}, LocationId={4}, RegIds={5})",
                AnimalId, AnimalName, SkinId, KindId, LocationId, string.Join(",", RegIds));
        }
    }


    /// <summary>
    /// Модель: ЦВЕТ ШКУРЫ
    /// </summary>
    public class Skin
    {
        public int Id { get; set; }
        public string SkinColor { get; set; }
    }

    /// <summary>
    /// Модель: ВИД ЖИВОТНОГО
    /// </summary>
    public class Kind
    {
        public int Id { get; set; }
        public string AnimalKind { get; set; }
    }

    /// <summary>
    /// Модель: МЕСТОНАХОЖДЕНИЕ
    /// </summary>
    public class Location
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
    }

    /// <summary>
    /// Модель: РЕГИОН
    /// </summary>
    public class Region
    {
        public int Id { get; set; }
        public string RegionName { get; set; }

        public ICollection<Animal> Animals { get; set; }
        public Region()
        {
            Animals = new List<Animal>();
        }
    }

}