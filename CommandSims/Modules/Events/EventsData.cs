using CommandSims.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Events
{
    public class EventsData
    {
        public List<EventEntity> Events { get; set; }


        public EventsData()
        {
            Events = new List<EventEntity>();

        }

        public void InitData()
        {
            Events.Clear();
            Events.Add(new EventEntity()
            {
                Id = 1,
                Name = "家境",
                Description = "Description",
                Enabled = true,
                Effects = Sims.Game.GetEffects(1),
                Selects = new List<EventSelectItem>()
            });
            Events.Add(new EventEntity()
            {
                Id = 2,
                Name = "家境",
                Description = "Description",
                Enabled = true,
                Selects = new List<EventSelectItem>()
            });
            Events.Add(new EventEntity()
            {
                Id = 3,
                Name = "家境",
                Description = "Description",
                Enabled = true,
                Selects = new List<EventSelectItem>()
            });
        }


    }
}
