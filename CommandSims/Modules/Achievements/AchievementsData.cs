using CommandSims.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Modules.Achievements
{
    public class AchievementsData
    {
        public List<AchievementEntity> Achievements;
        public AchievementsData()
        {
            Achievements = new List<AchievementEntity>();
            InitData();
        }

        public void InitData()
        {
            Achievements.Clear();
            int id = 1;
            // 重开次数
            Achievements.Add(new AchievementEntity()
            {
                Id = id++,
                Name = "既视感",
                Description = "轮回了10次",
                Grade = GradeEnum.DarkGray,
                Condition = "TMS>9",
                Opportunity = "END",
            });
            Achievements.Add(new AchievementEntity()
            {
                Id = id++,
                Name = "孟婆愁",
                Description = "轮回了50次",
                Grade = GradeEnum.DarkGreen,
                Condition = "TMS>49",
                Opportunity = "END",
            });
            Achievements.Add(new AchievementEntity()
            {
                Id = id++,
                Name = "所有人都是我",
                Description = "轮回了200次",
                Grade = GradeEnum.DarkBlue,
                Condition = "TMS>199",
                Opportunity = "END",
            });
            // 存活时间


        }

    }
}
