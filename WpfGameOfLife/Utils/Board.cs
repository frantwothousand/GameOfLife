using GameOfLifeApi;
using GameOfLifeApi.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfGameOfLife.Models;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace WpfGameOfLife.Utils;

public class Board
{

    #region Vars :)
    private const int BoardSizeX = 48; //
    private const int BoardSizeY = 84;
    private const int CellSize = 10;

    static Rectangle[,] position;
    #endregion

    #region Methods :)

    public void CreateBoard(Canvas board)
    {
        board.Children.Clear();
        position = new Rectangle[BoardSizeX, BoardSizeY];

        // Width and Height of Canvas (839 x 488)

        for (int i = 0; i < BoardSizeX; i++) 
        { 
            for(int j = 0; j < BoardSizeY; j++)
            {
                if(i == 0 || i == BoardSizeX -1 || j == 0 || j == BoardSizeY -1)
                {
                    Rectangle r = new Rectangle
                    {
                        Width = CellSize,
                        Height = CellSize,
                        //Fill = Brushes.Black,
                        Stroke = Brushes.Black,
                        //Stroke = Brushes.Blue,
                        StrokeThickness = 0.5,
                        Fill = Brushes.DarkGray,
                        //StrokeThickness = 1
                    };
                    position[i, j] = r;
                    Canvas.SetLeft(r, j * CellSize); // Columna
                    Canvas.SetTop(r, i * CellSize); // Fila
                    board.Children.Add(r);
                }
                else
                {
                    Cell cell = new Cell { IsAlive = false, Row = i, Column = j };
                    Rectangle r = new Rectangle
                    {
                        Width = CellSize,
                        Height = CellSize,

                        Fill = Brushes.LightGray, // can be AliceBlue
                        Stroke = Brushes.Black,
                        StrokeThickness = 0.5,
                    };
                    // TODO: Add event above.
                    position[i, j] = r;
                    Canvas.SetLeft(r, j * CellSize); // Columna
                    Canvas.SetTop(r, i * CellSize);
                    board.Children.Add(r);
                }
            }
        }
        
    }

    public static void LoadFromCollection(Canvas board, List<Person> _list)
    {
        for(int i = 0; i < BoardSizeX; i++)
        {
            for (int j = 0; j < BoardSizeY; j++)
            {
                if(!(i == 0 || i == BoardSizeX -1 || j == 0 || j == BoardSizeY -1))
                {
                    foreach (var item in _list)
                    {
                        if (item.Position.X.Equals(i) && item.Position.Y.Equals(j))
                        {
                            Rectangle r = new Rectangle
                            {
                                Width = CellSize,
                                Height = CellSize,
                                Stroke = Brushes.Black,
                                StrokeThickness = 0.5,
                                Fill = Brushes.DarkOliveGreen,
                            };
                            r.MouseDown += R_MouseDown;
                            position[i, j] = r;

                            Canvas.SetLeft(r, j * CellSize); // Columna
                            Canvas.SetTop(r, i * CellSize); // Fila
                            board.Children.Add(r);
                        }
                    }
                }
            }
        }
    }

    public static void LoadDataFromPlayer(Canvas canva, List<Person> _obj)
    {
        for (int i = 0; i < BoardSizeX; i++)
        {
            for (int j = 0; j < BoardSizeY; j++)
            {
                if (!(i == 0 || i == BoardSizeX - 1 || j == 0 || j == BoardSizeY - 1))
                {
                    foreach (var item in _obj)
                    {
                        if (item.Position.X.Equals(i) && item.Position.Y.Equals(j))
                        {
                            Rectangle r = new Rectangle
                            {
                                Width = CellSize,
                                Height = CellSize,
                                Stroke = Brushes.Black,
                                StrokeThickness = 0.5,
                                Fill = Brushes.GreenYellow,
                            };
                            r.MouseDown += R_MouseDown;
                            position[i, j] = r;

                            Canvas.SetLeft(r, j * CellSize); // Columna
                            Canvas.SetTop(r, i * CellSize); // Fila
                            canva.Children.Add(r);

                            FakeData.persons.Add(item);
                        }
                    }
                }
            }
        }
    }
    
    private static void R_MouseDown(object sender, MouseButtonEventArgs e)
    {
        Rectangle r = (Rectangle)sender;
        
        int x = (int)Canvas.GetTop(r) / CellSize;
        int y = (int)Canvas.GetLeft(r) / CellSize;

        foreach (var item in FakeData.persons)
        {
            if (x == item.Position.X && y == item.Position.Y)
            {
                Game.ShowPersonInfo(x, y);
                Debug.WriteLine($"Clicked on {item.GetFullName()}");
            }       
        }
    }

    #endregion
}