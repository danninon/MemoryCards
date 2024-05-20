# MemoryCards Documentation
MemoryCards is a full-stack application designed for interactive memorization exercises.
It leverages:
* React for the frontend, providing a responsive and dynamic user interface.
* The backend is powered by .NET
* utilizing MongoDB for efficient data storage and retrieval.




Installing the Application:

### React

To install React, follow these steps:

```bash
# Update apt package index
sudo apt update

# Install Node.js and npm
sudo apt install nodejs npm
```

### .NET 
To install .NET, follow these steps:

```bash
# Install .NET SDK (replace with the appropriate version if needed)
# This example assumes installation of .NET 6 SDK
sudo apt update
sudo apt install dotnet-sdk-6.0
```

### MongoDB:
To install MongoDB, follow these steps:

```bash
# Install MongoDB CLI tools or drivers if needed
# Example installation for MongoDB shell client
sudo apt update
sudo apt install mongodb-clients

```

Running the Application
Once installation is complete, follow these steps to run the application:

### Start MongoDB service
```bash 
# Start MongoDB service (adjust based on your setup)
sudo service mongod start
```

### .NET Backend
```bash 
# Navigate to your .NET project directory
# Restore dependencies and run the application
dotnet /path/to/your/dotnet/project/restore
dotnet /path/to/your/dotnet/project/run
```

### React Frontend
```bash
# Navigate to your React project directory
cd /path/to/your/react/project

# Install dependencies (if not already installed)
npm install

# Start the React development server
npm start

```

Replace /path/to/your/dotnet/project and /path/to/your/react/project with the actual paths to your .NET and React project directories, respectively.