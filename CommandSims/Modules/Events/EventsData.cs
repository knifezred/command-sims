﻿using CommandSims.Core;
using CommandSims.Entity;
using CommandSims.Enums;
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
            InitData();

        }

        public void InitData()
        {
            Events.Clear();
            Events.Add(new EventEntity()
            {
                Name = "天赋选择",
                Description = "由于你是域外之人，出生前可选择3个天赋",
                MaxSelect = 3,
                Selects = Sims.Game.GetRandomTalents(),
            });

            #region 家境 1-4

            Events.Add(new EventEntity()
            {
                Id = 1,
                Name = "家境",
                Description = "你出生在书香门第,自幼蒙读，智力+1，魅力+1",
                Effects =
                {
                    new EffectEntity()
                    {
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute
                        {
                            Intelligence=1,
                            Charisma=1,
                        }
                    }
                }
            });
            Events.Add(new EventEntity()
            {
                Id = 2,
                Name = "家境",
                Description = "你是个孤儿,吃百家饭长大。感知+1,幸运+1,体质-1",
                Effects =
                {
                    new EffectEntity()
                    {
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute(perception:1,endurance:-1,lucky:1)
                    }
                }
            });
            Events.Add(new EventEntity()
            {
                Id = 3,
                Name = "家境",
                Description = "你出生在武学世家，自幼习武。力量+1，体质+1",
                Effects =
                {
                    new EffectEntity()
                    {
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute(strength:1,endurance:1)
                    }
                }

            });
            Events.Add(new EventEntity()
            {
                Id = 4,
                Name = "家境",
                Description = "你出生在玄门家族,讲经习文,丹药调理。所有属性+1",
                Effects =
                {
                    new EffectEntity()
                    {
                        Type =EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute(1,1,1,1,1,1,1)
                    }
                },
                Weight = 0,
            });

            #endregion

            #region 抓周 10-19
            string babySeize = "1岁时，父母为你准备了抓周道具，你选择了";
            Events.Add(new EventEntity()
            {
                Id = 10,
                Name = "抓周",
                Description = babySeize + "一把小刀,全属性+1。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Agility=1,
                            Charisma=1,
                            Endurance=1,
                            Intelligence=1,
                            Lucky=1,
                            Perception =1,
                            Strength=1,
                        }
                    }
                }
            });

            Events.Add(new EventEntity()
            {
                Id = 11,
                Name = "抓周",
                Description = babySeize + "一把算盘,智力+2。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Intelligence=3,
                        }
                    }
                }
            });
            Events.Add(new EventEntity()
            {
                Id = 12,
                Name = "抓周",
                Description = babySeize + "一面镜子,魅力+2。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Charisma=2,
                        }
                    }
                }
            });
            Events.Add(new EventEntity()
            {
                Id = 13,
                Name = "抓周",
                Description = babySeize + "一柄如意,运气+2。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Lucky=2,
                        }
                    }
                }
            });
            Events.Add(new EventEntity()
            {
                Id = 14,
                Name = "抓周",
                Description = babySeize + "一双鞋子,敏捷+2。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Agility=2,
                        }
                    }
                }
            });
            Events.Add(new EventEntity()
            {
                Id = 15,
                Name = "抓周",
                Description = babySeize + "一个包子,体质+2。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Endurance=2,
                        }
                    }
                }
            });
            Events.Add(new EventEntity()
            {
                Id = 16,
                Name = "抓周",
                Description = babySeize + "一把大锤,力量+3。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Strength=3,
                        }
                    }
                }
            });
            Events.Add(new EventEntity()
            {
                Id = 17,
                Name = "抓周",
                Description = babySeize + "一副眼镜,感知+2。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Perception =2,
                        }
                    }
                }
            });

            Events.Add(new EventEntity()
            {
                Id = 18,
                Name = "抓周",
                Description = babySeize + "一把键盘,全属性+3。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Agility=3,
                            Charisma=3,
                            Endurance=3,
                            Intelligence=3,
                            Lucky=3,
                            Perception =3,
                            Strength=3,
                        }
                    }
                }
            });
            Events.Add(new EventEntity()
            {
                Id = 19,
                Name = "抓周",
                Description = babySeize + "一本经书,智力+2,感知+1。",
                Effects =
                {
                    new EffectEntity(){
                        Type=EffectEnum.Attribute,
                        Attribute=new Players.PlayerAttribute()
                        {
                            Intelligence=2,
                            Perception =1,
                        }
                    }
                }
            });
            #endregion

            #region MyRegion
            Events.Add(new EventEntity()
            {
                Id = 20,
                Name = "",
                Description = "",
            });
            #endregion




            foreach (var item in Events.Where(x => x.Id == 0))
            {
                item.Id = Events.Max(x => x.Id) + 1;
            }
        }


    }
}
