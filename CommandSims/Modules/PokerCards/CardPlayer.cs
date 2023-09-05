using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.PokerCards
{
    public class CardPlayer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Point { get; set; }

        public int HandIndex { get; set; }

        public string LastOutCard { get; set; }

        public List<CardEntity> Cards { get; set; }

    }
}
