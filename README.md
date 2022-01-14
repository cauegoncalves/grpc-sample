# grpc-sample
## Definition
A calculator grpc applictation. 

The [proto file](calculator.proto) has a service *CalculatorService* with four methods. Each method applies a different gprc communication:
- **Sum:** Perform a simple sum between the two numbers of the *CalculatorRequest* message.
  - GRPC Communication: Unary
- **DecomposeNumber:** Client sends a number to the server and the server decomposes the number and returns one number at the time, waiting 500ms between each response.
  - GRPC Communication: Server stream
- **Average:** Client send numbers to the server and when it finishes the server returns with the average of all numbers.
  - GRPC Communication: Client stream
- **Max:** While the client send numbers to the server, the server responds if that is the current maximum number sent in the stream.
  - GRPC Communication: Bi-Directional stream

## How to run
### Server

```powershell
dotnet run --project .\CalculatorServer\CalculatorServer.csproj
```

### Client
```powershell
dotnet run --project .\CalculatorClient\CalculatorClient.csproj
```