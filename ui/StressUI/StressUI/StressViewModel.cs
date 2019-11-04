using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace StressUI
{
    public class StressViewModel : ReactiveObject
    {
        public ObservableCollection<string> Subjects { get; set; }

        [Reactive]
        public string SelectedSubject1 { get; set; }

        [Reactive]
        public string SelectedSubject2 { get; set; }

        [Reactive]
        public string SelectedSubject3 { get; set; }

        [Reactive]
        public int Subject1Score { get; set; }
        
        [Reactive]
        public int Subject2Score { get; set; } 

        [Reactive]
        public int Subject3Score { get; set; }

        [Reactive]
        public int Coffee { get; set; } 

        [Reactive]
        public int EnergyDrinks { get; set; }

        [Reactive]
        public int SleepHours { get; set; } = 8;

        [Reactive]
        public int StudyHours { get; set; } = 0;

        public bool Subject1ScoreButtonEnabled { [ObservableAsProperty] get;  }
        public bool Subject2ScoreButtonEnabled { [ObservableAsProperty] get;  }
        public bool Subject3ScoreButtonEnabled { [ObservableAsProperty] get;  }

        public ReactiveCommand<Unit, DialogResult> CalculateStress { get; set; }

        public bool ButtonEnabled { [ObservableAsProperty] get; set; }

        public StressViewModel()
        {
            Subjects = new ObservableCollection<string>(SubjectsData.Subjects);

            this.WhenAnyValue(x => x.SelectedSubject1)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.ButtonEnabled);

            this.WhenAnyValue(x => x.SelectedSubject1)
                .Skip(1)
                .Do(_ => Subject1Score = Subject1Score == 0 ? 10 : Subject1Score)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.Subject1ScoreButtonEnabled);

            this.WhenAnyValue(x => x.SelectedSubject2)
                .Skip(1)
                .Do(_ => Subject2Score = Subject2Score == 0 ? 10 : Subject2Score)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.Subject2ScoreButtonEnabled);

            this.WhenAnyValue(x => x.SelectedSubject3)
                .Skip(1)
                .Do(_ => Subject3Score = Subject3Score == 0 ? 10 : Subject3Score)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.Subject3ScoreButtonEnabled);

            var currentDirectory = Directory.GetCurrentDirectory() + "/userinput.py";
            CalculateStress = ReactiveCommand.Create(() =>
            {
                var command =
                    $"{SelectedSubject1.SpacesToFloor()} {SelectedSubject2?.SpacesToFloor() ?? "0"} {SelectedSubject3?.SpacesToFloor() ?? "0"} {Subject1Score} {Subject2Score} {Subject3Score} {Coffee} {EnergyDrinks} {SleepHours} {StudyHours}";
                return MessageBox.Show("Wynik: " + RunCmd(currentDirectory, command));
            });
        }

        public string RunCmd(string cmd, string args)
        {
            var start = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"{cmd}\" \"{args}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using (var process = Process.Start(start))
            {
                using (var reader = process.StandardOutput)
                {
                    var stderr = process.StandardError.ReadToEnd();
                    var result = reader.ReadToEnd();
                    return result;
                }
            }
        }
    }
}