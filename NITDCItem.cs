using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Globalization;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace NITrainingDataCollector
{
    public class NITDCItem : GlobalItem
    {
        public static FileStream stream;
        public static StreamWriter writer;
        public static CsvWriter csv;

        public override bool OnPickup(Item item, Player player)
        {
            var config = new CsvConfiguration()
            {
                // Don't write the header again.
                HasHeaderRecord = false,
            };

            using (var stream = File.Open($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\My Games\\Terraria\\tModLoader\\ModSources\\NITrainingDataCollector\\data\\item.csv", FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecord(new ItemData
                {
                    HP = Main.LocalPlayer.statLifeMax,
                    Mana = Main.LocalPlayer.statManaMax,
                    Defense = Main.LocalPlayer.statDefense,
                    EOC = NPC.downedBoss1,
                    Skel = NPC.downedBoss3,
                    WOF = Main.hardMode,
                    Item = item.netID
                });
            }

            return base.OnPickup(item, player);
        }
    }

    public class ItemData
    {
        public int HP { get; set; }
        public int Mana { get; set; }
        public int Defense { get; set; }
        public bool EOC { get; set; }
        public bool Skel { get; set; }
        public bool WOF { get; set; }
        public int Item { get; set; }
    }
}