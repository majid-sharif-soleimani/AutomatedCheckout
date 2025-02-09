# Automated Checkout

## Some notes about the solution
- I tried the TDD approach to develope this project.
- In the problem description, the weight argument is double and the Sum method returns double. However, I believe floating point numbers are not ideal and shouldn't be used for finanical calculations. For further information, look at https://stackoverflow.com/questions/3730019/why-not-use-double-or-float-to-represent-currency.
- In most projects, I use dependency injection (specifically Microsoft.Extensions.DependencyInjection) to decouple components and improve maintainability. However, for the sake of simplicity in this project, I created the resources manually and injected them to the Checkout constructor.
- The project doesn't have any logs and we need to debug the code whenever it is needed. However, I usually utilize Microsoft.Extensions.Logging to log into Console, Cloudwatch, SEQ and etc.
- I have implemented two repositories, ProductRepository and DiscountStrategyRepository, which currently serve as factories for creating and initializing products and discount strategies. As a further improvement, we can read data from a database. I typically use Dapper to fetch data from databases.
- The project entry point is AutomatedCheckout.Tests/CheckoutTests.cs
- For the sake of simplicity, None of the Checkout methods are thread safe for now.

## Problem Description

A store has the following products

| Item ID | Item        | Price       |
|---------|-------------|-------------|
| 1       | Toothpaste  | 24.95Kr     |
| 2       | Cheese      | 59.00Kr/kg  |
| 3       | Bread       | 11.95Kr     |
| 4       | Coffee      | 22.49Kr     |
| 5       | Appels      | 32.95Kr/kg  |
| 6       | Flour       | 11.95Kr     |
| 7       | Ground Beef | 93.00Kr/kg  |
| 8       | Milk        | 9.32Kr      |

There is an automated checkout where each item is entered by its item identification and when all items have been entered it is possible to get the total cost for the items. There are however some special offers that needs to be taken into the account when calculating the total sum. The special offers are as follows:
- Buy two packs of coffee for 40kr. Buy two breads for 20kr.
- Buy three packs of toothpaste and pay for two.
- Shop other items for over 150kr and you can buy appels for the price of 16.95Kr/kg.

The task is as follows. Write a solution for this with a class named Checkouter It should have three methods:
- AddItem(itemId : int) is called each time an item that does not have a price per weight is entered into the checkout.
- AddItem(itemId : int, weight : double) is called each time an item that does have a price per weight is entered into the checkout.
- Sum() : double is called only when all items has been checked into the checkout. It returns the total sum for all items accounting for the special offers.
