docker buildx build -f .\Dockerfile -t m4rcel/ddb -t m4rcel/ddb:1 -t m4rcel/ddb:1.1 -t m4rcel/ddb:latest .
docker push m4rcel/ddb
docker push m4rcel/ddb:1
docker push m4rcel/ddb:1.1
docker push m4rcel/ddb:latest

docker build --no-cache -f Dockerfile.arm -t m4rcel/ddb:arm7 -t m4rcel/ddb:1-arm7 -t m4rcel/ddb:1.1-arm7 .
docker push m4rcel/ddb:arm7
docker push m4rcel/ddb:1-arm7
docker push m4rcel/ddb:1.1-arm7