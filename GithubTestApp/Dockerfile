# 1. Aşama: Runtime için baz imajı ayarlıyoruz
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# 2. Aşama: SDK imajı ile proje derlemesini yapıyoruz
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Sadece .csproj dosyasını kopyalıyoruz
COPY GithubTestApp.csproj .
RUN dotnet restore "GithubTestApp.csproj"

# Tüm dosyaları kopyalayıp projeyi derliyoruz
COPY . .
RUN dotnet build "GithubTestApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3. Aşama: Yayın için gerekli dosyaları oluşturuyoruz
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "GithubTestApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4. Aşama: Çalıştırılabilir imajı hazırlıyoruz
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Nginx'i kuruyoruz
RUN apt-get update && \
    apt-get install -y nginx && \
    rm -rf /var/lib/apt/lists/*

# Nginx yapılandırma dosyasını kopyalıyoruz
COPY nginx.conf /etc/nginx/nginx.conf

# Uygulamayı başlatıyoruz
ENTRYPOINT ["dotnet", "GithubTestApp.dll"]
