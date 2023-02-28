using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using WinForms = System.Windows.Forms;

namespace TxtFilesStatistic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string fileName = "Result.txt";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowClick(object sender, RoutedEventArgs e)
        {
            var dialog = new WinForms.FolderBrowserDialog();
            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                var files = Directory.GetFiles(dialog.SelectedPath, "*.txt");
                var taskList = new List<Task>();
                var reportList = new List<Report>();
                foreach (var file in files)
                {
                    var task = new Task(() =>
                    {
                        Report report = CalculateReport(file);
                        reportList.Add(report);
                    });
                    taskList.Add(task);
                    task.Start();
                }
                Task.WaitAll(taskList.ToArray());

                reportList.Sort((r1, r2) => String.Compare(r1.fileName, r2.fileName));

                StringBuilder sb = new StringBuilder();

                Report summary = new Report();
                summary.fileName = "All files";
                foreach (var report in reportList)
                {
                    sb.AppendLine(ReportToString(report));
                    summary.numberOfWords += report.numberOfWords;
                    summary.numberOfLines += report.numberOfLines;
                    summary.numberOfSymbols += report.numberOfSymbols;
                }
                sb.AppendLine(ReportToString(summary));
                sb.AppendLine(GetStringWithTopWords(files));

                var window = new Statistic();
                window.Show();
                window.tb.Text = sb.ToString();
            }
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            var dialog = new WinForms.FolderBrowserDialog();
            if (dialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                var files = Directory.GetFiles(dialog.SelectedPath, "*.txt");
                var taskList = new List<Task>();
                var reportList = new List<Report>();
                foreach (var file in files)
                {
                    var task = new Task(() =>
                    {
                        Report report = CalculateReport(file);
                        reportList.Add(report);
                    });
                    taskList.Add(task);
                    task.Start();
                }
                Task.WaitAll(taskList.ToArray());

                reportList.Sort((r1, r2) => String.Compare(r1.fileName, r2.fileName));
                using (StreamWriter sw = new StreamWriter(dialog.SelectedPath + "\\" + fileName, true))
                {
                    Report summary = new Report();
                    summary.fileName = "All files";

                    foreach (var report in reportList)
                    {
                        sw.WriteLine(ReportToString(report));
                        summary.numberOfWords += report.numberOfWords;
                        summary.numberOfLines += report.numberOfLines;
                        summary.numberOfSymbols += report.numberOfSymbols;
                    }

                    sw.WriteLine(ReportToString(summary));

                    var str = GetStringWithTopWords(files);
                    sw.WriteLine(str);
                }
            }
        }

        private string GetStringWithTopWords(string[] files)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var file in files)
            {
                sb.AppendLine(File.ReadAllText(file));
            }

            var list = CalculateTopOfWords(sb.ToString());
            int number = 10;
            if (list.Count < 10) number = list.Count;

            sb.Clear();
            sb.AppendLine("Top of the words:");
            for (int i = 0; i < number; i++)
            {
                sb.AppendLine($"{list[i].Key} - {list[i].Value}");
            }

            return sb.ToString();
        }

        private string ReportToString(Report report)
        {
            return $"{report.fileName}{Environment.NewLine}" +
                $"Number of lines: {report.numberOfLines}{Environment.NewLine}" +
                $"Number of words: {report.numberOfWords}{Environment.NewLine}" +
                $"Number of symbols: {report.numberOfSymbols}{Environment.NewLine}" +
                $"{Environment.NewLine}";
        }

        private Report CalculateReport(string filePath)
        {
            string text;
            using (StreamReader sr = new StreamReader(filePath))
            {
                text = sr.ReadToEnd();
            }
            var report = new Report
            {
                fileName = System.IO.Path.GetFileName(filePath),
                numberOfLines = Regex.Split(text, "\r\n|\r|\n").Length,
                numberOfWords = text.Split("(!?.,;«»\"'—) ".ToArray(), StringSplitOptions.RemoveEmptyEntries).Length,
                numberOfSymbols = text.Count((c) =>"(!?.,;«»\"')".Contains(c))
            };
            return report;
        }

        private List<KeyValuePair<string,int>> CalculateTopOfWords(string text)
        {
            var arr = text.Split("(!?.,;«»\"'—) \n\r".ToArray(), StringSplitOptions.RemoveEmptyEntries);
            var dictionary = new Dictionary<string, int>();

            foreach (var item in arr)
            {
                if (dictionary.ContainsKey(item))
                {
                    dictionary[item]++;
                }
                else
                {
                    dictionary.Add(item, 1);
                }
            }

            var list = dictionary.ToList();
            list.Sort((p1,p2) => p2.Value.CompareTo(p1.Value));
            return list;
        }
    }
}
