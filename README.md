# FinancialTechnology
# Project

Manager service to work with bank accounts.

# AccountAPI:
  * **AddAccount**: Insert a new Account in the database.
  * **DepositToAccount** Deposit a specific amount into the account 
  * **WithdrawFromAccount** Withdraw a specific amount from the account.
  * **GetAccountBalance** Get the balance of a given account

 # UserAPI:
  * **Login**: Log in to the APP.
  * **GetTaskById** Insert a new User in the database
  
# Installation and Startup
  * Create a database in the database engine of your choice.
  * Make sure that the database name matches the one in the connection string.
  * Run the command **Update-Database**.
  * Send a request to **Login** endoint to get Token.
  * Paste the token in **Authorize** from Swagger followed by "Bearer". Example: "Bearer {token}"
  * Enjoy

# Notes
  * Has Swagger incorporated
  * The service will log into "C:\Logs\AppLogs.log"
  * It could be more specific with the errors it returns and have better data validation, but I ran out of time. 👍
