using System;

namespace FFXIV_Timer.Model
{
    /// <summary>
    /// Class used to hold the structure for the table used to initialize database.
    /// This model class does not model the actual UnspoiledItem object.
    /// </summary>
    public class UnspoiledItem_Mock
    {
        #region Properties
        public string SourceClass { get; set; }

        public string Name { get; set; }

        public int ItemLevel { get; set; }

        public DateTime Time { get; set; }

        public bool BiDaily { get; set; }

        public int Slot { get; set; }

        public string Location { get; set; }

        public int AvailabilityHours { get; set; }
        #endregion
    }
}
