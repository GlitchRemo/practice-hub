# base image 

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app 

# publish folder should be exist on build agent
# this is the relative path

COPY publish/* ./

EXPOSE 8080

ENV ASPNETCORE_URLS=http://*:8080

ENTRYPOINT ["dotnet", "NGrid.Customer.ToReplace.Api.dll"]

