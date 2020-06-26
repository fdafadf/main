using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Labs.AI
{
    class ChartForm : Form
    {
        Chart chart;

        public ChartForm()
        {
            Width = 800;
            Height = 480;
            ChartArea chartArea = new ChartArea();
            Legend legend = new Legend();
            chart = new Chart();
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
            Controls.Add(chart);
            ((ISupportInitialize)chart).EndInit();
        }

        public Series NewSeries(string name)
        {
            Series series = chart.Series.Add(name);
            series.ChartType = SeriesChartType.Line;
            return series;
        }

        //public void Print(double value)
        //{
        //    series.Points.Add(value);
        //}
    }
}
