using System.Diagnostics;
using System.IO.Ports;
using System.Media;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Linq;
using System.Runtime.InteropServices;
using SiusTimer.Model;
using SiusTimer.Audio;
using SiusTimer.IO;

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
                listViewProgram.Items.Add(
                    new ListViewItem(
                        new string[]
                        {
                            group.Id,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            string.Empty,
                            group.Repeat.ToString(),
                            "0"
                        }
                    )
                    {
                        Tag = group,
                        BackColor = Color.LightYellow
                    }
                );

                foreach (var step in group.Steps)
                {
                    listViewProgram.Items.Add(
                        new ListViewItem(
                            new string[]
                            {
                                string.Empty,
                                step.Sound,
                                step.Wait?.ToString(),
                                step.Light,
                                step.Timer?.ToString(),
                                string.Empty,
                                string.Empty,
                            }
                        )
                        {
                            Tag = step
                        }
                    );
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
            ProgramDone(GetSelectedListViewItem());
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

        private void ProgramDone(ListViewItem lvi)
        {
            var next = lvi.GetNextListViewItem();

            if (next != null && next.IsStep())
            {
                next.SetSelected();
                Run();
            }
            else
            {
                var nextGroup = next;
                // group
                // is the current group complete?
                var currentGroup = lvi.GetGroupForStep();

                // check repeat
                var repeat = int.Parse(currentGroup.SubItems[5].Text);
                var times = int.Parse(currentGroup.SubItems[6].Text) + 1;

                // update group
                currentGroup.SubItems[6].Text = times.ToString();

                if (times < repeat)
                {
                    currentGroup.SetSelected();
                    Run();
                }
                else if (nextGroup != null)
                {
                    nextGroup.SetSelected();
                }
                // else repeat == 0, manual change light
            }
        }

        private ListViewItem GetSelectedListViewItem()
        {
            foreach (ListViewItem lvi in listViewProgram.Items)
            {
                if (lvi.Selected)
                {
                    return lvi;
                }
            }
            return null;
        }

        private void Run()
        {
            StopRunningTask();

            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            var lvi = GetSelectedListViewItem();

            if (lvi == null)
                return;

            if (lvi.IsGroup())
            {
                lvi = lvi.FirstStepInGroup();
            }

            lvi.SetSelected();

            runningTask = Task.Run(() => RunStep(lvi, token), token);
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

        private async Task RunStep(ListViewItem lvi, CancellationToken token)
        {
            var step = lvi.Step();

            TimeSpan stepDuration = checkBoxFast.Checked
                ? TimeSpan.FromMilliseconds(100)
                : TimeSpan.FromSeconds(1);

            try
            {
                SetLabelTimer(TimeSpan.FromSeconds(0), token);

                Speaker.Annonce(step.Sound);

                if (step.Timer.HasValue)
                {
                    siusData.Reset();
                    siusData.Start();
                }

                if (step.Light != null)
                {
                    await Tcu25PressButton(serialPort, token).ConfigureAwait(false);
                }

                if (step.Wait.HasValue)
                {
                    int seconds = (int)step.Wait.Value.TotalSeconds;

                    do
                    {
                        SetLabelTimer(TimeSpan.FromSeconds(seconds), token);

                        await Task.Delay(stepDuration, token).ConfigureAwait(false);

                        seconds--;
                    } while (seconds >= 0 && !token.IsCancellationRequested);

                    BeginInvoke(() => ProgramDone(lvi));
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

        private void testSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            siusData.Set(TimeSpan.FromMinutes(1));
        }

        private void testResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            siusData.Reset();
        }

        private void testStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            siusData.Start();
        }
    }
}
