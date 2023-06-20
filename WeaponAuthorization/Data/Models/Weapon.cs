using System.ComponentModel.DataAnnotations.Schema;

namespace WeaponAuthorization.Data.Models
{
    public enum WeaponType
    {
        Sword = 0,
        Gun = 1,
        MagicSpell = 2
    }

    [Table("Weapon")]
    public class Weapon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public WeaponType Type { get; set; }

    }
}
