# Используем базовый образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Используем образ SDK для построения приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "AutoMagazine8Net/AutoMagazine8Net.csproj"
RUN dotnet build "AutoMagazine8Net/AutoMagazine8Net.csproj" -c Release -o /app/build

# Публикуем приложение в папку publish
FROM build AS publish
RUN dotnet publish "AutoMagazine8Net/AutoMagazine8Net.csproj" -c Release -o /app/publish

# Переключаемся на базовый образ для запуска опубликованного приложения
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoMagazine8Net.dll"]

