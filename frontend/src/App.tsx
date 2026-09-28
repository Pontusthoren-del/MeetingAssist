import { NavLink, Navigate, Route, Routes } from "react-router";
import SummarizeForm from "./features/summarize/SummarizeForm";
import AgendaForm from "./features/agenda/AgendaForm";
import InvitationForm from "./features/invitation/InvitationForm";
import styles from "./App.module.css";

function App() {
    return (
        <div className={styles.app}>
            <h1>AI Mötesassistent</h1>
            <p className={styles.tagline}>Förslag, inte beslut.</p>

            <nav className={styles.tabs}>
                <NavLink
                    to="/summarize"
                    className={({ isActive }) =>
                        isActive ? styles.active : ""
                    }
                >
                    Sammanfatta
                </NavLink>
                <NavLink
                    to="/agenda"
                    className={({ isActive }) =>
                        isActive ? styles.active : ""
                    }
                >
                    Agenda
                </NavLink>
                <NavLink
                    to="/invitation"
                    className={({ isActive }) =>
                        isActive ? styles.active : ""
                    }
                >
                    Inbjudan
                </NavLink>
            </nav>

            <Routes>
                <Route path="/" element={<Navigate to="/summarize" />} />
                <Route path="/summarize" element={<SummarizeForm />} />
                <Route path="/agenda" element={<AgendaForm />} />
                <Route path="/invitation" element={<InvitationForm />} />
            </Routes>
        </div>
    );
}

export default App;
