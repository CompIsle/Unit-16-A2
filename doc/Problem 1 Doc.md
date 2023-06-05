# Problem 1
Problem 1: A Todo list.\
This Todo list implements at least: 
* Creation and deletion of tasks
* Tracking done state and allowing tasks to be set to complete
* Supporting title, description, due date, completed with description and due date being mutable (note also requirement to change completion status above)
* Displaying a list of tasks
* Toggling whether all tasks or only incomplete tasks are displayed
This program is primarily intended to demonstrate a GUI, alongside all of the other features listed above. The GUI will be implemented using WPF.

In this documents it will describe the designs, test plans and test results of problem 1.

## Design 
Application Overview:
The WPF application is a Todo list manager that allows users to create, delete, and edit tasks. It provides a graphical user interface (GUI) for managing tasks and supports features such as tracking the task's completion status, displaying task details, and filtering tasks based on completion status.

### UI Design
User Interface (UI):
The UI of the application is designed using XAML, which allows for creating a visually appealing and interactive interface. The main window consists of the following UI elements:

Title, Description, Due Date input fields: TextBlocks and TextBoxes for entering task details.
"Add Task" button: Allows users to add a new task with the provided details.
Task list: A ListBox to display the list of tasks.
Task item template: A DataTemplate used to define the visual representation of each task in the ListBox.
Checkboxes: Allows users to mark tasks as completed or incomplete.
"Delete" and "Edit" buttons: Allows users to delete or edit selected tasks.

### Code Design

## Test 
Test 

|  Test number | Purpose of test  | Test data  | Expected result  | Actual result  |  Comments |
|---|---|---|---|---|---|
| 1 |  Test if add task is working | Tittle = Get Milk / Description = Buy Milk / Due date = 2/03/2024 | Adds Get Milk to task list  | Its workings the task is added along with its description and duedate  |  Worked as expected |
| 2 |  Test to see if Deleting tasks is working | Tittle = Get Milk / Description = Buy Milk / Due date = 2/03/2024 | Task is delted from task list  | It worked and the task is delted from the task list |  Worked as expected |
| 3 |  Test to see if can edit task | Tittle = Get Milk / Description = Buy Milk / Due date = 2/03/2024 | Task is changed to new input  | Task is not changed  | it allows you to go to change it in the message box but it dosent change the task.  |
| 4 |  Redo of Test 3 after fixes |  Tittle = Get Milk / Description = Buy Milk / Due date = 2/03/2024 | Task is changed to new input  |  Task is changed to new input | Problem was fixed. had to add INotifyPropertyChanged to the task class to notify of any changes and update them. |
| 5 |  Test to see if the filters are working | One completed and one non completed task  | When clicking the button/checkbox markded Complted, only completed tasks will appear  |  |   |


## Evulaiton/Cosnlsuion ????
