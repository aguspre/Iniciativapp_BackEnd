using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.ModelsOld
{
    public class CharacterDeprecated
    {

        public CharacterDeprecated()
        {
            CombatCharacters = new HashSet<CombatCharacters>();
        }

        [Key]
        public int CharacterID { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Initiative { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? UrlAvatarImg { get; set; }

        [Column(TypeName = "int")]
        public int? HitPoints { get; set; }
        
        [Column(TypeName = "int")]
        public int? ArmorClass { get; set; }

        [Column(TypeName = "bit")]
        public bool? IsPlayable { get; set; }

        [Column (TypeName = "bit")]
        public bool IsDeletable { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Creator { get; set; }

        public ICollection<CombatCharacters> CombatCharacters { get; set; }
    }
}
