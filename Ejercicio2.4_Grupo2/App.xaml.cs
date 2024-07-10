namespace Ejercicio2._4_Grupo2
{
    public partial class App : Application
    {
        static Controllers.VideoController dbVideo;

        public static Controllers.VideoController DataBase
        {
            get
            {
                if (dbVideo == null)
                {
                    dbVideo = new Controllers.VideoController();
                }
                return dbVideo;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.PageInit());
        }
    }
}
