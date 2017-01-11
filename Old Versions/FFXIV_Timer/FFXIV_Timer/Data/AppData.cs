using System;
using System.Collections.Generic;

namespace FFXIV_Timer.Data
{
    public static class AppData
    {
        /// <summary>
        /// Static Data to hold default values for the Miner SourceClass
        /// </summary>
        public static readonly List<object[]> MinerNodes = new List<object[]>
                {
                    new object[] { "Miner", "Coblyn Larva", 1, CreateTimeOfDay(9), false, 7, "Eastern Thanalan", 3},
                    new object[] { "Miner", "Grade 3 La Noscean Topsoil", 50, CreateTimeOfDay(19), false, 8, "Middle La Noscea", 3},
                    new object[] { "Miner", "Grade 3 Shroud Topsoil", 50, CreateTimeOfDay(6), false, 8, "South Shroud", 3},
                    new object[] { "Miner", "Grade 3 Thanalan Topsoil", 50, CreateTimeOfDay(5), false, 8, "Western Thanalan", 3},
                    new object[] { "Miner", "Astral Rock", 55, CreateTimeOfDay(21), false, 7, "Coerthas Central Highlands", 3},
                    new object[] { "Miner", "Darksteel Ore", 55, CreateTimeOfDay(1), false, 3, "Coerthas Central Highlands", 3},
                    new object[] { "Miner", "Gold Ore", 55, CreateTimeOfDay(9), false, 6, "Eastern Thanalan", 3},
                    new object[] { "Miner", "Gold Sand", 55, CreateTimeOfDay(5), false, 2, "Eastern Thanalan", 3},
                    new object[] { "Miner", "Black Limestone", 70, CreateTimeOfDay(8), false, 0, "Western La Noscean", 3},
                    new object[] { "Miner", "Ferberite", 70, CreateTimeOfDay(3), false, 6, "Northern Thanalan", 3},
                    new object[] { "Miner", "Gregarious Worm", 70, CreateTimeOfDay(9), false, 1, "Eastern Thanalan", 3},
                    new object[] { "Miner", "Native Gold", 70, CreateTimeOfDay(4), false, 6, "Central Thanalan", 3},
                    new object[] { "Miner", "Raw Diamond", 70, CreateTimeOfDay(4), false, 8, "Central Thanalan", 3},
                    new object[] { "Miner", "Raw Emerald", 70, CreateTimeOfDay(4), false, 7, "Central Thanalan", 3},
                    new object[] { "Miner", "Raw Lolite", 70, CreateTimeOfDay(3), false, 8, "Northern Thanalan", 3},
                    new object[] { "Miner", "Raw Ruby", 70, CreateTimeOfDay(18), false, 7, "Lower La Noscea", 3},
                    new object[] { "Miner", "Raw Sapphire", 70, CreateTimeOfDay(18), false, 8, "Lower La Noscea", 3},
                    new object[] { "Miner", "Raw Topaz", 70, CreateTimeOfDay(3), false, 7, "Northern Thanalan", 3},
                    new object[] { "Miner", "Umbral Rock", 70, CreateTimeOfDay(6), false, 7, "South Shroud", 3},
                    new object[] { "Miner", "Volcanic Rock Salt", 70, CreateTimeOfDay(17), false, 1, "Eastern La Noscea", 3},
                    new object[] { "Miner", "Antumbral Rock", 75, CreateTimeOfDay(2), false, 7, "Eastern Thanalan", 3},
                    new object[] { "Miner", "Platinum Ore", 80, CreateTimeOfDay(4), false, 6, "Southern Thanalan", 3},
                    new object[] { "Miner", "Pumice", 80, CreateTimeOfDay(1), false, 1, "Eastern La Noscea", 3},
                    new object[] { "Miner", "Virgin Basilisk Egg", 90, CreateTimeOfDay(17), false, 6, "Northern Thanalan", 3},
                    new object[] { "Miner", "Yellow Copper Ore", 115, CreateTimeOfDay(0), true, 2, "Coerthas Western Highlands", 1},
                    new object[] { "Miner", "Pyrite", 120, CreateTimeOfDay(10), true, 3, "The Dravanian Forelands", 1},
                    new object[] { "Miner", "Yellow Quartz", 125, CreateTimeOfDay(2), true, 4, "The Churning Mists", 1},
                    new object[] { "Miner", "Chalcocite", 130, CreateTimeOfDay(10), true, 2, "The Dravanian Forelands", 1},
                    new object[] { "Miner", "Limonite", 133, CreateTimeOfDay(10), true, 4, "The Dravanian Forelands", 1},
                    new object[] { "Miner", "Green Quartz", 136, CreateTimeOfDay(2), true, 6, "The Churning Mists", 1},
                    new object[] { "Miner", "Abalathian Rock Salt", 139, CreateTimeOfDay(0), true, 1, "The Sea of Clouds", 1},
                    new object[] { "Miner", "Light Kidney Ore", 142, CreateTimeOfDay(2), true, 3, "The Dravanian Hinterlands", 1},
                    new object[] { "Miner", "Red Quartz", 145, CreateTimeOfDay(0), true, 6, "The Sea of Clouds", 1},
                    new object[] { "Miner", "Adamantite Ore", 148, CreateTimeOfDay(11), true, 6, "Azys Lla", 1},
                    new object[] { "Miner", "Cuprite", 148, CreateTimeOfDay(2), true, 2, "The Dravanian Hinterlands", 1},
                    new object[] { "Miner", "Astral Moraine", 160, CreateTimeOfDay(5), false, 4, "Coerthas Western Highlands", 1},
                    new object[] { "Miner", "Aurum Regis Ore", 160, CreateTimeOfDay(5), false, 6, "The Churning Mists", 1},
                    new object[] { "Miner", "Blue Quartz", 160, CreateTimeOfDay(7), false, 7, "The Dravanian Hinterlands", 1},
                    new object[] { "Miner", "Red Alumen", 160, CreateTimeOfDay(7), false, 2, "Azys Lla", 1},
                    new object[] { "Miner", "Sun Mica", 160, CreateTimeOfDay(5), false, 2, "The Sea of Clouds", 1},
                    new object[] { "Miner", "Violet Quartz", 160, CreateTimeOfDay(7), false, 6, "Coerthas Western Highlands", 1}
                };

        private static DateTime CreateTimeOfDay(int hour)
        {
            return new DateTime(1, 1, 1, hour, 0, 0);
        }
    }
}
