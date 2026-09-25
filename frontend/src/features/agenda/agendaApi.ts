import { API_URL } from "../../shared/config/config";

type AgendaRequest = {
    title: string;
    purpose: string;
    participants: string;
    lengthMinutes: number;
};

async function generateAgenda(request: AgendaRequest): Promise<string> {
    const response = await fetch(`${API_URL}/agenda`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(request),
    });

    if (!response.ok) {
        throw new Error(await response.text());
    }

    const data = await response.json();
    return data.agenda;
}

export { generateAgenda };
export type { AgendaRequest };
