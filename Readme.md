# Enter website
https://middags-mixern.herokuapp.com/ 

# Run local
stand in ../cykelfest

write `dotnet run`

enter the http://localhost:.. provided in your browser

# Deploy
```sh
heroku container:login
heroku container:push web --app middags-mixern
heroku container:release web --app middags-mixern
```
