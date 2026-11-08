# Build stage runs on the build platform (arm64 in the GitHub workflow)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Restore first so the layer is cached when only source files change
COPY src/Bytech.Web/Bytech.Web.csproj src/Bytech.Web/
RUN dotnet restore src/Bytech.Web/Bytech.Web.csproj

COPY src/Bytech.Web/ src/Bytech.Web/
RUN dotnet publish src/Bytech.Web/Bytech.Web.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# aspnet images run as a non-root user and listen on 8080 by default
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Bytech.Web.dll"]
