# Modern Notes
Modern Notes example solution. Simple web app for composing plain text notes.
Users can view, edit and delete notes. In addition to the front-end user
interface it has backend REST API. 

![screenshot](modernnotes.png)

We use an in-memory database for storing notes. Restarting the web app will
delete any previously written notes.

# Build and run  
You can build and run the Modern Notes example solution by running
```
cd ModernNotes
dotnet run
``` 
from this directory. The app should now be available in your web browser at
[localhost:8000](http://localhost:8000). This requires the [.NET
SDK](https://www.microsoft.com/net/download).

## Docker
A Docker image of the application is available online (for users without the .NET SDK installed). 

```
docker run -p 8000:80 -t fjukstad/modernnotes
```

The app should now be available at [localhost:8000](http://localhost:8000).
You can also build and run the image by running

```
cd ModernNotes
docker build -t modernnotes .
docker run -p 8000:80 -ti modernnotes
```

from this directory.  

# API Documentation
The API is documented with Swagger and is available at
[localhost:8000/swagger](http://localhost:8000/swagger) when you run the app
locally.

# Tests 
[ModernNotes.Tests/IntegrationTests](ModernNotes.Tests/IntegrationTests)
contain integration tests for the Modern Notes app. You can run them by
executing

```
dotnet test ModernNotes.Tests/IntegrationTests/
```

in this directory. 

# Questions?
Feel free to contact me at
[bjorn.fjukstad@uit.no](mailto:bjorn.fjukstad@uit.no).
