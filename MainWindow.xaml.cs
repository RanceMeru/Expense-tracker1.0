using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Expense_tracker
{
    public partial class MainWindow : Window
    {
        private Budget budget;

        public MainWindow()
        {
            InitializeComponent();
            budget = new Budget();
            expenseData.ItemsSource = budget.Expenses;
        }

        private void addExpense_Click(object sender, RoutedEventArgs e)
        {
            string expenseName = Expenses.Text;
            double expenseCostValue = double.Parse(expenseCost.Text);
            Expense expense = new Expense(expenseName, expenseCostValue);
            budget.AddExpense(expense);
            resultBox.Text = budget.CheckBudget();
            ClearInputFields();
        }

        private void removeExpense_Click(object sender, RoutedEventArgs e)
        {
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            Expenses.Text = "";
            expenseCost.Text = "";
            budgetAmount.Text = "";
        }

        private void budgetAmount_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox budgetAmountTextBox = (TextBox)sender;
            double budgetAmount = 0;
            if (double.TryParse(budgetAmountTextBox.Text, out budgetAmount))
            {
                budget.SetBudget(budgetAmount);
                resultBox.Text = budget.CheckBudget();
            }
        }
        private void expenseData_SelectionChanged(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for selecting and removing expenses from the DataGrid
        }

        private void Expenses_TextChanged(object sender, RoutedEventArgs e)
        {
            // TODO: Implement validation for expense name input
        }

        private void expenseCost_TextChanged(object sender, RoutedEventArgs e)
        {
            // TODO: Implement validation for expense cost input
        }

        private void resultBox_TextChanged(object sender, RoutedEventArgs e)
        {
            // TODO: Implement logic for displaying budget status message
        }
    }

    public class Expense
    {
        public string Name { get; set; }
        public double Cost { get; set; }

        public Expense(string name, double cost)
        {
            Name = name;
            Cost = cost;
        }
    }

    public class Budget
    {
        public double BudgetAmount { get; set; }
        public List<Expense> Expenses { get; set; }

        public Budget()
        {
            BudgetAmount = 0;
            Expenses = new List<Expense>();
        }

        public void SetBudget(double budgetAmount)
        {
            BudgetAmount = budgetAmount;
        }

        public void AddExpense(Expense expense)
        {
            Expenses.Add(expense);
        }

        public void RemoveExpense(Expense expense)
        {
            Expenses.Remove(expense);
        }

        public double GetTotalCost()
        {
            double totalCost = 0;
            foreach (Expense expense in Expenses)
            {
                totalCost += expense.Cost;
            }
            return totalCost;
        }

        public string CheckBudget()
        {
            double totalCost = GetTotalCost();
            if (totalCost <= BudgetAmount)
            {
                return "You have enough money in your budget for your expenses.";
            }
            else
            {
                return "You do not have enough money in your budget for your expenses.";
            }
        }
    }
}