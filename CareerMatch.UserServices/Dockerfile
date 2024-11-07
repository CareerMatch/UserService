# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Use the environment variable for the connection string
ARG DB_CONNECTION_STRING
ENV DB_CONNECTION_STRING=${DB_CONNECTION_STRING}

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the .csproj file explicitly from the current directory
COPY CareerMatch.UserServices.csproj ./
RUN dotnet restore "CareerMatch.UserServices.csproj"

# Copy the rest of the application files to the container
COPY . . 
WORKDIR "/src"

# Build the project
RUN dotnet build "CareerMatch.UserServices.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the project
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CareerMatch.UserServices.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CareerMatch.UserServices.dll"]
