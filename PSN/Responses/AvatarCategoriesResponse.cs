using System.Collections.Generic;

namespace PSNSharp.Responses
{
    public enum AvatarCategoryId
    {
        All,
        Puzzle,
        ActionAdventure,
        Characters,
        Creatures,
        Fighting,
        Family,
        Kanji,
        MilitaryCombat,
        ItemsMisc,

        //Sony doesn't want a 10th cat id I guess...
        Racing = 11,

        Retro = 12,
        Rpg = 13,
        Sports = 14,
        Vehicles = 15,
        PremiumAvatars = 100,
    }

    public class Category
    {
        public AvatarCategoryId CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class AvatarCategoriesResponse
    {
        public int Start { get; set; }
        public int Size { get; set; }
        public int TotalResults { get; set; }
        public List<Category> Categories { get; set; }
    }
}