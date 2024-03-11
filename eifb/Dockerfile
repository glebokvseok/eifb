FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /service
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["eifb/eifb.csproj", "eifb/"]
COPY ["modules/ArticleManager/ArticleManager.csproj", "modules/ArticleManager/"]
COPY ["shared/YandexCloud/YandexCloud.csproj", "shared/YandexCloud/"]
COPY ["core/DependencyRegistrar/DependencyRegistrar.csproj", "core/DependencyRegistrar/"]
RUN dotnet restore "eifb/eifb.csproj"
COPY . ./
WORKDIR "/src/eifb"
RUN dotnet build "eifb.csproj" -c Release -o /service/build

FROM build AS publish
RUN dotnet publish "eifb.csproj" -c Release -o /service/publish

FROM base AS service
WORKDIR /service
COPY --from=publish /service/publish .
ENTRYPOINT ["dotnet", "eifb.dll"]
