FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app 
#
COPY *.sln .
COPY Web/*.csproj ./Web/
COPY Application/*.csproj ./Application/
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/ 
COPY Tests.Common/*.csproj ./Tests.Common/ 
COPY Tests.Integration/*.csproj ./Tests.Integration/ 
COPY Tests.Unit/*.csproj ./Tests.Unit/ 

RUN dotnet restore 

COPY Web/. ./Web/
COPY Application/. ./Application/
COPY Domain/. ./Domain/ 
COPY Infrastructure/. ./Infrastructure/ 

WORKDIR /app/Web
RUN dotnet publish -c Release -o out 

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
WORKDIR /app 

COPY --from=build /app/Web/out ./

EXPOSE 5000

ENTRYPOINT ["dotnet", "Web.dll"]