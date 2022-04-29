export let appSettings: AppSettings;

interface AppSettings {
  apiURL: string;
}

export async function loadSettings() {
  const value = await import("./app.settings.json");

  if (!("apiURL" in value))
    throw new Error("apiURL not set in app settings and is required!");

  appSettings = value;
}
