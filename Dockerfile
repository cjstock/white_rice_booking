FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["white_rice_booking.csproj", "./"]
RUN dotnet restore "./white_rice_booking.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "white_rice_booking.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "white_rice_booking.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "white_rice_booking.dll"]
