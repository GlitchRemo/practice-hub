<<<<<<< HEAD
# bc-template
=======

# About the project

[![Build Status](https://github.com/nationalgrid-customer/premise/actions/workflows/ci.yml/badge.svg?branch=main)](https://github.com/nationalgrid-customer/premise/actions/workflows/ci.yml)

## Overview
- meter-api is developed to fetch the meter details from meter DB.
- Meter event consumer is developed to fetch the event message from kafka topic and call the meter search rest api to perform data manipulation commands.
- Authorization and Authentication are done as a part of validation.
- To get the meter details, GraphQl query is called with account number and region fuel type as parameters.
- For other CRUD operations, Rest-api is used.
- For Database, CosmosDB with Mongo Adapter is used.
- Kafka for message parsing between different Bounded Contexts to make the data in sync.
- Mongo2Go is being used for creating In-Memory Db to develop unit/integration test.
- WireMock is being used to mock the external service for testing.

## Built With
- [.Net Core 6](https://dotnet.microsoft.com/en-us//)
- [Kafka](https://kafka.apache.org/)
- [MongoDb](https://www.mongodb.com/)

## Component Design

# Get Started
#### Prerequisites:
- [.Net Core](https://dotnet.microsoft.com/en-us/)
- [Rancher Desktop](https://rancher.com/products/rancher-desktop) - docker for building
- IDE: [Visual studio 2022](https://visualstudio.microsoft.com/vs/)/[Rider](https://www.jetbrains.com/rider/)
- [Azure repository for meter](https://github.com/nationalgrid-customer/meter)-Need Access(Can refer [document](https://docs.google.com/document/d/1vtU9UtephBNh3wV0vy7i_Opof1fW7AKGbRq4T8dBdTk/edit?hl=en&forcehl=1) for Request Access)
- [Studio 3T](https://studio3t.com/)
- Docker-compose (Command: brew install docker-compose)


### Local Setup

##### Clone the Repository mentioned above:
Go to the particular local where you want to clone the repo (say UWP2.0)
>Note: Need azure devOps access to have a git credentials and  password for cloning


```sh
cd UWP2.0
git clone https://github.com/nationalgrid-customer/meter
Password: [generate a git tocken]
cd meter
```
##### Configure local environment src/api/appsettings.local.json
To point appsettings.local.json, change the environment setting in launchSettings.json or if using Rider add the following to the Run Configurations environment variables:
```
"environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "local",
        "DOTNET_ENVIRONMENT": "Local",
        "Environment": "Local"
       }
```

##### Rancher setup for local mongoDB
Refer infra-docker-setup in [infra-local](https://dev.azure.com/NGRID/US-4750J-ADOProject-01/_git/infra-local?path=/infra-docker-setup) repo

##### Build Solution
Open sln file from src >> Api in premise-api  your IDE(visual studio/Rider)
Run the below command
```sh
dotnet build
```
##### Python 3.8 for seeding the data in local DB (Docker-Compose)

TODO
```sh

```

## Dependencies

Dependencies used to setup the meter-api

| Dependencies | Commands |Version|
| ------ | ------ |----------|
| AutoMapper | dotnet add package AutoMapper  |11.0.1
| HotChocolate.AspNetCore | dotnet add package HotChocolate.AspNetCore  |12.12.1
| MediatR | dotnet add package MediatR  |10.0.1
| Swashbuckle.AspNetCore | dotnet add package Swashbuckle.AspNetCore |6.3.2
| Flurl | dotnet add package Flurl |3.0.6
| Fluent Validation | dotnet add package FluentValidation  |11.1.0
|Wire Mock|dotnet add package WireMock.net |1.5.1
|Mongo2Go |dotnet add package Mongo2Go |3.1.3
|OpenTelemetry|dotnet add package OpenTelemetry |1.3.0
|Serilog|dotnet add package Serilog | 2.10.0

# Usage
UI link: [swaggerGen](https://localhost:57779/swagger/index.html)
GraphQL ([BananaCake pop](https://localhost:57779/premise-api/graphQL))
>Note : Provide the Authorisation token from [this link](https://green-pond-082a9350f.1.azurestaticapps.net/) in inspect window.
>Credential to login in Account Dashboard UI



#### Contributing
- See [this document](https://nationalgridplc.sharepoint.com/:w:/s/GRP-INT-US-UWP2.0/Ec-K1IkJYTNHhsyQ88DXv3cB4VrwHIebgr7N7cZAM9Tekg?e=pFghPg&isSPOFile=1)  for the most up-to-date contributing guidelines for members of the UWP 2.0 team.

## Development

###### How to test:
Use Swagger and banana Cake pop that mentioned above for testing.
Run Unit and Integration testing in local
```
dotnet test
```

Want to contribute? Great!


## Local Deployment
Go to meter/deployment/script/deploy-local.sh
Run this command
```
./deploy-local.sh
```

## Version

V1.0

###### Contact-us 
##### JP Silva / Jason Roberts
...
>>>>>>> a378c8e (first commit)
