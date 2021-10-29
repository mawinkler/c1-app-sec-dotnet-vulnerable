ARG OS=bionic
ARG TARGET_FRAMEWORK_VERSION=3.1

FROM mcr.microsoft.com/dotnet/core/sdk:${TARGET_FRAMEWORK_VERSION}-${OS} AS build
WORKDIR /app

COPY . .
WORKDIR /app
RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:${TARGET_FRAMEWORK_VERSION}-${OS} AS runtime
WORKDIR /app

COPY --from=build /app/out ./

COPY TrendAppProtect/ TrendAppProtect/
ENV CORECLR_ENABLE_PROFILING=1
ENV CORECLR_PROFILER={a51743a9-9e05-4a9f-adcd-d39aa322615a}
ENV CORECLR_PROFILER_PATH=/app/TrendAppProtect/bin/libTrendAppProtectProfiler-x64-Linux.so
ENV TREND_AP_CONFIG_FILE=/app/TrendAppProtect/TrendAppProtect.config

ENV ASPNETCORE_ENVIRONMENT="Development"

EXPOSE 5000

ENTRYPOINT ["dotnet", "App.dll"]
