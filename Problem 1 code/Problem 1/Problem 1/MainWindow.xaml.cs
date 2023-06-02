using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
    public partial class MainWindow : Window
    {
        private ObservableCollection<Task> tasks;
        public MainWindow()
        {
            InitializeComponent();
            tasks = new ObservableCollection<Task>();
            listOfTasks.ItemsSource = tasks;
        }
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text;
            string description = txtDescription.Text;
            DateTime dueDate = dpDueDate.SelectedDate ?? DateTime.Now;

            if (!string.IsNullOrWhiteSpace(title))
            {
                tasks.Add(new Task { Title = title, Description = description, DueDate = dueDate });

                // Clear the input fields after adding the task
                txtTitle.Clear();
                txtDescription.Clear();
                dpDueDate.SelectedDate = null;
            }
        }

        private void EditTask_Click(object sender, RoutedEventArgs e)
        {
            // Check if a task is selected
            if (listOfTasks.SelectedItem is Task selectedTask)
            {
                // Retrieve the current values of the selected task
                string currentTitle = selectedTask.Title;
                string currentDescription = selectedTask.Description;
                DateTime currentDueDate = selectedTask.DueDate;

                // Call the ShowDialog method of the EditTaskDialog class to display a dialog box for editing the task.
                // Pass the current values as parameters.
                Task editedTask = EditTaskDialog.ShowDialog(currentTitle, currentDescription, currentDueDate);

                // If a valid task object is returned from the dialog, update the selected task with the edited values.
                if (editedTask != null)
                {
                    selectedTask.Title = editedTask.Title;
                    selectedTask.Description = editedTask.Description;
                    selectedTask.DueDate = editedTask.DueDate;
                }
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // Check if a task is selected
            if (listOfTasks.SelectedItem is Task selectedTask)
            {
                // Remove the selected task
                tasks.Remove(selectedTask);
            }
        }


        public static class EditTaskDialog
        {
            // This method displays a dialog box to edit the task title, description, and due date.
            // It takes the current values as input and returns a modified task object with the edited values.
            public static Task ShowDialog(string currentTitle, string currentDescription, DateTime currentDueDate)
            {
                // Create a new instance of the Task class with the current values
                Task editedTask = new Task
                {
                    Title = currentTitle,
                    Description = currentDescription,
                    DueDate = currentDueDate
                };

                // The InputBox method from the Microsoft.VisualBasic.Interaction class is used to display an input dialog box.
                // It prompts the user to enter new values and returns the entered values as a string array.
                string newTitle = Microsoft.VisualBasic.Interaction.InputBox("Edit Task", "Enter the new title:", currentTitle);
                if (!string.IsNullOrWhiteSpace(newTitle))
                {
                    editedTask.Title = newTitle;
                }

                // Prompt the user to enter a new description
                string newDescription = Microsoft.VisualBasic.Interaction.InputBox("Edit Task", "Enter the new description:", currentDescription);
                if (!string.IsNullOrWhiteSpace(newDescription))
                {
                    editedTask.Description = newDescription;
                }

                // Prompt the user to enter a new due date
                string newDueDate = Microsoft.VisualBasic.Interaction.InputBox("Edit Task", "Enter the new due date (MM/DD/YYYY):", currentDueDate.ToString("MM/dd/yyyy"));
                if (!string.IsNullOrWhiteSpace(newDueDate) && DateTime.TryParse(newDueDate, out DateTime parsedDueDate))
                {
                    editedTask.DueDate = parsedDueDate;
                }

                // Return the modified task object
                return editedTask;
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
            bool isTaskSelected = listOfTasks.SelectedItem is Task;
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
        private string description;
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private DateTime dueDate;
        public DateTime DueDate
        {
            get { return dueDate; }
            set
            {
                if (dueDate != value)
                {
                    dueDate = value;
                    OnPropertyChanged(nameof(DueDate));
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
