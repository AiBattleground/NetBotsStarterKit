using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBotsClient.Models
{
    public class GameBoard
    {
        public Grid Grid { get; set; }
        public int EnemyEnergy { get; set; }
        public int MyEnergy { get; set; }

        public int Turn { get; set; }
        public int TurnLimit { get; set; }
    }
}
