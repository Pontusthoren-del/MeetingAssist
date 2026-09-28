# MeetingAssist

# AI Mötesassistent

Min lösning på Laboration 3. Backend i ASP.NET Core Web API (.NET 10), frontend i React med TypeScript, och Gemini som AI-tjänst via paketet Google.GenAI.

## Upplägg

AI-anropen ligger i en IAiService med en GeminiAiService-implementation, så controllern inte är bunden till Gemini. Promptarna har jag lagt i en egen Prompts-mapp, uppdelade i system-prompt (regler) och user-prompt (användarens input). I promptarna har jag skrivit in att AI:n inte ska hitta på fakta eller fördela ansvar, och i frontenden visas alla svar i ett redigerbart fält, så att det blir förslag och inte beslut.

Om en Gemini-modell är överbelastad provar servicen automatiskt nästa modell, och om ingen svarar returnerar controllern 503 med ett felmeddelande som visas i frontenden. Tomma fält stoppas med 400 innan något AI-anrop görs.

Frontenden är feature-baserad, med React Router för navigeringen och CSS Modules för stylingen.

## Köra projektet

API-nyckeln läses från `Gemini__ApiKey` i launchSettings.json. Filen ligger i .gitignore, så du behöver lägga till nyckeln i din egen launchSettings. Backenden förväntas köras på https://localhost:7280.

Backend: starta MotesAssistant.Api från Visual Studio. Scalar finns på /scalar.

Frontend: `npm install` och `npm run dev` i mappen frontend, och öppna sedan http://localhost:5173.

