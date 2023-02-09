using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

namespace ToDoAplication;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    ObservableCollection<Task> tasksList = new ObservableCollection<Task>();
    public MainWindow()
    {
        InitializeComponent();

        ToDoListBox.ItemsSource = tasksList;
        ToDoListBox.DisplayMemberPath = "Name";
    }

    private void ToDoListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        Task selected = ToDoListBox.SelectedItem as Task;
        if(selected != null)
        {
            MessageBox.Show(selected.Description);
        }
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        NewTaskWindow window = new NewTaskWindow();
        window.Owner = this;
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        if (window.ShowDialog() == true)
        {
            Task newTask = window.Result;
            tasksList.Add(newTask);
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        Task selected = ToDoListBox.SelectedItem as Task;
        ObservableCollection<Task> filtred = new ObservableCollection<Task>();

        for (int i = 0; i < tasksList.Count; i++)
        {
            string name = selected.Name;

            if(selected != null && name == tasksList[i].Name)
            {
                tasksList.RemoveAt(i);
            }
        }
        ToDoListBox.ItemsSource = filtred;
    }

    private void CompleteButton_Click(object sender, RoutedEventArgs e)
    {
        int index = ToDoListBox.SelectedIndex;
        if (index != -1)
        {
            tasksList[index].IsCompleted = true;
        }
    }

    private void AllRadioButton_Checked_1(object sender, RoutedEventArgs e)
    {
        ToDoListBox.ItemsSource = tasksList;
    }

    private void NotCompletedRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        ObservableCollection<Task> filtered = new ObservableCollection<Task>();
        for (int i = 0; i < tasksList.Count; i++)
        {
            Task current = tasksList[i];
            if (current.IsCompleted == false)
            {
                filtered.Add(current);
            }
        }
        ToDoListBox.ItemsSource = filtered;
    }

    private void CompletedRadioButton_Checked(object sender, RoutedEventArgs e)
    {
        ObservableCollection<Task> filtered = new ObservableCollection<Task>();
        for (int i = 0; i < tasksList.Count; i++)
        {
            Task current = tasksList[i];
            if (current.IsCompleted == true)
            {
                filtered.Add(current);
            }
        }
        ToDoListBox.ItemsSource = filtered;
    }

    string fileName = "data.bin";

    private void Window_Closed(object sender, EventArgs e)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        Stream file = File.OpenWrite(fileName);
        formatter.Serialize(file, tasksList);
        file.Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        if(File.Exists(fileName))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            Stream file = File.OpenRead(fileName);
            tasksList = formatter.Deserialize(file) as ObservableCollection<Task>;
            file.Close();
            ToDoListBox.ItemsSource = tasksList;
        }
    }
}
