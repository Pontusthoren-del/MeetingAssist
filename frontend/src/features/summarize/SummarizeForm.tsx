import { useState } from "react";
import { summarize } from "./summarizeApi";
import styles from "../../shared/styles/Form.module.css";

function SummarizeForm() {
    const [notes, setNotes] = useState("");
    const [result, setResult] = useState("");
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    async function handleSubmit(e: React.FormEvent) {
        e.preventDefault();
        setLoading(true);
        setError("");

        try {
            const summary = await summarize(notes);
            setResult(summary);
        } catch (err) {
            setError(err instanceof Error ? err.message : "Något gick fel");
        } finally {
            setLoading(false);
        }
    }

    return (
        <form className={styles.container} onSubmit={handleSubmit}>
            <h2>Sammanfatta möte</h2>

            <textarea
                value={notes}
                onChange={(e) => setNotes(e.target.value)}
                placeholder="Klistra in mötesanteckningar här..."
            />
            <button type="submit" disabled={loading || !notes.trim()}>
                {loading ? "Sammanfattar..." : "Sammanfatta"}
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

export default SummarizeForm;
