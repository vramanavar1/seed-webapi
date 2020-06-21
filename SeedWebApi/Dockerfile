FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY . .
RUN dotnet restore "SeedWebApi/SeedWebApi.csproj"
WORKDIR "/src/SeedWebApi"
RUN dotnet build "SeedWebApi.csproj" -c Release -o /app

FROM build AS publish
WORKDIR "/src/SeedWebApi"
RUN dotnet publish "SeedWebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
EXPOSE 80
ENTRYPOINT ["dotnet", "SeedWebApi.dll"]