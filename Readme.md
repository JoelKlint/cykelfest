#Enter website
https://middags-mixern.herokuapp.com/ 

# Deploy
```sh
heroku container:login
heroku container:push web --app middags-mixern
heroku container:release web --app middags-mixern
```
