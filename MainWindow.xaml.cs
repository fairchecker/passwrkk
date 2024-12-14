using Microsoft.Win32;
using passwrkk.Model;
using passwrkk.View;
using passwrkk.View.Windows;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace passwrkk;

public partial class MainWindow : Window
{
    private string _password = "";
    private string _name = "";
    private string _passwords = "";
    private string _path = "";

    private TextBox _uTBox = new();
    private PasswordBox _uPBox = new();

    private StackPanel _stackPanel = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnCreateNewVaultButton(object sender, RoutedEventArgs e)
    {
        Button button = new();
        Button leaveButton = new();
        var panel = WindowsCreator.CreateNewVaultWindow(MainGrid, out _uTBox, out _uPBox, out button, out leaveButton);

        button.Click += OnEndNewVaultCreatingButton;
        leaveButton.Click += OnLeaveButton;
    }

    private void OnLeaveButton(object sender, RoutedEventArgs e)
    {
        ViewUtils.HideParentElement(sender, MainGrid);
    }

    private void OnEndNewVaultCreatingButton(object sender, RoutedEventArgs e)
    {
        ViewUtils.HideParentElement(sender, MainGrid);

        _name = _uTBox.Text;
        _password = _uPBox.Password;
        _passwords = @"{}";
        _path = ModelUtils.GetFolderPath() + "/" + _name + ".pswx";

        Button button = new();
        _stackPanel = WindowsCreator.DrawVaultWindow(MainGrid, _name, out button);
        button.Click += OnAddNewPasswordButton;
    }

    private void OnAddNewPasswordButton(object sender, RoutedEventArgs e)
    {
        Button button = new();
        var grid = WindowsCreator.CreatePasswordWindow(MainGrid, out _uPBox, out _uTBox, out button);

        button.Click += OnAddedNewPasswordButton;
    }

    private void OnAddedNewPasswordButton(object sender, RoutedEventArgs e)
    {
        ViewUtils.HideParentElement(sender, MainGrid);

        var password = _uPBox.Password;
        _uPBox = new();

        var name = _uTBox.Text;
        _uTBox = new();

        _passwords = PasswordController.AddNewObjectToVaultByName
            (
            new List<string>() { name, password },
            _name,
            _passwords,
            new List<string>() { "name", "password" }
            );

        Encrypter.SaveToFile(_path, _password, _passwords);
        RewritePasswords();
    }

    private void RewritePasswords()
    {
        _stackPanel.Children.Clear();

        Button button = new Button();
        _stackPanel = WindowsCreator.DrawVaultWindow(MainGrid, _name, out button);
        button.Click += OnAddNewPasswordButton;

        var list = PasswordController.GetAllByTypes
            (
            _passwords, 
            _name, 
            new List<string> { "name", "password" }
            );
        var buttonsList = WindowsCreator.WritePasswords(list, _stackPanel);

        foreach( var item in buttonsList )
        {
            item.Click += OnGetPasswordButton;
        }
    }

    private void OnGetPasswordButton(object sender, RoutedEventArgs e)
    {
        var password = ViewUtils.FindChild<PasswordBox>(VisualTreeHelper.GetParent((DependencyObject)sender));
        if (password != null)
        {
            MessageBox.Show(password.Password);
            Clipboard.SetText(password.Password);
        }
    }

    private void OnOpenVaultButton(object sender, RoutedEventArgs e)
    {
        _path = ModelUtils.GetFilePath("Vault (*.pswx)|*.pswx");
        _name = ModelUtils.GetVaultNameByPath(_path);

        Button button = new();
        WindowsCreator.OpenVaultWindow(MainGrid, out _uPBox, out button);
        button.Click += OnEndVaultOpeningButton;
    }

    private void OnEndVaultOpeningButton(object sender, RoutedEventArgs e)
    {
        _password = _uPBox.Password;
        _passwords = Encrypter.ReadDataFromFile(_path, _password);

        ViewUtils.HideParentElement(sender, MainGrid);

        Button addNewPassButton = new();
        _stackPanel = WindowsCreator.DrawVaultWindow(MainGrid, _name, out addNewPassButton);
        addNewPassButton.Click += OnAddNewPasswordButton;

        var list = WindowsCreator.WritePasswords
            (
            PasswordController.GetAllByTypes
                (
                _passwords, 
                _name, 
                new List<string> { "name", "password" }
                ), 
            _stackPanel
            );

        foreach(var item in list)
        {
            item.Click += OnGetPasswordButton;
        }
    }
}