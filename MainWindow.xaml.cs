using Microsoft.Win32;
using System.IO;
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

namespace passwrkk;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public partial class MainWindow : Window
{
    SolidColorBrush WHITE = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
    Canvas overlayPanel = new();

    private PasswordBox masterPassBox = new();
    private TextBox nameBox = new();
    public MainWindow()
    {
        InitializeComponent();
    }

    private void CreateNewVault(object sender, RoutedEventArgs eventArgs)
    {
        overlayPanel = new Canvas
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 64, 64, 64)),
            Width = 200,
            Height = 300
        };

        TextBlock vaultNameBlock = new TextBlock()
        {
            Foreground = WHITE,
            FontSize = 16,
            Text = "Set vault's name",
            Width = 200,
            Height = 20,
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 10, 0, 20),
        };

        nameBox = new()
        {
            Margin = new Thickness(40, 40, 40, 20),
            Width = 120,
            Height = 20
        };

        TextBlock masterPassBlock = new TextBlock()
        {
            Foreground = WHITE,
            FontSize = 16,
            Text = "Set vault's masterpass",
            Width = 200,
            Height = 21,
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 80, 0, 20),
        };

        masterPassBox = new()
        {
            Name = "masterPass",
            Margin = new Thickness(40, 110, 40, 20),
            Width = 120,
            Height = 21
        };

        Button endVaultsCreation = new()
        {
            Margin= new Thickness(40, 260, 40, 20),
            Width = 120,
            Height = 21,
            Content = "Create a new vault!"
        };
        endVaultsCreation.Click += CreateVault;

        overlayPanel.Children.Add(vaultNameBlock);
        overlayPanel.Children.Add(nameBox);

        overlayPanel.Children.Add(masterPassBlock);
        overlayPanel.Children.Add(masterPassBox);

        overlayPanel.Children.Add(endVaultsCreation);

        Canvas.SetLeft(overlayPanel, 50);
        Canvas.SetTop(overlayPanel, 50);

        MainGrid.Children.Add(overlayPanel);
    }

    private void CreateVault(object sender, RoutedEventArgs eventArgs)
    {
        MainGrid.Children.Remove(overlayPanel);
        var path = GetPath();
        if(path != "ERROR")
        {
            MessageBox.Show(path);
            File.Create(@path + "/" + nameBox.Text + ".pswx");
        }
    }

    private string GetPath()
    {
        OpenFolderDialog dialog = new();
        //dialog.Filter = "Vault (*.pswx)|*.pswx";
        if(dialog.ShowDialog() == true)
        {
            return dialog.FolderName;
        }
        return "ERROR";
    }

    private void OpenVault(object sender, RoutedEventArgs eventArgs)
    {
        OpenFileDialog dialog = new();
        dialog.Filter = "Vault (*.pswx, *.txt)|*.pswx;*.txt";
        dialog.Multiselect = false;

        if(dialog.ShowDialog() == true)
        {
            var path = dialog.FileName;
        }
    }
}