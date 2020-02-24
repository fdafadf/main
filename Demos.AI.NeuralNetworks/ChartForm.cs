using AI.NeuralNetworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Demos.AI.NeuralNetwork
{
    public partial class ChartForm : Form
    {
        public ChartForm()
        {
            InitializeComponent();
        }

        public ChartForm(string title)
        {
            InitializeComponent();
            this.Text = title;
        }

        public void Add(Optimizer optimizer, TrainingMonitorCollection monitors2)
        {
            var monitors = monitors2.Items.Where(m => m.CollectedData.Any()).GetEnumerator();

            if (monitors.MoveNext())
            {
                var series = Add($"{monitors.Current} - {optimizer}", monitors.Current.CollectedData);
                chart1.ApplyPaletteColors();

                while (monitors.MoveNext())
                {
                    Add($"{monitors.Current} - {optimizer}", monitors.Current.CollectedData, series.Color);
                }
            }
        }

        public Series Add(string name, IEnumerable<double> values, Color? color = null)
        {
            string baseName = name;
            int i = 0;

            while (chart1.Series.Any(s => s.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase)))
            {
                name = $"{++i} {baseName}"; 
            }

            Series series = chart1.Series.Add(name);
            series.ChartType = SeriesChartType.Line;

            if (color.HasValue)
            {
                series.Color = color.Value;
            }

            foreach (double value in values)
            {
                series.Points.Add(value); // > 2 ? 2 : value);
            }

            return series;
        }
    }
}
