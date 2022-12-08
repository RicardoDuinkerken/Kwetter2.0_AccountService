# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore ./AccountService --disable-parallel
RUN dotnet publish ./AccountService -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
Copy --from=build /app ./

EXPOSE 5003-5004

ENTRYPOINT ["dotnet", "AccountService.Api.dll"]