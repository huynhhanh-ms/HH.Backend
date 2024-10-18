.PHONY: docker-up

docker-up:
	git pull
	docker compose build
	docker compose down
	docker compose up -d
	docker compose logs -f

