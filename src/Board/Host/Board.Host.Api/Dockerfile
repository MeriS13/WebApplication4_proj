#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Board/Host/Board.Host.Api/Board.Host.Api.csproj", "src/Board/Host/Board.Host.Api/"]
COPY ["src/Board/Application/Board.Application.AppData/Board.Application.AppData.csproj", "src/Board/Application/Board.Application.AppData/"]
COPY ["src/Board/Contracts/Board.Contracts/Board.Contracts.csproj", "src/Board/Contracts/Board.Contracts/"]
COPY ["src/Board/Domain/Board.Domain/Board.Domain.csproj", "src/Board/Domain/Board.Domain/"]
COPY ["src/Board/Infrastructure/Board.Infrastructure/Board.Infrastructure.csproj", "src/Board/Infrastructure/Board.Infrastructure/"]
COPY ["src/Board/Infrastructure/Board.Infrastructure.DataAccess/Board.Infrastructure.DataAccess.csproj", "src/Board/Infrastructure/Board.Infrastructure.DataAccess/"]
RUN dotnet restore "src/Board/Host/Board.Host.Api/Board.Host.Api.csproj"
COPY . .
WORKDIR "/src/src/Board/Host/Board.Host.Api"
RUN dotnet build "Board.Host.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Board.Host.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Board.Host.Api.dll"]