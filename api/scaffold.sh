#!/bin/bash


dotnet tool install -g dotnet-ef
dotnet ef dbcontext scaffold "Server=ep-muddy-pine-adt1rxmn-pooler.c-2.us-east-1.aws.neon.tech;DB=neondb;UID=neondb_owner;PWD=npg_7ZhobSkaOmY1;SslMode=require" Npgsql.EntityFrameworkCore.PostgreSQL \
    --output-dir ./Entities \
    --context-dir . \
    --context MyDbContext \
    --no-onconfiguring \
    --context-namespace Infrastructure.Postgres.Scaffolding \
    --schema public \
    --force