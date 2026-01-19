FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj files (BẮT BUỘC đủ tất cả project)
COPY ["AlumniManagement.API/AlumniManagement.API.csproj", "AlumniManagement.API/"]
COPY ["AlumniManagement.BUS/AlumniManagement.BUS.csproj", "AlumniManagement.BUS/"]
COPY ["AlumniManagement.DAL/AlumniManagement.DAL.csproj", "AlumniManagement.DAL/"]
COPY ["AlumniManagement.Shared/AlumniManagement.Shared.csproj", "AlumniManagement.Shared/"]

# Restore
RUN dotnet restore "AlumniManagement.API/AlumniManagement.API.csproj"

# Copy toàn bộ source code
COPY . .

# Build
WORKDIR "/src/AlumniManagement.API"
RUN dotnet build "AlumniManagement.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "AlumniManagement.API.csproj" -c Release -o /app/publish

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AlumniManagement.API.dll"]
