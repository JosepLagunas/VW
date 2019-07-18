FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM node:10-alpine as build-node
WORKDIR /ClientApp
COPY ./ClientApp/package.json .
COPY ./ClientApp/package-lock.json .
RUN npm install
COPY ./ClientApp/ . 
RUN npm run build

FROM microsoft/dotnet:2.2-sdk AS build
ENV BuildingDocker true
ENV ASPNETCORE_ENVIRONMENT Development
WORKDIR /src
COPY ["./VWP.csproj", "VWSPA/"]
RUN dotnet restore "VWSPA/VWP.csproj"
COPY . .
RUN dotnet build "VWP.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "VWP.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=build-node /ClientApp ./ClientApp

ENTRYPOINT ["dotnet", "VWP.dll"]