# LoggingSample

An example .NET Core project that demonstrates logging handlers in .NET Core 2.0 or greater.

## Prerequisites

* Visual Studio Code or Visual Studio 2017
* .NET Core 2.0 or greater

## Setup and Test

Clone the repository to your local drive.  If you're running Visual Studio 2017, open the LoggingSample.sln file, build the project, then use the Test Explorer to execute the unit tests.  If you're using VS Code or another
code editor, open a command shell to the directory where you cloned the repository, then run the following commands:

* dotnet restore
* dotnet build
* cd LoggingSample.Tests
* dotnet test

## Demonstration

Start the API.  If you're using Visual Studio 2017, open the LoggingSample.sln file, and click Play, or press
F5.  If you're using VS Code, you can also press F5.  From the command line, you can do the following:

* dotnet restore
* dotnet build
* cd LoggingSample
* dotnet run

Use curl, PostMan, or even your web browser to make GET requests against the running API. You can see the
global logging happen by making a GET request against **/api/values**.  To see an example of the global
exception logging, make a GET request agasint **/api/error**.  Either way, you should see log messages being
produced in the shell window that launched at the startup of the API project.