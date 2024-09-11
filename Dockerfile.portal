FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PortalAboutEverything/PortalAboutEverything.csproj", "PortalAboutEverything/"]
COPY ["BestBoardGameApi.Dtos/BestBoardGameApi.Dtos.csproj", "BestBoardGameApi.Dtos/"]
COPY ["BoardGameOfDayApi.Dtos/BoardGameOfDayApi.Dtos.csproj", "BoardGameOfDayApi.Dtos/"]
COPY ["BoardGamesReviewsApi.Dtos/BoardGamesReviewsApi.Dtos.csproj", "BoardGamesReviewsApi.Dtos/"]
COPY ["PortalAboutEverything.Data/PortalAboutEverything.Data.csproj", "PortalAboutEverything.Data/"]
COPY ["PortalAboutEverything.VideoServices/PortalAboutEverything.VideoServices.csproj", "PortalAboutEverything.VideoServices/"]
RUN dotnet restore "PortalAboutEverything/PortalAboutEverything.csproj"
COPY . .
WORKDIR "/src/PortalAboutEverything"
RUN dotnet build "PortalAboutEverything.csproj" -c $BUILD_CONFIGURATION -o /app/build && \
    dotnet publish "PortalAboutEverything.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
USER $APP_UID
WORKDIR /app
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PortalAboutEverything.dll"]
