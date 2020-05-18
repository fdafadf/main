using AI.NeuralNetworks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Labs.Agents.Forms
{
    public partial class ChartForm : Form
    {
        TabControl TabControl;
        Chart Chart;

        public ChartForm()
        {
            InitializeComponent();
            InitializeChart();
        }

        public ChartForm(string title)
        {
            InitializeComponent();
            InitializeChart();
            Text = title;
        }

        public ChartForm(string title, IEnumerable<SimulationResults> results)
        {
            InitializeComponent();
            Text = title;
            TabControl = new TabControl();
            TabControl.SuspendLayout();
            TabControl.Location = new Point(529, 234);
            TabControl.Name = "tabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(200, 100);
            TabControl.TabIndex = 1;
            TabControl.Dock = DockStyle.Fill;

            foreach (var resultsItem in results)
            {
                INamed namedItem = resultsItem;

                foreach (var serie in resultsItem.Series)
                {
                    EnsureChart(EnsureTabPage(serie.Key)).AddSeries(namedItem.Name, serie.Value);
                }
            }

            Controls.Add(TabControl);
            TabControl.ResumeLayout(false);
        }

        private void InitializeChart()
        {
            Chart = new Chart();
            ((ISupportInitialize)Chart).BeginInit();
            Chart.Location = new Point(21, 12);
            Chart.Name = "chart";
            Chart.Size = new Size(577, 293);
            Chart.TabIndex = 0;
            Chart.Text = "chart1";
            Controls.Add(Chart);
            ((ISupportInitialize)Chart).EndInit();
        }

        private TabPage EnsureTabPage(string name)
        {
            if (TabControl.TabPages.ContainsKey(name) == false)
            {
                TabPage tabPage = new TabPage();
                tabPage.Location = new Point(4, 22);
                tabPage.Name = name;
                tabPage.Padding = new Padding(3);
                tabPage.Size = new Size(192, 74);
                tabPage.TabIndex = 0;
                tabPage.Text = name;
                tabPage.UseVisualStyleBackColor = true;
                TabControl.TabPages.Add(tabPage);
            }

            return TabControl.TabPages[name];
        }

        private Chart EnsureChart(Control control)
        {
            if (control.HasChildren == false)
            {
                ChartArea chartArea = new ChartArea();
                Legend legend = new Legend();
                Chart chart = new Chart();
                ((ISupportInitialize)chart).BeginInit();
                chartArea.Name = "ChartArea";
                chart.ChartAreas.Add(chartArea);
                legend.Name = "Legend1";
                chart.Legends.Add(legend);
                chart.Location = new Point(227, 12);
                chart.Name = "chart1";
                chart.Size = new Size(300, 300);
                chart.TabIndex = 0;
                chart.Text = "chart1";
                chart.Dock = DockStyle.Fill;
                control.Controls.Add(chart);
                ((ISupportInitialize)chart).EndInit();
            }

            return control.Controls[0] as Chart;
        }

        public void Add(Optimizer optimizer, TrainingMonitorCollection monitors2)
        {
            var monitors = monitors2.Items.Where(m => m.CollectedData.Any()).GetEnumerator();

            if (monitors.MoveNext())
            {
                var series = Add($"{monitors.Current} - {optimizer}", monitors.Current.CollectedData);
                Chart.ApplyPaletteColors();

                while (monitors.MoveNext())
                {
                    Add($"{monitors.Current} - {optimizer}", monitors.Current.CollectedData, series.Color);
                }
            }
        }

        public Series Add(string name, IEnumerable<double> values, Color? color = null)
        {
            return Chart.AddSeries(name, values);
        }

        public void Add<T>(Task<T>[] tasks) where T : Serie
        {
            Task.WaitAll(tasks);
            Add(tasks.Select(task => task.Result));
        }

        public void Add(IEnumerable<Serie> results)
        {
            foreach (var result in results)
            {
                Add(result);
            }
        }

        public void Add(Serie result)
        {
            Add(result.Name, result.Errors);
        }
    }

    public class Serie
    {
        public string Name;
        public IEnumerable<double> Errors;

        public Serie(string name, IEnumerable<double> errors)
        {
            Name = name;
            Errors = errors;
        }
    }
}
