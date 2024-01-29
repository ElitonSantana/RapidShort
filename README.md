# RapidShort

Bem-vindo à documentação da API do RapidShort. Esta API oferece recursos para encurtar uma URL.

## Endpoint Base

O endpoint base para todas as requisições é:
https://localhost:44363/api/Url/

## Como Começar

1. **Endpoints Principais:**

   - **Obter as Top 5 URLs Encurtadas:**
     ```http
     GET /api/Url/Top5
     ```
   
   - **Obter Todas as URLs Encurtadas:**
     ```http
     GET /api/Url
     ```

   - **Obter Detalhes de uma URL por ID:**
     ```http
     GET /api/Url/{id}
     ```

   - **Criar uma Nova URL Encurtada:**
     ```http
     POST /api/Url
     Content-Type: application/json
     {
       "url": "https://url.com"
     }
     ```

   - **Atualizar uma URL por ID:**
     ```http
     PUT /api/Url/{id}
     Content-Type: application/json
     {
       "id": 1,
       "url": "https://url-atualizada.com"
     }
     ```

   - **Remover uma URL por ID:**
     ```http
     DELETE /api/Url/{id}
     ```

   - **Acessar uma URL Encurtada:**
     ```http
     GET /api/Url/Access/{shortUrl}
     ```

   - **Validar uma URL Encurtada:**
     ```http
     GET /api/Url/Validate/{shortUrl}
     ```
