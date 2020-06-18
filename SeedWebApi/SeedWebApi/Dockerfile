FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["SeedWebApi/SeedWebApi.csproj", "SeedWebApi/"]
RUN dotnet restore "SeedWebApi/SeedWebApi.csproj"
COPY . .
WORKDIR "/src/SeedWebApi"
RUN dotnet build "SeedWebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SeedWebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SeedWebApi.dll"]