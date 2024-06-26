default:
  just --list

up:
  docker-compose up -d

down:
  docker-compose down

kill:
  docker-compose kill

build:
  docker-compose build

ps:
  docker-compose ps

exec *args:
  docker-compose exec app {{args}}

logs *args:
    docker-compose logs {{args}} -f

makem *args:
  # docker compose exec app alembic revision --autogenerate -m "{{args}}"
  docker compose exec app scripts/makemigrations {{args}}

migrate:
  docker compose exec app alembic upgrade head

downgrade *args:
  docker compose exec app alembic downgrade {{args}}

ruff *args:
  docker compose exec app ruff {{args}} src
  docker compose exec app ruff format src

lint:
  just ruff --fix

backup:
  docker compose exec app_db scripts/backup

# examples:
# "just get-backup dump_name_2021-01-01..backup.gz" to copy particular backup
# "just get-backup" to copy directory (backups) with all dumps
mount-docker-backup *args:
  docker cp app_db:/backups/{{args}} ./{{args}}

restore *args:
    docker compose exec app_db scripts/restore {{args}}

test *args:
    docker compose exec app pytest {{args}}

test-watch *args:
    docker compose exec app ptw {{args}}
