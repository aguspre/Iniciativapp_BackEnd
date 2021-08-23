using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class CombatCharacters
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CombatID { get; set; }
        [ForeignKey("CombatID")]
        public Combat Combat { get; set; }

        public int CharacterID { get; set; }
        [ForeignKey("CharacterID")]

        public Character Character { get; set; }
    }
}

