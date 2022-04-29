import ReactDOM from "react-dom";
import { App } from "./App";
import reportWebVitals from "./reportWebVitals";
import { loadSettings } from "./app.settings";

async function startApp() {
  ReactDOM.render(<App />, document.getElementById("root"));
  reportWebVitals();
}

startApp();
loadSettings();
