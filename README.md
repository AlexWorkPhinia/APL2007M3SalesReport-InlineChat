# APL2007M3SalesReport-InlineChat

## Overview

This project generates a quarterly sales report for different departments. It simulates sales data and calculates total sales and profit for each quarter.

## Project Structure



## Files

- **APL2007M3SalesReport-InlineChat.csproj**: Project file containing project configuration.
- **APL2007M3SalesReport-InlineChat.sln**: Solution file for the project.
- **Program.cs**: Main source code file containing the logic for generating sales data and creating the quarterly sales report.
- **README.md**: Project documentation file.

## Classes and Methods

### `QuarterlyIncomeReport`

#### `Main(string[] args)`

The entry point of the application. It creates an instance of `QuarterlyIncomeReport`, generates sales data, and calls the `QuarterlySalesReport` method.

#### `SalesData`

A struct representing sales data with the following fields:
- `DateOnly dateSold`
- `string departmentName`
- `string productID`
- `int quantitySold`
- `double unitPrice`
- `double baseCost`
- `int volumeDiscount`

#### `GenerateSalesData()`

Generates an array of 10,000 `SalesData` records with random values for each field.

#### `QuarterlySalesReport(SalesData[] salesData)`

Calculates and displays the total sales and profit for each quarter by department.

#### `GetCurrencySymbol(string manufacturingSites)`

Returns the currency symbol based on the manufacturing site.

#### `GetQuarter(int month)`

Returns the quarter (Q1, Q2, Q3, or Q4) based on the month.

## Usage

1. Build the project using Visual Studio or the .NET CLI.
2. Run the project to generate and display the quarterly sales report.

## Example Output
Quarterly Sales and Profit Report by Department
Q1: Electronics: Sales = $12345.67, Profit = $2345.67, Profit Percentage = 19.0% Clothing: Sales = $9876.54, Profit = $1876.54, Profit Percentage = 19.0% 
Q2: ...
## Dependencies

- .NET 8.0

## License

This project is licensed under the MIT License.
