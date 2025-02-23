# Use .NET 7 runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use .NET 7 SDK for building
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["SiwanDoctorAPI/SiwanDoctorAPI.csproj", "SiwanDoctorAPI/"]
RUN dotnet restore "SiwanDoctorAPI/SiwanDoctorAPI.csproj"

# Copy the rest of the files and build the app
COPY . .
WORKDIR "/src/SiwanDoctorAPI"
RUN dotnet publish "SiwanDoctorAPI.csproj" -c Release -o /app/publish

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

# Start the application
CMD ["dotnet", "SiwanDoctorAPI.dll"]
