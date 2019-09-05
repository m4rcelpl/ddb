docker buildx build -f -pull .\Dockerfile -t m4rcel/ddb -t m4rcel/ddb:1 -t m4rcel/ddb:1.1 -t m4rcel/ddb:latest .
docker push m4rcel/ddb
docker push m4rcel/ddb:1
docker push m4rcel/ddb:1.1
docker push m4rcel/ddb:latest

REM docker build --no-cache -f Dockerfile.arm --platform linux/arm/v7 -t m4rcel/ddb:arm7 -t m4rcel/ddb:1-arm7 -t m4rcel/ddb:1.1-arm7 .
REM docker push m4rcel/ddb:arm7
REM docker push m4rcel/ddb:1-arm7
REM docker push m4rcel/ddb:1.1-arm7