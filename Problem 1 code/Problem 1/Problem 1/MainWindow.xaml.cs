using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            // Retrieve the clicked button object from the sender argument.
            Button button = (Button)sender;
            // Get the associated Task object from the Tag property of the button.
            Task task = (Task)button.Tag;
            // Call the ShowDialog method of the EditTaskDialog class to display a dialog box for editing the task's title.
            // Pass the current title of the task as a parameter.
            string newTitle = EditTaskDialog.ShowDialog(task.Title);
            // If a non-empty string is returned from the dialog, update the task's title with the new value.
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                task.Title = newTitle;
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Task task = (Task)button.Tag;
            tasks.Remove(task);
        }*/

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            // Check if a task is selected
            if (lstTasks.SelectedItem is Task selectedTask)
            {
                // Retrieve the current title of the selected task
                string currentTitle = selectedTask.Title;

                // Call the ShowDialog method of the EditTaskDialog class to display a dialog box for editing the task's title.
                // Pass the current title of the task as a parameter.
                string newTitle = EditTaskDialog.ShowDialog(currentTitle);

                // If a non-empty string is returned from the dialog, update the task's title with the new value.
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    selectedTask.Title = newTitle;
                }
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Check if a task is selected
            if (lstTasks.SelectedItem is Task selectedTask)
            {
                // Remove the selected task from the ObservableCollection
                tasks.Remove(selectedTask);
            }
        }

        public static class EditTaskDialog
        {
            // This method displays a dialog box to edit the task title.
            // It takes the currentTitle as input and returns the new title entered by the user.
            public static string ShowDialog(string currentTitle)
            {
                // In a real application, you would display a dialog box or a separate window to edit the task title.
                // For simplicity, we'll use a simple input dialog using MessageBox in this example.
                //The InputBox method from the Microsoft.VisualBasic.Interaction class is used to display an input dialog box.
                // It prompts the user to enter a new title and returns the entered value as a string.
                string newTitle = Microsoft.VisualBasic.Interaction.InputBox("Edit Task", "Enter the new title:", currentTitle);
                return newTitle;
            }
        }

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
            // Enable or disable the edit and delete buttons based on task selection
            bool isTaskSelected = lstTasks.SelectedItem is Task;
            btnEdit.IsEnabled = isTaskSelected;
            btnDelete.IsEnabled = isTaskSelected;
        }


    }
    public class Task : INotifyPropertyChanged
    {
        // The Title property represents the title of the task.
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                // Check if the new value is different from the current value.
                if (title != value)
                {
                    title = value;
                    // Raise the PropertyChanged event to notify subscribers that the Title property has changed.
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        private bool completed;
        public bool Completed
        {
            get { return completed; }
            set
            {
                if (completed != value)
                {
                    completed = value;
                    OnPropertyChanged(nameof(Completed));
                }
            }
        }

        // The PropertyChanged event is used to notify subscribers (such as the UI) when a property value changes.
        public event PropertyChangedEventHandler PropertyChanged;

        // This method raises the PropertyChanged event.
        // It takes the name of the property that has changed as an argument.
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
