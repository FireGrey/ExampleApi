FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app

# copy csprojs and restore as distinct layers
COPY *.sln ./
COPY ExampleApi.Context/*.csproj ./ExampleApi.Context/
COPY ExampleApi.Domain/*.csproj ./ExampleApi.Domain/
COPY ExampleApi.Domain.Tests/*.csproj ./ExampleApi.Domain.Tests/
COPY ExampleApi.Web/*.csproj ./ExampleApi.Web/
RUN dotnet restore

# copy everything else and build app
COPY . ./
WORKDIR /app/ExampleApi.Web
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app
COPY --from=build /app/ExampleApi.Web/out ./
ENTRYPOINT ["dotnet", "ExampleApi.Web.dll"]