 How to Run the Project (Server + Client)

This project contains two separate applications:

Server (Backend) – ASP.NET Core (C#)

Client (Frontend) – Angular

Follow the steps below to run both parts of the system.

 1. Run the Server (ASP.NET Core)

Open the server project folder (usually named Api, Server, or the solution .sln).

Open the project in Visual Studio or run via terminal.

Restore dependencies:

dotnet restore


Run the server:

dotnet run


The API will start on a URL similar to:

https://localhost:5001
http://localhost:5000


Important: Copy the URL – you will need it in the Angular client (environment file).

 2. Run the Client (Angular)

Go to the Angular project folder:

cd client


Install dependencies:

npm install


Open the Angular environment file:

src/environments/environment.ts


Set the API URL to match the server address:

export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001' // your backend URL
};


Start the Angular app:

ng serve


Open the app in the browser:

http://localhost:4200
