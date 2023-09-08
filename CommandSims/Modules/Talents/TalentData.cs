using CommandSims.Core;
using CommandSims.Entity;
using CommandSims.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Talents
{
    public class TalentData
    {
        public List<Talent> Talents { get; set; }


        public TalentData()
        {
            Talents = new List<Talent>();
            LoadTalentPool();
        }
        public void LoadTalentPool()
        {
            Talents.Clear();
            Talents.Add(new Talent()
            {
                Id = 1,
                Name = "随身玉佩",
                Description = "或有庇护作用",
                Grade = GradeEnum.DarkGreen
            });
            Talents.Add(new Talent()
            {
                Id = 2,
                Name = "神秘的盒子",
                Description = "家传甚久的盒子，传说中祖上仙人所留，100岁时才能打开",
                Grade = GradeEnum.Yellow1
            });
            Talents.Add(new Talent()
            {
                Name = "天生神力",
                Description = "力量+5",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Attribute = new Players.PlayerAttribute(5)
                    }
                },
                Grade = GradeEnum.RoyalBlue1

            });
            Talents.Add(new Talent()
            {
                Name = "气壮如牛",
                Description = "力量+1,体力+1",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(1, 0, 1)
                    }
                }
            });
            Talents.Add(new Talent()
            {
                Name = "红颜薄命",
                Description = "颜值+2，体质-2",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(charisma:2,endurance:-2)
                    }
                }
            });
            Talents.Add(new Talent()
            {
                Name = "早产儿",
                Description = "所有属性-1",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(-1,-1,-1,-1,-1,-1,-1)
                    }
                },
                Grade = GradeEnum.Gray
            });
            Talents.Add(new Talent()
            {
                Name = "幸运儿",
                Description = "幸运+2",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(lucky:2)
                    }
                },
                Grade = GradeEnum.DarkGreen
            });
            Talents.Add(new Talent()
            {
                Name = "天命",
                Description = "幸运+9",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(lucky:9)
                    }
                },
                Grade = GradeEnum.Maroon
            });
            Talents.Add(new Talent()
            {
                Name = "天命之子",
                Description = "幸运+7",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(lucky:7)
                    }
                },
                Grade = GradeEnum.Orange1
            });
            Talents.Add(new Talent()
            {
                Name = "天命之孙",
                Description = "幸运+5",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(lucky:5)
                    }
                },
                Grade = GradeEnum.RoyalBlue1
            });
            Talents.Add(new Talent()
            {
                Name = "天命之曾孙",
                Description = "幸运+3",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(lucky:3)
                    }
                },
                Grade = GradeEnum.Navy
            });
            Talents.Add(new Talent()
            {
                Name = "胎教",
                Description = "智力+1",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(intelligence:1)
                    }
                }
            });
            Talents.Add(new Talent()
            {
                Name = "学前启蒙",
                Description = "5岁时智力+2",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Condition="age:=5",
                        Attribute = new Players.PlayerAttribute(intelligence:2)
                    }
                },
                Grade = GradeEnum.Navy
            });
            Talents.Add(new Talent()
            {
                Name = "十八变",
                Description = "18岁时颜值+2",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Condition="age:=18",
                        Attribute = new Players.PlayerAttribute(charisma:2)
                    }
                },
                Grade = GradeEnum.DarkGreen
            });
            Talents.Add(new Talent()
            {
                Name = "成熟",
                Description = "18岁时智力+2",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Condition="age:=18",
                        Attribute = new Players.PlayerAttribute(intelligence:2)
                    }
                },
                Grade = GradeEnum.DarkGreen
            });
            Talents.Add(new Talent()
            {
                Name = "四十不惑",
                Description = "四十岁时智力+2",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Condition="age:=40",
                        Attribute = new Players.PlayerAttribute(intelligence:2)
                    }
                },
                Grade = GradeEnum.DarkGreen
            });
            Talents.Add(new Talent()
            {
                Name = "宿慧",
                Description = "智力+5",
                Effects = new List<EffectEntity> {
                    new EffectEntity() {
                        Type = EffectEnum.Attribute,
                        Attribute = new Players.PlayerAttribute(intelligence:5)
                    }
                },
                Grade = GradeEnum.Purple
            });

            // rebuild id
            foreach (var item in Talents.Where(x => x.Id == 0))
            {
                item.Id = Talents.Max(x => x.Id) + 1;
            }
        }
    }
}
