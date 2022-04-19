import { appSettings } from "./app.settings";

const apiUrl = (relative: string) => `${appSettings.apiURL}/${relative}`;

export default apiUrl;
