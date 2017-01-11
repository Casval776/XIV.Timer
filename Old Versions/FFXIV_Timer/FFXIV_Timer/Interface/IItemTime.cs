namespace FFXIV_Timer.Interface
{
    public interface IItemTime
    {
        // ReSharper disable once InconsistentNaming
        int ItemTime_Id { get; set; }
        // ReSharper disable once InconsistentNaming
        int UnspoiledItem_Id { get; set; }
        int Time { get; set; }
    }
}
