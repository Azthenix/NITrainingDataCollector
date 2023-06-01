using Terraria;
using Terraria.ModLoader;

using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
using tModPorter;

namespace NITrainingDataCollector
{
    public class NITDCEnemy : GlobalNPC
    {
        public static FileStream stream;
        public static StreamWriter writer;
        public static CsvWriter csv;

        public override void OnKill(NPC npc)
        {
            base.OnKill(npc);

            if (npc.friendly || npc.CountsAsACritter || npc.boss)
                return;

            var config = new CsvConfiguration()
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };


            using (var stream = File.Open("Documents\\My Games\\Terraria\\tModLoader\\ModSources\\NITrainingDataCollector\\data\\enemy.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecord(new EnemyData
                {
                    HP = Main.LocalPlayer.statLifeMax,
                    Mana = Main.LocalPlayer.statManaMax,
                    Defense = Main.LocalPlayer.statDefense,
                    EOC = NPC.downedBoss1,
                    Skel = NPC.downedBoss3,
                    WOF = Main.hardMode,
                    Enemy = npc.netID
                });
            }
        }
    }

    public class EnemyData
    {
        public int HP { get; set; }
        public int Mana { get; set; }
        public int Defense { get; set; }
        public bool EOC { get; set; }
        public bool Skel { get; set; }
        public bool WOF { get; set; }
        public int Enemy { get; set; }
    }
}