using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ToDoAplication;

/// <summary>
/// Логика взаимодействия для NewTaskWindow.xaml
/// </summary>
public partial class NewTaskWindow : Window
{
    public Task Result { get; set; }

    public NewTaskWindow()
    {
        InitializeComponent();
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e)
    {
        if(NameTextBox.Text != "")
        {
            Task text = new Task();
            text.Name = NameTextBox.Text;
            text.IsCompleted = IsCompletedCheckBox.IsChecked.Value;
            text.Description = DescriptionTextBox.Text;
            Result = text;

            DialogResult = true;
        }
        else
        {
            NameTextBox.Background = Brushes.Pink;
        }
        
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;
    }

    private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}
