using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GRAF
{
    public partial class Form1 : Form
    {
        private string _path;
        BindingList<GrafikTemperatur> grafik;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadGraph()
        {

            SeriesCollection series = new SeriesCollection();
            ChartValues<int> temp = new ChartValues<int>();
            List<string> months = new List<string>();

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                months.Add(dataGridView1.Rows[i].Cells[0].Value.ToString());
                temp.Add(Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value));                
            }

            cartesianChart1.AxisX.Clear();

            cartesianChart1.AxisX.Add(
                new Axis()
                {
                    Title = "Месяц",
                    Labels = months
                });

            LineSeries tL = new LineSeries();
            tL.Title = "Температура";
            tL.Values = temp;

            series.Add(tL);
            cartesianChart1.Series = series;
        }
                

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            _path =  openFileDialog1.FileName;
            LoadData();
            LoadGraph();
        }

        private void LoadData()
        {
            grafik = new BindingList<GrafikTemperatur>();
            var result = JsonConvert.DeserializeObject<BindingList<GrafikTemperatur>>(File.ReadAllText(_path));
            grafik = result;
            dataGridView1.DataSource = grafik;            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (grafik == null)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }
                // путь к сохраняемому json
                _path = saveFileDialog1.FileName;
                // добавить в файл пустой список с одним обьектом User в формате .json
                File.WriteAllText(_path, JsonConvert.SerializeObject(new BindingList<GrafikTemperatur>()));
                MessageBox.Show("Файл сохранен");
                // загрузить данные из файла
                LoadData();
                // добавить пустой обьект User
                grafik.Add(new GrafikTemperatur("", 0));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            File.WriteAllText(_path, JsonConvert.SerializeObject(grafik));
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int cursor = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows.Remove(dataGridView1.Rows[cursor]);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            LoadGraph();
        }
    }
}
