# StellarTransactionDiscovery

The app can be run two ways:
1. Docker. Navigate to the root dir where docker-compose.yml is located. Run `docker-compose build` and `docker-compose up` afterwards. To verify the app is listening use: POST http://localhost:8000/accounts/transactions with the body
`[
    "someAccountId1",
    "someAccountId2",
    ...
]`

2. Kestrel. Navigate to TransactionDiscovery.Host and run `dotnet run`.
To verify the app is listening use: POST https://localhost:5001/accounts/transactions with the body specified above. 
