# 🐳 Docker Database Backup
DDB is simple tool to create backup (dump) of MySQL and MariaDB written in .NET Core 3. There is a version for the AMD64 and the ARM7 (tested on Raspberry PI 4).<br>
**Project now is in Beta**

# Supported tags and respective `Dockerfile` links
* AMD64: [`latest`](https://github.com/m4rcelpl/ddb/blob/master/Dockerfile), [`1.1`](https://github.com/m4rcelpl/ddb/blob/master/Dockerfile), [`1`](https://github.com/m4rcelpl/ddb/blob/master/Dockerfile), [`2`](https://github.com/m4rcelpl/ddb/blob/master/Dockerfile)<br>
* ARM7: [`arm7`](https://github.com/m4rcelpl/ddb/blob/master/Dockerfile.arm), [`1.1-arm7`](https://github.com/m4rcelpl/ddb/blob/master/Dockerfile.arm), [`1-arm7`](https://github.com/m4rcelpl/ddb/blob/master/Dockerfile.arm), [`2-arm7`](https://github.com/m4rcelpl/ddb/blob/master/Dockerfile.arm)

# 💻How to use
### Docker:<br>
```
docker run -d \
       --env MYSQL_ADRESS={adress} \
       --env MYSQL_USERNAME={username} \
       --env MYSQL_PASSWORD={password} \
       --env DB_DUMP_BEGIN={time} \
       --volume /path/to/my/backup/folder:/app/backup
       m4rcel/ddb
```
       

### Docker-Compose
```yml
version: "3.7"
services:
  ddb:
    image: m4rcel/ddb #use ARM7 tag for Raspberry PI
    environment:
      MYSQL_ADRESS: "{adress}"
      MYSQL_USERNAME: "{username}"
      MYSQL_PASSWORD: "{password}"
      DB_DUMP_BEGIN: "{time}"
    volumes:
      - "/path/to/my/backup/folder:/app/backup"
    restart: on-failure
```

# 🔌Variables

`MYSQL_ADRESS`: Adress. E.g. 'db', 'database.exemple.com'<br>
`MYSQL_PORT`: Port, default is 3306.<br>
`MYSQL_USERNAME`: Database username.<br>
`MYSQL_PASSWORD`: Database password.<br>
`DB_DUMP_FREQ`: Frequency of backups (in minutes). Default is 1440 (24h) <br>
`DB_DUMP_BEGIN`: Time of the backup. E.g. 1337 means 13:37 (1:37 PM). Remember to check the timezone of your container. DDB displays your current timezone when you launch it.<br>
`MYSQL_DB_NAMES`: Names of the databases you want to make a backup. Separated by spaces E.g. 'database1 database2 database3'. By default this is set to `--all-databases`.
`TZ`: You can set conteiner [timezone](https://en.wikipedia.org/wiki/List_of_tz_database_time_zones). E.g. 'Europe/Warsaw'. Default setting is 'UTC'.<br>
`FILES_TO_KEEP`: How many backup files you want to keep. If limit is reached the oldest one are deleted.
