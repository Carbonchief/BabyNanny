using BabyNanny.Data;

namespace BabyNanny
{
    public partial class App : Application
    {

        public static BabyNannyRepository? BabyNannyRepository { get; private set; }
        public static ChildState? ChildState { get; private set; }

        public App(BabyNannyRepository? babyNannyRepository, ChildState childState)
        {
            InitializeComponent();
            BabyNannyRepository = babyNannyRepository;
            ChildState = childState;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell(ChildState!));
        }
    }
}
