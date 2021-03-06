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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectOperator selecterOperator;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;
                if(lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                }
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber = 0;

            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selecterOperator)
                {
                    case SelectOperator.Multiplication:
                        result = SimpleMatch.Mult(lastNumber, newNumber);
                        break;
                    case SelectOperator.Addtion:
                        result = SimpleMatch.Add(lastNumber, newNumber);
                        break;
                    case SelectOperator.Subtraction:
                        result = SimpleMatch.Sub(lastNumber, newNumber);
                        break;
                    case SelectOperator.Division:
                        result = SimpleMatch.Div(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result;
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int valueSelected = int.Parse((sender as Button).Content.ToString());  
           
            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{valueSelected}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{valueSelected}";
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }
            if (sender == multipleButton) selecterOperator = SelectOperator.Multiplication;
            if (sender == divideButton) selecterOperator = SelectOperator.Division;
            if (sender == subButton) selecterOperator = SelectOperator.Subtraction;
            if (sender == addButton) selecterOperator = SelectOperator.Addtion;
        }

        private void PointButton_Click(Object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains("."))
            {
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }

        public enum SelectOperator 
        { 
            Addtion,
            Subtraction,
            Multiplication,
            Division
        }

        public class SimpleMatch
        {
            public static double Add( double n1, double n2)
            {
                return n1 + n2;  
            }
            public static double Sub(double n1, double n2)
            {
                return n1 - n2;
            }
            public static double Mult(double n1, double n2)
            {
                return n1 * n2;
            }
            public static double Div(double n1, double n2)
            {
                if(n2 == 0)
                {
                    MessageBox.Show("Divider by 0 is not supporter", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;   
                }
                return n1 / n2;
            }
        }
    }
}
