using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileNumUpDown
{
    public partial class Form1 : Form
    {
        private int CountNums;
        private List<int> Nums = new List<int>();
        private string Path;
        private List<NumericUpDown> Nuds = new List<NumericUpDown>();
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            Nums.Clear();
            Nuds.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            Path = ofd.FileName;
            FillNums(Path);
            int y = 50;
            foreach (var i in Nums)
            {
                NumericUpDown nud = new NumericUpDown();
                nud.Location = new Point(10, y);
                nud.Width = 50;
                nud.Height = 50;
                Height += 50;
                y += 50;
                nud.Value = i;
                Nuds.Add(nud);
                Controls.Add(nud);
                nud.ValueChanged += Nud_ValueChanged;
                lblMin.Text = Nums.Min().ToString();
                lblMax.Text = Nums.Max().ToString();
                double avg = Convert.ToInt32(Nums.Sum()) / Nums.Count;
                lblMed.Text = avg.ToString();
            }
            lblFilename.Text = Path;
        }

        private void Nud_ValueChanged(object sender, EventArgs e)
        {
            Nums.Clear();
            foreach (var i in Nuds)
            {
                Nums.Add(Convert.ToInt32(i.Value));
            }
            lblMin.Text = Nums.Min().ToString();
            lblMax.Text = Nums.Max().ToString();
            double avg = Convert.ToInt32(Nums.Sum()) / Nums.Count;
            lblMed.Text = avg.ToString();
        }

        private void FillNums(string path)
        {
            var nums = File.ReadAllLines(path);
            CountNums = Convert.ToInt32(nums[0]);
            for (int i = 1; i < nums.Length; i++)
            {
                Nums.Add(Convert.ToInt32(nums[i]));
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            File.Delete(Path);
            using (StreamWriter sr = new StreamWriter(Path))
            {
                sr.WriteLine(Nums.Count);
                foreach (var i in Nuds)
                {
                    sr.WriteLine(i.Value);
                }
            }
            MessageBox.Show("Сохранено");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
