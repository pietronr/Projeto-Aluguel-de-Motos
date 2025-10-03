# Projeto

Sistema Back-end completo para aluguel de motos.

Segue a **Clean Architecture**, organizado em 4 camadas:

- **Service**
- **Repository**
- **Web API**
- **Domain**

Adotei o padrão **Rich Model** nas entidades, encapsulando as regras de negócio nelas.  
Para o cálculo da taxa de aluguel, utilizei o padrão **Builder**.

Integração com banco **PostgreSQL**, usando **Docker** e **Docker Compose**.

---

## Como rodar

1. Faça o build do Docker Compose na raiz do projeto:
```bash
docker compose up --build
```

2. Rode a aplicação no Visual Studio 2022, usando a opção Container.
O Dockerfile será executado junto às migrations automaticamente, e o banco já estará integrado.
