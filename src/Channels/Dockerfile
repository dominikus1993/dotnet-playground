FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Channels/Channels.csproj", "src/Channels/"]
RUN dotnet restore "src/Channels/Channels.csproj"
COPY . .
WORKDIR "/src/src/Channels"
RUN dotnet build "Channels.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Channels.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Channels.dll"]
