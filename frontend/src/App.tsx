import { useState } from "react";
import "./App.css";
import SummarizeForm from "./features/summarize/SummarizeForm";
import AgendaForm from "./features/agenda/AgendaForm";

function App() {
    return (
        <div>
            <SummarizeForm />
            <AgendaForm />
        </div>
    );
}

export default App;
