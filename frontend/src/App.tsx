import "./App.css";
import SummarizeForm from "./features/summarize/SummarizeForm";
import AgendaForm from "./features/agenda/AgendaForm";
import InvitationForm from "./features/invitation/InvitationForm";

function App() {
    return (
        <div>
            <SummarizeForm />
            <AgendaForm />
            <InvitationForm />
        </div>
    );
}

export default App;
