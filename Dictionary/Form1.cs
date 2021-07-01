using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace Dictionary
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable tableWord = new DataTable();
        DataTable tableDefinition = new DataTable();

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        void Load_Data()
        {
            tableWord.Columns.Add("word", typeof(string));
            StreamReader sr = new StreamReader("D:\\Dictionary\\data.txt");
            string str;
            string word = null;
            string definition = null;
            string type = null;
            string preWord = null;
            tableDefinition.Columns.Add("word", typeof(string));
            tableDefinition.Columns.Add("type", typeof(string));
            tableDefinition.Columns.Add("definition", typeof(string));
            while ((str = sr.ReadLine()) != null)
            {
                //string[] st = str.Split('\n');

                if (str == "")
                {
                    if (string.IsNullOrWhiteSpace(definition))
                    {
                        tableDefinition.Rows.Add(word, type, definition);
                    }
                    preWord = word;
                    word = null;
                } else
                {
                    if (word == null)
                    {
                        int startTypeIndex = str.IndexOf(" (");
                        int endTypeIndex = str.IndexOf(") ");
                        word = str.Substring(0, startTypeIndex);
                        if (word.StartsWith("  "))
                        {
                            word = word.Replace("  ", "").ToLower();
                        }
                        definition = str.Substring(endTypeIndex + 2);
                        string types = str.Substring(startTypeIndex + 2, endTypeIndex - startTypeIndex - 2);

                        if (preWord != word)
                        {
                            tableWord.Rows.Add(word);
                        }
                    } else
                    {
                        if (str.StartsWith("  "))
                        {
                            definition += str.Replace("  ", "");
                        } else
                        {
                            definition += str;
                        }
                    }
                }
            }
            sr.Close();
        }
    }
}
