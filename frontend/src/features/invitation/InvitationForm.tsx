import { useState } from "react";
import { generateInvitation } from "./invitationApi";
import styles from "../../shared/styles/Form.module.css";

function InvitationForm() {
    const [meetingName, setMeetingName] = useState("");
    const [time, setTime] = useState("");
    const [location, setLocation] = useState("");
    const [purpose, setPurpose] = useState("");
    const [result, setResult] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setLoading(true);
        setError("");
        try {
            const invitation = await generateInvitation({
                meetingName,
                time,
                location,
                purpose,
            });
            setResult(invitation);
        } catch (err) {
            setError(err instanceof Error ? err.message : "Något gick fel");
        } finally {
            setLoading(false);
        }
    }

    return (
        <form className={styles.container} onSubmit={handleSubmit}>
            <h2>Skapa inbjudan</h2>

            <input
                value={meetingName}
                onChange={(e) => setMeetingName(e.target.value)}
                placeholder="Mötets namn"
            />
            <input
                value={purpose}
                onChange={(e) => setPurpose(e.target.value)}
                placeholder="Syfte med mötet"
            />
            <input
                value={time}
                onChange={(e) => setTime(e.target.value)}
                placeholder="Tid, t.ex. 15 okt kl 10:00"
            />
            <input
                value={location}
                onChange={(e) => setLocation(e.target.value)}
                placeholder="Mötets plats"
            />

            <button type="submit" disabled={loading || !meetingName.trim()}>
                {loading ? "Skapar inbjudan..." : "Skapa inbjudan"}
            </button>

            {error && <p className={styles.error}>{error}</p>}

            {result && (
                <div className={styles.result}>
                    <span className={styles.resultLabel}>
                        AI-förslag · redigera fritt
                    </span>
                    <textarea
                        value={result}
                        onChange={(e) => setResult(e.target.value)}
                    />
                </div>
            )}
        </form>
    );
}

export default InvitationForm;
