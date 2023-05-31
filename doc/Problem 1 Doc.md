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
Design of problem 1 ...

## Test 
Test 

|  Test number | Purpose of test  | Test data  | Expected result  | Actual result  |  Comments |
|---|---|---|---|---|---|
| 1 |  Test if add task is working | Task = Get Milk | Adds Get Milk to task list  |   |   |
| 2 |  Test to see if Deleting tasks is working | Task = Get Milk  | Task is delted from task list  |   |   |
| 3 |  Test to see if can edit task |  Task = Get Milk | Task is changed to new input  | Task is not changed  | it allows you to go to change it in the message box but it dosent change the task.  |
| 4 |  Redo of Test 3 after fixes | Task = Get Milk  | Task is changed to new input  |  Task is changed to new input | Problem was fixed. had to add INotifyPropertyChanged to the task class to notify of any changes and update them. |
| 5 |  Test to see if the filters are working | One completed and one non completed task  | When clicking the button/checkbox markded Complted, only completed tasks will appear  |   |   |


## Evulaiton/Cosnlsuion ????