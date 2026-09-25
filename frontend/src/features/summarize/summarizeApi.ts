import { API_URL } from "../../shared/config/config";

async function summarize(meetingNotes: string): Promise<string> {
    const response = await fetch(`${API_URL}/summarize`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ meetingNotes }),
    });
    if (!response.ok) {
        throw new Error(await response.text());
    }

    const data = await response.json();
    return data.summary;
}

export { summarize };
