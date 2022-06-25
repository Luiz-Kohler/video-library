FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app 
#
COPY *.sln .
COPY Web/*.csproj ./Web/
COPY Application/*.csproj ./Application/
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/ 

RUN dotnet restore 

COPY Web/. ./Web/
COPY Application/. ./Application/
COPY Domain/. ./Domain/ 
COPY Infrastructure/. ./Infrastructure/ 

WORKDIR /app/Web
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
WORKDIR /app 

COPY --from=build /app/Experiment/out ./
ENTRYPOINT ["dotnet", "Experiment.dll"]