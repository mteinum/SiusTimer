using System.Diagnostics;
using System.IO.Ports;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;
using System.Runtime.InteropServices;
using SiusTimer.Model;

namespace SiusTimer
{
    public partial class MainForm : Form
    {
        SerialPort serialPort;
        SiusData siusData;
        List<Competition> Programs;

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async Task Tcu25PressButton(SerialPort port, CancellationToken token)
        {
            port.Write("on");
            await Task.Delay(1000).ConfigureAwait(false);
            port.Write("off");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // try find com port
            var ports = PortInfoLoader.GetPortInfo();
            var selected = ports.GetArduinoPort();

            if (selected != null)
            {
                toolStripStatusLabelArduino.Text = selected.Description;

                // initialize COM port
                serialPort = new SerialPort(selected.Name, 9600);

                serialPort.Open();
            }
            else
            {
                toolStripStatusLabelArduino.Text = "Arduino not connected";
            }

            // try find sius
            siusData = new SiusData();

            if (siusData.Initialize())
            {
                toolStripStatusLabelSiusData.Text = siusData.MainWindowTitle;
            }
            else
            {
                toolStripStatusLabelSiusData.Text = "SiusData not running";
            }

            Programs = new List<Competition>();

            // load programs
            foreach (var x in Directory.GetFiles("Programs", "*.xml"))
            {
                Programs.Add(CompetitionLoader.Load(x));
            }

            var toMap = Programs.Select(Value => new { Value.Name, Value }).ToList();

            comboBoxPrograms.DataSource = toMap;
            comboBoxPrograms.DisplayMember = "Name";
            comboBoxPrograms.ValueMember = "Value";
            comboBoxPrograms.SelectedIndex = 0;
        }

        private void testTCU25ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tcu25PressButton(serialPort, CancellationToken.None);
        }

        private void buttonProgramLoad_Click(object sender, EventArgs e)
        {
            listViewProgram.Items.Clear();

            var program = comboBoxPrograms.SelectedValue as Competition;

            foreach (var group in program.Groups)
            {
                var id = group.Id;

                foreach (var step in group.Steps)
                {
                    var lvi = new ListViewItem(
                        new string[]
                        {
                            id,
                            step.Sound,
                            step.DelaySeconds,
                            step.Light,
                            step.Timer,
                            group.Repeat.ToString()
                        }
                    )
                    {
                        Tag = step
                    };

                    listViewProgram.Items.Add(lvi);
                }
            }

            if (listViewProgram.Items.Count > 0)
            {
                listViewProgram.Items[0].Selected = true;
            }
        }

        private void listViewProgram_DoubleClick(object sender, EventArgs e)
        {
            Run();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            Run();
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            // find selected, toggle with next
            for (int i = 0; i < listViewProgram.Items.Count - 1; i++)
            {
                if (listViewProgram.Items[i].Selected)
                {
                    listViewProgram.Items[i].Selected = false;
                    listViewProgram.Items[i + 1].Selected = true;
                    break;
                }
            }

            Run();
        }

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        Task runningTask;

        private void StopRunningTask()
        {
            if (runningTask != null)
            {
                tokenSource.Cancel();

                try
                {
                    runningTask.Wait();
                }
                catch (System.AggregateException) { }
            }
        }

        private void Run()
        {
            StopRunningTask();

            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            foreach (ListViewItem lvi in listViewProgram.Items)
            {
                if (lvi.Selected)
                {
                    runningTask = Task.Run(() => Run((Step)lvi.Tag, token), token);
                }
            }
        }

        private void ProgramDone()
        {
            // check if the next program is in the same group, then run that
            for (int i = 0; i < listViewProgram.Items.Count - 1; i++)
            {
                if (listViewProgram.Items[i].Selected)
                {
                    var group = listViewProgram.Items[i].Text;

                    if (listViewProgram.Items[i + 1].Text == group)
                    {
                        listViewProgram.Items[i].Selected = false;
                        listViewProgram.Items[i + 1].Selected = true;

                        Run();
                    }

                    break;
                }
            }
        }

        private void SetLabelTimer(TimeSpan ts, CancellationToken token)
        {
            // don't deadlock main thread
            if (token.IsCancellationRequested)
                return;

            labelTimer.Invoke(() =>
            {
                labelTimer.Text = ts.ToString();
            });
        }

        private async Task Run(Step step, CancellationToken token)
        {
            try
            {
                SetLabelTimer(TimeSpan.FromSeconds(0), token);

                var resourceName = $"SiusTimer.Audio.en.{step.Sound}.wav";

                using (
                    var stream = typeof(MainForm).Assembly.GetManifestResourceStream(resourceName)
                )
                {
                    var soundPlayer = new SoundPlayer(stream);
                    soundPlayer.PlaySync();
                }

                if (step.Timer != null)
                {
                    siusData.Start();
                }

                if (step.Light != null)
                {
                    await Tcu25PressButton(serialPort, token).ConfigureAwait(false);
                }

                if (step.DelaySeconds != null && int.TryParse(step.DelaySeconds, out var seconds))
                {
                    do
                    {
                        SetLabelTimer(TimeSpan.FromSeconds(seconds), token);

                        await Task.Delay(TimeSpan.FromSeconds(1), token).ConfigureAwait(false);

                        seconds--;
                    } while (seconds >= 0 && !token.IsCancellationRequested);

                    BeginInvoke(new MethodInvoker(ProgramDone));
                }
            }
            catch (TaskCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
