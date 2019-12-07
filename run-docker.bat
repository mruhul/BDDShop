docker build . -t ruhul/bddshop
docker run -it -p 3000:80 -e "ASPNETCORE_ENVIRONMENT=production" ruhul/bddshop