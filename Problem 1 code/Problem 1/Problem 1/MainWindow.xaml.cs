using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Problem_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Task> tasks;
        public MainWindow()
        {
            InitializeComponent();
            tasks = new ObservableCollection<Task>();
            lstTasks.ItemsSource = tasks;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            if (!string.IsNullOrWhiteSpace(title))
            {
                tasks.Add(new Task { Title = title });
                txtTitle.Clear();
            }
        }

        /*private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Task task = (Task)button.Tag;
            string newTitle = EditTaskDialog.ShowDialog(task.Title);
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                task.Title = newTitle;
            }
        }*/

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Task task = (Task)button.Tag;
            tasks.Remove(task);
        }

        /*public static class EditTaskDialog
        {
            public static string ShowDialog(string currentTitle)
            {
                // In a real application, you would display a dialog box or a separate window to edit the task title.
                // For simplicity, we'll use a simple input dialog using MessageBox in this example.
                string newTitle = Microsoft.VisualBasic.Interaction.InputBox("Edit Task", "Enter the new title:", currentTitle);
                return newTitle;
            }
        }*/

        /*private void Filter_Checked(object sender, RoutedEventArgs e)
        {
            if (radAllTasks.IsChecked == true)
            {
                lstTasks.ItemsSource = tasks;
            }
            else if (radIncompleteTasks.IsChecked == true)
            {
            var incompleteTasks = tasks.Where(t => !t.Completed).ToList();
                lstTasks.ItemsSource = incompleteTasks;
            }
        }*/

        private void lstTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Clear the selection
            lstTasks.SelectedIndex = -1;
        }


    }
    public class Task
    {
        public string Title { get; set; }
        public bool Completed { get; set; }
    }

}
