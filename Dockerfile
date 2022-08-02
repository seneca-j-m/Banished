FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["External.csproj", "./"]
RUN dotnet restore "External.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "External.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "External.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "External.dll"]
