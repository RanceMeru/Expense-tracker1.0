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

namespace Expense_tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //making a GUI 
    //simple buttons
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //using math to have a set budget that can be inputed and deleted much like a calculator
            //tracked with a data picker or chart research table what they are used for
            //then add or subtract your expense items and monthly occurences 
            //have it displayed as a list then another box to see how much money is needed and left over 
            //allow it to go into the negatives
            //the expense and its amount
           


            //pattern making variable to start with and that stay as unchanging variables
            InitializeComponent();
        }

        private void addExpense_Click(object sender, RoutedEventArgs e)
        {
            //adds an expense you input
        }

        private void removeExpense_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Expenses_TextChanged(object sender, TextChangedEventArgs e)
        {
            //name of the expenses
        }

        private void expenseCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            //cost of the expenses
        }

        private void resultBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //calculations if you can meet those expenses
        }

        private void Budget_TextChanged(object sender, TextChangedEventArgs e)
        {
            //our budget or the amount that we have or that we are allowed to use\
            

        }

        private void expenseGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //displays the names of the my expenses and the amount that it costs
        }

      

        private void budgetAmount_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}
