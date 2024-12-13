using System.DirectoryServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace passwrkk.View.Windows;

public static class WindowsCreator
{
    public static Grid CreatePasswordWindow(Grid mainGrid, out PasswordBox passwordBox, out TextBox nameBox, out Button submitButton)
    {
        Grid currentGrid = new()
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 30, 30, 30)),
            Width = 200,
            Height = 300,
        };
        TextBlock name = new()
        {
            Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
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
            Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
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

        submitButton = new()
        {
            Content = "Submit!",
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Bottom,
        };
        currentGrid.Children.Add(name);
        currentGrid.Children.Add(nameBox);
        currentGrid.Children.Add(password);
        currentGrid.Children.Add(passwordBox);
        currentGrid.Children.Add(submitButton);
        mainGrid.Children.Add(currentGrid);

        return currentGrid;
    }

    public static StackPanel DrawVaultWindow(Grid mainGrid, string name, out Button addNewPasswordButton)
    {
        ScrollViewer scroll = new()
        {
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            Margin = new Thickness(0, 0, 0, 0)
        };

        StackPanel currentStackPanel = new()
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 64, 64, 64)),
            Width = 700,
            Orientation = Orientation.Vertical
        };

        TextBlock vaultNameBlock = new TextBlock()
        {
            Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
            FontSize = 16,
            Text = "Vault " + name,
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

        addNewPasswordButton = new()
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 50, 50, 50)),
            Content = "+",
            Width = 40,
            Height = 20,
            Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Bottom
        };

        header.Children.Add(vaultNameBlock);
        header.Children.Add(addNewPasswordButton);
        currentStackPanel.Children.Add(header);

        scroll.Content = currentStackPanel;
        mainGrid.Children.Add(scroll);

        return currentStackPanel;
    }

    public static Canvas CreateNewVaultWindow
        (
        Grid mainGrid,
        out TextBox nameBox,
        out PasswordBox masterPasswordBox,
        out Button endVaultsCreationButton,
        out Button leaveButton
        )
    {
        Canvas currentCanvas = new()
        {
            Background = new SolidColorBrush(Color.FromArgb(255, 64, 64, 64)),
            Width = 200,
            Height = 300
        };

        leaveButton = new()
        {
            Margin = new Thickness(0, 0, 100, 100),
            Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
            Width = 30,
            Content = "<"
        };

        TextBlock vaultNameBlock = new TextBlock()
        {
            Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
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
            Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)),
            FontSize = 16,
            Text = "Set vault's masterpass",
            Width = 200,
            Height = 21,
            TextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 80, 0, 20),
        };

        masterPasswordBox = new()
        {
            Name = "masterPass",
            Margin = new Thickness(40, 110, 40, 20),
            Width = 120,
            Height = 21
        };

        endVaultsCreationButton = new()
        {
            Margin = new Thickness(40, 260, 40, 20),
            Width = 120,
            Height = 21,
            Content = "Create a new vault!"
        };
        currentCanvas.Children.Add(leaveButton);
        currentCanvas.Children.Add(vaultNameBlock);
        currentCanvas.Children.Add(nameBox);
        currentCanvas.Children.Add(masterPassBlock);
        currentCanvas.Children.Add(masterPasswordBox);
        currentCanvas.Children.Add(endVaultsCreationButton);

        Canvas.SetLeft(currentCanvas, 50);
        Canvas.SetTop(currentCanvas, 50);

        mainGrid.Children.Add(currentCanvas);

        return currentCanvas;
    }

    public static List<Button> WritePasswords(List<List<string>> passwords, StackPanel panel)
    {
        List<Button> list = new();

        foreach (var item in passwords)
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

            PasswordBox passwordBox = new()
            {
                Visibility = Visibility.Hidden,
                Password = item[1],
            };

            TextBlock text = new()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Text = item[0],
                Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
            };

            Button passwordButton = new()
            {
                Content = "get password",
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Center,
                Width = 100,
                Height = 20
            };

            grid.Children.Add(passwordButton);
            grid.Children.Add(passwordBox);
            grid.Children.Add(text);
            panel.Children.Add(grid);

            list.Add(passwordButton);
        }

        return list;
    }
}

