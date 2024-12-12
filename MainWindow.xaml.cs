using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using passwrkk.Model;


namespace passwrkk;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
/// 
public partial class MainWindow : Window
{
    SolidColorBrush WHITE = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
    Canvas overlayPanel = new();
    Grid current = new();

    private PasswordBox masterPassBox = new();
    private PasswordBox passwordBox = new PasswordBox();
    private TextBox nameBox = new();
    private StackPanel panel = new();

    private string _password = "";
    private string _name = "";
    private string _passwords = "";
    private string _path = "";

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

        Button leaveButton = new()
        {
            Margin = new Thickness(0, 0, 100, 100),
            Background = WHITE,
            Width = 30,
            Content = "<"
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

        leaveButton.Click += Leave;
        overlayPanel.Children.Add(leaveButton);

        overlayPanel.Children.Add(vaultNameBlock);
        overlayPanel.Children.Add(nameBox);

        overlayPanel.Children.Add(masterPassBlock);
        overlayPanel.Children.Add(masterPassBox);

        overlayPanel.Children.Add(endVaultsCreation);

        Canvas.SetLeft(overlayPanel, 50);
        Canvas.SetTop(overlayPanel, 50);

        MainGrid.Children.Add(overlayPanel);
    }

    private void Leave(object sender, RoutedEventArgs e)
    {
        if(sender is DependencyObject element)
        {
            var parent = VisualTreeHelper.GetParent(element);
            MainGrid.Children.Remove((UIElement)parent);
        }
    }

    private void Vault()
    {
        ScrollViewer scroll = new()
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            Margin = new Thickness(0, 0, 0, 0)
        };

        panel = new StackPanel()
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 64, 64, 64)),
            Width = 700,
            Orientation = Orientation.Vertical
        };

        TextBlock vaultNameBlock = new TextBlock()
        {
            Foreground = WHITE,
            FontSize = 16,
            Text = "Vault " + _name,
            Width = 200,
            Height = 20,
            TextAlignment = TextAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        Grid header = new()
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50)),
            Width = 600,
            Height = 50,
        };

        Button addNewPassword = new()
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50)),
            Content = "+",
            Width = 40,
            Height = 20,
            Foreground= WHITE,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom
        };

        header.Children.Add(vaultNameBlock);
        header.Children.Add(addNewPassword);
        panel.Children.Add(header);

        addNewPassword.Click += AddNewPasswordWindow;

        scroll.Content = panel;
        MainGrid.Children.Add(scroll);
    }

    private void AddNewPasswordWindow(object sender, RoutedEventArgs e)
    {
        current = new()
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30)),
            Width = 200,
            Height = 300,
        };
        TextBlock name = new()
        {
            Foreground = WHITE,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Text = "Enter a username for the password"
        };
        nameBox = new()
        {
            Margin = new Thickness(0, 20, 0, 0),
            Width = 140,
            Height = 20,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
        };
        TextBlock password = new()
        {
            Foreground = WHITE,
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
            Margin = new Thickness(0, 40, 0, 0),
            Text = "Enter the password"
        };
        passwordBox = new()
        {
            Width = 140,
            Height = 20,
            Margin = new Thickness(0, 60, 0, 0),
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Top,
        };

        Button submit = new()
        {
            Content = "Submit!",
            HorizontalAlignment= HorizontalAlignment.Center,
            VerticalAlignment= VerticalAlignment.Bottom,
        };

        submit.Click += AddPassword;
        current.Children.Add(name);
        current.Children.Add(nameBox);
        current.Children.Add(password);
        current.Children.Add(passwordBox);
        current.Children.Add(submit);
        MainGrid.Children.Add(current);
    }

    private void AddPassword(object sender, RoutedEventArgs e)
    {
        MainGrid.Children.Remove(current);
        var password = passwordBox.Password;
        passwordBox = new();

        var name = nameBox.Text;

        current.Children.Clear();

        _passwords = PasswordController.AddNewObjectToVaultByName(
            new List<string>() { name, password }, 
            _name, 
            _passwords, 
            new List<string>() { "name", "password" });
        Encrypter.SaveToFile(_path, _password, _passwords);
        RewritePasswords();
    }

    private void RewritePasswords()
    {
        try
        {
            panel.Children.Clear();
            TextBlock vaultNameBlock = new TextBlock()
            {
                Foreground = WHITE,
                FontSize = 16,
                Text = "Vault " + _name,
                Width = 200,
                Height = 20,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            Button addNewPassword = new()
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50)),
                Content = "+",
                Width = 40,
                Height = 20,
                Foreground = WHITE,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            Grid header = new()
            {
                Background = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50)),
                Width = 600,
                Height = 50,
            };

            header.Children.Add(addNewPassword);
            header.Children.Add(vaultNameBlock);
            panel.Children.Add(header);

            addNewPassword.Click += AddNewPasswordWindow;


            var list = PasswordController.GetAllByTypes(_passwords, _name, new List<string> { "name" });
            foreach (var names in list)
            {
                Grid grid = new()
                {
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Margin = new Thickness(0, 40, 0, 0),
                    Background = new SolidColorBrush(Color.FromArgb(255, 33, 33, 33)),
                    Width = 200,
                    Height = 40,
                };
                TextBlock text = new()
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = names[0],
                    Foreground = WHITE
                };
                grid.Children.Add(text);
                panel.Children.Add(grid);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }

    private void CreateVault(object sender, RoutedEventArgs eventArgs)
    {
        MainGrid.Children.Remove(overlayPanel);

        _name = nameBox.Text;
        _password = masterPassBox.Password;
        _passwords = @"{}";
        masterPassBox = new();
        nameBox = new();

        _path = GetPath() + "/" + _name + ".pswx";
        if(_path != "ERROR")
        {
            Encrypter.SaveToFile(_path, _password, "yoboba");
            Vault();
        }
    }

    private string GetPath()
    {
        OpenFolderDialog dialog = new();
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

        if (dialog.ShowDialog() == true)
        {
            var path = dialog.FileName;
            var text = Encrypter.ReadDataFromFile(path, "12345678");
        }
    }
}