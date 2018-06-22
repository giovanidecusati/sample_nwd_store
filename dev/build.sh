# Build all images to dev environment
# Run at root project directory
docker build . -f ./src/Services/BackOffice/Dockerfile -t nwdstore/backoffice:dev --rm
docker build . -f ./src/Services/Commerce.Catalogs/Dockerfile -t nwdstore/commerce.catalogs:dev --rm
docker rmi $(docker images -f "dangling=true" -q) -f