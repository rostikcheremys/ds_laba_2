using System.Windows;
using System.Windows.Controls;

namespace Program
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartCalculation_Click(object sender, RoutedEventArgs e)
        {
            bool isX1Valid = ValidateInput(X1, "X1", out double x1);
            bool isX2Valid = ValidateInput(X2, "X2", out double x2);

            CalcThread.E = 0.01; 
            
            if (isX1Valid)
            {
                Thread thread1 = new Thread(() => ThreadFunction1(x1));
                thread1.Start();
            }
            
            if (isX2Valid)
            {
                Thread thread2 = new Thread(() => ThreadFunction2(x2));
                thread2.Start();
            }
            
            if (!isX1Valid && !isX2Valid)
            {
                MessageBox.Show("Both inputs are invalid. Please correct them.");
            }
        }
        
        private bool ValidateInput(TextBox inputField, string fieldName, out double result)
        {
            if (!double.TryParse(inputField.Text, out result))
            {
                MessageBox.Show($"Please enter a valid number for {fieldName}.");
                return false;
            }
            return true;
        }

        
        private void UpdateUI1(string result)
        {
            Dispatcher.Invoke(() =>
            {
                ResultTextBlock1.Text = result;
            });
        }

        private void UpdateUI2(string result)
        {
            Dispatcher.Invoke(() =>
            {
                ResultTextBlock2.Text = result;
            });
        }
        
        private void ThreadFunction1(double x)
        {
            CalcThread.MyFunc g = CalcThread.G1;
            x = CalcThread.Calc(x, g);
            
            string result = $"Equation 2x - cos(x) = 0\nAnswer: X= {x}, precision = {CalcThread.E}";
            
            UpdateUI1(result); 
        }

        private void ThreadFunction2(double x)
        {
            CalcThread.MyFunc g = CalcThread.G2;
            x = CalcThread.Calc(x, g);
            
            string result = $"Equation x + ln(x) = 0\nAnswer: X= {x}, precision = {CalcThread.E}";
            
            UpdateUI2(result);
        }
    }
}
