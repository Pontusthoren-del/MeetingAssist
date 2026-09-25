import { API_URL } from "../../shared/config/config";

type InvitationRequest = {
    meetingName: string;
    time: string;
    location: string;
    purpose: string;
};

async function generateInvitation(request: InvitationRequest): Promise<string> {
    const response = await fetch(`${API_URL}/invitation`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(request),
    });

    if (!response.ok) {
        throw new Error(await response.text());
    }

    const data = await response.json();
    return data.invite;
}

export { generateInvitation };
export type { InvitationRequest };
