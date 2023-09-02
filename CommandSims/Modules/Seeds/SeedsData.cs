using CommandSims.Constants;
using CommandSims.Utils;
using KnifeZ.Unity.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CommandSims.Modules.Seeds
{
    public class SeedsData
    {
        /// <summary>
        /// 楚辞
        /// </summary>
        public List<SeedEntity> ChuciSeeds { get; set; }
        /// <summary>
        /// 辞赋
        /// </summary>
        public List<SeedEntity> CifuSeeds { get; set; }
        /// <summary>
        /// 古诗三百首
        /// </summary>
        public List<SeedEntity> GushiSeeds { get; set; }
        /// <summary>
        /// 诗经
        /// </summary>
        public List<SeedEntity> ShijingSeeds { get; set; }
        /// <summary>
        /// 唐诗
        /// </summary>
        public List<SeedEntity> TangshiSeeds { get; set; }
        /// <summary>
        /// 宋词
        /// </summary>
        public List<SeedEntity> SongciSeeds { get; set; }
        /// <summary>
        /// 乐府诗集
        /// </summary>
        public List<SeedEntity> YuefuSeeds { get; set; }
        public List<SeedEntity> SurnameSeeds { get; set; }
        public List<SeedEntity> NameSeeds { get; set; }

        public List<SeedEntity> SkillNameSeeds { get; set; }

        public SeedsData()
        {
            #region SurnameData
            var surnames = @"赵 100
钱 100
孙 100
李 100
周
吴
郑
王
冯
陈
褚
卫
蒋
沈
韩
杨
朱
秦
尤
许
何
吕
施
张
孔
曹
严
华
金
魏
陶
姜
戚
谢
邹
喻
柏
水
窦
章
云
苏
潘
葛
奚
范
彭
郎
鲁
韦
昌
马
苗
凤
花
方
俞
任
袁
柳
酆
鲍
史
唐
费
廉
岑
薛
雷
贺
倪
汤
滕
殷
罗
毕
郝
邬
安
常
乐
于
时
傅
皮
卞
齐
康
伍
余
元
卜
顾
孟
平
黄
和
穆
萧
尹
姚
邵
湛
汪
祁
毛
禹
狄
米
贝
明
臧
计
伏
成
戴
谈
宋
茅
庞
熊
纪
舒
屈
项
祝
董
梁
杜
阮
蓝
闵
席
季
麻
强
贾
路
娄
危
江
童
颜
郭
梅
盛
林
刁
钟
徐
邱
骆
高
夏
蔡
田
樊
胡
凌
霍
虞
万
支
柯
咎
管
卢
莫
经
房
裘
缪
干
解
应
宗
宣
丁
贲
邓
郁
单
杭
洪
包
诸
左
石
崔
吉
钮
龚
程
嵇
邢
滑
裴
陆
荣
翁
荀
羊
於
惠
甄
魏
加
封
芮
羿
储
靳
汲
邴
糜
松
井
段
富
巫
乌
焦
巴
弓
牧
隗
山
谷
车
侯
宓
蓬
全
郗
班
仰
秋
仲
伊
宫
宁
仇
栾
暴
甘
钭
厉
戎
祖
武
符
刘
姜
詹
束
龙
叶
幸
司
韶
郜
黎
蓟
薄
印
宿
白
怀
蒲
台
从
鄂
索
咸
籍
赖
卓
蔺
屠
蒙
池
乔
阴
郁
胥
能
苍
双
闻
莘
党
翟
谭
贡
劳
逄
姬
申
扶
堵
冉
宰
郦
雍
却
璩
桑
桂
濮
牛
寿
通
边
扈
燕
冀
郏
浦
尚
农
温
别
庄
晏
柴
瞿
阎
充
慕
连
茹
习
宦
艾
鱼
容
向
古
易
慎
戈
廖
庚
终
暨
居
衡
步
都
耿
满
弘
匡
国
文
寇
广
禄
阙
东
殴
殳
沃
利
蔚
越
夔
隆
师
巩
厍
聂
晁
勾
敖
融
冷
訾
辛
阚
那
简
饶
空
曾
毋
沙
养
鞠
须
丰
巢
关
蒯
相
查
后
江
红
游
竺
权
逯
盖
益
桓
公
万俟
司马
上官
欧阳
夏侯
诸葛
闻人
东方
赫连
皇甫
尉迟
公羊
澹台
公冶
宗政
濮阳
淳于
仲孙
太叔
申屠
公孙
乐正
轩辕
令狐
钟离
闾丘
长孙
慕容
鲜于
宇文
司徒
司空
官
司寇
颛孙
端木
巫马
公西
漆雕
乐正
壤驷
公良
拓拔
夹谷
宰父
谷粱
百里
东郭
南门
呼延
归
海
羊舌
微生
岳
帅
缑亢
况后
有琴
梁丘
左丘
东门
西门
商牟
佘佴
伯赏
南宫
墨
哈
年
爱
阳
佟
第五
言
福".Split("\n");
            SurnameSeeds = new();
            foreach (var s in surnames)
            {
                SurnameSeeds.Add(new SeedEntity()
                {

                    Text = s.Split(' ')[0].Trim(),
                    Weight = s.Contains(' ') ? int.Parse(s.Split(" ")[1]) : 10,
                });
            }

            #endregion

            #region DistSeeds
            YuefuSeeds = InitializeSeed("yuefu.json");
            TangshiSeeds = InitializeSeed("tangshi.json");
            SongciSeeds = InitializeSeed("songci.json");
            ShijingSeeds = InitializeSeed("shijing.json");
            GushiSeeds = InitializeSeed("gushi.json");
            CifuSeeds = InitializeSeed("cifu.json");
            ChuciSeeds = InitializeSeed("chuci.json");
            #endregion

            #region NameData
            NameSeeds = new();
            NameSeeds.AddRange(CifuSeeds);
            NameSeeds = CombineSeeds(NameSeeds, YuefuSeeds);
            NameSeeds = CombineSeeds(NameSeeds, ChuciSeeds);
            NameSeeds = CombineSeeds(NameSeeds, TangshiSeeds);
            NameSeeds = CombineSeeds(NameSeeds, YuefuSeeds);
            NameSeeds = CombineSeeds(NameSeeds, GushiSeeds);
            //NameSeeds = CombineSeeds(NameSeeds, SongciSeeds);
            //NameSeeds = CombineSeeds(NameSeeds, ShijingSeeds);
            #endregion

            SkillNameSeeds = new();
            SkillNameSeeds.AddRange(CifuSeeds);    // 9
            SkillNameSeeds = CombineSeeds(SkillNameSeeds, TangshiSeeds);
            SkillNameSeeds = CombineSeeds(SkillNameSeeds, ChuciSeeds);
            //SkillNameSeeds.AddRange(TangshiSeeds); // 8.5
            //SkillNameSeeds.AddRange(ChuciSeeds);   // 8
            //SkillNameSeeds.AddRange(YuefuSeeds);   // 7.5
            //SkillNameSeeds.AddRange(GushiSeeds);   // 7
            //SkillNameSeeds.AddRange(SongciSeeds);  // 6.5
            //SkillNameSeeds.AddRange(ShijingSeeds); // 6
        }
        public List<SeedEntity> InitializeSeed(string dist)
        {
            List<SeedEntity> seeds = new();
            var book = FileUtils.ReadFile(Path.Join(PathConst.DIST_PATH, dist));
            var nameDicts = JsonSerializer.Deserialize<List<NameDistEntity>>(book);
            if (nameDicts != null)
            {
                foreach (var dict in nameDicts)
                {
                    if (dict.content == null)
                    {
                        continue;
                    }
                    var chars = dict.content.ToCharArray();
                    foreach (var word in chars)
                    {
                        if (!" .　()（）[]【】\"”“；;,，、。：:?？?*&^%$#@!！~+-——<>《》0123456789".Contains(word))
                        {
                            if (seeds.Any(x => x.Text == word.ToString()))
                            {
                                var seed = seeds.First(x => x.Text == word.ToString());
                                seed.Weight++;
                            }
                            else
                            {
                                seeds.Add(new SeedEntity()
                                {
                                    Text = word.ToString(),
                                    Weight = 1
                                });
                            }
                        }
                    }
                }
            }

            return seeds;
        }

        public List<SeedEntity> CombineSeeds(List<SeedEntity> seeds, List<SeedEntity> seeds2)
        {
            foreach (var seed in seeds2)
            {
                if (seeds.Any(x => x.Text == seed.Text))
                {
                    seeds.First(x => x.Text == seed.Text).Weight += seed.Weight;
                }
                else
                {
                    seeds.Add(seed);
                }
            }
            return seeds;

        }

        #region 姓名种子池

        public string GetRandomFullName(string surname = "")
        {
            var fullName = "";
            if (surname != "")
            {
                fullName += surname;
            }
            else
            {
                fullName += GetFirstName();
            }
            fullName += GetLastName();
            return fullName;
        }
        public string GetFirstName()
        {
            var weights = SurnameSeeds.Select(x => x.Weight).ToList();
            return SurnameSeeds[RandomUtils.GetNextWithWeight(weights)].Text;
        }

        public string GetLastName()
        {
            var result = "";
            var charCount = RandomUtils.Next(1, 3);
            var weights = NameSeeds.Select(x => x.Weight).ToList();
            var words = RandomUtils.GetNextListWithWeight(weights, charCount);
            foreach (var index in words)
            {
                result += NameSeeds[index].Text;
            }
            return result;
        }

        public string GetSkillName(int count = 2)
        {
            var result = "";
            var weights = SkillNameSeeds.Select(x => x.Weight).ToList();
            var words = RandomUtils.GetNextListWithWeight(weights, count);
            foreach (var index in words)
            {
                result += SkillNameSeeds[index].Text;
            }
            return result;
        }
        #endregion


    }
}
