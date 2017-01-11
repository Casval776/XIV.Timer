using FFXIV_Timer.Global;

namespace FFXIV_Timer.Controller
{
    public class MainController
    {
        // ReSharper disable once InconsistentNaming
        private static readonly MainController _instance = new MainController();
        // ReSharper disable once InconsistentNaming
        public DatabaseConn dbConn = DatabaseConn.Instance;

        public static MainController Instance => _instance;

        private MainController()
        {

        }

        static MainController() { }

    }
}
