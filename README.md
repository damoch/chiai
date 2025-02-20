# chiai
Prototyp chatbota AI

## Funkcje
- Czat w czasie rzeczywistym z symulowanymi odpowiedziami AI.
- Opinie użytkownika na temat wiadomości AI (polubienie/niepolubienie).
- Możliwość zatrzymania generowania wiadomości.
- Historia czatu i oceny wiadomości.
- API backendowe zbudowane z użyciem MediatR oraz CQRS dla czystego rozdzielenia odpowiedzialności.

## Frontend
- Frontend oparty na Angularze do zarządzania interfejsem i obsługi interakcji z użytkownikiem.
- RxJS do obsługi zdarzeń w czasie rzeczywistym na czacie.
- Komponenty Material Design do tworzenia przejrzystego i responsywnego UI.

## Backend
- ASP.NET Core z MediatR do obsługi zapytań w stylu CQRS.
- Integracja z bazą danych przy użyciu Entity Framework do przechowywania danych.
- Symulowane generowanie wiadomości podobne do ChatGPT.

## Jak to symuluje ChatGPT

Odpowiedzi AI są symulowane z wcześniej zdefiniowanymi wiadomościami, a użytkownicy mogą wchodzić w interakcje, oceniając wiadomości. AI może zatrzymać generowanie kolejnych wiadomości na podstawie interakcji użytkownika, tak jak w rzeczywistości ChatGPT. Backend jest zaprojektowany do zarządzania czatami użytkowników, ocenami i historią z użyciem wzorców CQRS i MediatR.