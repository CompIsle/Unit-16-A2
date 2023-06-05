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
MainWindow.xaml.cs: This file contains the code-behind logic for the main window and handles the UI interactions.
   - It initializes the task list as an `ObservableCollection<Task>` to store the tasks.
   - The "Add Task" button click event handler retrieves the task details from the input fields, creates a new `Task` object, and adds it to the `taskList` collection.
   - The "Delete Task" button click event handler removes the selected task from the `taskList` collection.
   - The "Edit Task" button click event handler opens a dialog window (`EditTaskDialog`) to edit the selected task.

Task.cs: This class represents a single task.
   - It implements the `INotifyPropertyChanged` interface to notify changes to UI elements.
   - The `Task` class has properties such as `Title`, `Description`, `DueDate`, and `Completed`, which can be accessed and modified.
   - Each property includes the `OnPropertyChanged` method, which raises the `PropertyChanged` event to notify the UI of property changes.

This design separates the initialization of the task list, provides clearer property change handling, and keeps the event handling code in the code-behind file. It aims to improve code organization, readability, and maintainability.

### Flowchart:
Below is a flowchart illustrating the flow of the application:

```mermaid
graph LR
    A[Start] --> B{User Interactions}
    B -- Add Task --> C(Create New Task)
    C --> D(Update Task List)
    D --> B
    B -- Delete Task --> E(Delete Selected Task)
    E --> D
    B -- Edit Task --> F(Edit Selected Task)
    F --> D
    D -- Display Task List --> B
```

The flowchart starts from the "Start" node and loops back to the "User Interactions" node until the user decides to exit the application. The user can perform various actions such as adding a new task, deleting a task, or editing a task, which will result in updating the task list display accordingly.

This flowchart provides a visual representation of the main flow of the application, highlighting the interactions between the user and the system.

With this design documentation, you have a detailed overview of the application's design, including the UI elements, code structure, and flow of the application.

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

### Implementation of AddTask_Click:
The code correctly handles the click event of the "Add Task" button.
It retrieves the task details from the input fields and creates a new Task object.
The new task is added to the ObservableCollection, and the input fields are cleared.
The implementation meets the requirement of creating and adding tasks.

### Implementation of DeleteTask_Click:
The code handles the click event of the "Delete" button.
It removes the selected task from the ObservableCollection.
The implementation meets the requirement of deleting tasks.

### Implementation of EditTask_Click:

### Task class
The Task class represents a single task and implements the INotifyPropertyChanged interface.
It includes properties for Title, Description, DueDate, and Completed.
