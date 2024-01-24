using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Expense_tracker
{
    public partial class MainWindow : Window
    {
        private TotalBalance totalBalance;
        private Budget budget;

        public MainWindow()
        {
            InitializeComponent();
            budget = new Budget();
            totalBalance = new TotalBalance();
            expenseData.ItemsSource = budget.Expenses;

            // Load the session when the application starts
            LoadSession();

            // Attach the event handler for the Window Closed event
            Closed += Window_Closed;

            clearAll.Click += clearAll_Click;

            changeBudget.Click += changeBudget_Click;

        }

        private void SaveSession()
        {
            string json = JsonSerializer.Serialize(budget);
            File.WriteAllText("session.json", json);
        }

        private void LoadSession()
        {
            try
            {
                string json = File.ReadAllText("session.json");
                budget = JsonSerializer.Deserialize<Budget>(json);
                expenseData.ItemsSource = budget.Expenses;
                resultBox.Text = budget.CheckBudget();
                budgetAmountLabel.Text = budget.BudgetAmount.ToString("C"); // Display the initial budget as currency
            }
            catch (FileNotFoundException)
            {
                // Handle the case where the file is not found (first-time use)
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                MessageBox.Show($"Error loading session: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void ClearAll()
        {
            budget = new Budget();
            expenseData.ItemsSource = budget.Expenses;
            resultBox.Text = "";
            ClearInputFields();
        }

        private void addExpense_Click(object sender, RoutedEventArgs e)
        {
            string expenseName = Expenses.Text;
            double expenseCostValue;

            // Check if expense name is blank
            if (string.IsNullOrWhiteSpace(expenseName))
            {
                MessageBox.Show("You have to add an expense.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Check if expense cost is a valid number
            if (!double.TryParse(expenseCost.Text, out expenseCostValue))
            {
                MessageBox.Show("You have to add a cost.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

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
                budgetAmountLabel.Text = budgetAmount.ToString("C"); // Display the budget as currency
            }
        }


        private void expenseData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Expense selectedExpense = (Expense)e.AddedItems[0];
                if (Mouse.RightButton == MouseButtonState.Pressed)
                {
                    budget.RemoveExpense(selectedExpense);
                    resultBox.Text = budget.CheckBudget();
                }
            }
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

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveSession();
        }

        private void clearAll_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void changeBudget_Click(object sender, RoutedEventArgs e)
        {
            double newBudgetAmount;
            if (double.TryParse(budgetAmount.Text, out newBudgetAmount))
            {
                budget.SetBudget(newBudgetAmount);
                resultBox.Text = budget.CheckBudget();
            }
            else
            {
                MessageBox.Show("Invalid budget amount. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    [Serializable]
    public class TotalBalance
    {
        // TotalBalance class implementation
    }

    [Serializable]
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

    [Serializable]
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
