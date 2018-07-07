FROM microsoft/dotnet:2.1-sdk-stretch AS build-env

WORKDIR /app

COPY . .

RUN dotnet publish src/SampleBackgroundApp/SampleBackgroundApp.csproj -c Release -o /build/Publish

FROM microsoft/dotnet:2.1.1-runtime-stretch-slim

WORKDIR /app

COPY --from=build-env /build/Publish .

ENTRYPOINT ["dotnet","SampleBackgroundApp.dll"]