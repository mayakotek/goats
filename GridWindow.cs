using System.Reflection;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Microsoft.Extensions.FileProviders;

public class GridWindow {
    int[] GridCol = new int[9];
    public Window win;

    public GridWindow() {
        
        //int[] GridCol = new int[8];

        win = new Window {
            Width = 900,
            Height = 100,
            Title = "GridWindow",
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
        };

        Grid grid = new Grid {
            RowDefinitions = RowDefinitions.Parse("*"),
            ColumnDefinitions = ColumnDefinitions.Parse("*, *, *, *, *, *, *, *, *"),
            Background = Brushes.DeepSkyBlue,
            ShowGridLines = true,
        };

        Bitmap goatLeft = GetBitmap("GoatLeft.png");
        Bitmap goatRight = GetBitmap("GoatRight.png");

        Image imageL1 = new Image {
            Source = goatLeft,
        };

        Image imageL2 = new Image {
            Source = goatLeft,
        };

        Image imageL3 = new Image {
            Source = goatLeft,
        };

        Image imageL4 = new Image {
            Source = goatLeft,
        };

        Image imageR1 = new Image {
            Source = goatRight,
        };

        Image imageR2 = new Image {
            Source = goatRight,
        };

        Image imageR3 = new Image {
            Source = goatRight,
        };

        Image imageR4 = new Image {
            Source = goatRight,
        };

        imageL1.SetValue(Grid.ColumnProperty, 8); //put in right most column
        imageR1.SetValue(Grid.ColumnProperty, 0);
        imageL2.SetValue(Grid.ColumnProperty, 7); 
        imageR2.SetValue(Grid.ColumnProperty, 1);
        imageL3.SetValue(Grid.ColumnProperty, 6); 
        imageR3.SetValue(Grid.ColumnProperty, 2);
        imageL4.SetValue(Grid.ColumnProperty, 5); 
        imageR4.SetValue(Grid.ColumnProperty, 3);
        
        //set gridcol to 1 where goat and 0 where not
        GridCol[0] = 1;
        GridCol[1] = 1;
        GridCol[2] = 1;
        GridCol[3] = 1;
        GridCol[4] = 0;
        GridCol[5] = 1;
        GridCol[6] = 1;
        GridCol[7] = 1;
        GridCol[8] = 1;
        
        

        imageL1.PointerPressed += LeftJump;   //add jump method to pointer pressed event
        imageR1.PointerPressed += RightJump;
        imageL2.PointerPressed += LeftJump;   
        imageR2.PointerPressed += RightJump;
        imageL3.PointerPressed += LeftJump;   
        imageR3.PointerPressed += RightJump;
        imageL4.PointerPressed += LeftJump;   
        imageR4.PointerPressed += RightJump;

        grid.Children.Add(imageL1);     //add images to the grid
        grid.Children.Add(imageR1);
        grid.Children.Add(imageL2);
        grid.Children.Add(imageR2);
        grid.Children.Add(imageL3);
        grid.Children.Add(imageR3);
        grid.Children.Add(imageL4);
        grid.Children.Add(imageR4);
        win.Content = grid;
    }

    void LeftJump(object sender, RoutedEventArgs e) {
        Image image = sender as Image;  //lets the object be treated as a reference to an image

        int column = image.GetValue(Grid.ColumnProperty); 

        if (column > 0) { //can only move if above 0 
            if (GridCol[column-1] == 0) { //if front column is empty move 1
                image.SetValue(Grid.ColumnProperty, column - 1);    //sets image to one column less than before
                
                GridCol[column] = 0;    //set previous position to empty
                GridCol[column-1] = 1;  //set current position to full
            }
            else if (column - 1 > 0 && GridCol[column-2] == 0) { //if 2+ is empty jump and move 2
                image.SetValue(Grid.ColumnProperty, column - 2);

                GridCol[column] = 0;
                GridCol[column-2] = 1;
            }
        }

    }

    void RightJump(object sender, RoutedEventArgs e) {
        Image image = sender as Image;  //lets the object be treated as a reference to an image

        int column = image.GetValue(Grid.ColumnProperty);

        if (column < 8) {
            if (GridCol[column+1] == 0) { //if front column is empty move 1
                image.SetValue(Grid.ColumnProperty, column + 1);    //sets image to one column less than before

                GridCol[column] = 0;
                GridCol[column+1] = 1;
            }
            else if (column + 1 < 8 && GridCol[column+2] == 0) { //if 2+ is empty jump and move 2
                image.SetValue(Grid.ColumnProperty, column + 2);

                GridCol[column] = 0;
                GridCol[column+2] = 1;
            }
        }
    }

    Bitmap GetBitmap(string resourceName) {
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());

        using (var reader = embeddedProvider.GetFileInfo(resourceName).CreateReadStream()) {
            return new Bitmap(reader);
        }
    }

}