import { useState } from "react";
import { generateAgenda } from "./agendaApi";
import styles from "./AgendaForm.module.css";

function AgendaForm() {
    const [title, setTitle] = useState("");
    const [purpose, setPurpose] = useState("");
    const [participants, setParticipants] = useState("");
    const [lengthMinutes, setLengthMinutes] = useState(30);
    const [result, setResult] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    async function handleSubmit() {
        setLoading(true);
        setError("");
        try {
            const agenda = await generateAgenda({
                title,
                purpose,
                participants,
                lengthMinutes,
            });
            setResult(agenda);
        } catch (err) {
            setError(err instanceof Error ? err.message : "Något gick fel");
        } finally {
            setLoading(false);
        }
    }

    return (
        <div className={styles.container}>
            <h2>Skapa agenda</h2>

            <input
                value={title}
                onChange={(e) => setTitle(e.target.value)}
                placeholder="Mötets titel"
            />
            <input
                value={purpose}
                onChange={(e) => setPurpose(e.target.value)}
                placeholder="Syfte med mötet"
            />
            <input
                value={participants}
                onChange={(e) => setParticipants(e.target.value)}
                placeholder="Deltagare, t.ex Anna,Erik etc."
            />
            <input
                type="number"
                min={5}
                value={lengthMinutes}
                onChange={(e) => setLengthMinutes(Number(e.target.value))}
            />

            <button
                onClick={handleSubmit}
                disabled={loading || !purpose.trim()}
            >
                {loading ? "Skapar agenda..." : "Skapa agenda"}
            </button>

            {result && (
                <textarea
                    value={result}
                    onChange={(e) => setResult(e.target.value)}
                />
            )}
        </div>
    );
}

export default AgendaForm;
