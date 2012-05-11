using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace Hanssens.Applications.HandleThis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // do this!
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties["Filename"] != null)
            {
                InspectFile(Application.Current.Properties["Filename"].ToString());
            }
        }

        protected void InspectFile(string fileName)
        {
            if (!File.Exists(fileName))
                MessageBox.Show("File does not exist:\n" + fileName, "File does not exist", MessageBoxButton.OK, MessageBoxImage.Error);

            // Initialize the handle.exe process
            var tool = new Process();
            tool.StartInfo.FileName = @".\Assets\handle.exe";
            tool.StartInfo.Arguments = fileName;
            tool.StartInfo.UseShellExecute = false;
            tool.StartInfo.RedirectStandardOutput = true;

            // Ensure no console window appears
            tool.StartInfo.CreateNoWindow = true;
            tool.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            // Execute, and wait for results
            tool.Start();
            tool.WaitForExit();
            var fullOutputMessage = tool.StandardOutput.ReadToEnd();

            // Split the line endings and filter out the actual result
            var result = fullOutputMessage
                .Trim()         // Trim all line breaks and spaces
                .Split('\r')    // Split by hard returns
                .LastOrDefault()// Fetch the last argument
                .Trim();        // Again, trim all line breaks and spaces from the result

            //MessageBox.Show(result);
            HandleResultText.Text = result;
        }

        private void InspectFileCommand_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(FileToInspectText.Text))
                InspectFile(FileToInspectText.Text);
        }
    }
}
