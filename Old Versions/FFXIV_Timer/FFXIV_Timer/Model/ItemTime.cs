using FFXIV_Timer.Interface;

namespace FFXIV_Timer.Model
{
    public class ItemTime : IItemTime
    {
        #region Properties
        public int ItemTime_Id { get; set; }
        public int UnspoiledItem_Id { get; set; }
        public int Time { get; set; }
        #endregion
    }
}
