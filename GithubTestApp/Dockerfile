# 1. Aşama: Runtime için baz imajı ayarlıyoruz
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# 2. Aşama: SDK imajı ile proje derlemesini yapıyoruz
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Tüm dosyaları kopyalayıp bağımlılıkları geri yüklüyoruz
COPY . .
WORKDIR "/src/GithubTestApp"
RUN dotnet restore "GithubTestApp.csproj"

# Projeyi derliyoruz
RUN dotnet build "GithubTestApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3. Aşama: Yayın için gerekli dosyaları oluşturuyoruz
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GithubTestApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4. Aşama: Çalıştırılabilir imajı hazırlıyoruz
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Uygulamayı başlatıyoruz
ENTRYPOINT ["dotnet", "GithubTestApp.dll"]
