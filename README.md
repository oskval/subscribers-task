# subscribers-task

## Total elapsed time: ~9h

## Note
This project aims to complete all tasks specified in the [Developer Assessment Task v2.pdf](https://github.com/oskval/subscribers-task/blob/master/Developer%20assessment%20task%20v2.pdf).

## TODOs
- Implement SMS, Slack, and email services (mocked or actual APIs ?)
- Add filtering by date range and email
- Implement backend validation exceptions with custom response middleware
- Enhance frontend with additional CSS styling
- Add backend Data Transfer Objects (DTOs)
- Move data and file validations to actual validators
- Tests for everything (integration also :))

## How to Set Up

### Prerequisites
1. **Install .NET 8 SDK**
   - Download and install from the [official .NET website](https://dotnet.microsoft.com/download).

2. **Install Node.js**
   - Download and install from the [official Node.js website](https://nodejs.org/).

3. **Install Angular CLI**
   - Run the following command:
     ```bash
     npm install -g @angular/cli
     ```

### Setting Up the Backend
1. **Create and Update the SQLite Database**
   - Navigate to the `subscribers-api` directory.
   - Run the following command to create the SQLite database:
     ```bash
     dotnet ef database update --project ../Persistence/Persistence.csproj --startup-project ./API.csproj
     ```

2. **Build and Run the API**
   - Navigate to the `subscribers-api/API` directory.
   - Build and run the API:
     ```bash
     dotnet build
     dotnet run
     ```

### Setting Up the Frontend
1. **Build and Serve the Angular Application**
   - Navigate to the `subscribers-web` directory.
   - Build and serve the Angular application:
     ```bash
     ng build
     ng serve
     ```

### Configuration
- **Configure Ports (if needed)**
  - Ensure that the ports for the API and Angular application do not conflict. You can configure these in their respective configuration files or command line options.
