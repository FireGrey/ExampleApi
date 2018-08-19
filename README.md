# ExampleApi

An example project for a RESTful Api using ASP.NET Core, EF Core, xUnit and Docker.

## Prerequisites

* [Docker](https://www.docker.com/get-started) - Building and Running
* [.NET Core 2.1 SDK](https://www.microsoft.com/net/download) - Debugging and Tests

## Running with Docker

```
docker build -t exampleapi .
docker run -p 8080:80 exampleapi
```

## Running the tests

```
dotnet test
```

## Auth

This project uses JWT Bearer auth due to it being a popular choice among larger scale web apis, but was simplified heavily in this implementation for brevity. A larger project would have it's own auth service with user stores and more.

Auth is only required for the `DELETE /api/customers/{id}` endpoint, and was kept as basic as possible.

### Retrieving a token

Currently there's only a single user, with username: "admin" and password: "admin"

To retrieve a token, POST the following payload to `/auth/token`

```
{
    "userName": "admin",
    "password": "admin"
}
```

### Using a token

To include a [Bearer Token](https://tools.ietf.org/html/rfc6750) in your request, add a header with the following key a value.

```
Authorization: Bearer <token>
```

Where `<token>` is the token retrieved in the previous step.

The tokens each last up to 30 minutes.

## Api

### GET /api/customers

Returns a list of customers with the option to query and sort by different fields.

Available query string filters:
* firstName - filter by firstName
* lastName - filter by lastName
* orderBy - field to order by (ie. FirstName, LastName, DateOfBirth)

Example:
```
GET /api/customers?firstName=Jerry&lastName=Seinfield&orderBy=DateOfBirth
```

### GET /api/customers/{id}

Retrieve a single customer by their Id

Example:
```
GET /api/customers/1
```

### POST /api/customers

Add a customer to the database

Example
```
POST /api/customers
{
    "firstName": "Zero",
    "lastName": "Cool",
    "dateOfBirth": "1972-11-15T00:00:00"
}
```

### PUT /api/customers/{id}

Replace an existing customer record with a new one

Example
```
PUT /api/customers/1
{
    "firstName": "Zero",
    "lastName": "Cool",
    "dateOfBirth": "1972-11-15T00:00:00"
}
```

### DELETE /api/customers/{id}

Delete a customer from the database

Requires auth

Example
```
DELETE /api/customers/1
```