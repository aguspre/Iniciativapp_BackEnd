using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.ModelsOld
{
    public class CombatDeprecated
    {
        [Key]
        public int CombatID { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public DateTime DofCombat { get; set; }

        //public ICollection<CombatCharacter> CombatCharacters { get; set; }

    }
}
