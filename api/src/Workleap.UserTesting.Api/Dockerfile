#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /userTesting

#See https://github.com/microsoft/artifacts-credprovider/blob/master/samples/dockerfile.sample.txt to see an exemple of using Azure Artifact Credential Provider
ARG FEED_ACCESSTOKEN
ARG HANGFIRE_PRO_USERNAME
ARG HANGFIRE_PRO_PASSWORD

ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS "{\"endpointCredentials\": [{\"endpoint\":\"https://pkgs.dev.azure.com/workleap/_packaging/workleap/nuget/v3/index.json\", \"username\":\"docker\", \"password\":\"${FEED_ACCESSTOKEN}\"}]}"
RUN curl -L https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh  | bash

COPY ["/userTesting/api/src", "api/src"]
# COPY ["/userTesting/common/src", "common/src"]  ***Uncomment if you use a "common" folder***
COPY ["/NuGet.Config", "."]

RUN dotnet nuget update source HangfirePro -u ${HANGFIRE_PRO_USERNAME} -p ${HANGFIRE_PRO_PASSWORD} --store-password-in-clear-text
RUN dotnet restore "/userTesting/api/src/Workleap.UserTesting.Api/Workleap.UserTesting.Api.csproj"
COPY . .

RUN dotnet build "/userTesting/api/src/Workleap.UserTesting.Api/Workleap.UserTesting.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "/userTesting/api/src/Workleap.UserTesting.Api/Workleap.UserTesting.Api.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS=http://+:8080
RUN adduser --disabled-password --home /app --gecos '' app && chown -R app /app
USER app
WORKDIR /app
COPY --from=publish /app/publish .
# nosemgrep: dockerfile.security.missing-user.missing-user
ENTRYPOINT ["dotnet", "Workleap.UserTesting.Api.dll"]
