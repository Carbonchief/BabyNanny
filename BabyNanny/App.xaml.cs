using BabyNanny.Data;

namespace BabyNanny
{
    public partial class App : Application
    {

        public static BabyNannyRepository? BabyNannyRepository { get; private set; }

        public App(BabyNannyRepository? babyNannyRepository)
        {
            InitializeComponent();      
            BabyNannyRepository = babyNannyRepository;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new MainPage());
        }
    }
}
