﻿using EnsoulSharp;
using EnsoulSharp.SDK.MenuUI;
using EnsoulSharp.SDK.MenuUI.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPredictionMash;
using SharpDX;
using EnsoulSharp.SDK.Prediction;
using EnsoulSharp.SDK.Utility;
using EnsoulSharp.SDK;

using static SPredictionMash.MinionManager;
using MinionTypes = SPredictionMash.MinionManager.MinionTypes;
using Color = System.Drawing.Color;
using System.Text.RegularExpressions;
using SpellDatabase = DaoHungAIO.Evade.SpellDatabase;
using SafePathResult = DaoHungAIO.Evade.SafePathResult;
using SkillshotDetector = DaoHungAIO.Evade.SkillshotDetector;
using Skillshot = DaoHungAIO.Evade.Skillshot;
using FoundIntersection = DaoHungAIO.Evade.FoundIntersection;
using EvadeManager = DaoHungAIO.Evade.EvadeManager;
using DetectionType = DaoHungAIO.Evade.DetectionType;
using CollisionObjectTypes = DaoHungAIO.Evade.CollisionObjectTypes;
using MinionTeam = SPredictionMash.MinionManager.MinionTeam;
using MinionOrderTypes = SPredictionMash.MinionManager.MinionOrderTypes;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Drawing;

using FunnySlayerCommon;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            GameEvent.OnGameLoad += GameEvent_OnGameLoad;
        }

        private static void GameEvent_OnGameLoad()
        {
            FSpred.Prediction.Prediction.Initialize();

            if (ObjectManager.Player.CharacterName == "Yasuo")
                ("This is Beta Yasuo 's Script, almost bug have been fixed so good luck").YasuoLoad();

            if (ObjectManager.Player.CharacterName == "Yone")
                ("Yone Script make by FunnySlayer, Good luck").YoneLoad();

            try
            {
                Game.Print("SkinHack v1.0.1");
                Game.Print("Thanks for help 011110001");
                Game.Print("Creator: emredeger");

                var menu = new Menu("skinhack", "SkinHack", true);

                var champs = menu.Add(new Menu("Champions", "Champions"));
                var allies = champs.Add(new Menu("Allies", "Allies"));
                var enemies = champs.Add(new Menu("Enemies", "Enemies"));

                foreach (var hero in GameObjects.Heroes.Where(h => !h.CharacterName.Equals("Ezreal")))
                {
                    var champMenu = new Menu(hero.CharacterName, hero.CharacterName);
                    champMenu.Add(new MenuSlider("SkinIndex", "Skin Index", 1, 1, 50));
                    champMenu.GetValue<MenuSlider>("SkinIndex").ValueChanged += (s, e) =>
                    {
                        Console.WriteLine($"[SKINHACK] Skin ID: {champMenu.GetValue<MenuSlider>("SkinIndex").Value}");
                        GameObjects.Heroes.ForEach(
                            p =>
                            {
                                if (p.CharacterName == hero.CharacterName)
                                {
                                    Console.WriteLine($"[SKINHACK] Changed: {hero.CharacterName}");
                                    p.SetSkin(champMenu.GetValue<MenuSlider>("SkinIndex").Value);
                                }
                            });
                    };

                    var rootMenu = hero.IsAlly ? allies : enemies;
                    rootMenu.Add(champMenu);
                }

                menu.Attach();
            }
            catch
            {

            }
        }
    }
    internal static class Yasuo_GodLike
    {
        public static void YasuoLoad(this string a)
        {
            Game.Print(a);
            Console.WriteLine(a);
            Yasuo.YasuoLoad();
        }
    }
    internal static class Yone_GodLike
    {
        public static void YoneLoad(this string a)
        {
            Game.Print(a);
            Console.WriteLine(a);
            Yone.YoneLoaded();
        }
    }

    #region Yasuo God Like    

    internal class YasuoMenu
    {
        public static MenuKeyBind DrawObjPlayerPos = new MenuKeyBind("DrawObjPlayerPos", "DrawObjPlayerPos", System.Windows.Forms.Keys.Z, KeyBindType.Press);
        public static MenuBool ChatWibu = new MenuBool("ChatWibu", "Chat All", false);
        public class RangeCheck
        {
            public static MenuSlider Qrange = new MenuSlider("Qrange", "Q Range", 475, 0, 475);
            public static MenuSlider Q3range = new MenuSlider("Q3range", "Wind Range", 1000, 0, 1100);
            public static MenuSlider EQrange = new MenuSlider("EQrange", "EQ Range", 175, 100, 200);
            public static MenuSlider Erange = new MenuSlider("Erange", "E Range", 475, 0, 475);
            public static MenuSlider Egaprange = new MenuSlider("Egaprange", "E Gapcloser Range", 2000, 0, 3000);
            public static MenuSlider Rrange = new MenuSlider("Rrange", "R Range", 1400, 0, 1400);
        }
        public class Yasuo_target
        {
            public static MenuBool Yasuo_Target_lock = new MenuBool("Yasuo_Target_lock", "Logic Select Target");
        }

        public class Qcombo
        {
            public static MenuBool Yasuo_Qcombo = new MenuBool(",Yasuo_Qcombo", "Yasuo Q in Combo");
            public static MenuBool Yasuo_Windcombo = new MenuBool(",Yasuo_Windcombo", "Yasuo Wind in Combo");
            public static MenuBool Yasuo_Qaa = new MenuBool(",Yasuo_Qaa", "----> Q After AA", false);
            public static MenuBool Yasuo_Qba = new MenuBool(",Yasuo_Qba", "----> Q Before AA", false);
            public static MenuBool Yasuo_Qalways = new MenuBool(",Yasuo_Qalwaays", "----> Q always in combo");
            public static MenuSlider Yasuo_Qoa = new MenuSlider(",Yasuo_Qoa", "----> Q Cancel aa", 30, 0, 100);
        }

        public class EQCombo
        {
            public static MenuBool Yasuo_EQcombo = new MenuBool(",Yasuo_EQcombo", "Yasuo EQ in Combo");
            public static MenuBool Yasuo_EWindcombo = new MenuBool(",Yasuo_EWindcombo", "Yasuo EQ Wind in Combo");
        }

        public class Wcombo
        {
            public static MenuSeparator Yasuo_Wcombo = new MenuSeparator(",Yasuo_Wcombo", "Yasuo W in Combo");
        }

        public class Ecombo
        {
            public static MenuBool Yasuo_ERange = new MenuBool(",Yasuo_ERange", "Yasuo E in Combo");
            public static MenuBool Yasuo_Eziczac = new MenuBool(",Yasuo_Eziczac", "----> E zic zac");
            public static MenuSlider Yasuo_zizzacRange = new MenuSlider(",Yasuo_zizzacRange", "Target is Valid", 850, 0, 2000);
            public static MenuBool Yasuo_Eziczac_Qready = new MenuBool(",Yasuo_Eziczac_Qready", "----> E zic zac only when Q not ready", true);
            public static MenuList Yasuo_EziczacMode = new MenuList(",Yasuo_EziczacMode", "Yasuo E ZZ", new string[] { "Gap obj is validable", "Target Count > 1", "Solo Mode", "Flower Dashing" }, 0);
            public static MenuList Yasuo_EMode = new MenuList(",Yasuo_EMode", "Yasuo E Mode", new string[] { "Target Pos", "Cursor Pos", "Logic Target Gapcloser" }, 2);
            public static MenuBool ddtest = new MenuBool("ddtest", "Disable move When dash (Not recommand)", false);
        }

        public class Rcombo
        {
            public static MenuBool Yasuo_Rcombo = new MenuBool(",Yasuo_Rcombo", "Yasuo R in Combo");
            public static MenuBool Yasuo_Rcombo_EQR = new MenuBool(",Yasuo_Rcombo_EQR", "Logic EQ R");
            public static MenuBool AlwaysEQR = new MenuBool("...AlwaysEQR", "R Tornado always");
            public static MenuSlider RtargetHeath = new MenuSlider("RtargetHeath", "---> Target heath ", 70, 0, 101);
            public static MenuSlider RDelayTime = new MenuSlider("R Delay Time (ms)", "R Delay Cast (ms)", 10);
        }

        public class Yasuo_Clear
        {
            public static MenuBool Yasuo_Qclear = new MenuBool(",Yasuo_Qclear", "Q in Clear");
            public static MenuBool Yasuo_Qclear_aa = new MenuBool(",Yasuo_Qclear_aa", "----> Q clear after aa", false);
            public static MenuBool Yasuo_Qclear_ba = new MenuBool(",Yasuo_Qclear_ba", "----> Q clear before aa", false);
            public static MenuBool Yasuo_Qclear_always = new MenuBool(",Yasuo_Qclear_always", "----> Q clear always");
            public static MenuBool Yasuo_Windclear = new MenuBool(",Yasuo_Windclear", "Wind in Clear");
            public static MenuSlider Yasuo_Windclear_ifhit = new MenuSlider(",Yasuo_Windclear_ifhit", "----> minions count ", 1, 1, 5);
            public static MenuBool Yasuo_Eclear = new MenuBool(",Yasuo_Eclear", "E in clear");
            public static MenuBool Yasuo_Eclear_ks = new MenuBool(",Yasuo_Eclear_ks", "----> Ks minions", false);
        }

        public class Yasuo_Keys
        {
            public static MenuKeyBind Yasuo_Flee = new MenuKeyBind(",Yasuo_Flee", "Flee Key", System.Windows.Forms.Keys.E, KeyBindType.Press);
            public static MenuKeyBind Yasuo_AutoQharass = new MenuKeyBind(",Yasuo_AutoQ", "Auto Q harass Key", System.Windows.Forms.Keys.A, KeyBindType.Toggle);
            public static MenuKeyBind Yasuo_AutoStacks = new MenuKeyBind(",Yasuo_AutoStacks", "Auto Stacks Q", System.Windows.Forms.Keys.A, KeyBindType.Press);
            public static MenuKeyBind AutoQifDashOnTarget = new MenuKeyBind(",AutoQifDashOnTarget", "Auto use Q if dash [Flee , Dashing on target, Exploit]", System.Windows.Forms.Keys.N, KeyBindType.Toggle) { Active = true};
            public static MenuKeyBind TurretKey = new MenuKeyBind(",TurretKey", "Accept E turret", System.Windows.Forms.Keys.T, KeyBindType.Toggle);
            public static MenuKeyBind EQFlashKey = new MenuKeyBind(",EQFlashKey", "EQ flash key", System.Windows.Forms.Keys.G, KeyBindType.Press);

            public static MenuKeyBind WallJumpYS = new MenuKeyBind(",WallJumpYS", "Wall Jump", System.Windows.Forms.Keys.Z, KeyBindType.Press);
            public static MenuKeyBind ComboEQ3Flash = new MenuKeyBind(",EQ3 Flash when Combo", "EQ3 Flash when Combo [Toggle]", System.Windows.Forms.Keys.G, KeyBindType.Toggle);

        }

        public static MenuBool UseExploit = new MenuBool("Use Exploit Q", "Bug Q When Dash");
    }   

    internal class Yasuo
    {
        #region Logic
        public static Vector3 PosExploit(AIBaseClient target = null)
        {           
            if (!YasuoMenu.UseExploit.Enabled) return (objPlayer.Position);
            else
            {
                if (target == null) return (new Vector3(50000, 50000, 50000));
                return target.Position.Extend(ObjectManager.Player.Position, -500000);
            }        
        }

        public static Vector3 PosAfterE(AIBaseClient target)
        {
            if (target.DistanceToPlayer() > 410)
            {
                return target.Position.Extend(ObjectManager.Player.Position, -50);
            }
            return ObjectManager.Player.Position.Extend(
                target.Position, 475);
        }

        public static bool UnderTower(Vector3 pos)
        {
            return
                ObjectManager.Get<AITurretClient>()
                    .Any(i => i.IsEnemy && !i.IsDead && (i.Distance(pos) < 850 + ObjectManager.Player.BoundingRadius)) || GameObjects.EnemySpawnPoints.Any(i => i.Position.Distance(pos) < 850 + ObjectManager.Player.BoundingRadius);
        }
        public static bool CanE(AIBaseClient t)
        {
            return !t.HasBuff("YasuoE");      
        }
        public static bool HaveQ2
        {
            get
            {
                return ObjectManager.Player.HasBuff("YasuoQ1");
            }
        }
        public static bool HaveQ3
        {
            get
            {
                return Q.Name == "YasuoQ3Wrapper";
            }
        }

        public static AIBaseClient GetNearObj(AIBaseClient target = null)
        {
            var pos = Epred(target);

            switch (YasuoMenu.Ecombo.Yasuo_EMode.SelectedValue)
            {
                case "Target Pos":
                    pos = target.Position;
                    break;
                case "Cursor Pos":
                    pos = Game.CursorPos;
                    break;
                case "Logic Target Gapcloser":
                    pos = Epred(target);
                    break;
            }


            var obj = new List<AIBaseClient>();
            obj.AddRange(ObjectManager.Get<AIMinionClient>().Where(i => i.IsValidTarget(E.Range) && !i.IsAlly && CanE(i)));
            obj.AddRange(ObjectManager.Get<AIHeroClient>().Where(i => i.IsValidTarget(E.Range) && !i.IsAlly && CanE(i)));
            obj.AddRange(ObjectManager.Get<AIBaseClient>().Where(i => i.IsValidTarget(E.Range) && !i.IsAlly && CanE(i)));
            if (CanE(target) && E.IsReady() && (!Q.IsReady() || HaveQ2))
            {
                if (YasuoMenu.Ecombo.Yasuo_Eziczac.Enabled)
                {
                    return
                obj.Where(
                    i =>
                    CanE(i)
                    && (pos.Distance(PosAfterE(i)) <= pos.DistanceToPlayer()
                        || (Q.IsReady() ? pos.Distance(PosAfterE(i)) <= YasuoMenu.RangeCheck.EQrange + 15
                        : pos.Distance(PosAfterE(i)) <= pos.DistanceToPlayer())
                        || pos.Distance(PosAfterE(i)) <= 410
                        )
                    )
                    .OrderByDescending(i => pos.Distance(PosAfterE(i))).FirstOrDefault();
                }
                else
                {
                    return
                obj.Where(
                    i =>
                    CanE(i)
                    && (pos.Distance(PosAfterE(i)) <= pos.DistanceToPlayer()
                        || (Q.IsReady() ? pos.Distance(PosAfterE(i)) <= YasuoMenu.RangeCheck.EQrange + 15
                        : pos.Distance(PosAfterE(i)) <= pos.DistanceToPlayer())
                        || pos.Distance(PosAfterE(i)) <= 410
                        )
                    )
                    .OrderBy(i => pos.Distance(PosAfterE(i))).FirstOrDefault();
                }               
            }
            else
            {
                if (YasuoMenu.Ecombo.Yasuo_Eziczac.Enabled)
                {
                    return
                    obj.Where(
                        i =>
                        CanE(i)
                        && (pos.Distance(PosAfterE(i)) <= pos.DistanceToPlayer()
                        || (Q.IsReady() ? pos.Distance(PosAfterE(i)) <= YasuoMenu.RangeCheck.EQrange + 15
                        : pos.Distance(PosAfterE(i)) <= pos.DistanceToPlayer()))
                        )
                        .OrderByDescending(i => pos.Distance(PosAfterE(i))).FirstOrDefault();
                }
                else
                {
                    return
                    obj.Where(
                        i =>
                        CanE(i)
                        && (pos.Distance(PosAfterE(i)) <= pos.DistanceToPlayer()
                        || (Q.IsReady() ? pos.Distance(PosAfterE(i)) <= 230
                        : pos.Distance(PosAfterE(i)) <= pos.DistanceToPlayer()))
                        )
                        .OrderBy(i => pos.Distance(PosAfterE(i))).FirstOrDefault();
                }
            }
        }
        #endregion

        #region List string

        public static List<string> Lolicon = new List<string>()
        {
            "Hestia",
            "Sagari Izumi",
            "Shiro",
            "Aihara Enju",
            "Kanna Kamui",
            "Rory Mercury",
            "Kinue Hayase",
            "Elucia de Lute Ima",
            "Kobato Hasegawa",
            "Ikazuchi & Inazuma Twins",
            "Raiha Uesugi",
            "Mayuka Volker",
            "Akatsuki (Kantai Collection)",
            "Beatrice",
            "Haru Onodera",
            "Chino Kafuu",
            "Illyasviel von Einzbern",
            "Mei Sunohara",
            "Chiyo Mihama",
            "Miu Takanashi",
            "Aria Holmes Kanzaki",
            "Popura Taneshima",
            "Sharo Kirima",
            "Nagi Sanzenin",
            "Yoshino",
            "Shinobu Oshino",
            "Wendy Marvell",
            "Hecate",
            "Tsukiko Tsutsukakushi",
            "Rika Furude",
            "Azusa “Azu-nyan” Nakano",
            "megumin"
        };
        public static List<string> Waifu = new List<string>()
        {
            "Kei Karuizawa",
            "Honami Ichinose",
            "Suzune Horikita",
            "Arisu Sakayanagi",
            "Kikyō Kushida",
            "Sae Chabashira",
            "Airi Sakura",
            "Akane Tachibana",
            "Masumi Kamuro",
            "Maya Sato",
            "Mei-Yu Wang",
            "Mio Ibuki",
            "Chiaki Matsushita",
            "Chie Hoshinomiya",
            "Chihiro Shiranami",
            "Emilia",
            "Saber (Arthur PenDragon)",
            "Sun Seto",
            "Megumi Tadokoro",
            "Umiko Ahagon",
            "Mirajane Strauss",
            "Jeanne D Arc (Fate)",
            "Nao Tomori",
            "Lucy Heartfilia",
            "Uryuu Minene",
            "Ginshuu",
            "Rem",
            "Rukia Kuchiki",
            "Eru Chitanda",
            "Sakura (Naruto)",
            "Ochako Uraraka",
            "Alice Nakiri",
            "Teletha Testarossa",
            "Ursula Callistis",
            "Sheele",
            "Holo The Wise Wolf",
            "Mikoto Misaka",
            "Karen Tendou",
            "Minori Kushieda",
            "Asuna Yuuki",
            "Erza Scarlet",
            "Raphtalia",
            "Kasumigaoka Utaha",
            "Kyoka Jiro",
            "Maki Oze",
            "Kyoko Mogami",
            "Belldandy",
        };
        #endregion

        #region List Vector
        private static List<Vector2> WallJumpPos = new List<Vector2>()
        {
            new Vector2(1678, 8428),
            new Vector2(2232, 8412),
            new Vector2(3674, 7058),
            new Vector2(3892, 6466),
            new Vector2(4324, 6258),
            new Vector2(6424, 5208),
            new Vector2(6574, 12256),
            new Vector2(6650, 11766),
            new Vector2(7046, 5426),
            new Vector2(7274, 5908),
            new Vector2(7784, 9494),
            new Vector2(7672, 8906),


            new Vector2(7976, 9486),
            new Vector2(6862, 5460),


            new Vector2(8222, 3158),
            new Vector2(8322, 2658),
            new Vector2(8372, 9606),
            new Vector2(10372, 8456),
            new Vector2(10882, 8416),
            new Vector2(11072, 8306),
            new Vector2(11222, 7856),
            new Vector2(12582, 6402),
            new Vector2(13172, 6508),                    
        };

        #endregion

        private static AIHeroClient objPlayer = ObjectManager.Player;
        private static Menu YasuoTheMenu = new Menu("Yasuo God Like", "Yasuo God Like", true);
        public static Menu EvadeSkillshotMenu = new Menu("EvadeSkillshot", "Evade Skillshot");
        private static Menu PredictionHelper = new Menu("Prediction Helper", "Helper");
        public static bool isYasuoDashing = false;
        public static bool baa = false;
        public static bool oaa = false;
        public static bool aaa = false;
        public static Spell Q, Q3, W, E, E1, R;
        public static Spell EQFlash;
        public static SpellSlot Flash;

        #region Yasuo Menu
        public static void YasuoLoad()
        {
            SPredictionMash.ConfigMenu.Initialize(PredictionHelper, "@@");
            new SebbyLibPorted.Orbwalking.Orbwalker(PredictionHelper);
            var one_or_two = new Random().Next(3);
            var loli_or_waifu = "";
            switch (one_or_two)
            {
                case 0:
                    loli_or_waifu = "Loli";
                    break;
                case 1:
                    loli_or_waifu = "Waifu";
                    break;
                case 2:
                    loli_or_waifu = "fs";
                    break;
            }
            var Name = "";
            if (loli_or_waifu == "Loli")
            {
                Name = Lolicon[(new Random().Next(Lolicon.Count))];
            }
            if (loli_or_waifu == "Waifu")
            {
                Name = Waifu[(new Random().Next(Waifu.Count))];
            }
            if(loli_or_waifu == "fs")
            {
                Name = "FunnySlayer";
            }
            var animemenu = new Menu(loli_or_waifu, loli_or_waifu + " for this load :");
            animemenu.Add(new MenuSeparator(Name, 
                "__________________ " + Name + " __________________"))
                .Permashow(true, Name, SharpDX.Color.Azure);
            
            YasuoTheMenu.Add(YasuoMenu.ChatWibu);

            if (YasuoMenu.ChatWibu.Enabled)
            {
                if (Name != "FunnySlayer")
                    Game.Say(Name + " is playing this game", true);

                else Game.Say("-   FunnySlayer   - God Like is Playing", true);               
            }
           
            YasuoTheMenu.Add(animemenu);
            YasuoTheMenu.Add(PredictionHelper);
            YasuoTheMenu.Add(YasuoMenu.Yasuo_target.Yasuo_Target_lock);

            var SkillRange = new Menu("SkillRange", "Yasuo Skill Range");
            var Qcombo = new Menu("YasuoQincombo", "Yasuo_Q Combo");
            var Ecombo = new Menu("YasuoEincombo", "Yasuo_E Combo");
            var EQcombo = new Menu("YasuoEQincombo", "Yasuo_EQ Combo");
            var Rcombo = new Menu("YasuoRincombo", "Yasuo_R Combo");
            var Wcombo = new Menu("YasuoWincombo", "Yasuo_W Combo");
            var ysClear = new Menu("ysClear", "Clear Settings");
            var yskeys = new Menu("YasuoKeys", "All Key Settings");

            SkillRange.Add(YasuoMenu.RangeCheck.Qrange);
            SkillRange.Add(YasuoMenu.RangeCheck.Q3range);
            SkillRange.Add(YasuoMenu.RangeCheck.EQrange);
            SkillRange.Add(YasuoMenu.RangeCheck.Erange);
            SkillRange.Add(YasuoMenu.RangeCheck.Egaprange);
            SkillRange.Add(YasuoMenu.RangeCheck.Rrange);

            yskeys.Add(YasuoMenu.Yasuo_Keys.Yasuo_Flee).Permashow();
            yskeys.Add(YasuoMenu.Yasuo_Keys.WallJumpYS).Permashow();
            yskeys.Add(YasuoMenu.Yasuo_Keys.ComboEQ3Flash).Permashow();
            yskeys.Add(YasuoMenu.Yasuo_Keys.EQFlashKey).Permashow();
            yskeys.Add(YasuoMenu.Yasuo_Keys.TurretKey).Permashow();
            yskeys.Add(YasuoMenu.Yasuo_Keys.Yasuo_AutoQharass).Permashow();
            yskeys.Add(YasuoMenu.Yasuo_Keys.Yasuo_AutoStacks).Permashow();
            yskeys.Add(YasuoMenu.Yasuo_Keys.AutoQifDashOnTarget).Permashow(true, "Auto Q in Dash", SharpDX.Color.Azure);


            ysClear.Add(YasuoMenu.Yasuo_Clear.Yasuo_Qclear);
            ysClear.Add(YasuoMenu.Yasuo_Clear.Yasuo_Qclear_always);
            ysClear.Add(YasuoMenu.Yasuo_Clear.Yasuo_Qclear_aa);
            ysClear.Add(YasuoMenu.Yasuo_Clear.Yasuo_Qclear_ba);
            ysClear.Add(YasuoMenu.Yasuo_Clear.Yasuo_Windclear);
            ysClear.Add(YasuoMenu.Yasuo_Clear.Yasuo_Windclear_ifhit);
            ysClear.Add(YasuoMenu.Yasuo_Clear.Yasuo_Eclear);
            ysClear.Add(YasuoMenu.Yasuo_Clear.Yasuo_Eclear_ks);

            Qcombo.Add(YasuoMenu.Qcombo.Yasuo_Qcombo);
            Qcombo.Add(YasuoMenu.Qcombo.Yasuo_Windcombo);
            Qcombo.Add(YasuoMenu.Qcombo.Yasuo_Qalways);
            Qcombo.Add(YasuoMenu.Qcombo.Yasuo_Qaa);
            Qcombo.Add(YasuoMenu.Qcombo.Yasuo_Qba);
            Qcombo.Add(YasuoMenu.Qcombo.Yasuo_Qoa);

            Ecombo.Add(YasuoMenu.Ecombo.Yasuo_ERange);
            Ecombo.Add(YasuoMenu.Ecombo.Yasuo_EMode).Permashow();
            Ecombo.Add(YasuoMenu.Ecombo.Yasuo_Eziczac);
            Ecombo.Add(YasuoMenu.Ecombo.Yasuo_zizzacRange);
            Ecombo.Add(YasuoMenu.Ecombo.Yasuo_Eziczac_Qready);
            Ecombo.Add(YasuoMenu.Ecombo.Yasuo_EziczacMode).Permashow(true, YasuoMenu.Ecombo.Yasuo_Eziczac.Enabled ? YasuoMenu.Ecombo.Yasuo_EziczacMode.SelectedValue : "Disabled", SharpDX.Color.Yellow);
            Ecombo.Add(YasuoMenu.Ecombo.ddtest);

            EQcombo.Add(YasuoMenu.EQCombo.Yasuo_EQcombo);
            EQcombo.Add(YasuoMenu.EQCombo.Yasuo_EWindcombo);

            Rcombo.Add(YasuoMenu.Rcombo.Yasuo_Rcombo);
            Rcombo.Add(YasuoMenu.Rcombo.RDelayTime);
            Rcombo.Add(YasuoMenu.Rcombo.Yasuo_Rcombo_EQR);
            Rcombo.Add(YasuoMenu.Rcombo.AlwaysEQR);
            Rcombo.Add(YasuoMenu.Rcombo.RtargetHeath);

            Wcombo.Add(YasuoMenu.Wcombo.Yasuo_Wcombo);
            EvadeSkillshot.Init(Wcombo);
            EvadeTarget.Init(Wcombo);

            var fstarget = new Menu("fs target", "FS Target Selector");
            fstarget.AddTargetSelectorMenu();

            YasuoTheMenu.Add(fstarget);
            YasuoTheMenu.Add(Qcombo);
            YasuoTheMenu.Add(Ecombo);
            YasuoTheMenu.Add(EQcombo);
            YasuoTheMenu.Add(Rcombo);
            YasuoTheMenu.Add(Wcombo);
            YasuoTheMenu.Add(SkillRange);
            YasuoTheMenu.Add(ysClear);
            YasuoTheMenu.Add(yskeys);

            YasuoTheMenu.Add(YasuoMenu.UseExploit).Permashow();
            //YasuoTheMenu.Add(YasuoMenu.Yasuo_target.Yasuo_Target_lock);
            //YasuoTheMenu.AddTargetSelectorMenu();
            YasuoTheMenu.Add(YasuoMenu.DrawObjPlayerPos);

            YasuoTheMenu.Attach();
            
            Q = new Spell(SpellSlot.Q, 475);
            Q3 = new Spell(SpellSlot.Q, 1100);
            W = new Spell(SpellSlot.W, 100);
            E = new Spell(SpellSlot.E, 475);
            E1 = new Spell(SpellSlot.E, 475);
            R = new Spell(SpellSlot.R, 1400);

            Q.SetSkillshot(0.4f, 55, float.MaxValue, false, false, SkillshotType.Line);
            Q3.SetSkillshot(0.4f, 80, 1200, false, false, SkillshotType.Line);
            E1.SetTargetted(0f, 1000f);
            E.SetSkillshot(0.3f, 175, 1000f, false, SkillshotType.Line);

            Flash = ObjectManager.Player.GetSpellSlot("summonerflash");
            EQFlash = new Spell(Flash, 850f);
            EQFlash.SetSkillshot(0, 175, float.MaxValue, false, false, SkillshotType.Circle);

            Game.OnUpdate += Game_OnUpdate1;
            Orbwalker.OnAction += Orbwalker_OnAction;
            Game.OnUpdate += Game_OnUpdate;
            AIHeroClient.OnPlayAnimation += AIHeroClient_OnPlayAnimation;
            Drawing.OnDraw += Drawing_OnDraw;
            //AIHeroClient.OnDoCast += EEvade;          
            YasuoMenu.ChatWibu.ValueChanged += (sender, e) => {

                if (YasuoMenu.ChatWibu.Enabled)
                {

                    if (Name != "FunnySlayer")
                        Game.Say(Name + " is playing this game", true);

                    else Game.Say("-   FunnySlayer   - God Like is Playing", true);
                    YasuoTheMenu.Add(YasuoMenu.Yasuo_target.Yasuo_Target_lock);
                }
            };

            YasuoMenu.DrawObjPlayerPos.ValueChanged += DrawObjPlayerPos_ValueChanged;
        }

        private static void Game_OnUpdate1(EventArgs args)
        {
            if (objPlayer.IsDead) return;

            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkerMode.Combo:
                    Yasuo_DoCombo();
                    break;
                case OrbwalkerMode.LaneClear:
                    Yasuo_DoClear();
                    break;
                case OrbwalkerMode.Harass:
                    Yasuo_DoHarass();
                    break;
            }
        }

        private static void DrawObjPlayerPos_ValueChanged(object sender, EventArgs e)
        {
            Game.Print(objPlayer.Position);
        }
        #endregion

        #region Events
        private static void Drawing_OnDraw(EventArgs args)
        {
            if (YasuoMenu.Yasuo_Keys.WallJumpYS.Active)
            {
                foreach (var pos in WallJumpPos
                //.Where(x => x.Distance(objPlayer.Position.ToVector2()) <= 1200)
                )
                {
                    Render.Circle.DrawCircle(pos.ToVector3(), 70, Color.FromArgb(251, 209, 0), 5);
                }

                Drawing.DrawCircle(WallJumpPos.MinOrDefault(i => i.DistanceToPlayer()).ToVector3(), 70, System.Drawing.Color.White);
            }

            if (objPlayer.IsDead) return;

            var target = FSTargetSelector.GetFSTarget(10000);
            if (Q.IsReady() && target != null)
            {
                var Q3prediction = FSpred.Prediction.Prediction.GetPrediction(Q3, target);
                var Qprediction = FSpred.Prediction.Prediction.GetPrediction(Q, target);
                if (HaveQ3)
                {
                    Render.Circle.DrawCircle(Q3prediction.CastPosition, 10, System.Drawing.Color.Red, 10);
                }
                else
                {
                    Render.Circle.DrawCircle(Qprediction.CastPosition, 10, System.Drawing.Color.Red, 10);
                }
            }
        }

        private static void EEvade(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
        {
            if (!sender.IsAlly && args.Slot != SpellSlot.Unknown && (sender is AIHeroClient))




            {






            }
        }
        private static void AIHeroClient_OnPlayAnimation(AIBaseClient sender, AIBaseClientPlayAnimationEventArgs args)
        {
            if (sender.IsMe && args.Animation == "Spell3")
            {
                isYasuoDashing = true;               
            }

            if (isYasuoDashing)
            {
                if (YasuoMenu.Yasuo_Keys.AutoQifDashOnTarget.Active && Q.IsReady(0))
                {
                    foreach (var target in GameObjects.EnemyHeroes.Where(i => !i.IsDead && !i.IsAlly && i.IsVisible && i.IsValidTarget(600)))
                    {
                        if (target != null && target.Distance(objPlayer.GetDashInfo().EndPos) <= YasuoMenu.RangeCheck.EQrange.Value && objPlayer.GetDashInfo().EndPos.DistanceToPlayer() < 200)
                        {
                            Q.Cast(PosExploit(target));
                        }
                    }
                    if (YasuoMenu.Yasuo_Keys.Yasuo_Flee.Active && Q.Name != "YasuoQ3Wrapper")
                    {
                        Q.Cast(PosExploit());
                    }
                    if (YasuoMenu.Yasuo_Keys.EQFlashKey.Active && Q.Name != "YasuoQ3Wrapper")
                    {
                        Q.Cast(PosExploit());
                    }
                    if (ObjectManager.Player.HasBuff("YasuoQ1"))
                    {
                        Q.Cast(PosExploit());
                    }
                }
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (Hacks.DisableAntiDisconnect == false) Hacks.DisableAntiDisconnect = true;
            if (objPlayer.IsDead) return;
            if (objPlayer.IsDashing()) isYasuoDashing = true;
            if (!objPlayer.IsDashing()) isYasuoDashing = false;

            E.Speed = 750f + 0.6f * ObjectManager.Player.MoveSpeed;           

            if (YasuoMenu.Ecombo.ddtest.Enabled)
            {
                if (isYasuoDashing)
                {
                    Orbwalker.MovementState = false;
                    DelayAction.Add((int)(objPlayer.GetDashInfo().EndPos.DistanceToPlayer() / E.Speed * 1000 - 75), () =>
                    {
                        Orbwalker.MovementState = true;
                    });
                }
                else
                {
                    Orbwalker.MovementState = true;
                }
            }
            else
            {
                Orbwalker.MovementState = true;
            }
            if (YasuoMenu.Yasuo_Keys.Yasuo_Flee.Active || YasuoMenu.Yasuo_Keys.EQFlashKey.Active || YasuoMenu.Yasuo_Keys.WallJumpYS.Active) Orbwalker.AttackState = false; else Orbwalker.AttackState = true;
            if (YasuoMenu.Yasuo_Keys.Yasuo_Flee.Active && E.IsReady(0) && !oaa)
            {
                if (!isYasuoDashing)
                    objPlayer.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                if (isYasuoDashing)
                    DelayAction.Add((int)(objPlayer.GetDashInfo().EndPos.DistanceToPlayer() / E.Speed * 1000 + 100), () =>
                    {
                        objPlayer.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                    });

                var obj = new List<AIBaseClient>();
                obj.AddRange(ObjectManager.Get<AIBaseClient>().Where(i => i.IsValidTarget(E.Range) && !i.IsAlly && CanE(i)));
                if (E.Level >= 1)
                {
                    foreach (AIBaseClient getobj in obj)
                    {
                        if (getobj != null && CanE(getobj))
                        {
                            if (PosAfterE(getobj).DistanceToCursor() <= objPlayer.DistanceToCursor())
                            {
                                if (E1.Cast(getobj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(getobj))
                                    return;
                            }
                            if (getobj.DistanceToCursor() <= 50)
                            {
                                if (E1.Cast(getobj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(getobj))
                                    return;
                            }
                            if (getobj.DistanceToCursor() <= objPlayer.DistanceToCursor())
                            {
                                if (E1.Cast(getobj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(getobj))
                                    return;
                            }
                        }
                    }
                }
            }

            if (YasuoMenu.Yasuo_Keys.WallJumpYS.Active && E.IsReady())
            {
                objPlayer.IssueOrder(GameObjectOrder.MoveTo, WallJumpPos.MinOrDefault(i => i.DistanceToPlayer()).ToVector3());
            }

            if (YasuoMenu.Yasuo_Keys.EQFlashKey.Active && Flash.IsReady() && E.IsReady())
            {
                if (!isYasuoDashing)
                    objPlayer.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                if(isYasuoDashing)
                    DelayAction.Add((int)(objPlayer.GetDashInfo().EndPos.DistanceToPlayer() / E.Speed * 1000 + 100), () =>
                    {
                        objPlayer.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                    });

                var obj = new List<AIBaseClient>();
                obj.AddRange(ObjectManager.Get<AIBaseClient>().Where(i => i.IsValidTarget(E.Range) && !i.IsAlly && CanE(i)));
                if (E.Level >= 1)
                {
                    foreach (AIBaseClient getobj in obj)
                    {
                        if (getobj != null && CanE(getobj))
                        {
                            if (PosAfterE(getobj).DistanceToCursor() <= objPlayer.DistanceToCursor())
                            {
                                if (E1.Cast(getobj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(getobj))
                                    return;
                            }
                            if (getobj.DistanceToCursor() <= 50)
                            {
                                if (E1.Cast(getobj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(getobj))
                                    return;
                            }
                            if (getobj.DistanceToCursor() <= objPlayer.DistanceToCursor())
                            {
                                if (E1.Cast(getobj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(getobj))
                                    return;
                            }
                        }
                    }
                }

                YasuoEQFlash();
            }            
        }
        private static void Orbwalker_OnAction(object sender, OrbwalkerActionArgs args)
        {
            if (args.Type == OrbwalkerType.AfterAttack)
            {
                aaa = true;
                baa = false;
                oaa = false;
            }
            else aaa = false;

            if (args.Type == OrbwalkerType.BeforeAttack)
            {
                aaa = false;
                baa = true;
                oaa = false;
            }
            else baa = false;

            if (args.Type == OrbwalkerType.OnAttack)
            {
                aaa = false;
                baa = false;
                oaa = true;
            }
            else oaa = false;
        }
        #endregion

        #region Evade
        public class EvadeSkillshot
        {
            #region Public Methods and Operators
            public static void Init(Menu menu)
            {
                {
                    EvadeSkillshotMenu.Add(new MenuBool("Credit", "Credit: Evade#"));
                    var evadeSpells = new Menu("Spells", "Spells");
                    {
                        foreach (var spell in EvadeSpellDatabase.Spells)
                        {
                            try
                            {
                                var sub = new Menu("ESSS_" + spell.Name, spell.Name + " (" + spell.Slot + ")");
                                {
                                    if (spell.Name == "YasuoDashWrapper")
                                    {
                                        sub.Add(new MenuBool("ETower", "Under Tower", false));
                                    }
                                    else if (spell.Name == "YasuoWMovingWall")
                                    {
                                        sub.Add(new MenuSlider("WDelay", "Extra Delay", 100, 0, 150));
                                    }
                                    sub.Add(new MenuSlider("DangerLevel", "If Danger Level >=", spell.DangerLevel, 1, 5));
                                    sub.Add(new MenuBool("Enabled", "Enabled", false));
                                    evadeSpells.Add(sub);
                                }
                            }
                            catch { }
                        }
                        EvadeSkillshotMenu.Add(evadeSpells);
                    }
                    foreach (var hero in
                        HeroManager.Enemies.Where(i => SpellDatabase.Spells.Any(a => a.ChampionName == i.CharacterName)))
                    {
                        try
                        {
                            EvadeSkillshotMenu.Add(new Menu("EvadeSS_" + hero.CharacterName, "-> " + hero.CharacterName));
                        }
                        catch { }
                    }
                    foreach (var spell in
                        SpellDatabase.Spells.Where(i => HeroManager.Enemies.Any(a => a.CharacterName == i.ChampionName)))
                    {
                        try
                        {
                            var sub = new Menu("ESS_" + spell.MenuItemName, spell.SpellName + " (" + spell.Slot + ")");
                            {
                                sub.Add(new MenuSlider("DangerLevel", "Danger Level", spell.DangerValue, 1, 5));
                                sub.Add(new MenuBool("Enabled", "Enabled", !spell.DisabledByDefault));
                                ((Menu)EvadeSkillshotMenu["EvadeSS_" + spell.ChampionName]).Add(sub);
                            }
                        }
                        catch { }
                    }
                }
                menu.Add(EvadeSkillshotMenu);
                DaoHungAIO.Evade.Collision.Init();
                Game.OnUpdate += OnUpdateEvade;
                SkillshotDetector.OnDetectSkillshot += OnDetectSkillshot;
                SkillshotDetector.OnDeleteMissile += OnDeleteMissile;
            }

            public static IsSafeResult IsSafePoint(Vector2 point)
            {
                var result = new IsSafeResult { SkillshotList = new List<Skillshot>() };
                foreach (var skillshot in
                    EvadeManager.DetectedSkillshots.Where(i => i.Evade() && !i.IsSafePoint(point)))
                {
                    result.SkillshotList.Add(skillshot);
                }
                result.IsSafe = result.SkillshotList.Count == 0;
                return result;
            }

            #endregion

            #region Methods

            private static bool IsWard(AIMinionClient obj)
            {
                return obj.Team != GameObjectTeam.Neutral && !MinionManager.IsMinion(obj) && !IsPet(obj)
                       && MinionManager.IsMinion(obj);
            }
            public static bool IsPet(AIMinionClient obj)
            {
                var pets = new[]
                               {
                               "annietibbers", "elisespiderling", "heimertyellow", "heimertblue", "leblanc",
                               "malzaharvoidling", "shacobox", "shaco", "yorickspectralghoul", "yorickdecayedghoul",
                               "yorickravenousghoul", "zyrathornplant", "zyragraspingplant"
                           };
                return pets.Contains(obj.CharacterName.ToLower());
            }
            private static IEnumerable<AIBaseClient> GetEvadeTargets(
                EvadeSpellData spell,
                bool onlyGood = false,
                bool dontCheckForSafety = false)
            {
                var badTargets = new List<AIBaseClient>();
                var goodTargets = new List<AIBaseClient>();
                var allTargets = new List<AIBaseClient>();
                foreach (var targetType in spell.ValidTargets)
                {
                    switch (targetType)
                    {
                        case SpellTargets.AllyChampions:
                            allTargets.AddRange(
                                GameObjects.AllyHeroes.Where(i => i.IsValidTarget(spell.MaxRange, false) && !i.IsMe));
                            break;
                        case SpellTargets.AllyMinions:
                            allTargets.AddRange(GetMinions(spell.MaxRange, MinionManager.MinionTypes.All, MinionTeam.Ally));
                            break;
                        case SpellTargets.AllyWards:
                            allTargets.AddRange(
                                ObjectManager.Get<AIMinionClient>()
                                    .Where(
                                        i =>
                                        IsWard(i) && i.IsValidTarget(spell.MaxRange, false) && i.Team == objPlayer.Team));
                            break;
                        case SpellTargets.EnemyChampions:
                            allTargets.AddRange(HeroManager.Enemies.Where(i => i.IsValidTarget(spell.MaxRange)));
                            break;
                        case SpellTargets.EnemyMinions:
                            allTargets.AddRange(GetMinions(spell.MaxRange, MinionManager.MinionTypes.All, MinionTeam.NotAlly));
                            break;
                        case SpellTargets.EnemyWards:
                            allTargets.AddRange(
                                ObjectManager.Get<AIMinionClient>()
                                    .Where(i => IsWard(i) && i.IsValidTarget(spell.MaxRange)));
                            break;
                    }
                }
                foreach (var target in
                    allTargets.Where(i => dontCheckForSafety || IsSafePoint(i.Position.ToVector2()).IsSafe))
                {
                    if (spell.Name == "YasuoDashWrapper" && target.HasBuff("YasuoDashWrapper"))
                    {
                        continue;
                    }
                    var pathToTarget = new List<Vector2> { objPlayer.Position.ToVector2(), target.Position.ToVector2() };
                    if (IsSafePath(pathToTarget, Configs.EvadingFirstTimeOffset, spell.Speed, spell.Delay).IsSafe)
                    {
                        goodTargets.Add(target);
                    }
                    if (IsSafePath(pathToTarget, Configs.EvadingSecondTimeOffset, spell.Speed, spell.Delay).IsSafe)
                    {
                        badTargets.Add(target);
                    }
                }
                return goodTargets.Any() ? goodTargets : (onlyGood ? new List<AIBaseClient>() : badTargets);
            }

            private static SafePathResult IsSafePath(List<Vector2> path, int timeOffset, int speed = -1, int delay = 0)
            {
                var isSafe = false;
                var intersections = new List<FoundIntersection>();
                var intersection = new FoundIntersection();
                foreach (SafePathResult sResult in
                    EvadeManager.DetectedSkillshots.Where(i => i.Evade())
                        .Select(i => i.IsSafePath(path, timeOffset, speed, delay)))
                {
                    isSafe = sResult.IsSafe;
                    if (sResult.Intersection.Valid)
                    {
                        intersections.Add(sResult.Intersection);
                    }

                }
                return isSafe
                           ? new SafePathResult(true, intersection)
                           : new SafePathResult(
                                 false,
                                 intersections.Count > 0 ? intersections.MinOrDefault(i => i.Distance) : intersection);
            }

            private static void OnDeleteMissile(Skillshot skillshot, MissileClient missile)
            {
                if (skillshot.SpellData.SpellName != "VelkozQ"
                    || EvadeManager.DetectedSkillshots.Count(i => i.SpellData.SpellName == "VelkozQSplit") != 0)
                {
                    return;
                }
                var spellData = SpellDatabase.GetByName("VelkozQSplit");
                for (var i = -1; i <= 1; i = i + 2)
                {
                    EvadeManager.DetectedSkillshots.Add(
                        new Skillshot(
                            DetectionType.ProcessSpell,
                            spellData,
                            Variables.GameTimeTickCount,
                            missile.Position.ToVector2(),
                            missile.Position.ToVector2() + i * skillshot.Perpendicular * spellData.Range,
                            skillshot.Unit));
                }
            }

            private static void OnDetectSkillshot(Skillshot skillshot)
            {
                Game.Print("detected:");
                var alreadyAdded =
                    EvadeManager.DetectedSkillshots.Any(
                        i =>
                        i.SpellData.SpellName == skillshot.SpellData.SpellName
                        && i.Unit.NetworkId == skillshot.Unit.NetworkId
                        && skillshot.Direction.AngleBetween(i.Direction) < 5
                        && (skillshot.Start.Distance(i.Start) < 100 || skillshot.SpellData.FromObjects.Length == 0));
                if (skillshot.Unit.Team == objPlayer.Team)
                {
                    return;
                }
                if (skillshot.Start.Distance(objPlayer.Position.ToVector2())
                    > (skillshot.SpellData.Range + skillshot.SpellData.Radius + 1000) * 1.5)
                {
                    return;
                }
                if (alreadyAdded && !skillshot.SpellData.DontCheckForDuplicates)
                {
                    return;
                }
                if (skillshot.DetectionType == DetectionType.ProcessSpell)
                {
                    if (skillshot.SpellData.MultipleNumber != -1)
                    {
                        var originalDirection = skillshot.Direction;
                        for (var i = -(skillshot.SpellData.MultipleNumber - 1) / 2;
                             i <= (skillshot.SpellData.MultipleNumber - 1) / 2;
                             i++)
                        {
                            EvadeManager.DetectedSkillshots.Add(
                                new Skillshot(
                                    skillshot.DetectionType,
                                    skillshot.SpellData,
                                    skillshot.StartTick,
                                    skillshot.Start,
                                    skillshot.Start
                                    + skillshot.SpellData.Range
                                    * originalDirection.Rotated(skillshot.SpellData.MultipleAngle * i),
                                    skillshot.Unit));
                        }
                        return;
                    }
                    if (skillshot.SpellData.SpellName == "UFSlash")
                    {
                        skillshot.SpellData.MissileSpeed = 1600 + (int)skillshot.Unit.MoveSpeed;
                    }
                    if (skillshot.SpellData.SpellName == "SionR")
                    {
                        skillshot.SpellData.MissileSpeed = (int)skillshot.Unit.MoveSpeed;
                    }
                    if (skillshot.SpellData.Invert)
                    {
                        EvadeManager.DetectedSkillshots.Add(
                            new Skillshot(
                                skillshot.DetectionType,
                                skillshot.SpellData,
                                skillshot.StartTick,
                                skillshot.Start,
                                skillshot.Start
                                + -(skillshot.End - skillshot.Start).Normalized()
                                * skillshot.Start.Distance(skillshot.End),
                                skillshot.Unit));
                        return;
                    }
                    if (skillshot.SpellData.Centered)
                    {
                        EvadeManager.DetectedSkillshots.Add(
                            new Skillshot(
                                skillshot.DetectionType,
                                skillshot.SpellData,
                                skillshot.StartTick,
                                skillshot.Start - skillshot.Direction * skillshot.SpellData.Range,
                                skillshot.Start + skillshot.Direction * skillshot.SpellData.Range,
                                skillshot.Unit));
                        return;
                    }
                    if (skillshot.SpellData.SpellName == "SyndraE" || skillshot.SpellData.SpellName == "syndrae5")
                    {
                        const int Angle = 60;
                        var edge1 =
                            (skillshot.End - skillshot.Unit.Position.ToVector2()).Rotated(
                                -Angle / 2f * (float)Math.PI / 180);
                        var edge2 = edge1.Rotated(Angle * (float)Math.PI / 180);
                        foreach (var skillshotToAdd in from minion in ObjectManager.Get<AIMinionClient>()
                                                       let v =
                                                           (minion.Position - skillshot.Unit.Position).ToVector2(
                                                               )
                                                       where
                                                           minion.Name == "Seed" && edge1.CrossProduct(v) > 0
                                                           && v.CrossProduct(edge2) > 0
                                                           && minion.Distance(skillshot.Unit) < 800
                                                           && minion.Team != objPlayer.Team
                                                       let start = minion.Position.ToVector2()
                                                       let end =
                                                           skillshot.Unit.Position.Extend(
                                                               minion.Position,
                                                               skillshot.Unit.Distance(minion) > 200 ? 1300 : 1000)
                                                           .ToVector2()
                                                       select
                                                           new Skillshot(
                                                           skillshot.DetectionType,
                                                           skillshot.SpellData,
                                                           skillshot.StartTick,
                                                           start,
                                                           end,
                                                           skillshot.Unit))
                        {
                            EvadeManager.DetectedSkillshots.Add(skillshotToAdd);
                        }
                        return;
                    }
                    if (skillshot.SpellData.SpellName == "AlZaharCalloftheVoid")
                    {
                        EvadeManager.DetectedSkillshots.Add(
                            new Skillshot(
                                skillshot.DetectionType,
                                skillshot.SpellData,
                                skillshot.StartTick,
                                skillshot.End - skillshot.Perpendicular * 400,
                                skillshot.End + skillshot.Perpendicular * 400,
                                skillshot.Unit));
                        return;
                    }
                    if (skillshot.SpellData.SpellName == "DianaArc")
                    {
                        EvadeManager.DetectedSkillshots.Add(
                            new Skillshot(
                                skillshot.DetectionType,
                                SpellDatabase.GetByName("DianaArcArc"),
                                skillshot.StartTick,
                                skillshot.Start,
                                skillshot.End,
                                skillshot.Unit));
                    }
                    if (skillshot.SpellData.SpellName == "ZiggsQ")
                    {
                        var d1 = skillshot.Start.Distance(skillshot.End);
                        var d2 = d1 * 0.4f;
                        var d3 = d2 * 0.69f;
                        var bounce1SpellData = SpellDatabase.GetByName("ZiggsQBounce1");
                        var bounce2SpellData = SpellDatabase.GetByName("ZiggsQBounce2");
                        var bounce1Pos = skillshot.End + skillshot.Direction * d2;
                        var bounce2Pos = bounce1Pos + skillshot.Direction * d3;
                        bounce1SpellData.Delay =
                            (int)(skillshot.SpellData.Delay + d1 * 1000f / skillshot.SpellData.MissileSpeed + 500);
                        bounce2SpellData.Delay =
                            (int)(bounce1SpellData.Delay + d2 * 1000f / bounce1SpellData.MissileSpeed + 500);
                        EvadeManager.DetectedSkillshots.Add(
                            new Skillshot(
                                skillshot.DetectionType,
                                bounce1SpellData,
                                skillshot.StartTick,
                                skillshot.End,
                                bounce1Pos,
                                skillshot.Unit));
                        EvadeManager.DetectedSkillshots.Add(
                            new Skillshot(
                                skillshot.DetectionType,
                                bounce2SpellData,
                                skillshot.StartTick,
                                bounce1Pos,
                                bounce2Pos,
                                skillshot.Unit));
                    }
                    if (skillshot.SpellData.SpellName == "ZiggsR")
                    {
                        skillshot.SpellData.Delay =
                            (int)(1500 + 1500 * skillshot.End.Distance(skillshot.Start) / skillshot.SpellData.Range);
                    }
                    if (skillshot.SpellData.SpellName == "JarvanIVDragonStrike")
                    {
                        var endPos = new Vector2();
                        foreach (var s in EvadeManager.DetectedSkillshots)
                        {
                            if (s.Unit.NetworkId == skillshot.Unit.NetworkId && s.SpellData.Slot == SpellSlot.E)
                            {
                                var extendedE = new Skillshot(
                                    skillshot.DetectionType,
                                    skillshot.SpellData,
                                    skillshot.StartTick,
                                    skillshot.Start,
                                    skillshot.End + skillshot.Direction * 100,
                                    skillshot.Unit);
                                if (!extendedE.IsSafePoint(s.End))
                                {
                                    endPos = s.End;
                                }
                                break;
                            }
                        }
                        foreach (var m in ObjectManager.Get<AIMinionClient>())
                        {
                            if (m.CharacterName == "jarvanivstandard" && m.Team == skillshot.Unit.Team)
                            {
                                var extendedE = new Skillshot(
                                    skillshot.DetectionType,
                                    skillshot.SpellData,
                                    skillshot.StartTick,
                                    skillshot.Start,
                                    skillshot.End + skillshot.Direction * 100,
                                    skillshot.Unit);
                                if (!extendedE.IsSafePoint(m.Position.ToVector2()))
                                {
                                    endPos = m.Position.ToVector2();
                                }
                                break;
                            }
                        }
                        if (endPos.IsValid())
                        {
                            skillshot = new Skillshot(
                                DetectionType.ProcessSpell,
                                SpellDatabase.GetByName("JarvanIVEQ"),
                                Variables.GameTimeTickCount,
                                skillshot.Start,
                                endPos + 200 * (endPos - skillshot.Start).Normalized(),
                                skillshot.Unit);
                        }
                    }
                }
                if (skillshot.SpellData.SpellName == "OriannasQ")
                {
                    EvadeManager.DetectedSkillshots.Add(
                        new Skillshot(
                            skillshot.DetectionType,
                            SpellDatabase.GetByName("OriannaQend"),
                            skillshot.StartTick,
                            skillshot.Start,
                            skillshot.End,
                            skillshot.Unit));
                }
                if (skillshot.SpellData.DisableFowDetection && skillshot.DetectionType == DetectionType.RecvPacket)
                {
                    return;
                }
                EvadeManager.DetectedSkillshots.Add(skillshot);
            }

            private static void OnUpdateEvade(EventArgs args)
            {
                //Game.Print("Evade:" + EvadeManager.DetectedSkillshots.Count());
                EvadeManager.DetectedSkillshots.RemoveAll(i => !i.IsActive());
                foreach (var skillshot in EvadeManager.DetectedSkillshots)
                {
                    skillshot.OnUpdate();
                }
                if (objPlayer.IsDead)
                {
                    return;
                }
                if (objPlayer.HasBuffOfType(BuffType.SpellImmunity) || objPlayer.HasBuffOfType(BuffType.SpellShield))
                {
                    return;
                }
                var safePoint = IsSafePoint(objPlayer.Position.ToVector2());
                var safePath = IsSafePath(objPlayer.GetWaypoints(), 100);
                if (!safePath.IsSafe && !safePoint.IsSafe)
                {
                    TryToEvade(safePoint.SkillshotList, Game.CursorPos.ToVector2());
                }
            }

            private static void TryToEvade(List<Skillshot> hitBy, Vector2 to)
            {
                var dangerLevel =
                    hitBy.Select(i => Yasuo.EvadeSkillshotMenu["Spells"]["ESS_" + i.SpellData.MenuItemName].GetValue<MenuSlider>("DangerLevel").Value)
                        .Concat(new[] { 0 })
                        .Max();
                foreach (var evadeSpell in
                    EvadeSpellDatabase.Spells.Where(i => i.Enabled && i.DangerLevel <= dangerLevel && i.IsReady)
                        .OrderBy(i => i.DangerLevel))
                {
                    if (evadeSpell.EvadeType == EvadeTypes.Dash && evadeSpell.CastType == CastTypes.Target)
                    {
                        var targets =
                            GetEvadeTargets(evadeSpell)
                                .Where(
                                    i =>
                                    IsSafePoint(PosAfterE(i).ToVector2()).IsSafe
                                    && (!UnderTower(PosAfterE(i)) || Yasuo.EvadeSkillshotMenu["Spells"]["ESSS_" + evadeSpell.Name].GetValue<MenuBool>("ETower")))
                                .ToList();
                        if (targets.Count > 0)
                        {
                            var closestTarget = targets.MinOrDefault(i => PosAfterE(i).ToVector2().Distance(to));
                            objPlayer.Spellbook.CastSpell(evadeSpell.Slot, closestTarget);
                            return;
                        }
                    }
                    if (evadeSpell.EvadeType == EvadeTypes.WindWall
                        && hitBy.Where(
                            i =>
                            i.SpellData.CollisionObjects.Contains(CollisionObjectTypes.YasuoWall)
                            && i.IsAboutToHit(
                                150 + evadeSpell.Delay - Yasuo.EvadeSkillshotMenu["Spells"]["ESSS_" + evadeSpell.Name].GetValue<MenuSlider>("WDelay").Value,
                                objPlayer))
                               .OrderByDescending(
                                   i => Yasuo.EvadeSkillshotMenu["Spells"]["ESS_" + i.SpellData.MenuItemName].GetValue<MenuSlider>("DangerLevel").Value)
                               .Any(
                                   i =>
                                   objPlayer.Spellbook.CastSpell(
                                       evadeSpell.Slot,
                                       objPlayer.Position.Extend(i.Start.ToVector3(), 100))))
                    {
                        return;
                    }
                }
            }

            #endregion

            internal struct IsSafeResult
            {
                #region Fields

                public bool IsSafe;

                public List<Skillshot> SkillshotList;

                #endregion
            }
        }

        public class EvadeTarget
        {
            #region Static Fields

            private static readonly List<Targets> DetectedTargets = new List<Targets>();

            private static readonly List<SpellData> Spells = new List<SpellData>();

            private static Vector2 wallCastedPos;

            #endregion

            #region Properties

            private static GameObject Wall
            {
                get
                {
                    return
                        ObjectManager.Get<GameObject>()
                            .FirstOrDefault(
                                i => i.IsValid && i.Name.Contains("_w_windwall"));
                }
            }

            #endregion

            #region Public Methods and Operators
            private static Menu evadeMenu;
            public static void Init(Menu menu)
            {
                LoadSpellData();
                evadeMenu = new Menu("EvadeTarget", "Evade Target");
                {
                    evadeMenu.Add(new MenuBool("W", "Use W"));
                    evadeMenu.Add(new MenuBool("E", "Use E (To Dash Behind WindWall)"));
                    evadeMenu.Add(new MenuBool("ETower", "-> Under Tower", false));
                    evadeMenu.Add(new MenuBool("BAttack", "Basic Attack"));
                    evadeMenu.Add(new MenuSlider("BAttackHpU", "-> If Hp <", 35));
                    evadeMenu.Add(new MenuBool("CAttack", "Crit Attack"));
                    evadeMenu.Add(new MenuSlider("CAttackHpU", "-> If Hp <", 40));
                    foreach (var hero in
                        HeroManager.Enemies.Where(i => Spells.Any(a => a.CharacterName == i.CharacterName)))
                    {
                        evadeMenu.Add(new Menu("ET_" + hero.CharacterName, "-> " + hero.CharacterName));
                    }
                    foreach (
                        var spell in Spells.Where(i => HeroManager.Enemies.Any(a => a.CharacterName == i.CharacterName)))
                    {

                        ((Menu)evadeMenu["ET_" + spell.CharacterName]).Add(new MenuBool(
                        spell.MissileName,
                        spell.MissileName + " (" + spell.Slot + ")"));
                    }
                }
                menu.Add(evadeMenu);
                Game.OnUpdate += OnUpdateTarget;
                GameObject.OnMissileCreate += ObjSpellMissileOnCreate;
                GameObject.OnDelete += ObjSpellMissileOnDelete;
                AIBaseClient.OnDoCast += OnProcessSpellCast;
            }

            #endregion

            #region Methods

            private static bool GoThroughWall(Vector2 pos1, Vector2 pos2)
            {
                if (Wall == null)
                {
                    return false;
                }
                var wallWidth = 300 + 50 * Convert.ToInt32(Wall.Name.Substring(Wall.Name.Length - 6, 1));
                var wallDirection = (Wall.Position.ToVector2() - wallCastedPos).Normalized().Perpendicular();
                var wallStart = Wall.Position.ToVector2() + wallWidth / 2f * wallDirection;
                var wallEnd = wallStart - wallWidth * wallDirection;
                var wallPolygon = new DaoHungAIO.Evade.Geometry.Polygon.Rectangle(wallStart, wallEnd, 75);
                var intersections = new List<Vector2>();
                for (var i = 0; i < wallPolygon.Points.Count; i++)
                {
                    var inter =
                        wallPolygon.Points[i].Intersection(
                            wallPolygon.Points[i != wallPolygon.Points.Count - 1 ? i + 1 : 0],
                            pos1,
                            pos2);
                    if (inter.Intersects)
                    {
                        intersections.Add(inter.Point);
                    }
                }
                return intersections.Any();
            }

            private static void LoadSpellData()
            {
                Spells.Add(
                    new SpellData
                    { CharacterName = "Ahri", SpellNames = new[] { "ahrifoxfiremissiletwo" }, Slot = SpellSlot.W });
                Spells.Add(
                    new SpellData
                    { CharacterName = "Ahri", SpellNames = new[] { "ahritumblemissile" }, Slot = SpellSlot.R });
                Spells.Add(
                    new SpellData { CharacterName = "Anivia", SpellNames = new[] { "frostbite" }, Slot = SpellSlot.E });
                Spells.Add(
                    new SpellData { CharacterName = "Annie", SpellNames = new[] { "annieq" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Brand",
                        SpellNames = new[] { "brandconflagrationmissile", "brandemissile" },
                        Slot = SpellSlot.E
                    });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Brand",
                        SpellNames = new[] { "brandwildfire", "brandwildfiremissile", "brandr", "brandrmissile" },
                        Slot = SpellSlot.R
                    });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Caitlyn",
                        SpellNames = new[] { "caitlynaceintheholemissile" },
                        Slot = SpellSlot.R
                    });
                Spells.Add(
                    new SpellData
                    { CharacterName = "Cassiopeia", SpellNames = new[] { "cassiopeiatwinfang", "cassiopeiae" }, Slot = SpellSlot.E });
                Spells.Add(
                    new SpellData { CharacterName = "Elise", SpellNames = new[] { "elisehumanq" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Ezreal",
                        SpellNames = new[] { "ezrealarcaneshiftmissile" },
                        Slot = SpellSlot.E
                    });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "FiddleSticks",
                        SpellNames = new[] { "fiddlesticksdarkwind", "fiddlesticksdarkwindmissile" },
                        Slot = SpellSlot.E
                    });
                Spells.Add(
                    new SpellData { CharacterName = "Gangplank", SpellNames = new[] { "parley", "gangplankqproceed" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData { CharacterName = "Jhin", SpellNames = new[] { "jhinq", "jhinqmisbounce" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData { CharacterName = "Janna", SpellNames = new[] { "sowthewind" }, Slot = SpellSlot.W });
                Spells.Add(
                    new SpellData { CharacterName = "Kassadin", SpellNames = new[] { "nulllance" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Katarina",
                        SpellNames = new[] { "katarinaq", "katarinaqmis" },
                        Slot = SpellSlot.Q
                    });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Katarina",
                        SpellNames = new[] { "katarinar", "katarinarmis" },
                        Slot = SpellSlot.R
                    });
                Spells.Add(
                    new SpellData
                    { CharacterName = "Kayle", SpellNames = new[] { "judicatorreckoning" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Leblanc",
                        SpellNames = new[] { "leblancchaosorb", "leblancchaosorbm", "leblancq" },
                        Slot = SpellSlot.Q
                    });
                Spells.Add(new SpellData { CharacterName = "Lulu", SpellNames = new[] { "luluw", "luluwtwo" }, Slot = SpellSlot.W });
                Spells.Add(
                    new SpellData
                    { CharacterName = "Malphite", SpellNames = new[] { "seismicshard" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "MissFortune",
                        SpellNames = new[] { "missfortunericochetshot", "missFortunershotextra" },
                        Slot = SpellSlot.Q
                    });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Nami",
                        SpellNames = new[] { "namiwenemy", "namiwmissileenemy" },
                        Slot = SpellSlot.W
                    });
                Spells.Add(
                    new SpellData { CharacterName = "Nunu", SpellNames = new[] { "iceblast" }, Slot = SpellSlot.E });
                Spells.Add(
                    new SpellData { CharacterName = "Pantheon", SpellNames = new[] { "pantheonq" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Ryze",
                        SpellNames = new[] { "spellflux", "spellfluxmissile", "ryzee" },
                        Slot = SpellSlot.E
                    });
                Spells.Add(
                    new SpellData { CharacterName = "Shaco", SpellNames = new[] { "twoshivpoison" }, Slot = SpellSlot.E });
                Spells.Add(
                    new SpellData { CharacterName = "Sona", SpellNames = new[] { "sonaqmissile" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData { CharacterName = "Swain", SpellNames = new[] { "swaintorment" }, Slot = SpellSlot.E });
                Spells.Add(
                    new SpellData { CharacterName = "Syndra", SpellNames = new[] { "syndrar", "syndrarcasttime" }, Slot = SpellSlot.R });
                Spells.Add(
                    new SpellData { CharacterName = "Taric", SpellNames = new[] { "dazzle" }, Slot = SpellSlot.E });
                Spells.Add(
                    new SpellData { CharacterName = "Teemo", SpellNames = new[] { "blindingdart" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData
                    { CharacterName = "Tristana", SpellNames = new[] { "detonatingshot" }, Slot = SpellSlot.E });
                Spells.Add(
                    new SpellData
                    { CharacterName = "Tristana", SpellNames = new[] { "tristanar" }, Slot = SpellSlot.R });
                Spells.Add(
                    new SpellData
                    { CharacterName = "TwistedFate", SpellNames = new[] { "bluecardattack" }, Slot = SpellSlot.W });
                Spells.Add(
                    new SpellData
                    { CharacterName = "TwistedFate", SpellNames = new[] { "goldcardattack" }, Slot = SpellSlot.W });
                Spells.Add(
                    new SpellData
                    { CharacterName = "TwistedFate", SpellNames = new[] { "redcardattack" }, Slot = SpellSlot.W });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Urgot",
                        SpellNames = new[] { "urgotheatseekinghomemissile" },
                        Slot = SpellSlot.Q
                    });
                Spells.Add(
                    new SpellData { CharacterName = "Vayne", SpellNames = new[] { "vaynecondemn", "vaynecondemnmissile" }, Slot = SpellSlot.E });
                Spells.Add(
                    new SpellData
                    { CharacterName = "Veigar", SpellNames = new[] { "veigarprimordialburst", "veigarr" }, Slot = SpellSlot.R });
                Spells.Add(
                    new SpellData
                    { CharacterName = "Viktor", SpellNames = new[] { "viktorpowertransfer" }, Slot = SpellSlot.Q });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Vladimir",
                        SpellNames = new[] { "vladimirtidesofbloodnuke" },
                        Slot = SpellSlot.E
                    });
                Spells.Add(
                    new SpellData
                    {
                        CharacterName = "Lillia",
                        SpellNames = new[] { "Nothing" },
                        Slot = SpellSlot.R
                    });
            }

            private static void ObjSpellMissileOnCreate(GameObject sender, EventArgs args)
            {
                if (!(sender is MissileClient))
                {
                    return;
                }
                var missile = (MissileClient)sender;
                if (!(missile.SpellCaster is AIHeroClient) || missile.SpellCaster.Team == objPlayer.Team)
                {
                    return;
                }
                var unit = (AIHeroClient)missile.SpellCaster;

                var spellData =
                    Spells.FirstOrDefault(
                        i =>
                        {
                            return i.SpellNames.Contains(missile.SData.Name.ToLower())
                        && evadeMenu["ET_" + i.CharacterName][i.MissileName] != null
                        && evadeMenu["ET_" + i.CharacterName].GetValue<MenuBool>(i.MissileName);
                        }
                        );
                if (spellData == null && Orbwalker.IsAutoAttack(missile.SData.Name)
                    && (!missile.SData.Name.ToLower().Contains("crit")
                            ? evadeMenu.GetValue<MenuBool>("BAttack")
                              && objPlayer.HealthPercent < evadeMenu.GetValue<MenuSlider>("BAttackHpU").Value
                            : evadeMenu.GetValue<MenuBool>("CAttack")
                              && objPlayer.HealthPercent < evadeMenu.GetValue<MenuSlider>("CAttackHpU").Value))
                {
                    spellData = new SpellData
                    { CharacterName = unit.CharacterName, SpellNames = new[] { missile.SData.Name } };
                }

                if (spellData == null || !missile.CastInfo.Target.IsMe)
                {
                    return;
                }
                DetectedTargets.Add(new Targets { Start = unit.Position, Obj = missile });
            }

            private static void ObjSpellMissileOnDelete(GameObject sender, EventArgs args)
            {
                if (!(sender is MissileClient))
                {
                    return;
                }
                var missile = (MissileClient)sender;
                if (missile.SpellCaster is AIHeroClient && missile.SpellCaster.Team != objPlayer.Team)
                {
                    DetectedTargets.RemoveAll(i => i.Obj.NetworkId == missile.NetworkId);
                }
            }

            private static void OnProcessSpellCast(AIBaseClient sender, AIBaseClientProcessSpellCastEventArgs args)
            {
                //if(sender.IsMe && args.Slot == SpellSlot.E)
                //{
                //    Q.CastOnUnit(Player);
                //}
                if (!sender.IsValid || sender.Team != ObjectManager.Player.Team || args.SData.Name != "YasuoWMovingWall")
                {
                    return;
                }
                wallCastedPos = sender.Position.ToVector2();
            }

            private static void OnUpdateTarget(EventArgs args)
            {
                if (objPlayer.IsDead)
                {
                    return;
                }

                if (objPlayer.HasBuffOfType(BuffType.SpellImmunity) || objPlayer.HasBuffOfType(BuffType.SpellShield))
                {
                    return;
                }

                if (!W.IsReady(300) && (Wall == null || !E.IsReady(200)))
                {
                    return;
                }


                foreach (var target in
                    DetectedTargets.Where(i => objPlayer.Distance(i.Obj.Position) < 700))
                {
                    if (E.IsReady() && evadeMenu.GetValue<MenuBool>("E") && Wall != null
                        && Variables.TickCount - W.LastCastAttemptT > 1000
                        && !GoThroughWall(objPlayer.Position.ToVector2(), target.Obj.Position.ToVector2())
                        && W.IsInRange(target.Obj, 250))
                    {
                        var obj = new List<AIBaseClient>();
                        obj.AddRange(GetMinions(E.Range, MinionManager.MinionTypes.All, MinionTeam.Enemy));
                        obj.AddRange(HeroManager.Enemies.Where(i => i.IsValidTarget(E.Range)));
                        if (
                            obj.Where(
                                i =>
                                !i.HasBuff("YasuoE") && EvadeSkillshot.IsSafePoint(i.Position.ToVector2()).IsSafe
                                && EvadeSkillshot.IsSafePoint(PosAfterE(i).ToVector2()).IsSafe
                                && (!UnderTower(PosAfterE(i)) || evadeMenu.GetValue<MenuBool>("ETower"))
                                && GoThroughWall(objPlayer.Position.ToVector2(), PosAfterE(i).ToVector2()))
                                .OrderBy(i => PosAfterE(i).Distance(Game.CursorPos))
                                .Any(i => E.CastOnUnit(i)))
                        {
                            return;
                        }
                    }
                    if (W.IsReady() && evadeMenu.GetValue<MenuBool>("W") && W.IsInRange(target.Obj, 500)
                        && W.Cast(objPlayer.Position.Extend(target.Start, 100)))
                    {
                        return;
                    }
                }
            }

            #endregion

            private class SpellData
            {
                #region Fields

                public string CharacterName;

                public SpellSlot Slot;

                public string[] SpellNames = { };

                #endregion

                #region Public Properties

                public string MissileName
                {
                    get
                    {
                        return this.SpellNames.First();
                    }
                }

                #endregion
            }

            private class Targets
            {
                #region Fields

                public MissileClient Obj;

                public Vector3 Start;

                #endregion
            }
        }
        #endregion

        #region Orbwalker
        private static void Yasuo_DoCombo()
        {
            if (YasuoMenu.Yasuo_Keys.ComboEQ3Flash.Active)
            {
                EQFlashInCombo();
            }

            if (YasuoMenu.Yasuo_target.Yasuo_Target_lock.Enabled)
            {
                var target1 = TargetSelector.SelectedTarget;
                if (target1 == null || !target1.IsValidTarget(3000))
                {
                    target1 = FSTargetSelector.GetFSTarget(3000);               
                }
                foreach (var target in GameObjects.EnemyHeroes.Where(x => x.IsValidTarget(2000)))
                {
                    if (target == null || target.HasBuffOfType(BuffType.Invulnerability) || target.HasBuffOfType(BuffType.SpellImmunity))
                        return;
                    if (target != null || target1 != null)
                    {                       
                        if ((target.HasBuffOfType(BuffType.Knockup) || target.HasBuffOfType(BuffType.Knockback)) && R.IsReady() && target.IsValidTarget(R.Range) && YasuoMenu.Rcombo.RtargetHeath.Value >= target.HealthPercent && YasuoMenu.Rcombo.Yasuo_Rcombo.Enabled)
                        {
                            var buff = target.Buffs.FirstOrDefault(i => i.Type == BuffType.Knockback || i.Type == BuffType.Knockup);
                            if ((buff.EndTime - Game.Time) * 1000 <= YasuoMenu.Rcombo.RDelayTime.Value && !YasuoMenu.Rcombo.AlwaysEQR.Enabled)
                            {
                                Game.Print("R accepted");
                                R.Cast(target.Position);
                            }
                            if (Q.IsReady() && E.IsReady())
                            {
                                var EQR = GameObjects.Get<AIBaseClient>().Where(i => i.IsValidTarget(E.Range) && !i.IsAlly && !i.HasBuff("YasuoE"));

                                if (EQR.Any())
                                {
                                    foreach (var obj in EQR)
                                    {
                                        E1.CastOnUnit(obj);
                                        Q.Cast(PosExploit(obj));
                                        R.Cast(target.Position);
                                    }
                                }
                            }
                            if (Q.IsReady() && isYasuoDashing)
                            {
                                Q.Cast(PosExploit());
                                R.Cast(target.Position);
                            }
                        }
                        else
                        {
                            Egaptarget(target1);

                            if (HaveQ3)
                            {
                                if (Q3.GetPrediction(target1).CastPosition.DistanceToPlayer() <= YasuoMenu.RangeCheck.Q3range.Value)
                                {
                                    QcastTarget(target1);
                                }
                                else
                                {
                                    QcastTarget(target);
                                }
                            }
                            else
                            {
                                if (Q.GetPrediction(target1).CastPosition.DistanceToPlayer() <= YasuoMenu.RangeCheck.Qrange.Value)
                                {
                                    QcastTarget(target1);
                                }
                                else
                                {
                                    QcastTarget(target);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(x => x.IsValidTarget(3000)))
                {
                    if (target == null || target.HasBuffOfType(BuffType.Invulnerability) || target.HasBuffOfType(BuffType.SpellImmunity))
                        return;
                    if (target != null)
                    {
                        if ((target.HasBuffOfType(BuffType.Knockup) || target.HasBuffOfType(BuffType.Knockback)) && R.IsReady() && target.IsValidTarget(R.Range) && YasuoMenu.Rcombo.RtargetHeath.Value >= target.HealthPercent && YasuoMenu.Rcombo.Yasuo_Rcombo.Enabled)
                        {
                            var buff = target.Buffs.FirstOrDefault(i => i.Type == BuffType.Knockback || i.Type == BuffType.Knockup);
                            if (buff.EndTime * 1000 < 200 + Game.Ping)
                            {
                                R.Cast(target);
                            }
                            if (Q.IsReady() && E.IsReady())
                            {
                                var EQR = new List<AIBaseClient>();
                                EQR.AddRange(ObjectManager.Get<AIBaseClient>().Where(i => i.IsValidTarget(E.Range) && !i.IsAlly && !i.HasBuff("YasuoE")));

                                if (EQR.Any())
                                {
                                    foreach (var obj in EQR)
                                    {
                                        E1.CastOnUnit(obj);
                                        Q.Cast(PosExploit(obj));
                                        R.Cast(target.Position);
                                    }
                                }
                                else
                                {

                                }
                            }
                            if (Q.IsReady() && isYasuoDashing)
                            {
                                Q.Cast(PosExploit());
                                R.Cast(target.Position);
                            }
                        }
                        else
                        {
                            Egaptarget(target);

                            if (HaveQ3)
                            {
                                QcastTarget(target);
                            }
                            else
                            {
                                QcastTarget(target);
                            }
                        }
                    }
                }
            }
        }

        private static void Yasuo_DoClear()
        {
            //laneclear
            var Qminions = GameObjects.Enemy.Where(i => i.IsValidTarget(HaveQ3 ? YasuoMenu.RangeCheck.Q3range : YasuoMenu.RangeCheck.Qrange) && !i.Position.IsBuilding());
            var Eminions = GameObjects.EnemyMinions.Where(i => i.IsValidTarget(E.Range) && CanE(i));
            if (Qminions != null && YasuoMenu.Yasuo_Clear.Yasuo_Qclear.Enabled && Q.IsReady())
            {
                foreach (var min in Qminions)
                {
                    if (isYasuoDashing)
                    {
                        YasuoMenu.Yasuo_Keys.AutoQifDashOnTarget.Active = true;
                    }
                    else
                    {
                        //YasuoMenu.Yasuo_Keys.AutoQifDashOnTarget.Active = false;
                        if (!min.IsMinion())
                        {
                            if (UnderTower(objPlayer.Position) && min.HealthPercent >= 30) return;

                            else
                            {
                                if (HaveQ3)
                                {
                                    if (!YasuoMenu.Yasuo_Clear.Yasuo_Windclear.Enabled) return;

                                    else
                                    {
                                        if (Q3.SPredictionCast(min as AIHeroClient, HitChance.High))
                                            return;
                                    }
                                }
                                else
                                {
                                    if(Q.SPredictionCast(min as AIHeroClient, HitChance.High))
                                            return;
                                }
                            }
                        }
                        else
                        {
                            if (HaveQ3)
                            {
                                if (!YasuoMenu.Yasuo_Clear.Yasuo_Windclear.Enabled) return;

                                else
                                {
                                    var qFarm = Q3.GetLineFarmLocation(Qminions.ToList());

                                    if (qFarm.MinionsHit >= YasuoMenu.Yasuo_Clear.Yasuo_Windclear_ifhit.Value)
                                    {
                                        Q3.Cast(qFarm.Position);
                                    }
                                }
                            }
                            else
                            {
                                var qFarm = Q.GetLineFarmLocation(Qminions.ToList());

                                if (qFarm.MinionsHit >= 1)
                                {
                                    Q.Cast(qFarm.Position);
                                }
                            }
                        }
                    }
                }
            }
            if (Eminions != null && YasuoMenu.Yasuo_Clear.Yasuo_Eclear.Enabled && E.IsReady())
            {
                foreach(var min in Eminions)
                {
                    if(min.Health <= E.GetDamage(min))
                    {
                        if (YasuoMenu.Yasuo_Keys.TurretKey.Active)
                        {
                            if (E1.Cast(min) == CastStates.SuccessfullyCasted || E1.CastOnUnit(min))
                                return;
                        }
                        else
                        {
                            if (UnderTower(PosAfterE(min))) return;

                            else
                            {
                                if (E1.Cast(min) == CastStates.SuccessfullyCasted || E1.CastOnUnit(min))
                                    return;
                            }
                        }
                    }
                }

                if(Eminions.All(i => !i.IsValidTarget(350)) && !YasuoMenu.Yasuo_Clear.Yasuo_Eclear_ks.Enabled)
                {
                    if (isYasuoDashing)
                    {
                        YasuoMenu.Yasuo_Keys.AutoQifDashOnTarget.Active = true;
                    }

                    foreach (var min in Eminions)
                    {
                        if (YasuoMenu.Yasuo_Keys.TurretKey.Active)
                        {
                            if (E1.Cast(min) == CastStates.SuccessfullyCasted || E1.CastOnUnit(min))
                                return;
                        }
                        else
                        {
                            if (UnderTower(PosAfterE(min))) return;

                            else
                            {
                                if (E1.Cast(min) == CastStates.SuccessfullyCasted || E1.CastOnUnit(min))
                                    return;
                            }
                        }
                    }
                }
            }

            //jungleclear
            var jungles = GameObjects.Jungle.Where(i => i.IsValidTarget(HaveQ3 ? YasuoMenu.RangeCheck.Q3range : YasuoMenu.RangeCheck.Qrange));

            if(jungles != null)
            {
                if (isYasuoDashing)
                {
                    YasuoMenu.Yasuo_Keys.AutoQifDashOnTarget.Active = true;
                }

                foreach (var jl in jungles)
                {
                    if(jl.IsValidTarget(YasuoMenu.RangeCheck.Erange.Value) && CanE(jl) && jl.DistanceToPlayer() > 240)
                    {
                        if (E1.Cast(jl) == CastStates.SuccessfullyCasted || E1.CastOnUnit(jl))
                            return;
                    }
                    if(Q.IsReady())
                        if (HaveQ3)
                        {
                            if (isYasuoDashing)
                            {
                                if (Epred(jl).Distance(objPlayer.GetDashInfo().EndPos) <= YasuoMenu.RangeCheck.EQrange.Value)
                                {
                                    Q3.Cast(PosExploit(jl));
                                }
                            }
                            else
                            {
                                Q3.Cast(Q3.GetPrediction(jl).CastPosition);
                            }
                        }
                        else
                        {
                            if (isYasuoDashing)
                            {
                                if (Epred(jl).Distance(objPlayer.GetDashInfo().EndPos) <= YasuoMenu.RangeCheck.EQrange.Value)
                                {
                                    Q.Cast(PosExploit(jl));
                                }
                            }
                            else
                            {
                                Q.Cast(Q.GetPrediction(jl).CastPosition);
                            }
                        }
                }
            }
        }

        private static void Yasuo_DoHarass()
        {
            var targets = ObjectManager.Get<AIHeroClient>().Where(i => !i.IsAlly && !i.IsDead && i.IsValidTarget(Q3.Range));

            if (targets != null)
            {
                foreach(var target in targets)
                {
                    var obj = GetNearObj(target);

                    if(obj != null)
                    {
                        if (E.IsReady() && Q.IsReady() && (HaveQ2 || HaveQ3) &&
                                                PosAfterE(obj).Distance(Epred(target)) <= YasuoMenu.RangeCheck.EQrange && !UnderTower(PosAfterE(obj))
                                                )
                        {
                            if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                            {
                                YasuoMenu.Yasuo_Keys.AutoQifDashOnTarget.Active = true;
                                return;
                            }
                        }
                        else
                        {
                            if (target.IsValidTarget(HaveQ3 ? Q3.Range : Q.Range) && Q.IsReady() && !isYasuoDashing)
                            {
                                if (HaveQ3)
                                {
                                    if (Q3.SPredictionCast(target as AIHeroClient, HitChance.High))
                                        return;
                                }
                                else
                                {
                                    if (Q.SPredictionCast(target as AIHeroClient, HitChance.High))
                                        return;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (target.IsValidTarget(HaveQ3 ? Q3.Range : Q.Range) && Q.IsReady() && !isYasuoDashing)
                        {
                            if (HaveQ3)
                            {
                                if (Q3.SPredictionCast(target as AIHeroClient, HitChance.High))
                                    return;
                            }
                            else
                            {
                                if (Q.SPredictionCast(target as AIHeroClient, HitChance.High))
                                    return;
                            }
                        }
                    }
                }
            }

            var minions = ObjectManager.Get<AIMinionClient>().Where(i => !i.IsAlly && !i.IsDead && i.IsValidTarget(Q.Range) && i.Health < Q.GetDamage(i));
            if(minions != null)
            {
                foreach(var min in minions)
                {
                    if (Q.IsReady() && !HaveQ3)
                    {
                        Q.Cast(min);
                    }
                }
            }
        }

        private static void LogicEZicZac(AIBaseClient target)
        {
            // return if on auto attack
            if (oaa || baa) return;

            if (target == null || !target.IsValidTarget(1000)) return;

            if (UnderTower(objPlayer.Position)) return;

            if (!YasuoMenu.Ecombo.Yasuo_Eziczac.Enabled) return;

            if (YasuoMenu.Ecombo.Yasuo_Eziczac_Qready.Enabled && Q.IsReady()) return;

            AIBaseClient obj1 = null;
            AIBaseClient obj2 = null;
            bool ready = false;

            //search
            var AllObj = new List<AIBaseClient>();
            AllObj.AddRange(ObjectManager.Get<AIMinionClient>().Where(i => i.IsValidTarget(1000) && !i.IsAlly && !i.HasBuff("YasuoE")));
            AllObj.AddRange(ObjectManager.Get<AIHeroClient>().Where(i => i.IsValidTarget(1000) && !i.IsAlly && !i.HasBuff("YasuoE")));

            //MenuChecker
            if (YasuoMenu.Ecombo.Yasuo_zizzacRange.Value < target.DistanceToPlayer()) return;

            if(YasuoMenu.Ecombo.Yasuo_EziczacMode.Index == 0)
            {
                if (GetNearObj(target) == null) return;
            }
            if (YasuoMenu.Ecombo.Yasuo_EziczacMode.Index == 1)
            {
                if (objPlayer.CountEnemyHeroesInRange(YasuoMenu.Ecombo.Yasuo_zizzacRange.Value) <= 1) return;
            }
            if (YasuoMenu.Ecombo.Yasuo_EziczacMode.Index == 2)
            {
                if (
                    !target.CanMove
                    || !target.CanAttack
                    ) return;

                if (
                    target.Armor < objPlayer.Armor
                    && target.IsRanged
                    ) return;

                //Some Champion
                if (
                    target.CharacterName == "Shaco"
                    || target.CharacterName == "Fizz"
                    || target.CharacterName == "Akali"
                    || target.CharacterName == "Camille"
                    || target.CharacterName == "Gangplank"
                    || target.CharacterName == "Graves"
                    || target.CharacterName == "Hecarim"
                    || target.CharacterName == "Katarina"
                    || target.CharacterName == "Poppy"
                    ) return;
            }
            if (YasuoMenu.Ecombo.Yasuo_EziczacMode.Index == 3)
            {
                //nothing happaned
            }

            //set
            if (AllObj.Count < 2) return;
            foreach(var aobj in AllObj)
            {
                obj1 = aobj;
                foreach (var bobj in AllObj.Where(i => i.NetworkId != aobj.NetworkId))
                {
                    obj2 = bobj;

                    //ready ?
                    if (obj1.NetworkId != obj2.NetworkId && obj1 != null && obj2 != null)
                    {
                        ready = true;
                    }
                    else
                    {
                        ready = false;
                    }

                    if((PosAfterE(obj1).Distance(obj2) < 475 && obj1.IsValidTarget(E.Range)) || (PosAfterE(obj2).Distance(obj1) < 475 && obj2.IsValidTarget(E.Range)))
                    {
                        ready = true;
                    }
                    else
                    {
                        ready = false;
                    }

                    //logic
                    if (ready == true)
                    {
                        var ziczacpos1 = PosAfterE(obj1).Extend(Epred(obj2), 475);
                        var ziczacpos2 = PosAfterE(obj2).Extend(Epred(obj1), 475);

                        if (ziczacpos1.Distance(Epred(target)) <= YasuoMenu.RangeCheck.EQrange.Value || ziczacpos1.Distance(Epred(target)) <= Epred(target).DistanceToPlayer())
                        {
                            //Render.Circle.DrawCircle(ziczacpos1, 100, Color.Red);

                            if (YasuoMenu.Yasuo_Keys.TurretKey.Active || !UnderTower(PosAfterE(obj1)))
                                if(obj1.IsValidTarget(E.Range))
                                    if (E1.Cast(obj1) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj1))
                                        return;
                                    else return;
                            else return;
                        }
                        if (ziczacpos2.Distance(Epred(target)) <= YasuoMenu.RangeCheck.EQrange.Value || ziczacpos2.Distance(Epred(target)) <= Epred(target).DistanceToPlayer())
                        {
                            //Render.Circle.DrawCircle(ziczacpos2, 100, Color.Red);

                            if (YasuoMenu.Yasuo_Keys.TurretKey.Active || !UnderTower(PosAfterE(obj2)))
                                if (obj2.IsValidTarget(E.Range))
                                    if (E1.Cast(obj2) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj2))
                                        return;
                                    else return;
                            else return;
                        }
                    }
                }
            }                               
        }
        private static void Egaptarget(AIBaseClient target)
        {
            if (target == null || oaa || baa) return;

            if (YasuoMenu.Ecombo.Yasuo_Eziczac.Enabled && E.Level >= 1)
            {
                LogicEZicZac(target);
            }

            var obj = GetNearObj(target);
           
            if (obj != null && E.Level >= 1)
            {
                if (UnderTower(objPlayer.Position) && !UnderTower(target.Position))
                {
                    YasuoMenu.Ecombo.Yasuo_Eziczac.Enabled = false;
                    if (obj.NetworkId == target.NetworkId)
                    {
                        if (Epred(obj).DistanceToPlayer() > 300)
                        {
                            if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                                return;
                        }
                    }
                    else
                    {
                        if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                            return;
                    }

                    if (Epred(target).Distance(PosAfterE(obj)) <= YasuoMenu.RangeCheck.EQrange.Value)
                    {
                        if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                            return;
                    }
                }
                else
                {
                    //YasuoMenu.Ecombo.Yasuo_Eziczac.Enabled = true;
                }
                if (YasuoMenu.Yasuo_Keys.TurretKey.Active)
                {
                    if (obj.NetworkId == target.NetworkId)
                    {
                        if(Epred(obj).DistanceToPlayer() > 300)
                        {
                            if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                                return;
                        }
                    }
                    else
                    {
                        if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                            return;
                    }

                    if(Epred(target).Distance(PosAfterE(obj)) <= YasuoMenu.RangeCheck.EQrange.Value)
                    {
                        if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                            return;
                    }
                }
                else
                {
                    if (UnderTower(PosAfterE(obj))) return;

                    if (obj.NetworkId == target.NetworkId)
                    {
                        if (E.GetPrediction(obj).CastPosition.DistanceToPlayer() > 300)
                        {
                            if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                                return;
                        }
                    }
                    else
                    {
                        if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                            return;
                    }

                    if (Epred(target).Distance(PosAfterE(obj)) <= YasuoMenu.RangeCheck.EQrange.Value)
                    {
                        if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                            return;
                    }
                }                   
            }

        }

        public static Vector3 Epred(AIBaseClient target)
        {
            if (isYasuoDashing) return target.Position;
            return E.GetPrediction(target).CastPosition;
        }

        private static void QcastTarget(AIBaseClient target)
        {
            if (target == null || !Q.IsReady()) return;

            if (!isYasuoDashing)
            {
                CastQNormal(target);
            }
            else
            {
                CastQcircle(target);
            }
        }

        private static void CastQNormal(AIBaseClient target)
        {
            if (HaveQ3)
            {
                if (!YasuoMenu.Qcombo.Yasuo_Windcombo.Enabled) return;
                if (oaa && target.HealthPercent > YasuoMenu.Qcombo.Yasuo_Qoa.Value) return;

                if (Q3.SPredictionCast(target as AIHeroClient, HitChance.High))
                    return;


                /*var Qpred = Q3.GetPrediction(target, false, -1, CollisionObjects.YasuoWall);
                if (Qpred.Hitchance >= HitChance.High && !Qpred.CastPosition.IsZero && Qpred.CastPosition.Distance(target) <= YasuoMenu.RangeCheck.Q3range)
                {
                    Q3.Cast(Qpred.CastPosition);
                }*/
            }
            else
            {
                if (!YasuoMenu.Qcombo.Yasuo_Windcombo.Enabled) return;
                if (oaa && target.HealthPercent > YasuoMenu.Qcombo.Yasuo_Qoa.Value) return;

                if (Q.SPredictionCast(target as AIHeroClient, HitChance.High))
                    return;
            }
        }
        private static void CastQcircle(AIBaseClient target)
        {
            if (HaveQ3)
            {
                if (!YasuoMenu.EQCombo.Yasuo_EWindcombo) return;
                if (objPlayer.GetDashInfo().EndPos.Distance(objPlayer.Position) < 200)
                {
                    if (objPlayer.GetDashInfo().EndPos.Distance(target) <= YasuoMenu.RangeCheck.EQrange.Value)
                    {
                        Q3.Cast(PosExploit(target));
                    }
                }
            }
            else
            {
                if (!YasuoMenu.EQCombo.Yasuo_EQcombo.Enabled) return;
                if (HaveQ2)
                {
                    Q.Cast(PosExploit(target));
                }
                else
                {
                    var Eobjs = new List<AIBaseClient>();
                    Eobjs.AddRange(ObjectManager.Get<AIBaseClient>().Where(i => i.Distance(objPlayer.GetDashInfo().EndPos) <= YasuoMenu.RangeCheck.EQrange.Value && !i.IsAlly));

                    if (target.DistanceToPlayer() > 450 && Eobjs != null)
                    {
                        Q.Cast(PosExploit(target));
                    }
                    if(target.Distance(objPlayer.GetDashInfo().EndPos) < YasuoMenu.RangeCheck.EQrange.Value && objPlayer.GetDashInfo().EndPos.DistanceToPlayer() < 200)
                    {
                        Q.Cast(PosExploit(target));
                    }
                }
            }
        }

        private static void EQFlashInCombo()
        {
            if (Flash == SpellSlot.Unknown || !EQFlash.IsReady() || !Flash.IsReady()) return;
            var targets = TargetSelector.GetTargets(850);
            Vector3 FlashPos = Vector3.Zero;

            if (!targets.Any()) return;

            var target = FSTargetSelector.GetFSTarget(850);

            foreach (var EQprediction in targets.Select(i => FSpred.Prediction.Prediction.GetPrediction(EQFlash, i)).Where(i => i.Hitchance >= FSpred.Prediction.HitChance.High && i.AoeTargetsHitCount >= 1).OrderByDescending(i => i.AoeTargetsHitCount))
            {
                FlashPos = EQprediction.CastPosition;

                if (isYasuoDashing && HaveQ3 && Q.IsReady() && FlashPos != Vector3.Zero)
                {
                    if (FlashPos.Distance(objPlayer.Position) <= 400 + 175 && FlashPos.Distance(objPlayer.Position) >= 175)
                    {
                        Q3.Cast(PosExploit(target));
                        DelayAction.Add(50, () => { EQFlash.Cast(FlashPos); });
                    }
                }

                if (!isYasuoDashing && HaveQ3 && Q.IsReady() && FlashPos != Vector3.Zero)
                {
                    if (FlashPos.Distance(objPlayer.Position) <= 400 + 175 && FlashPos.Distance(objPlayer.Position) >= 175)
                    {
                        var objs = GameObjects.Enemy.Where(i => !i.Position.IsBuilding() && CanE(i) && i.IsValidTarget(E.Range));
                        foreach(var obj in objs)
                        {
                            if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                                return;
                        }
                    }
                }
            }
        }

        private static void YasuoEQFlash()
        {
            if (Flash == SpellSlot.Unknown || !EQFlash.IsReady() || !Flash.IsReady()) return;
            var targets = TargetSelector.GetTargets(850);
            Vector3 FlashPos = Vector3.Zero;

            if (!targets.Any()) return;

            var target = FSTargetSelector.GetFSTarget(3000);

            foreach (var EQprediction in targets.Select(i => EQFlash.GetPrediction(i)).Where(i => i.Hitchance >= HitChance.High || (i.Hitchance >= HitChance.Medium && i.AoeTargetsHitCount > 1)).OrderByDescending(i => i.AoeTargetsHitCount))
            {
                FlashPos = EQprediction.CastPosition;
                //Render.Circle.DrawCircle(FlashPos, 230, System.Drawing.Color.Red, 10);

                if (isYasuoDashing && HaveQ3 && Q.IsReady() && FlashPos != Vector3.Zero)
                {
                    if (FlashPos.Distance(objPlayer.Position) <= 400 + 175 && FlashPos.Distance(objPlayer.Position) > 175)
                    {
                        //Render.Circle.DrawCircle(FlashPos, 230, System.Drawing.Color.Blue, 13);
                        Q3.Cast(PosExploit(target));
                        DelayAction.Add(100, () => { EQFlash.Cast(FlashPos); });
                    }
                }

                if (!isYasuoDashing && target != null)
                {
                    var obj = GetNearObj(target);
                    if (obj != null)
                    {
                        if (obj.NetworkId == target.NetworkId)
                        {
                            if (Epred(obj).DistanceToPlayer() > 300)
                            {
                                if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                                    return;
                            }
                        }
                        else
                        {
                            if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                                return;
                        }

                        if (Epred(target).Distance(PosAfterE(obj)) <= YasuoMenu.RangeCheck.EQrange.Value)
                        {
                            if (E1.Cast(obj) == CastStates.SuccessfullyCasted || E1.CastOnUnit(obj))
                                return;
                        }
                    }
                }
            }
        }
        #endregion
    }

    #region Help Evade
    internal class EvadeSpellDatabase
    {
        #region Static Fields

        public static List<EvadeSpellData> Spells = new List<EvadeSpellData>();

        #endregion

        #region Constructors and Destructors

        static EvadeSpellDatabase()
        {
            if (ObjectManager.Player.CharacterName != "Yasuo")
            {
                return;
            }
            Spells.Add(
                new EvadeSpellData
                {
                    Name = "YasuoDashWrapper",
                    DangerLevel = 2,
                    Slot = SpellSlot.E,
                    EvadeType = EvadeTypes.Dash,
                    CastType = CastTypes.Target,
                    MaxRange = 475,
                    Speed = 1000,
                    Delay = 50,
                    FixedRange = true,
                    ValidTargets = new[] { SpellTargets.EnemyChampions, SpellTargets.EnemyMinions }
                });
            Spells.Add(
                new EvadeSpellData
                {
                    Name = "YasuoWMovingWall",
                    DangerLevel = 3,
                    Slot = SpellSlot.W,
                    EvadeType = EvadeTypes.WindWall,
                    CastType = CastTypes.Position,
                    MaxRange = 400,
                    Speed = int.MaxValue,
                    Delay = 250
                });
        }

        #endregion
    }
    internal static class Configs
    {
        #region Constants

        public const int EvadingFirstTimeOffset = 250;

        public const int EvadingSecondTimeOffset = 80;

        public const int GridSize = 10;

        public const int SkillShotsExtraRadius = 9;

        public const int SkillShotsExtraRange = 20;

        #endregion
    }
    public enum CastTypes
    {
        Position,

        Target,

        Self
    }

    public enum SpellTargets
    {
        AllyMinions,

        EnemyMinions,

        AllyWards,

        EnemyWards,

        AllyChampions,

        EnemyChampions
    }

    public enum EvadeTypes
    {
        Blink,

        Dash,

        Invulnerability,

        MovementSpeedBuff,

        Shield,

        SpellShield,

        WindWall
    }
    internal class HeroManager
    {
        public static IEnumerable<AIHeroClient> Enemies
        {
            get { return GameObjects.EnemyHeroes; }
        }
    }

    internal class EvadeSpellData
    {
        #region Fields

        public CastTypes CastType;

        public string CheckSpellName = "";

        public int Delay;

        public EvadeTypes EvadeType;

        public bool FixedRange;

        public float MaxRange;

        public string Name;

        public SpellSlot Slot;

        public int Speed;

        public SpellTargets[] ValidTargets;

        private int dangerLevel;

        #endregion

        #region Public Properties

        public int DangerLevel
        {
            get
            {
                try
                {
                    return Yasuo.EvadeSkillshotMenu["Spells"]["ESSS_" + this.Name]["DangerLevel"] != null
                               ? Yasuo.EvadeSkillshotMenu["Spells"]["ESSS_" + this.Name].GetValue<MenuSlider>("DangerLevel").Value
                    : this.dangerLevel;
                }
                catch
                {
                    return this.dangerLevel;
                }
                //return this.dangerLevel;
            }
            set
            {
                this.dangerLevel = value;
            }
        }

        public bool Enabled
        {
            get
            {
                return Yasuo.EvadeSkillshotMenu["Spells"]["ESSS_" + this.Name].GetValue<MenuBool>("Enabled");
            }
        }

        public bool IsReady
        {
            get
            {
                return (this.CheckSpellName == ""
                        || ObjectManager.Player.Spellbook.GetSpell(this.Slot).Name == this.CheckSpellName)
                       && this.Slot.IsReady();
            }
        }
        #endregion
    }
    #endregion
    #endregion

    #region Yone God Like
    internal class YoneMenu
    {
        public class Cancelaa
        {
            public static MenuBool Q_cancel = new MenuBool("Q", "Accept Q Cancel AA", false);
            public static MenuBool W_cancel = new MenuBool("W", "Accept W Cancel AA", false);
            public static MenuBool E_cancel = new MenuBool("E", "Accept E Cancel AA", false);
            public static MenuBool R_cancel = new MenuBool("R", "Accept R Cancel AA", false);
        }
        public class Qcombo
        {
            public static MenuBool Combo_Qcombo = new MenuBool("Qcombo", "Q in combo");
            public static MenuBool Combo_Qwindcombo = new MenuBool("Qwindcombo", "_Wind in combo");
            public static MenuBool Combo_Qbeforeaa = new MenuBool("Qbeforeaa", "_ Only use Q [Before aa]", false);
            public static MenuBool Combo_Qafteraa = new MenuBool("Qafteraa", "_ Only use Q [After aa]", false);

            public static MenuBool Combo_Qauto = new MenuBool("Comb_Qauto", "Auto use Q", false);
            public static MenuBool AcceptQ3 = new MenuBool("AcceptQ3", "---> Wind", false);
        }
        public class Wcombo
        {
            public static MenuBool Combo_Wcombo = new MenuBool("Wcombo", "W in combo");
            public static MenuBool Combo_Wafteraa = new MenuBool("Wafteraa", "_ Only use W [After aa]", true);
            public static MenuBool Combo_Woutaarange = new MenuBool("Woutaarange", "_ Only use W [Out aa range]", true);
            public static MenuBool Combo_Wifhavewind = new MenuBool("Wifhavewind", "_ Only use W [Player have Wind]", false);
            public static MenuSliderButton Combo_Whit = new MenuSliderButton("Whit", ": : Use W if hit x target", 1, 0, 5);
        }
        public class Ecombo
        {
            public static MenuBool Combo_Ecombo = new MenuBool("Ecombo", "E in combo");
            public static MenuBool Combo_Edashturret = new MenuBool("Edashturret", "_ E dash turret");
            public static MenuSliderButton Combo_Etargetheath = new MenuSliderButton("Etargetheath", ": : Use E if target heath <=", 100);
            public static MenuSliderButton Combo_Eplayerheath = new MenuSliderButton("Eplayerheath", ": : Use E if player heath <=", 80);
            public static MenuBool Combo_Ereturn = new MenuBool("Ereturn", "_ E return [gap closer]");
            public static MenuBool Combo_Eoutaarange = new MenuBool("Eoutaarange", "_ only use E [Out aa range]");
            public static MenuBool Combo_Eifhavewind = new MenuBool("Eifhavewind", "_ Only use E [Player have Wind]", false);
            public static MenuBool Combo_Etargetcount = new MenuBool("Etargetcount", ": : Use E if target count > 1");
        }
        public class Rcombo
        {
            public static MenuBool Combo_Rcombo = new MenuBool("Rcombo", "R in combo");
            public static MenuSliderButton Combo_Rtargetheath = new MenuSliderButton("Rtargetheath", ": : Use R if target heath <= ", 40);
            public static MenuSliderButton Combo_Rhitcount = new MenuSliderButton("Rhitcount", "Use R if can hit >= ", 3, 1, 5);
            
        }

        public class EQwind
        {
            public static MenuBool Combo_EQWind = new MenuBool("Combo_EQWind", "EWind logic");
        }
        public class Keys
        {
            public static MenuKeyBind TurretKey = new MenuKeyBind("TurretKey", "Allow combo in Turret", System.Windows.Forms.Keys.A, KeyBindType.Toggle);
            public static MenuKeyBind SemiR = new MenuKeyBind("SemiR", "R Using Key", System.Windows.Forms.Keys.T, KeyBindType.Press);
            public static MenuKeyBind SemiE = new MenuKeyBind("SemiE", "E Using Key", System.Windows.Forms.Keys.A, KeyBindType.Toggle);
        }
    }
    internal class Yone
    {
        private static Spell Q1, Q3, W, E, E2, R;
        private static AIHeroClient objPlayer = ObjectManager.Player;
        public static Menu YoneTheMenu;
        public static bool baa = false;
        public static bool oaa = false;
        public static bool aaa = false;
        private static bool isQ3()
        {
            if(Q1.Name == "YoneQ3")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool isE2()
        {
            if(ObjectManager.Player.Mana > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static Vector3 EShadowPos()
        {
            var shadows = GameObjects.Get<AIBaseClient>().Where(i => i.CharacterName == "TestCubeRender10Vision" && i.HealthPercent == 100);
            var pos = Vector3.Zero;
            foreach(var shadow in shadows)
            {
                if (shadow == null)
                {
                    pos =  Vector3.Zero;
                }
                else
                {
                    pos = shadow.Position;
                }
            }
            return pos;
        }
        public static void YoneLoaded()
        {
            YoneTheMenu = new Menu("YoneTheMenu", "Yone God Like", true);
            var combomenu = new MenuSeparator("combomenu", "Combo Settings");
            /*{
                YoneMenu.Cancelaa.Q_cancel,
                YoneMenu.Cancelaa.W_cancel,
                YoneMenu.Cancelaa.E_cancel,
                YoneMenu.Cancelaa.R_cancel,
                YoneMenu.Qcombo.Combo_Qcombo,
                YoneMenu.Qcombo.Combo_Qwindcombo,
                YoneMenu.Qcombo.Combo_Qbeforeaa,
                YoneMenu.Qcombo.Combo_Qafteraa,
                YoneMenu.Qcombo.Combo_Qauto,
                YoneMenu.Qcombo.AcceptQ3,
                YoneMenu.Wcombo.Combo_Wcombo,
                YoneMenu.Wcombo.Combo_Wafteraa,
                YoneMenu.Wcombo.Combo_Woutaarange,
                YoneMenu.Wcombo.Combo_Wifhavewind,
                YoneMenu.Wcombo.Combo_Whit,
                YoneMenu.Wcombo.Combo_Wtargetheath,
                YoneMenu.Wcombo.Combo_Wplayerheath,
                YoneMenu.Ecombo.Combo_Ecombo,
                YoneMenu.Ecombo.Combo_Edashturret,
                YoneMenu.Ecombo.Combo_Etargetheath,
                YoneMenu.Ecombo.Combo_Eplayerheath,
                YoneMenu.Ecombo.Combo_Ereturn,
                YoneMenu.Ecombo.Combo_Eoutaarange,
                YoneMenu.Ecombo.Combo_Eifhavewind,
                YoneMenu.Ecombo.Combo_Etargetcount,
                YoneMenu.Rcombo.Combo_Rcombo,
                YoneMenu.Rcombo.Combo_Rhitcount,
                YoneMenu.Rcombo.Combo_Rtargetheath,
                YoneMenu.EQwind.Combo_EQWind
            };*/

            Menu Qcb = new Menu("Qcombo", "Q Settings") { YoneMenu.Qcombo.Combo_Qcombo,
                YoneMenu.Qcombo.Combo_Qwindcombo,
                YoneMenu.Qcombo.Combo_Qbeforeaa,
                YoneMenu.Qcombo.Combo_Qafteraa,
                YoneMenu.Qcombo.Combo_Qauto,
                YoneMenu.Qcombo.AcceptQ3,};
            Menu Wcb = new Menu("Wcombo", "W Settings") { YoneMenu.Wcombo.Combo_Wcombo,
                YoneMenu.Wcombo.Combo_Wafteraa,
                YoneMenu.Wcombo.Combo_Woutaarange,
                YoneMenu.Wcombo.Combo_Wifhavewind,
                YoneMenu.Wcombo.Combo_Whit,};
            Menu Ecb = new Menu("Ecombo", "E Settings") { YoneMenu.Ecombo.Combo_Ecombo,
                YoneMenu.Ecombo.Combo_Edashturret,
                YoneMenu.Ecombo.Combo_Etargetheath,
                YoneMenu.Ecombo.Combo_Eplayerheath,
                YoneMenu.Ecombo.Combo_Ereturn,
                YoneMenu.Ecombo.Combo_Eoutaarange,
                YoneMenu.Ecombo.Combo_Eifhavewind,
                YoneMenu.Ecombo.Combo_Etargetcount,};
            Menu Rcb = new Menu("Rcombo", "R Settings") { YoneMenu.Rcombo.Combo_Rcombo,
                YoneMenu.Rcombo.Combo_Rhitcount,
                YoneMenu.Rcombo.Combo_Rtargetheath,};

            Menu AA = new Menu("AA", "AA Settings") { YoneMenu.Cancelaa.Q_cancel,
                YoneMenu.Cancelaa.W_cancel,
                YoneMenu.Cancelaa.E_cancel,
                YoneMenu.Cancelaa.R_cancel,};

            Menu Keys = new Menu("Keys", "Keys Settings");
            Keys.Add(YoneMenu.Keys.TurretKey).Permashow();
            Keys.Add(YoneMenu.Keys.SemiE).Permashow();
            Keys.Add(YoneMenu.Keys.SemiR).Permashow();

            YoneTheMenu.Add(combomenu);
            YoneTheMenu.Add(Qcb);
            YoneTheMenu.Add(Wcb);
            YoneTheMenu.Add(Ecb);
            YoneTheMenu.Add(Rcb);
            YoneTheMenu.Add(AA);
            YoneTheMenu.Add(Keys);
            YoneTheMenu.Attach();


            Q1 = new Spell(SpellSlot.Q, 475);
            Q3 = new Spell(SpellSlot.Q, 900);
            W = new Spell(SpellSlot.W, 600);
            E = new Spell(SpellSlot.E, 300);
            R = new Spell(SpellSlot.R, 1000);

            Q1.SetSkillshot(0.2f, 20, float.MaxValue, false, SkillshotType.Line);
            Q3.SetSkillshot(0.25f, 50, 1500, false, SkillshotType.Line);
            W.SetSkillshot(0.25f, 100, float.MaxValue, false, SkillshotType.Line);
            R.SetSkillshot(0.7f, 150, float.MaxValue, false, SkillshotType.Line);

            Game.OnUpdate += Game_OnUpdate;
            Orbwalker.OnAction += Orbwalker_OnAction;
            Drawing.OnDraw += Drawing_OnDraw;
            Game.OnUpdate += Game_OnUpdate1;
        }

        private static void Game_OnUpdate1(EventArgs args)
        {
            if (objPlayer.IsDead) return;

            if(Orbwalker.ActiveMode == OrbwalkerMode.LastHit)
            {
                var Minions = GameObjects.EnemyMinions.Where(i => !i.IsDead && i.IsValidTarget(Q1.Range));
                if (Minions == null)
                    return;

                foreach(var minion in Minions.Where(i => i.Health <= objPlayer.GetSpellDamage(i, SpellSlot.Q)))
                {
                    if (!isQ3())
                    {
                        if (Q1.Cast(minion.Position))
                            return;
                    }
                }
            }
        }

        private static void Drawing_OnDraw(EventArgs args)
        {
            if(isE2() && EShadowPos() != Vector3.Zero)
            {
                Drawing.DrawCircle(EShadowPos(), 70, Color.SkyBlue);
            }

            if(Orbwalker.GetTarget() != null && Orbwalker.GetTarget() is AIHeroClient && Orbwalker.GetTarget().IsVisibleOnScreen)
            {
                Drawing.DrawCircle(Orbwalker.GetTarget().Position, 70, Color.Gold);
            }
        }

        private static void Orbwalker_OnAction(object sender, OrbwalkerActionArgs args)
        {
            if (args.Type == OrbwalkerType.AfterAttack)
            {
                aaa = true;
                baa = false;
                oaa = false;
            }
            else aaa = false;

            if (args.Type == OrbwalkerType.BeforeAttack)
            {
                aaa = false;
                baa = true;
                oaa = false;
            }
            else baa = false;

            if (args.Type == OrbwalkerType.OnAttack)
            {
                aaa = false;
                baa = false;
                oaa = true;
            }
            else oaa = false;

            if (args.Type == OrbwalkerType.AfterAttack)
            {
                var Qminions = GameObjects.Jungle.Where(i => i.IsValidTarget(isQ3() ? 900 : 475) && !i.Position.IsBuilding());
                if (Qminions != null && Q1.IsReady())
                {
                    foreach (var min in Qminions)
                    {
                        if (isQ3())
                        {
                            var qFarm = Q3.GetPrediction(min);

                            if (!UnderTower(objPlayer.Position.Extend(qFarm.CastPosition, 500)))
                            {
                                Q3.Cast(qFarm.CastPosition);
                            }
                        }
                        else
                        {
                            var qFarm = Q1.GetPrediction(min);

                            Q1.Cast(qFarm.CastPosition);
                        }
                    }
                }
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (objPlayer.IsDead) return;
            if (YoneMenu.Keys.SemiR.Active && R.IsReady())
            {
                var targets = TargetSelector.GetTargets(2000);
                Vector3 Rpos = Vector3.Zero;

                if (!targets.Any()) return;
                foreach (var Rprediction in targets.Select(i => R.GetPrediction(i)).Where(i => i.Hitchance >= HitChance.High || (i.Hitchance >= HitChance.Medium && i.AoeTargetsHitCount > 1)).OrderByDescending(i => i.AoeTargetsHitCount))
                {
                    Rpos = Rprediction.CastPosition;
                }
                if(Rpos != Vector3.Zero)
                {
                    R.Cast(Rpos);
                }
            }
            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkerMode.Harass:
                    Yone_Clear();
                    break;
                case OrbwalkerMode.Combo:
                    Yone_Combo();
                    break;
                case OrbwalkerMode.LaneClear:
                    Yone_Clear();
                    break;
            }
        }
        private static void Yone_Clear()
        {
            var Qminions = GameObjects.Enemy.Where(i => i.IsValidTarget(isQ3() ? 900 : 475) && !i.Position.IsBuilding()).OrderByDescending(i => i.Health);
            if (Qminions != null && Q1.IsReady())
            {
                foreach (var min in Qminions)
                {
                    if (!min.IsMinion())
                    {
                        if (min.HealthPercent >= 30) return;

                        else
                        {
                            if (isQ3())
                            {
                                var qFarm = Q3.GetPrediction(min);

                                if (qFarm.Hitchance >= HitChance.High && qFarm.CastPosition.DistanceToPlayer() <= 900 && !UnderTower(objPlayer.Position.Extend(qFarm.CastPosition, 500)))
                                    Q3.Cast(qFarm.CastPosition);
                            }
                            else
                            {
                                var qFarm = Q1.GetPrediction(min);

                                if (qFarm.Hitchance >= HitChance.High && qFarm.CastPosition.DistanceToPlayer() <= 475)
                                    Q1.Cast(qFarm.CastPosition);
                            }
                        }
                    }
                    else
                    {
                        if (isQ3())
                        {
                            if (Orbwalker.ActiveMode == OrbwalkerMode.Harass)
                            {
                                return;
                            }
                            else
                            {
                                var qFarm = Q3.GetLineFarmLocation(Qminions.ToList());

                                if (qFarm.MinionsHit >= 1 && !UnderTower(objPlayer.Position.Extend(qFarm.Position, 500)))
                                {
                                    Q3.Cast(qFarm.Position);
                                }
                            }
                        }
                        else
                        {
                            if (Orbwalker.ActiveMode == OrbwalkerMode.Harass)
                            {
                                if(min.Health < objPlayer.GetSpellDamage(min, SpellSlot.Q) && min.IsValidTarget(Q1.Range))
                                {
                                    Q1.Cast(min.Position);
                                }
                            }
                            else
                            {
                                var qFarm = Q1.GetLineFarmLocation(Qminions.ToList());

                                if (qFarm.MinionsHit >= 1)
                                {
                                    Q1.Cast(qFarm.Position);
                                }
                            }                           
                        }
                    }
                }
            }
        }
        public static bool UnderTower(Vector3 pos, int bonus = 0)
        {
            return
                ObjectManager.Get<AITurretClient>()
                    .Any(i => i.IsEnemy && !i.IsDead && (i.Distance(pos) < 850 + ObjectManager.Player.BoundingRadius + bonus));
        }

        private static void Yone_Combo()
        {
            var target = TargetSelector.SelectedTarget;

            if (target == null || !target.IsValidTarget(750))
            {
                target = TargetSelector.GetTarget(2000, DamageType.Physical);
            }

            if (target == null || target.InAutoAttackRange())
            {
                target = Orbwalker.GetTarget() as AIHeroClient;
            }

            if (target == null || !(target is AIHeroClient)) return;

            /*if(isE2())
            if(EShadowPos() != Vector3.Zero)
            {
                Game.Print("Shadow Detected");
                
            }
            else
            {
                Game.Print("Shadow Undetected");
            }*/


            QCombo(target);
            WCombo(target);
            ECombo(target);

            Rcombo(target);
            
        }

        private static void Rcombo(AIBaseClient target)
        {
            if (!YoneMenu.Rcombo.Combo_Rcombo.Enabled)
                return;

            try
            {
                var targets = TargetSelector.GetTargets(1000);
                Vector3 Rpos = Vector3.Zero;

                if (!targets.Any()) return;
                foreach (var Rprediction in targets.Select(i => R.GetPrediction(i)).Where(i => (i.Hitchance >= HitChance.Medium && i.AoeTargetsHitCount >= YoneMenu.Rcombo.Combo_Rhitcount.ActiveValue)).OrderByDescending(i => i.AoeTargetsHitCount))
                {
                    Rpos = Rprediction.CastPosition;
                }
                if (Rpos != Vector3.Zero)
                {
                    R.Cast(Rpos);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("R.cast Error" + ex);
            }
        }
        
        private static void QCombo(AIBaseClient target1)
        {
            if (oaa && !YoneMenu.Cancelaa.Q_cancel.Enabled) return;
            if (!Q1.IsReady()) return;
            foreach(AIBaseClient target in GameObjects.Get<AIHeroClient>().Where(i => !i.IsAlly && !i.IsDead))
            {
                if (target == null) return;
                if (target.IsValidTarget(isQ3() ? 1000 : 500))
                {
                    if (Q1.IsReady())
                    {
                        if (!isQ3())
                        {
                            var qpred = Q1.GetPrediction(target);
                            if (qpred.Hitchance >= HitChance.High || (qpred.Hitchance >= HitChance.Medium && qpred.AoeTargetsHitCount > 1))
                                if (qpred.CastPosition.DistanceToPlayer() <= 475 && YoneMenu.Qcombo.Combo_Qcombo.Enabled)
                                    if ((!YoneMenu.Qcombo.Combo_Qafteraa.Enabled || aaa) || (!YoneMenu.Qcombo.Combo_Qbeforeaa.Enabled || baa))
                                        Q1.Cast(qpred.CastPosition);
                        }
                        else
                        {
                            var qpred = Q3.GetPrediction(target);
                            if (qpred.Hitchance >= HitChance.High || (qpred.Hitchance >= HitChance.Medium && qpred.AoeTargetsHitCount > 1))
                                if (qpred.CastPosition.DistanceToPlayer() <= 900 && YoneMenu.Qcombo.Combo_Qwindcombo.Enabled)
                                    if (!UnderTower(objPlayer.Position.Extend(qpred.CastPosition, 500)) || YoneMenu.Keys.TurretKey.Active)
                                        Q3.Cast(qpred.CastPosition);
                        }
                    }
                }
            }           
        }
        private static void WCombo(AIBaseClient target)
        {
            if (!W.IsReady()) return;
            if (oaa && !YoneMenu.Cancelaa.W_cancel.Enabled) return;

            if (W.IsReady() && target.IsValidTarget(600))
            {               
                if (YoneMenu.Wcombo.Combo_Woutaarange.Enabled && !target.IsValidTarget(objPlayer.GetRealAutoAttackRange() -20))
                {
                    if (W.GetPrediction(target).Hitchance >= HitChance.Medium && W.GetPrediction(target).CastPosition.DistanceToPlayer() <= 600)
                    {
                        W.Cast(W.GetPrediction(target).CastPosition);
                    }
                }
                else
                {
                    if (YoneMenu.Wcombo.Combo_Wifhavewind.Enabled && isQ3())
                    {
                        if (W.GetPrediction(target).Hitchance >= HitChance.Medium && W.GetPrediction(target).CastPosition.DistanceToPlayer() <= 600)
                        {
                            W.Cast(W.GetPrediction(target).CastPosition);
                        }
                    }
                    else
                    {
                        if (YoneMenu.Wcombo.Combo_Wafteraa.Enabled && aaa)
                        {
                            if (W.GetPrediction(target).Hitchance >= HitChance.Medium && W.GetPrediction(target).CastPosition.DistanceToPlayer() <= 600)
                            {
                                W.Cast(W.GetPrediction(target).CastPosition);
                            }
                        }
                        else
                        {
                            if (W.GetPrediction(target).Hitchance >= HitChance.Medium && W.GetPrediction(target).CastPosition.DistanceToPlayer() <= 600)
                            {
                                W.Cast(W.GetPrediction(target).CastPosition);
                            }
                        }
                    }
                }                
            }
        }
        private static void ECombo(AIBaseClient target)
        {
            if (oaa && !YoneMenu.Cancelaa.E_cancel.Enabled) return;
            if (!YoneMenu.Ecombo.Combo_Ecombo.Enabled) return;
            if (!E.IsReady()) return;

            if (isE2())
            {
                if(EShadowPos() != Vector3.Zero)
                {
                    if (!target.IsValidTarget(isQ3() ? 900 + 200 : 475 + 200) && target.Position.Distance(EShadowPos()) > (isQ3() ? 900 + 200 : 475 + 200))
                        return;
                }                
            }
            else
            {
                if (!target.IsValidTarget(isQ3() ? 900 + 200 : 475 + 200))
                    return;
            }

            if (!isE2())
            {
                if (YoneMenu.Ecombo.Combo_Edashturret.Enabled)
                {
                    if (UnderTower(objPlayer.Position) && UnderTower(target.Position) && !UnderTower(objPlayer.Position.Extend(target.Position, -300)))
                    {
                        E.Cast(target.Position);
                    }
                    if (UnderTower(objPlayer.Position) && UnderTower(target.Position) && UnderTower(objPlayer.Position.Extend(target.Position, -300)))
                    {
                        foreach(AITurretClient turret in GameObjects.EnemyTurrets.Where(i => !i.IsAlly && !i.IsDead && i.DistanceToPlayer() < 850))
                        {
                            if(turret != null)
                            {
                                if(!UnderTower(objPlayer.Position.Extend(turret.Position, -300)))
                                {
                                    E.Cast(turret.Position);
                                }
                            }
                        }
                    }
                }
                if (YoneMenu.Ecombo.Combo_Etargetheath.Enabled && YoneMenu.Ecombo.Combo_Etargetheath.Value >= target.HealthPercent && !UnderTower(target.Position, 300))
                {
                    if (!UnderTower(objPlayer.Position.Extend(target.Position, 300)) && !UnderTower(objPlayer.Position.Extend(target.Position, -300)))
                        E.Cast(target.Position);
                }
                if (YoneMenu.Ecombo.Combo_Etargetcount.Enabled)
                {
                    if (objPlayer.CountEnemyHeroesInRange(600) > 1)
                    {
                        E.Cast(target.Position);
                    }
                }
                if(UnderTower(target.Position, 250) && YoneMenu.Keys.SemiE.Active)
                {
                    E.Cast(target.Position);
                }
            }
            if(isE2() && EShadowPos() != Vector3.Zero && YoneMenu.Ecombo.Combo_Ereturn.Enabled)
            {
                if (target.DistanceToPlayer() > (isQ3() ? 900 : 475 + 200))
                {
                    if (target.Distance(EShadowPos()) < target.DistanceToPlayer() - 250)
                    {
                        E.Cast(target.Position);
                    }
                }
                if (isQ3() && Q1.IsReady())
                {
                    if(target.DistanceToPlayer() - 200 > EShadowPos().Distance(target) && objPlayer.ManaPercent < 40)
                    {
                        E.Cast(target.Position);
                    }
                }
            }               
        }
    }
    #endregion
}
