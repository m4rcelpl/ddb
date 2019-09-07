docker buildx build -f Dockerfile -t m4rcel/ddb -t m4rcel/ddb:2 -t m4rcel/ddb:latest .
docker push m4rcel/ddb:2
docker push m4rcel/ddb:latest

docker buildx build -f Dockerfile.arm --platform linux/arm/v7 -t m4rcel/ddb:arm7 -t m4rcel/ddb:2-arm7 .
docker push m4rcel/ddb:2-arm7