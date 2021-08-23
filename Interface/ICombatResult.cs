using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Interface
{
    interface ICombatResult
    {
        public int CombatID { get; set; }

        public List<Character> CharactersInvolved { get; set; }

        public DateTime DoFCombat { get; set; }
    }
}
