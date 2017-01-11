using FFXIV_Timer.Interface;

namespace FFXIV_Timer.Model
{
    public class UnspoiledItem : IUnspoiledItem
    {
        #region Properties

        public bool Selected { get; set; } = false;
        public int ItemLevel { get; set; }

        public string Location { get; set; }

        public string Name { get; set; }

        public int Slot { get; set; }

        public string SourceClass { get; set; }

        public string DisplayTime { get; set; }

        public int UnspoiledItem_Id { get; set; }

        public XIVTimer NextSpawn { get; set; }
        #endregion

        public UnspoiledItem()
        {
            NextSpawn = new XIVTimer();
        }
    }
}
