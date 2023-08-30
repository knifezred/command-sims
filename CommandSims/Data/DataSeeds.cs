using CommandSims.Entity;
using CommandSims.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandSims.Data
{
    public class DataSeeds
    {
        public List<SeedEntity> SurnameSeeds { get; set; }
        public List<SeedEntity> NameSeeds { get; set; }

        public DataSeeds()
        {
            SurnameSeeds = new();
            #region SurnameData
            SurnameSeeds.Add(
                new SeedEntity()
                {
                    Text = "赵",
                    Weight = 10,
                });
            SurnameSeeds.Add(
                new SeedEntity()
                {
                    Text = "钱",
                    Weight = 10,
                });
            #endregion

            NameSeeds = new();
            #region NameData

            #endregion

        }
        public string GetFirstName()
        {
            var weights = SurnameSeeds.Select(x => x.Weight).ToList();
            return SurnameSeeds[RandomUtils.GetNextWithWeight(weights)].Text;
        }

        public string GetLastName()
        {
            var weights = NameSeeds.Select(x => x.Weight).ToList();
            return NameSeeds[RandomUtils.GetNextWithWeight(weights)].Text;
        }
    }
}
