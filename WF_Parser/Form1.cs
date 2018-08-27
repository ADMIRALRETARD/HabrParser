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
using WF_Parser.Core;
using WF_Parser.Core.Habr;

namespace WF_Parser
{
    public partial class Form1 : Form
    {

        ParserWorker<string[]> parser;
        
      

        public Form1()
        {
            InitializeComponent();
            parser = new ParserWorker<string[]>(

                new HabrParser()
                );
            
            parser.OnCompleted += Parser_OnCompleted;
            parser.OnNewData += Parser_OnNewData;
        }

        private void Parser_OnNewData(object arg1 ,string[] arg2)
        {
            listTitles.Items.AddRange(arg2);
            
        }

        private void Parser_OnCompleted(object obj)
        {
            
        }

        private void buttonStart_Click(object sender ,EventArgs e)
        {
            parser.Settings = new HabrSettings((int)numericStart.Value ,(int)numericEnd.Value);
            parser.Start();
        }

        private void buttonStop_Click(object sender ,EventArgs e)
        {
            parser.Abort();
        }

        private void ButtonClear_Click(object sender ,EventArgs e)
        {
            listTitles.Items.Clear();
            progressBar1.Value = 0;
        }

        private void ButtonSave_Click(object sender ,EventArgs e)
        {
            TextWriter tw = new StreamWriter("C:\\Users\\Gercules\\Documents\\Visual Studio 2017\\Projects\\WF_Parser\\WF_Parser\\Logs\\Log.txt");
            foreach (var item in listTitles.Items) tw.WriteLine(item.ToString());

            

            tw.Close();
        }

        private void ButtonLoad_Click(object sender ,EventArgs e)
        {
            progressBar1.Value = 0;
            StreamReader sr=new StreamReader("C:\\Users\\Gercules\\Documents\\Visual Studio 2017\\Projects\\WF_Parser\\WF_Parser\\Logs\\Log.txt");
            while (!sr.EndOfStream)
            {
                progressBar1.Maximum = listTitles.Items.Add(sr.ReadLine());
                progressBar1.Increment(1);
            }
            sr.Close();
        }

        private void ButtonImage_Click(object sender ,EventArgs e)
        {

            
            
            pictureBox1.LoadAsync(listTitles.SelectedItem.ToString());
        }
    }
}
