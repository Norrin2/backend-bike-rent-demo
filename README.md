# Bike Rent Demo.
This is a demo project for a bike rent app

## Technologies Used
- .NET 8
- MongoDB
- RabbitMQ
  
## What is the project/how to run
- BikeRent.Publisher - Publisher project that allows renting bikes, creating new deliverymen, posting orders. .NET 8 project
- BikeRent.Consumer - Consumer project that allows consumes order messages sent by the publisher. .NET 8 background service project
- There is a docker-compose.yaml file used to run the database and rabbitMQ service, can be run with -docker-compose up -d
  

