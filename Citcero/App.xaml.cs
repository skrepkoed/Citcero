using Citcero.Resources.DbServices;

namespace Citcero
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            using var db = new ApplicationDbContext();
            db.Database.EnsureCreated();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

    }
}