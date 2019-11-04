using ReactiveUI;

namespace StressUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<StressViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new StressViewModel();

            this.OneWayBind(ViewModel, vm => vm.Subjects, v => v.Subject1.ItemsSource);
            this.Bind(ViewModel, vm => vm.SelectedSubject1, v => v.Subject1.SelectedItem);
            this.OneWayBind(ViewModel, vm => vm.Subject1ScoreButtonEnabled, v => v.Subject1Score.IsEnabled);
            this.Bind(ViewModel, vm => vm.Subject1Score, v => v.Subject1Score.Value);

            this.OneWayBind(ViewModel, vm => vm.Subjects, v => v.Subject2.ItemsSource);
            this.Bind(ViewModel, vm => vm.SelectedSubject2, v => v.Subject2.SelectedItem);
            this.OneWayBind(ViewModel, vm => vm.Subject2ScoreButtonEnabled, v => v.Subject2Score.IsEnabled);
            this.Bind(ViewModel, vm => vm.Subject2Score, v => v.Subject2Score.Value);

            this.OneWayBind(ViewModel, vm => vm.Subjects, v => v.Subject3.ItemsSource);
            this.Bind(ViewModel, vm => vm.SelectedSubject3, v => v.Subject3.SelectedItem);
            this.OneWayBind(ViewModel, vm => vm.Subject3ScoreButtonEnabled, v => v.Subject3Score.IsEnabled);
            this.Bind(ViewModel, vm => vm.Subject3Score, v => v.Subject3Score.Value);

            this.Bind(ViewModel, vm => vm.Coffee, v => v.Coffee.Value);
            this.Bind(ViewModel, vm => vm.EnergyDrinks, v => v.EnergyDrinks.Value);
            this.Bind(ViewModel, vm => vm.SleepHours, v => v.SleepHours.Value);
            this.Bind(ViewModel, vm => vm.StudyHours, v => v.StudyHours.Value);

            this.OneWayBind(ViewModel, vm => vm.ButtonEnabled, v => v.Calculate.IsEnabled);
            this.BindCommand(ViewModel, vm => vm.CalculateStress, v => v.Calculate);
        }
    }
}