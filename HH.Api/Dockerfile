FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
# ENV APP_NAME=HH.Api - not working 

# Install necessary dependencies for Puppeteer and Chromium
RUN mkdir -p /var/lib/apt/lists/partial \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
        apt-transport-https \
        ca-certificates \
        curl \
        gnupg \
        hicolor-icon-theme \
        libcanberra-gtk* \
        libgl1-mesa-dri \
        libgl1-mesa-glx \
        libpango1.0-0 \
        libpulse0 \
        libv4l-0 \
        fonts-symbola \
        chromium \
    && apt-get clean \
    && rm -rf /var/lib/apt/lists/*

# Switch back to the non-root user
USER app

WORKDIR /app
EXPOSE 8080
EXPOSE 6789

COPY --chown=app:app . .

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HH.Api/HH.Api.csproj", "HH.Api/"]
RUN dotnet restore "HH.Api/HH.Api.csproj"
COPY . .
WORKDIR "/src/HH.Api"
RUN dotnet build "HH.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HH.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV PUPPETEER_EXECUTABLE_PATH=/usr/bin/chromium
ENTRYPOINT ["dotnet", "HH.Api.dll"]
