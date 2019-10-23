using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;
using DynamicData;
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
        public int SleepHours { get; set; }

        [Reactive]
        public int StudyHours { get; set; }

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
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.Subject1ScoreButtonEnabled);

            this.WhenAnyValue(x => x.SelectedSubject2)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.Subject2ScoreButtonEnabled);

            this.WhenAnyValue(x => x.SelectedSubject3)
                .Select(x => x != null)
                .ToPropertyEx(this, x => x.Subject3ScoreButtonEnabled);

            var currentDirectory = Directory.GetCurrentDirectory() + "/userinput.py";
            CalculateStress = ReactiveCommand.Create(() => MessageBox.Show(RunCmd(currentDirectory,
                $"{SelectedSubject1} {SelectedSubject2 ?? "0"} {SelectedSubject2 ?? "0"} {Subject1Score} {Subject2Score} {Subject3Score} {Coffee} {EnergyDrinks} {SleepHours} {StudyHours}")));
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