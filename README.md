# ReactIntus

ReactIntus is a full-stack application built with **.NET** for the backend and **React** for the frontend using **Vite**.

## Features
- The application renders SVG rectangle according to the dimensions in the **rectangleDimensions.json** file located in **ReactIntus.Server/wwwroot/**.
- The user interface allows resizing the rectangular using a mouse.
- The perimeter of the rectangular is calculated and shown in the application as it is resized.
- After resizing, dimensions are updated in **rectangleDimensions.json** file.
- If the rectangular width exceeds height, it shows error message and timeouts calculations for 10 seconds.
- After error message is displayed, size of the rectangular can still be changed.

## Technologies

- **Frontend:
  - React
  - Vite

- **Backend:**
  - ASP.NET Core
  - .NET 8
  - C# 12.0

- **Tools:**
  - Visual Studio 2022
  - Node.js & npm
  - Git


## Installation

1. **Clone the Repository**

git clone https://github.com/RokasCoding/reactintus.git

2. **Setup Backend**

cd ReactIntus.Server dotnet restore

3. **Setup Frontend**

cd ../reactintus.client npm install


## Running the Application

1. **Start Backend Server**

cd ReactIntus.Server dotnet run

The backend API will be available at `https://localhost:7034/api/Rectangle`.

2. **Start Frontend Development Server**

cd ../reactintus.client npm run dev

The frontend will be accessible at `https://localhost:11338`. 
