namespace MyCryptoMonitor.Statics
{
    public class MainService
    {
        public static void Startup()
        {

        }

        public static void Reset()
        {
            PortfolioService.DeleteAll();
            AlertService.Delete();
            UserConfigService.Delete();
        }
    }
}
