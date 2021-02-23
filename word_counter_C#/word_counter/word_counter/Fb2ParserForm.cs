using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace word_counter
{
    public partial class Fb2ParserForm : Form
    {
        Dictionary<String, Int32> wordsFrequency = new Dictionary<string, int>();

        Regex regex = new Regex(@"[^ ,.\/\\><:;\-0-9\[\]\(\)=" + "?\"!{ }]+");

        string lastOutputFile = string.Empty;

        XmlNamespaceManager namespaceManager = new XmlNamespaceManager(new NameTable());


        public Fb2ParserForm()
        {
            namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.1");
            InitializeComponent();
        }

        private void FileSelectButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "fb2 files (*.fb2)|*.fb2";
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Fb2FilePath.Text = dialog.FileName;
                }
            }
        }

        private void ChooseOutputFolderButton_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.RootFolder = Environment.SpecialFolder.MyDocuments;

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    OutputToTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private void ProceedParsingButton_Click(object sender, EventArgs e)
        {

        }


        private void OpenResultFile_Click(object sender, EventArgs e)
        {
            if (lastOutputFile.Length != 0)
            {
                Process.Start("notepad.exe", $@"{lastOutputFile}");
            }
        }

        private void ParsieFile(object sender, EventArgs e)
        {
            // Doing procedural solution for file that already downloaded (fb2 format)

            if (wordsFrequency.Keys.Count != 0)
            {
                wordsFrequency.Clear();
            }

            if (Fb2FilePath.Text.Length != 0 && OutputToTextBox.Text.Length != 0)
            {
                var fileToRead = $@"{Fb2FilePath.Text}";
                var fileToOut = $@"{OutputToTextBox.Text}\analayzed_{fileToRead.Split("\\").Last().Split(".")[0]}.txt";

                XDocument doc = XDocument.Load(fileToRead);
                var body = doc.Root.XPathSelectElements("fb:body/fb:section/fb:p", namespaceManager).ToList();

                body.ForEach(delegate (XElement name)
                {
                    MatchCollection matches = regex.Matches(name.Value);

                    foreach (Match match in matches)
                    {
                        var matchAsString = match.ToString().ToLower();

                        if (wordsFrequency.ContainsKey(matchAsString))
                        {
                            wordsFrequency[matchAsString] += 1;
                        }
                        else
                        {
                            wordsFrequency[matchAsString] = 1;
                        }
                    }
                });

                var sortedWordsFrequency = from item in wordsFrequency orderby item.Value descending select item;

                using (StreamWriter outputFile = new StreamWriter(fileToOut))
                {
                    foreach (var item in sortedWordsFrequency)
                    {
                        var key = item.Key;
                        var value = item.Value;
                        outputFile.WriteLine($"{key} : {value}");
                    }
                }
                lastOutputFile = fileToOut;
            }
        }
    }
}
