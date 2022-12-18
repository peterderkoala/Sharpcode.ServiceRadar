#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Sharpcode.ServiceRadar.Ui.Blazor/Sharpcode.ServiceRadar.Ui.Blazor.csproj", "Sharpcode.ServiceRadar.Ui.Blazor/"]
RUN dotnet restore "Sharpcode.ServiceRadar.Ui.Blazor/Sharpcode.ServiceRadar.Ui.Blazor.csproj"
COPY . .
WORKDIR "/src/Sharpcode.ServiceRadar.Ui.Blazor"
RUN dotnet build "Sharpcode.ServiceRadar.Ui.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sharpcode.ServiceRadar.Ui.Blazor.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sharpcode.ServiceRadar.Ui.Blazor.dll"]