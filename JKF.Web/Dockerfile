##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
#WORKDIR /src
#COPY ["JKF.Web/JKF.Web.csproj", "JKF.Web/"]
#COPY ["Sup.OpenApi.Sdk/Sup.OpenApi.Sdk.csproj", "Sup.OpenApi.Sdk/"]
#COPY ["JFK.BLL/JKF.BLL.csproj", "JFK.BLL/"]
#COPY ["JKF.Utils/JKF.Utils.csproj", "JKF.Utils/"]
#COPY ["JKF.DB/JKF.DB.csproj", "JKF.DB/"]
#RUN dotnet restore "JKF.Web/JKF.Web.csproj"
#COPY . .
#WORKDIR "/src/JKF.Web"
#RUN dotnet build "JKF.Web.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "JKF.Web.csproj" -c Release -o /app/publish


#FROM base AS final
FROM microsoft/donet:5.0-aspnetcore-runtime
WORKDIR /app
COPY . .
ENTRYPOINT ["dotnet", "JKF.Web.dll"]