using FFXIV_Timer.Model;

namespace FFXIV_Timer.Interface
{
    public interface IUnspoiledItem
    {
        bool Selected { get; set; }
        // ReSharper disable once InconsistentNaming
        int UnspoiledItem_Id { get; set; }
        string SourceClass { get; set; }
        string Name { get; set; }
        int ItemLevel { get; set; }
        string DisplayTime { get; set; }
        int Slot { get; set; }
        string Location { get; set; }
        XIVTimer NextSpawn { get; set; }
    }
}
